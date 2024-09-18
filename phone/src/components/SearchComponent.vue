<template>
     <div class="relative w-full max-w-md mx-auto">
                <input type="text" 
                       class="w-full px-10 py-2 border rounded-lg focus:outline-none focus:ring-2  focus:ring-purple-500" 
                       placeholder="Search..."
                       v-model="searchQuery"
                       @input="handleInput">
                <div class="absolute inset-y-0 left-0 flex items-center pl-3">
                    <svg class="w-5 h-5 text-gray-500" fill="currentColor" viewBox="0 0 20 20">
                        <path fill-rule="evenodd" d="M12.9 14.32a8 8 0 111.42-1.42l5.34 5.33a1 1 0 01-1.42 1.42l-5.33-5.33zM10 16a6 6 0 100-12 6 6 0 000 12z" clip-rule="evenodd"/>
                    </svg>
                </div>
            </div>
  </template>

<script>
import { mapActions, mapState } from 'vuex';
export default {
    data() {
    return {
      searchQuery : null,
      
    };
  },
  computed: {
    ...mapState(['searchResult'])
  },
  watch: {
    searchQuery(newValue) {
      if (newValue) {
        this.SearchModel(newValue);
        this.$store.commit('SET_SEARCH_VALUE', newValue);
        console.log(newValue)
      }
    }
  },

  methods: {
    ...mapActions(['SearchModel', 'updateSearchValue']),
    // async handleInput(event) {
    //   this.searchQuery = event.target.value;
    //   if (this.searchQuery) {
    //     console.log(this.searchQuery)
    //     //await this.updateSearchValue(this.searchQuery);
    //    // this.$router.push({ name: 'product-search' });
    //   }
    // }
    handleInput(event) {
      this.$emit('update-query', event.target.value);
      if(event.target.value){
        this.$router.push({ name: 'product-search'});
      }
      else{
        this.$router.back(); // Làm mới trang hiện tại
      }
    }
  }
}
</script>