<template>
    <div class="flex flex-wrap">
        <div v-for="(color, index) in colors" 
        :key="index" 
        :class="['border-2 rounded-md text-xs px-2 py-1 mr-2 mb-2', 
                    selectedColorID === color.colorID ? 'border-purple-400 text-purple-500'  : 'border-gray-300 text-gray-400']"
        @click="handleColorClick(color.colorID)">
            <button> {{ color.colorName }}</button>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            selectedColorID: null
        }
    },
    props: {
        colors: {
            type: Array,
            required: true
        }
    },
    methods: {
        handleColorClick(colorID) {
            if (this.selectedColorID === colorID) {
                this.selectedColorID = null; // Bỏ chọn nếu đã được chọn
            } else {
                this.selectedColorID = colorID; // Chọn màu mới
            }
            this.$emit('color-click', this.selectedColorID);
        }
    }
}
</script>
