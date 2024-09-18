<template>
    <div >
    <ProductItem :searchResult="resultFilter" />

    </div>
</template>


<script>
import ProductItem from '../../components/ProductItem.vue';
import axiosClient from '../../axiosClient';
export default {
    data(){
        return{
            resultFilter :[]
        }
    },
    components:{
        ProductItem,
    },
    async mounted(){
        await this.GetModel(this.$route.params.id)
    },
    watch: {
    '$route.params.id': {
      immediate: true,
      handler(newVal) {
        console.log('Route param id changed:', newVal);
        this.GetModel(newVal)
      }
    
      }
    },
    methods:{
       async GetModel(id){
            var response = await axiosClient.get(`Models/GetModelByBrand?brand=${id}`)
            console.log('hi',response)
            if(response){
                this.resultFilter = response.data.result.$values
            }
           
        }
    },
}
</script>