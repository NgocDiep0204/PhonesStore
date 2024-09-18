<template>
    <div class="bg-gradient-to-r from-purple-200 to-pink-200 w-full h-2 my-7"></div>
    <div class="flex flex-row">
    <div class="flex flex-col pl-4" v-if="cartdetail">
    <div v-if="cartdetail.length === 0">No items in the cart</div>
      <div class="w-4/6 border-purple-100 border-4 rounded-lg mb-5" v-for="(detail, index) in cartdetail" :key="detail.cartID">
        <div class="flex flex-row">
          <div class="ml-2 flex justify-center">
            <input type="checkbox" class="mr-2" @click="checkClick(detail.cartID, detail.productID, $event)" />
          </div>  
          <div class="w-2/5 pr-8">
            <div v-for="(image, imgIndex) in detail.images" :key="imgIndex">
              <img :src="image" alt="product">
            </div>
          </div>
          <div class="w-3/5 pt-2">
            <span>{{ detail.product.variant.model.modelName }}</span>
            <div class="flex flex-row pt-2">
              <div class="border-2 border-gray-400 w-10 h-7" @click="minusClick(detail.productID)">
                <button class="flex justify-center items-center w-full">-</button>
              </div>
              <div class="border-t-2 border-b-2 border-gray-400 w-10 h-7">
                <span class="flex justify-center items-center w-full">{{ detail.quantity }}</span>
              </div>
              <div class="border-2 border-gray-400 w-10 h-7" @click="addClick(detail.productID)">
                <button class="flex justify-center items-center w-full">+</button>
              </div>
            </div>
            <div class="pt-2">
              <span class="pr-2">Type: {{ detail.product.variant.variantName }} </span>
              <span>{{ detail.product.color.colorName }}</span>
            </div>
          </div>
          <div class="flex justify-center items-center m-2">
            <span >{{ detail.product.price }}$</span>
          </div>
        </div>
      </div>
    </div>
    <div class="w-2/6 border-pink-300 border-4 rounded-lg h-32" v-if="cartdetail.length >=0">
      <div class="flex justify-center m-5 w-[200px]">
        <div class="flex flex-col">
          <strong class="">Total Quantity: {{ totalquanlity }} </strong>
          <strong class="">Total Amount:   {{ total }}</strong>
        </div>
        
      </div>
     
    </div>
    </div>
  </template>


<script>
import { mapActions, mapState } from 'vuex/dist/vuex.cjs.js';   
import axiosClient from '../../axiosClient';

export default {
    data() {
        return {
            total: 0,
            totalquanlity: 0,
            selectedItems: [],
            checkitem:{},
            images: [],
            cartdetailUP :{
              cartID: null,
              productID: null,
              quantity: null,
            }
        }
    },
    computed: {
        ...mapState(['cartdetail']),
  
    },
    async mounted() {
      await this.getCartDetail(this.$route.params.id);
      await this.fetchImagesForCart();
    },
    
    methods: {
        ...mapActions(['getCartDetail']),
        addClick(productID) {
          console.log( productID); 
            const detail = this.cartdetail.find(detail => detail.cartID === this.cartdetail[0].cartID && detail.productID === productID);
            if (detail) {
            detail.quantity++;
            this.updateCartDetail(detail.cartID, productID, detail.quantity);
          } else {
              console.error(`No detail found for cartID:  and productID: ${productID}`);
          }
        },
        minusClick( productID) {
          const detail = this.cartdetail.find(detail => detail.cartID === this.cartdetail[0].cartID && detail.productID === productID);
            if(detail.quantity > 1) {
              detail.quantity--;
            this.updateCartDetail(detail.cartID, productID, detail.quantity);
            
            }else if(detail.quantity === 1) {
              const userConfirmed = confirm("Are you sure you want to delete this item from the cart?");
              if (userConfirmed) {
                this.deleteCartDetail(detail.cartID, productID);
              }
            }
        },
        async updateCartDetail(cartID, productID, quantity) {
          this.cartdetailUP.cartID = cartID
          this.cartdetailUP.productID = productID
          this.cartdetailUP.quantity = quantity
          const response = await axiosClient.put('CartDetails/UpdateCartDetail', this.cartdetailUP);
        },

        async deleteCartDetail(cartID, productID) {
          const response = await axiosClient.delete(`CartDetails/DeleteCartDetail?cartID=${cartID}&productID=${productID}`);
          if (response.status === 200) {
            console.log('Cart detail deleted successfully');
            const detail = this.cartdetail.find(detail => detail.cartID === cartID && detail.productID === productID);
            if(detail){
              this.cartdetail.remove(detail); 
            }
          } 
        },
        async fetchImagesForCart() {
          for (let detail of this.cartdetail) {
            detail.images = await this.getImage(detail.productID);
          }
        },
        async getImage(id){
          try {
            const response = await axiosClient.get(`Images/GetImages?productId=${id}`);
            if (response.data.$values && response.data.$values.length > 0) {
              const imageUrls = response.data.$values.map(image => image.imageUrl);
              this.images = imageUrls;
              return imageUrls;
            } else {
              return [];
            }
          } catch (error) {
            return [];
          }
        },
        
      async checkClick(cartID, productID, event) {
        const isChecked = event.target.checked;
        const amount = this.cartdetail
                           .filter(detail => detail.cartID == cartID && detail.productID == productID)
                           .reduce((acc, detail) => acc + detail.product.price * detail.quantity, 0);
        if (isChecked) {
          this.totalquanlity += 1
          this.total += amount;
        } else {
          this.totalquanlity -= 1
          this.total -= amount;
        }
      },
    }
}
</script>