<template>
        <div class="bg-gradient-to-r from-purple-200 to-pink-200 w-full h-2 my-7"></div>
            <div class="flex flex-row">
                <div class="w-4/6 border-purple-100 border-4 rounded-lg mr-5">
                    <div class="flex flex-row">
                        <div class="w-2/5">
                            <img src="" alt="product">
                        </div>
                        <div class="3/5">
                                hi
                        </div>
                    </div>
                </div>
                <div class="w-2/6  border-pink-200 border-4 rounded-lg ml-5">
                    hi
                </div>
            </div>
       
</template>

<script>
export default {
    props:{
        cartdetail:{
            type: Array,
            required: true
        }
    },
}
</script>

<style>

</style>