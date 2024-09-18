import axiosClient from "../axiosClient";

export async function GetBrand({ commit }) {
    const response = await axiosClient.get(`Brands/GetBrands`);
    if (response.data && response.data.$values) {
      commit('SET_BRAND', response.data.$values);
    }
}

export async function GetImage({ commit }, productId) {
    const response = await axiosClient.get(`Images/GetImages/${productId}`);
    if (response.data && response.data.$values) {
      commit('SET_IMAGE', response.data.$values);
    }
}

export async function GetModel({ commit }, currentPage) {
  const response = await axiosClient.get(`Models/GetAllModels?currentPageNumber=${currentPage}`);
  if (response.data && response.data.paged.$values) {
    commit('SET_MODEL', response.data.paged.$values);
    commit('SET_TOTAL_PAGES', response.data.totalPages);
  }
}

export async function GetImgByIdProduct({ commit }, productId) {
  const response = await axiosClient.get(`Images/GetImages/${productId}`);
  if (response.data && response.data.$values) {
    commit('SET_IMG_BY_ID_PRODUCT', response.data.$values);
  }
}

export async function GetProductByModelId({ commit }, modelId) {
    const response = await axiosClient.get(`Prodcuts/GetProductByModel?model=${modelId}`);
    if (response.data) {
      commit('SET_PRODUCT_BY_MODELID', response.data.result.$values);
    }
}

export async function GetColorByModelID({ commit }, modelId) {
  const response = await axiosClient.get(`Prodcuts/GetColorsByModelId?modelId=${modelId}`);
  if (response.data) {
    commit('SET_COLOR_BY_MODELID', response.data.$values);
  }
}

export async function GetVariantByModelId({ commit }, modelId) {
  const response = await axiosClient.get(`Variants/GetVariants?modelID=${modelId}`);
  if (response.data) {
    commit('SET_VARIANT_BY_MODELID', response.data.$values);
    console.log(response)
  }
}

export async function GetImageByModelID({ commit }, modelId) {
  const response = await axiosClient.get(`Images/GetImageByModelID?modelId=${modelId}`);
  if (response.data) {
    commit('SET_IMAGE_BY_MODELID', response.data.$values);
  }
}

export async function GetProductByColorAndVariant({commit},{ colorID, variantID, modelID }) {
  try {
    const response = await axiosClient.get(`Prodcuts/GetProductByColorAndVariant?colorID=${colorID}&variantID=${variantID}&modelID=${modelID}`);
    if (response.data) {
      commit('SET_PRODUCT_BY_COLOR_VARIANT', response.data.$values);
    }
  } catch (error) {
    console.log(error);     
  }
}

export async function logout({ commit }) {
  commit('logout');
}

export async function GetUser({ commit }) {
  try {
    const response = await axiosClient.get('Authencations/GetUserProfile');
    commit('setUser', response.data);
  } catch (error) {
    console.error('Error fetching user info:', error);
  }
}

export async function getCartByUserID({commit}, id){
  try {
  const response = await axiosClient.get(`Carts/GetCartsByUserId?userId=${id}`);
  commit('setCart', response.data.$values);
  } catch (error) {
    console.error('Error fetching user info:', error);
  }
}

export async function getCartDetail({commit}, id){
  try {
  const response = await axiosClient.get(`CartDetails/GetCartDetailsByUserID?userID=${id}`);
  commit('setCartDetail', response.data.$values);
  } catch (error) {
    console.error('Error fetching user info:', error);
  }
}

export async function GetProductByBrand({ commit }, brandId) {
  try{
    const response = await axiosClient.get(`Models/GetModelByBrand?brand=${brandId}`);
    if (response.data) {
     // console.log("hi",response)
      commit('SET_PRODUCT_BY_BRAND', response.data.result.$values);
    }
  } catch (error) {
    console.log(error);
  }
}

export async function SearchModel({ commit }, searchValue) {
    try {
      const response = await axiosClient.get(`Models/GetModelByName?name=${searchValue}`);
      console.log(response.data)
      if(response.data){
        commit('SET_SEARCH_RESULT', response.data.result.$values);
      }
    } catch (error){
      console.log(error)
    }
}

export function updateSearchValue({ commit }, searchValue) {
  commit('SET_SEARCH_VALUE', searchValue);
  console.log(searchValue)    
}