using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OtpStorages",
                table: "OtpStorages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91dcf565-53f2-42c3-9140-9a1486c3f1cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99bbba55-2fe1-40ad-84e1-6de467caf997");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "OtpStorages");

            migrationBuilder.RenameColumn(
                name: "TimeCreatAt",
                table: "OtpStorages",
                newName: "ExpiryTime");

            migrationBuilder.AlterColumn<string>(
                name: "Otp",
                table: "OtpStorages",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "OtpStorages",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OtpStorages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtpStorages",
                table: "OtpStorages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b72598f-592f-47db-a5ae-e9284807cffa", "2", "User", "User" },
                    { "f98c7a07-3936-4ed9-acc7-6e33824f286a", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtpStorages_UserId",
                table: "OtpStorages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpStorages_AspNetUsers_UserId",
                table: "OtpStorages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtpStorages_AspNetUsers_UserId",
                table: "OtpStorages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtpStorages",
                table: "OtpStorages");

            migrationBuilder.DropIndex(
                name: "IX_OtpStorages_UserId",
                table: "OtpStorages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b72598f-592f-47db-a5ae-e9284807cffa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f98c7a07-3936-4ed9-acc7-6e33824f286a");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OtpStorages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OtpStorages");

            migrationBuilder.RenameColumn(
                name: "ExpiryTime",
                table: "OtpStorages",
                newName: "TimeCreatAt");

            migrationBuilder.UpdateData(
                table: "OtpStorages",
                keyColumn: "Otp",
                keyValue: null,
                column: "Otp",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Otp",
                table: "OtpStorages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OtpStorages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtpStorages",
                table: "OtpStorages",
                column: "Otp");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91dcf565-53f2-42c3-9140-9a1486c3f1cb", "1", "Admin", "Admin" },
                    { "99bbba55-2fe1-40ad-84e1-6de467caf997", "2", "User", "User" }
                });
        }
    }
}
