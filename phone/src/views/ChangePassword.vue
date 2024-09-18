<template>
    <section class="bg-gray-300 min-h-screen flex justify-center items-center pb-24">
        <div class="bg-gray-100 flex flex-col justify-center rounded-2xl shadow-lg max-w-3xl p-5">
            <!-- Sử dụng flex-col để sắp xếp thành phần theo chiều dọc -->
            <h2 class="font-bold text-xl text-center">Change Password</h2>
            <!-- <p class="text-sm mt-4 text-center">Please! Enter your email</p> -->
            <form @submit.prevent="handleSubmit" action="" class="flex flex-col gap-4">
                <div class="relative">
                    <input v-model="password" class="p-2 mt-8 rounded-xl border w-full" type="password" name="email" placeholder="Enter new password">
                    <input v-model="password" class="p-2 mt-8 rounded-xl border w-full" type="password" name="email" placeholder="Confirm password">
                </div>    
                <button class="bg-purple-800 rounded-xl text-white py-2 hover:scale-105 duration-300 w-full">Change Password</button>
                <!-- <div class="text-center text-xs text-[#002D74]">
                    <router-link :to="{name: 'Login'}">Login Now</router-link>
                </div> -->
            </form>
        </div>
    </section>
</template>

<script>
export default {
    data() {
        return {
            email: ''
        };
    },
    methods: {
        handleSubmit() {
            alert('Check your email now')
        }
    }
};
</script>
