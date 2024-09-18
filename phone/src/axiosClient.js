import axios from 'axios';
import router from './router';

const axiosClient = axios.create({
  baseURL: 'https://localhost:7285/api/', 
});

axiosClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});
export default axiosClient;