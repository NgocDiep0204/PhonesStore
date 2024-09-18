<template>
      <!-- begin hedaer -->
  <div class="containner h-8 mx-auto p-5 w-full">
    <div class="md:flex md:flex-row md:justify-between text-center">
        <div class="flex flex-row justify-center">
            <div class="bg-gradient-to-r from-purple-600 to-red-600 h-8 w-8 rounded-lg"> </div>
            <h1 class="text-gray-600 text-xl ml-2">VirGo</h1>
        </div>
        <div class="mt-2">
            <router-link :to="{name:'mainlayout'}" class=" text-gray-600 hover:text-purple-400 p-4">Home</router-link>
            <router-link :to="{name:'AllProduct'}" class=" text-gray-600 hover:text-purple-400 p-4">Shop</router-link>
            <router-link v-if="user" :to="{name: 'Cart', params: {id: user.id}}" class=" bg-gradient-to-r from-purple-300 to-pink-300  text-gray-50 hover:text-purple-400 p-1.5 px-4 rounded-full ">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6 inline-block">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z" />
                </svg>
                Cart
            </router-link>
            <router-link :to="{name:'Login'}" class=" text-gray-600 hover:text-purple-400 p-4" v-if="!user">Login</router-link>
                <span @click="toggle" class="text-gray-600 hover:text-purple-400 p-4" v-if="user">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6 inline-block">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                </svg>
                <Menu id="overlay_menu" ref="menu" :model="items" :popup="true" />
            </span>
           
        </div>
    </div>
  </div>

 

  <!-- end header -->
</template>

<script>
import { mapState, mapActions } from 'vuex/dist/vuex.cjs.js';
export default {
    data(){
        return {
          
            
            value: null,
            items: [
                {
                    label: 'Profile',
                    icon: 'pi pi-user',
                    command: () => {
                        this.$router.push('/profile');
                    }
                },
                {
                    label: 'Logout',
                    icon: 'pi pi-sign-out',
                    command: () => {
                        localStorage.removeItem('token');
                        this.$store.dispatch('logout'); 
                        this.$router.push('/login');
                    }
                }
            ]
        };
    },
    computed:{
        ...mapState(['user', 'token']),
    },
    watch:{
        token: {
            handler(newToken) {
                if (newToken) {
                    this.GetUser();
                }
            }
        }
    },
    
    async created() {
        if(this.token){
            await this.GetUser();
        }
    //  await this.GetUser();
    //     console.log("user:", this.user);
    },
    methods:{
        ...mapActions(['GetUser']),
        toggle(event) {
           this.$refs.menu.toggle(event);
        },
    }
}
</script>

<style>

</style>