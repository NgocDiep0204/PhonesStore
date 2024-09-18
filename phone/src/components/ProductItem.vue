<template>
  <div class="flex flex-col items-center justify-center min-h-screen px-5">
    <div class="grid grid-flow-row md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-3 gap-10 max-w-6xl" v-if="searchResult.length > 0">
      <div class="shadow-lg rounded-lg w-70" v-for="model in searchResult" :key="model.modelID">
        <p class="flex justify-center w-full">
          <img :src="model.modelImgUrl" alt="" class="w-44 h-40" @click="ProductView(model.modelID)">
        </p>
        <div class="p-6 flex justify-center w-full">
          <h3>{{ model.modelName }}</h3>
        </div>
        <div class="flex flex-row justify-between items-center m-5 space-x-4 p-2">
          <p class="bg-gradient-to-r from-red-600 to-pink-500 rounded-full py-1 px-2 text-gray-50 text-sm  flex items-center hover:from-pink-600 hover:to-red-600">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4 mr-1">
              <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z" />
            </svg>
            Add to cart
          </p>
          <p class="bg-gradient-to-r from-purple-600 to-purple-500 rounded-full py-1 px-2 text-gray-50 text-sm flex items-center hover:from-purple-500 hover:to-purple-600">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4 mr-1">
              <path stroke-linecap="round" stroke-linejoin="round" d="M13.5 4.5 21 12m0 0-7.5 7.5M21 12H3" />
            </svg>
            View Details
          </p>
        </div>
      </div>
    </div>
    <p v-else>No results found</p>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex';

export default {
  props: {
    searchResult: {
      type: Array,
      required: true
    }
  },
  data() {
    return {
      //localSearchQuery: this.searchQuery
    };
  },
  computed: {
   // ...mapState(['searchResult', 'searchValue'])
  },
  methods: {
    ...mapActions(['SearchModel', ]),
    async fetchSearchResults() {
      if (this.searchValue) {
        await this.SearchModel(this.searchValue);
      }
    },
    ProductView(modelID) {
      this.$router.push({ name: 'DetailProduct', params: { id: modelID } });
    }
  },
  
  mounted() {
   // this.fetchSearchResults();
  }
};
</script>
