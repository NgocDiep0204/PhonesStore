import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue';  
import MainLayout from '../views/layout/MainLayout.vue';
import AllProduct from '../views/clients/AllProduct.vue';
import Login from '../views/Login.vue';
import Register from '../views/Register.vue';
import Cart from '../views/clients/Cart.vue';
import DetailProduct from '../views/clients/DetailProduct.vue';
import SearchResultComponent from '../components/SearchResultComponent.vue';
import FIlterByBrandName from '../views/clients/FIlterByBrandName.vue';
import SendOTP from '../views/SendOTP.vue';
import VerifyOTP from '../views/VerifyOTP.vue';
import ChangePassword from '../views/ChangePassword.vue';
const routes = [
    {
        path: '/',
        name: 'mainlayout',
        component: MainLayout,
        children: [
            {
                path: '/',
                name: '/',
                component: Home
            },
            {
                path: '/login',
                name: 'Login',
                component: Login
            },
            {
                path: '/register',
                name: 'Register',
                component: Register
            },
            {
                path:'shop',
                name:'AllProduct',
                component: AllProduct
            },
            {
                path:'cart/:id',
                name:'Cart',
                component: Cart,
                meta: { requiresAuth: true },
            },
            {
                path:'product-detail/:id',
                name:'DetailProduct',
                component: DetailProduct
            },
            {
                path:'search-product',
                name:'product-search',
                component:SearchResultComponent,
            },
            {
                path:'brand-filter/:id',
                name:'filter',
                component: FIlterByBrandName,
            },
            {
                path:'send-email',
                name:'SendEmail',
                component: SendOTP,
            },
            {
                path:'verify-otp',
                name:'Verify-OTP',
                component: VerifyOTP,
            },
            {
                path:'change-password',
                name:'ChangePassword',
                component: ChangePassword,
            },

        ]
    }
]
const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('token');
    if (to.matched.some(record => record.meta.requiresAuth) && !token) {
      next({ name: 'Login' });
    } else {
      next();
    }
  });

export default router;
