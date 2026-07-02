import api from './api';

export const clienteService = {
    listarClientes: async () => {
        const response = await api.get('/cliente');
        return response.data;
    },
    
    obtenerClientePorId: async (id) => {
        const response = await api.get(`/cliente/${id}`);
        return response.data;
    },
    
    crearCliente: async (cliente) => {
        const response = await api.post('/cliente', cliente);
        return response.data;
    },

    actualizarCliente: async (id, cliente) => {
        const response = await api.put(`/cliente/${id}`, cliente);
        return response.data;
    },

    eliminarCliente: async (id) => {
        const response = await api.delete(`/cliente/${id}`);
        return response.data;
    }
};