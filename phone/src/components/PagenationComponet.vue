<template>
    <div class="pagination">
      <button
        v-for="page in totalPages"
        :key="page"
        @click="changePage(page)"
        :class="{'active': currentPage === page}">
        {{ page }}
      </button>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      totalPages: {
        type: Number,
        required: true
      },
      currentPage: {
        type: Number,
        required: true
      }
    },
    methods: {
      changePage(page) {
        this.$emit('page-changed', page);
      },
      changefetchData() {
        this.$emit('fetch-data');
      }
    }
  }
  </script>
  
  <style>
  .pagination button.active {
    background-color: #007bff;
    color: white;
  }
  </style>