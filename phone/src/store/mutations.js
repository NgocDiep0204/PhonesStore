export function SET_BRAND(state, brands) {
    state.brands = brands
}
export function SET_IMAGE(state, image) {
    state.image = image
}
export function SET_MODEL(state, models) {
    state.models = models
}
///////////PRODUCT/////////////////////
export function SET_IMG_BY_ID_PRODUCT(state, imgByIdProduct) {
    state.imgByIdProduct = imgByIdProduct
}
export function SET_PRODUCT_BY_MODELID(state, productByModelId) {
    state.productByModelId = productByModelId
}
export function SET_COLOR_BY_MODELID(state, colorByModelId) {
    state.colorByModelId = colorByModelId
}

export function SET_VARIANT_BY_MODELID(state, variantByModelId) {
    state.variantByModelId = variantByModelId
}

export function SET_IMAGE_BY_MODELID(state, imageByModelId) {
    state.imageByModelId = imageByModelId
}
export function SET_PRODUCT_BY_COLOR_VARIANT(state, productByColorAndVariant) {
    state.productByColorAndVariant = productByColorAndVariant
}

////////AUTH/////////////////////
export function setUser(state, user) {
    state.user = user;
}
export function setToken(state, token) {
    state.token = token;
}
export function clearAuthData(state) {
    state.user = null;
    state.token = null;
  }
export function logout(state) {
    state.user = null; 
}
////////CART/////////////////////
export function setCart(state, cart) {
    state.cart = cart
}

export function setCartDetail(state, cartdetail) {
    state.cartdetail = cartdetail
}

export function SET_PRODUCT_BY_BRAND(state, productByBrand) {
    state.productByBrand = productByBrand
}

//////PAGINATION//////////
export function SET_TOTAL_PAGES(state, totalPages) {
    state.totalPages = totalPages
}

///////SEARCH///////////////
export function SET_SEARCH_RESULT(state, searchResult){
    state.searchResult = searchResult
}

export function SET_SEARCH_VALUE(state, value) {
    state.searchValue = value;
  }

////////////////LOC/////////
export function SET_PREVIOUS_MODELS(state, models) {
    state.previousModels = models;
}
export function SET_IS_PREVIOUS_MODELS_STORED(state, isStored) {
    state.isPreviousModelsStored = isStored;
}



///////////////CHHANGE PASSWORD///////////
export function SET_EMAIL(state, email){
    state.email = email
}

export function SET_OTP(state, otp){
    state.email = otp
}