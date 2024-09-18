<template>
  <div class="overflow-x-auto whitespace-nowrap">
    <div class="inline-flex">
      <div class="shadow-lg rounded-lg w-70 m-4 inline-block transform transition-transform duration-300 hover:scale-110" v-for="(product, index) in brandproducts" :key="index">
        <p class="flex justify-center w-full">
          <img :src="product.modelImgUrl" alt="" class="w-44 h-40 " @click="ProductView(product.modelID)">
        </p>
        <div class="p-6 flex justify-center w-full">
          <h3>{{ product.modelName }}</h3>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    brandproducts: {
      type: Array,
      required: true
    }
  },
  methods: {
    ProductView(modelID) {
     this.$router.push({ name: 'DetailProduct', params: { id: modelID } }).then(() => {
        this.$router.go();
      });
    }
  }
}
</script>

