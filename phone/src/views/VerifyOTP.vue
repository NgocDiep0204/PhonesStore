<template>
    <section class="bg-gray-300 min-h-screen flex justify-center items-center">
        <div class="bg-gray-100 flex flex-col justify-center rounded-2xl shadow-lg max-w-3xl p-5">
            <h2 class="font-bold text-xl text-center">Verify OTP</h2>
            <p class="text-sm mt-4 text-center">Please! Enter your OTP</p>
            <form @submit.prevent="handleSubmit" class="flex flex-col gap-4">
                <div class="relative">
                    <input v-model="otp" class="p-2 mt-8 rounded-xl border w-full" type="text" name="otp" placeholder="Enter OTP">
                </div>    
                <button class="bg-purple-800 rounded-xl text-white py-2 hover:scale-105 duration-300 w-full">Verify OTP</button>
                <div class="flex flex-row justify-center text-center text-xs text-[#002D74]">
                    <router-link class="pr-2" :to="{name:'SendEmail'}">Resend OTP</router-link>
                    <router-link :to="{name: 'Login'}">Login Now</router-link>
                </div>
            </form>
        </div>
    </section>
</template>

<script>
import axiosClient from '../axiosClient';
import { mapActions, mapState } from 'vuex';

export default {
    data() {
        return {
            otp: ''
        };
    },
    computed: {
        ...mapState(['email'])
    },
    methods: {
        ...mapActions(['resendOtp']), // Add your Vuex action here
        async handleSubmit() {
            try {
                const response = await axiosClient.post(`Authencations/VerifyOTP?email=${this.email}&otp=${this.otp}`);
              
               
                if (response.status === 200) {
                    this.$store.commit('SET_OTP', this.otp);
                    this.$router.push('/change-password');
                } else {
                    alert("OTP is invalid!");
                }
            } catch (error) {
                console.error("Error verifying OTP:", error);
                alert("An error occurred while verifying the OTP.");
            }
        }
    }
};
</script>
