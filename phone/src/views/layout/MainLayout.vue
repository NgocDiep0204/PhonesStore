<template>
    <div :class="{'w-full h-screen bg-gray-300': isChangePasswordPage || isSendEmailPage || isVerifyOtpPage, 'w-full h-screen': !isChangePasswordPage && !isSendEmailPage}">
        <HeaderComponent v-if="!isChangePasswordPage && !isSendEmailPage && !isVerifyOtpPage"/>
        <div class="flex flex-row justify-between items-center pt-10">
            <div class="w-1/2 pt-6 md:block">
                <SearchComponent v-if="!isHomePage && !isLoginPage && !isRegisterPage && !isChangePasswordPage && !isSendEmailPage && !isVerifyOtpPage" @search-results="handleSearchResults" />
            </div>
            <div class="w-1/2 hidden md:block">
                <SideBarComponent v-if="!isHomePage && !isLoginPage && !isRegisterPage && !isChangePasswordPage && !isSendEmailPage && !isVerifyOtpPage" />
            </div>
        </div>
        
        <div>
            <router-view />
        </div>
        <FooterComponent/>
    </div>
</template>

<script>
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import HeaderComponent from './../../components/HeaderComponent.vue'
import FooterComponent from './../../components/FooterComponent.vue'
import SideBarComponent from './../../components/SideBarComponent.vue'
import SearchComponent from './../../components/SearchComponent.vue'
import ProductItem from '../../components/ProductItem.vue';
import { mapState, mapActions } from 'vuex'
export default {
    components: {
        HeaderComponent,
        FooterComponent,
        SideBarComponent,
        SearchComponent,
        ProductItem,
    },
    setup() {
        const route = useRoute();
        const isHomePage = computed(() => route.name === '/');
        const isLoginPage = computed(() => route.name === 'Login');
        const isRegisterPage = computed(() => route.name === 'Register');
        const isSendEmailPage = computed(() => route.name === 'SendEmail');
        const isChangePasswordPage = computed(() => route.name === 'ChangePassword');
        const isVerifyOtpPage = computed(() => route.name === 'Verify-OTP');
      
        return { isHomePage, isLoginPage, isRegisterPage, isSendEmailPage, isChangePasswordPage, isVerifyOtpPage };
    },
    computed:{
        ...mapState(['searchResult']),
    },
    methods:{
        ...mapActions(['SearchModel']),
    }
}
</script>
