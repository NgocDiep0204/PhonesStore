<template>
    <div class="flex mt-2">
        <div v-for="(brand, index) in brands" :key="index" class="mr-4">
           <p class="bg-purple-100 rounded-full  text-gray-600 hover:text-purple-400 p-2" @click="brandClick(brand.brandID)"> {{ brand.brandName}}</p>
        </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex/dist/vuex.cjs.js';
import axiosClient from '../axiosClient';
export default {
    data(){
        return {
        }
    },
    computed:{
         ...mapState(['brands', 'models', 'previousModels', 'isPreviousModelsStored']),
    },
    async mounted(){
       await this.GetBrand();
    },
    methods:{
        ...mapActions(['GetBrand', 'GetModel']),
        brandClick(id){
            this.$router.push({ name: 'filter', params: { id: id } });
        }
    },
}
</script>

<style>

</style>