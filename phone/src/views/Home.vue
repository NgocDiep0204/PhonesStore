<template>
<div class="md:flex md:flex-row mt-20 max-w-screen overflow-hidden">
    <div class="md:w-2/5 flex flex-col justify-center items-center pl-5">
        <h2 class="font-serif text-3xl text-gray-600 mb-4 text-center md:text-left">Some Catchy Title Here</h2>
        <p class="uppercase text-gray-600 tracking-wide text-center md:text-left">Our brand tagline goes here.</p>
        <p class="uppercase text-gray-600 tracking-wide text-center md:text-left">Our brand motto goes here.</p>
        <router-link :to="{name:'AllProduct'}" class="bg-gradient-to-r from-red-600 to-pink-500 rounded-full p-3 text-gray-50 uppercase text-sm my-5 text-center mx-auto">Shop Now</router-link>
    </div>
    <div class="md:w-3/5 rounded-lg">
        <img src="/src/assets/img/logo.jpg" alt="" class="w-full h-auto max-h-[800px] pl-32 pr-3 rounded-full object-cover"> <!-- Sử dụng max-h-[800px] và object-cover để tránh tràn -->
    </div>
</div>
</template>

<script>
export default {
    
}
</script>

<style>

</style>