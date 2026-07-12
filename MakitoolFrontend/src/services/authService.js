
import api from './api';

export const authService = {
    login: async (credenciales) =>{
        const response = await api.post('/Auth/login', credenciales);
        return response.data;
    },

    solicitarCorreo: async(correo)=>{
        const response = await api.post('/Auth/forgot-password', {correo});
        return response.data;
    },

    restablecerPassword: async (token, nuevaClave) => {
        const response = await api.post('/Auth/reset-password', {token, nuevaClave});
        return response.data;
    }
};