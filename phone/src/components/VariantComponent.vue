<template>
    <div class="flex flex-wrap">
  <div v-for="(variant, index) in variants" 
       :key="index" 
       :class="['border-2 rounded-md text-xs px-2 py-1 mr-2 mb-2', 
                selectedVariantID === variant.variantID ? 'border-purple-400 text-purple-500' : 'border-gray-300 text-gray-400']"
       @click="handleVariantClick(variant.variantID)">
    <button>
      {{ variant.variantName }}
    </button>
  </div>
</div>
</template>

<script>
export default {
    data() {
        return {
            selectedVariantID: null
        }
    },
    props: {
        variants: {
            type: Array,
            required: true
        }
    },
    methods: {
        handleVariantClick(variantID) {
            if (this.selectedVariantID === variantID) {
                this.selectedVariantID = null;
            } else {
                this.selectedVariantID = variantID; 
            }
            this.$emit('variant-click', this.selectedVariantID);
        },
    }
}
</script>
