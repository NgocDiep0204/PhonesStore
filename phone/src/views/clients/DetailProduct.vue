<template>
  <div v-if="imageByModelId">
    <div class="gap-10 w-full max-w-6xl my-10" v-if="imageByModelId.length">
      <div class="rounded-lg" >
        <div class="flex md:flex-row flex-col justify-center items-center px-5 pb-10">
          <div class="w-2/5 relative group">
            <div class="flex justify-center">
                <img :src="currentImageUrl" alt="" class="w-64 h-64">
                <button @click="prevImage" class="absolute left-0 top-1/2 transform -translate-y-1/2 bg-gray-500 text-white px-2 py-1 opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-9">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M15.75 19.5 8.25 12l7.5-7.5" />
                  </svg>
                </button>
                <button @click="nextImage" class="absolute right-0 top-1/2 transform -translate-y-1/2 bg-gray-500 text-white px-2 py-1 opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                  <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-9">
                    <path class="" stroke-linecap="round" stroke-linejoin="round" d="m8.25 4.5 7.5 7.5-7.5 7.5" />
                  </svg>
                </button>
              </div>
            </div>
          <div class="pl-6 w-3/5">
            <h3 class="pb-6 font-bold">{{ imageByModelId[0].product.variant.model.modelName }}</h3>
            <div class="">
              <div class="flex md:flex-row flex-col">
                <span class="pr-8">Colors:</span><ColorComponent v-if="colorByModelId.length" :colors="colorByModelId"  @color-click="handleColorClick" />
              </div>
              <div class="flex md:flex-row flex-col">
                <span class="pr-5">Variants:</span><VariantComponent v-if="variantByModelId.length" :variants="variantByModelId" @variant-click="handleVariantClick" />
              </div>
            </div>
            <div class="flex md:flex-row flex-col">
              <label class="pr-4">Quantity:</label> 
            <div class="flex flex-row-3">
              <div class="border-2 border-gray-400 w-10 h-7" @click="minusClick">
                <button class="flex justify-center items-center w-full">-</button>
              </div>
              <div class="border-t-2 border-b-2 border-gray-400 w-10 h-7">
                <span class="flex justify-center items-center w-full">{{ addcartdetail.quantity }}</span>
              </div>
              <div class="border-2 border-gray-400 w-10 h-7" @click="addClick">
                <button class="flex justify-center items-center w-full">+</button>
              </div>
            </div>
            </div>
            <div class="flex flex-rowl">
              <span class="pr-2">
                Price:
              </span>
              <p class="text-gray-600 pl-9" v-if="!productByColorAndVariant">{{ minPrice }}$ -> {{maxPrice}}$</p>
              <p class="text-gray-600 pl-9" v-if="productByColorAndVariant">{{ minPrice }}$</p>
            </div>
            <div class="flex flex-row">
              <span class="pr-2">
                Description:
              </span><p>{{imageByModelId[0].product.description}}</p>
            </div>
            <div class="flex flex-row justify-between items-center pt-10">
              <button @click="addToCartClick" class="bg-gradient-to-r from-red-600 to-pink-500 rounded-full py-1 px-2 text-gray-50 text-sm  flex items-center hover:from-pink-600 hover:to-red-600">
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-4 h-4 mr-1">
                  <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 3h1.386c.51 0 .955.343 1.087.835l.383 1.437M7.5 14.25a3 3 0 0 0-3 3h15.75m-12.75-3h11.218c1.121-2.3 2.1-4.684 2.924-7.138a60.114 60.114 0 0 0-16.536-1.84M7.5 14.25 5.106 5.272M6 20.25a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Zm12.75 0a.75.75 0 1 1-1.5 0 .75.75 0 0 1 1.5 0Z" />
                </svg>
                Add to cart
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div class="overflow-x-auto mt-0">
    <h2 class="font-bold pb-3 pl-10 text-xl">Recommended Products</h2>
    <RecommendedProducts :brandproducts="productByBrand" />
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import ColorComponent from '../../components/ColorComponent.vue';
import VariantComponent from '../../components/VariantComponent.vue';
import RecommendedProducts from '../../components/RecommendedProducts.vue';
import axiosClient from '../../axiosClient';
export default {
  data() {
    return {
      token : localStorage.getItem('token'),
      currentImageIndex: 0,
      colorId: null,
      variantId: null,
      maxPrice: 0,
      minPrice: 0,
      addcartdetail:{
        cartID : null,
        productID : null,
        quantity : 1,
      }
    }
  },
    components: {
        ColorComponent,
        VariantComponent,
        RecommendedProducts,
    },
   
    watch: {
    '$route.params.id': {
      immediate: true,
      handler(newVal) {
        console.log('Route param id changed:', newVal);
        this.GetImageByModelID(newVal)
      }
    
      }
    },
    computed: {
      ...mapState(['colorByModelId', 'variantByModelId', 'imageByModelId',
       'productByColorAndVariant','cart', 'user', 'cartdetail', 'productByBrand', ]),
      currentImageUrl() {
        var img = this.imageByModelId[this.currentImageIndex].imageUrl;
        return img;
      }
    },
    async mounted() {
        await this.GetColorByModelID(this.$route.params.id)
        await this.GetVariantByModelId(this.$route.params.id)
        await this.GetImageByModelID(this.$route.params.id)
        if(this.token){
          await this.getCartByUserID(this.user.id)  
          console.log("carrt ", this.cart)

          await this.getCartDetail(this.user.id)
        }
        console.log('hi',this.imageByModelId)
        await this.GetProductByBrand(this.imageByModelId[0].product.variant.model.brandID)
        this.calculatePrices();
    },
    methods:{
      ...mapActions(['GetColorByModelID', 'GetVariantByModelId', 'GetImageByModelID', 
      'GetProductByColorAndVariant', 'getCartByUserID', 'getCartDetail','GetProductByBrand','GetUser']),
      async addToCartClick() {
        if (!this.token) {
          alert('Please login to add to cart');
          this.$router.push({ name: 'Login' });
          return;
        }
        if (!this.productByColorAndVariant || this.productByColorAndVariant.length === 0) {
          alert('Please choose color and variant');
          return;
        }
        this.addcartdetail.cartID = this.cart[0].cartID;
        this.addcartdetail.productID = this.productByColorAndVariant[0].productID;
        let productFound = false;
        for(let i = 0; i < this.cartdetail.length; i++){
          if(this.cartdetail[i].productID === this.productByColorAndVariant[0].productID){
            productFound = true;
            this.cartdetail[i].quantity += this.addcartdetail.quantity;
            try {
              var response = await axiosClient.put(`CartDetails/UpdateCartDetail`, this.cartdetail[i]);
              if(response.status === 200){
                alert('Add to cart successfully');
                console.log('Cart detail updated successfully');
              } else {
                alert('Add to cart failed');
                console.log('Response status:', response.status);
              } 
            } catch (error) {
              console.error('Error updating cart detail:', error);
            }
          }
        }
        if (productFound == false) {
        // Handle the case where the product is not found in the cart
        console.log('Product not found in cart, adding new product');
        try {
          var response = await axiosClient.post(`CartDetails/AddToCart`, this.addcartdetail);
          if(response.status === 200){
          alert('Add to cart successfully');
          console.log('New cart detail added successfully');
          } else {
            alert('Add to cart failed');
            console.log('Response status:', response.status);
          }
        } catch (error) {
          console.error('Error adding new cart detail:', error);
        }
      }
      },
      prevImage() {
        if (this.currentImageIndex > 0) {
          this.currentImageIndex--;
        } else {
          this.currentImageIndex = this.imageByModelId.length - 1;
        }
      },
    nextImage() {
      if (this.currentImageIndex < this.imageByModelId.length - 1) {
        this.currentImageIndex++;
      } else {
        this.currentImageIndex = 0;
      }
    },
    async handleColorClick(id) {
      this.colorId = id;
      this.checkAndFetchProduct()
    },
    async handleVariantClick(id) {
      this.variantId = id;
      this.checkAndFetchProduct()
    },
    async checkAndFetchProduct() {
      if (this.colorId && this.variantId) {
        try {
          await this.$store.dispatch('GetProductByColorAndVariant', { colorID: this.colorId, variantID: this.variantId, modelID: this.$route.params.id });
          if(this.productByColorAndVariant){
          this.minPrice = this.productByColorAndVariant[0].price;
          this.maxPrice = null}
        } catch (error) {
          console.error('Error fetching product:', error);
        }
      }else{
        this.$store.commit('SET_PRODUCT_BY_COLOR_VARIANT', null);
        this.calculatePrices();
      }
    },
    addClick() {
      this.addcartdetail.quantity += 1;
    },
    minusClick() {
      if (this.addcartdetail.quantity > 1) {
        this.addcartdetail.quantity -= 1;
      }
    },
   
    calculatePrices() {
      const prices = this.imageByModelId.map(item => item.product.price);
      this.maxPrice = Math.max(...prices);
      this.minPrice = Math.min(...prices);
    },
    GoDetail(id) {
      this.$router.push({ name: 'DetailProduct', params: { id: id } });
    }
   
  }

}
</script>