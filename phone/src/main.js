import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import store from './store'
import primeVue from 'primevue/config'
import Tooltip from 'primevue/tooltip'


import 'primevue/resources/themes/tailwind-light/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import Menu from 'primevue/menu';

//import store from './store'
const app = createApp(App)
app.use(router)
app.use(store)
app.use(primeVue)
app.component('Menu', Menu)
app.directive('tooltip', Tooltip)

app.mount('#app')
