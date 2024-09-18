using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models.AuthenticationModels;
using api.Data;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using api.Services.MailServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using MySqlX.XDevAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.Ocsp;
namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthencationsController : ControllerBase
    {
        private readonly IAuthencationService _authencationService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMailService _mailService;
        private readonly ApplicationDbContext _context;
        public AuthencationsController(IAuthencationService authencationService, RoleManager<IdentityRole> roleManager,
                                    UserManager<IdentityUser> userManager, IMailService mailService, ApplicationDbContext context)
        {
            _authencationService = authencationService;
            _roleManager = roleManager;
            _userManager = userManager;
            _mailService = mailService;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authencationService.RegisterAsync(request);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authenlication", new { result.token, email = request.Email }, Request.Scheme);
            var emailBody = $@"
                Chào {request.Name},<br><br>
                Cảm ơn bạn đã đăng ký tài khoản với chúng tôi.<br>
                lòng nhấp vào liên kết dưới đây để xác nhận email của bạn:<br>
                <a href=""{confirmationLink}"">Xác nhận email</a><br><br>
                Trân trọng,<br>
                Đội ngũ hỗ trợ
                ";
            var message = new Messages(new string[] { request.Email }, "Confirm your email", emailBody);
            _mailService.SendEmail(message);
            if (result.Flag == false) return StatusCode(StatusCodes.Status400BadRequest, new { Status = "Error", StatusMessage = result });
            return Ok(new
            {
                result = result.user
            });
        }
        #region ConfrimMail
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return StatusCode(StatusCodes.Status400BadRequest, null);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new { message = "Email confirmed successfully" });
            }
            return BadRequest("Email not confirmed");
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null && await _userManager.CheckPasswordAsync(existingUser, request.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(existingUser);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = _authencationService.GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    existingUser,
                    userRoles,
                    expession = jwtToken.ValidTo,
                });
            }
            return Unauthorized();
        }
        [HttpGet]
        public IActionResult GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (userName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Status = "Error", StatusMessage = "User not found" });
            }
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == userName);
            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var existingOtp = await _context.OtpStorages.FirstOrDefaultAsync(o => o.UserId == user.Id);
                if (existingOtp != null)
                {
                    _context.OtpStorages.Remove(existingOtp);
                }
            }
            else
            {
                return NotFound("User not found");
            }

            string otp = _authencationService.GenerateRandomOTP();
            var otpRecord = new OtpStorage
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Otp = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(2) 
            };

            _context.OtpStorages.Add(otpRecord);
            await _context.SaveChangesAsync();

            string body = $@"Your OTP is: {otp} </br> Note: this OTP will be out of time after 2 minutes";

            var message = new Messages(new string[] { email }, "Confirm your email", body);
            _mailService.SendEmail(message);
            return Ok($"OTP sent to email: {otp}");
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOTP(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest();
            }
            var exignOtp = await _context.OtpStorages.FirstOrDefaultAsync(o => o.Otp == otp && o.UserId == user.Id);
            if (exignOtp == null)
            {
                return BadRequest();
            }
            if (exignOtp.ExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid OTP");
            }
            return Ok("Đc mà");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string otp, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");

            // Remove expired OTPs for all users
            await _authencationService.RemoveExpiredOtps();

            var otpRecord = await _context.OtpStorages.FirstOrDefaultAsync(o => o.UserId == user.Id && o.Otp == otp);
            if (otpRecord == null)
                return BadRequest("Invalid OTP");
            if (otpRecord.ExpiryTime <= DateTime.UtcNow)
                return BadRequest("OTP has expired");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                // Remove the used OTP
                _context.OtpStorages.Remove(otpRecord);
                await _context.SaveChangesAsync();
                return Ok("Password has been reset successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!isOldPasswordValid)
            {
                return BadRequest("Old password is incorrect");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password changed successfully");
        }


        /*        [HttpPost]
                public async Task<IActionResult> SendConfirmLink(string email, string url)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null) return StatusCode(StatusCodes.Status400BadRequest, new { Status = "Error", StatusMessage = "User not found, please try again!" });

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var forgotpasswordlink = Url.Action(nameof(ResetPassword), "Authencations", new { email = user.Email, token }, Request.Scheme);
                    var message = new Messages(new string[] { user.Email }, "Reset your password link", url);
                    _mailService.SendEmail(message);
                    return Ok(token);
                }

                [HttpPost]
                [AllowAnonymous]
                public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changepassword)
                {
                    var user = await _userManager.FindByEmailAsync(changepassword.Email);
                    if (user == null) return StatusCode(StatusCodes.Status400BadRequest, new { Status = "Error", StatusMessage = "User not found, please try again!" });

                    var resetPassResult = await _userManager.ResetPasswordAsync(user, changepassword.token, changepassword.Password);
                    if (resetPassResult.Succeeded)
                    {
                        foreach ( var error in resetPassResult.Errors )
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        return Ok(ModelState);

                    }
                    return StatusCode(StatusCodes.Status200OK, new { Status = "Success", StatusMessage = "Reset hassbenn changed" });

                }

                [HttpGet]
                public async Task<IActionResult> ResetPassword([Required] string email, [Required] string token)
                {
                    var model = new ChangePasswordDTO
                    {
                        Email = email,
                        token = token
                    };
                    return await Task.FromResult(Ok(new
                    {
                        model
                    }));
                }

            }*/
    }
}
