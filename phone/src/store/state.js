const state = {
    brands: [],
    models: [],
    imgByIdProduct: null,
    productByModelId: [],
    colorByModelId: [],
    variantByModelId: [],
    imageByModelId: [],
    productByColorAndVariant: null,
    user: null,
    token: localStorage.getItem('token') || null,  
    cart:null,
    cartdetail: [],
    productByBrand: [],
    totalPages: 0,
    searchValue: null,
    searchResult: [],
    previousModels: [],
    isPreviousModelsStored: false,
    email:'',
    otp:'',

}
export default state;