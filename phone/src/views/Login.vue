<template>
  <section class="bg-gray-300 min-h-screen flex items-center justify-center">
   <!-- Login contaoner -->
    <div class="bg-gray-100 flex rounded-2xl shadow-lg max-w-3xl p-5">
        <!-- Form -->
        <div class="sm:w-1/2 px-16">
           <h2 class="font-bold text-2xl">Login</h2>
           <p class="text-sm mt-4">If you already a member, eassily login</p>
           <form  @submit.prevent="handleSubmit" action="" class="flex flex-col gap-4">
              <div class="relative">
                  <input v-model="email" class="p-2 mt-8 rounded-xl border" type="text" name="email" placeholder="Enter your email">
                  <input v-model="password" class="p-2 mt-8 rounded-xl border" type="password" name="password" placeholder="Enter your password">
              </div>    
              <button class="bg-purple-800 rounded-xl text-white py-2 hover:scale-105 duration-300 w-full">Login</button>
              <div class="ml-24 text-xs text-[#002D74] ">
                <router-link :to="{name: 'SendEmail'}">Forgot your password?</router-link>
              </div>
             
          </form>
         
          <div class="mt-5 text-xs border-b border-[#002D74] py-4 text-[#002D74]">
              <router-link :to="{name: 'Register'}">Don't have accout?</router-link>
          </div>
        </div >
        <!-- Image -->
        <div class="w-1/2 p-5 items-center">
            <img class="md:block hidden rounded-xl w-72 h-96" :src=imgUrl alt="library">
        </div>
    </div>
  </section>
</template>

<script>
import axiosClient from '../axiosClient';
export default {
    data(){
        return{
              email: '',
              password: '',
              imgUrl: "src/assets/img/login.jpg"
        }
    },

    methods: {
      async handleSubmit(){
          if(!this.email || !this.password){
              alert('Please enter your email and password');
              return;
          }
          try {
              const response = await axiosClient.post(`Authencations/Login`, {
                  email: this.email,
                  password: this.password
              });
              if (response.data.token) {
                  localStorage.setItem('token', response.data.token);
                  this.$store.commit('setToken', response.data.token);
                  this.$router.push('/home');
              } else {
                  alert('Login failed. Please check your email and password.');
              }
          } catch (error) {
          if (error.response) {
              console.error('Login error:', error.response.data);
              if (error.response.status === 401) {
                  alert('Incorrect email or password. Please try again.');
              } else {
                  alert('An error occurred during login. Please try again.');
              }
          } else if (error.request) {
              console.error('Login error: No response from server', error.request);
          } else {
              console.error('Login error:', error.message);
          }
      }
  },
},
}
</script>