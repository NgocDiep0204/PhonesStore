import { createStore } from 'vuex';
import axiosClient from "../axiosClient";
import state  from './state';
import * as mutations from './mutations';
import * as actions from './actions';
import * as getters  from './getters'
// const state = {
//     brands: [],
   
// }
// const mutations = {
//     SET_BRAND(state, brands) {
//         state.brands = brands
//         console.log('df',state.brands)
//     }
// }
// const actions = {
//     async GetBrand({commit, state}) {
//         try {
//             var response = await axiosClient.get(`Brands/GetBrands`)
//             commit('SET_BRAND', response.data.$values)
//         } catch (error) {
//             console.log(error)
//         }
      
//     }
// }

const store = createStore({
    state,
    actions,
    mutations,
    getters
  });

export default store;