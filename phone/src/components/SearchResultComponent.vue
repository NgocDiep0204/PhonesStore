<template>
     <ProductItem :searchResult="searchResult" />
</template>

<script>
import ProductItem from './ProductItem.vue';
import { mapActions, mapState } from 'vuex';
export default {
  components: {
    ProductItem
  },
  data() {
    return {
      searchQuery: ''
    };
  },
  computed: {
    ...mapState(['searchResult', 'searchValue'])
  },
  mounted(){
    this.fetchSearchResults();
  },
  methods:{
    ...mapActions(['SearchModel', ]),
    async fetchSearchResults() {
      if (this.searchValue) {
        await this.SearchModel(this.searchValue);
      }
    },
  },

  
 
};
</script>

<style>

</style>