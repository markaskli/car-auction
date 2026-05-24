import axios from 'axios';
import keycloak from './keycloak-config';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL ?? 'api/v1',
  withCredentials: true,
});

api.interceptors.request.use((config) => {
  if (keycloak.token) {
    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${keycloak.token}`,
    };
  }

  return config;
});

export default api;
