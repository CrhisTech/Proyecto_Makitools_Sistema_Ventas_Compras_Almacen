
import api from './api';

export const authService = {
    login: async (credenciales) =>{
        const response = await api.post('/Auth/login', credenciales);
        return response.data;
    }
};