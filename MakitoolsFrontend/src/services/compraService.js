import api from './api';

export const compraService = {
    // GET: 
    listarCompras: async () => {
        const response = await api.get('/Compra');
        return response.data;
    },
    
    // GET:
    obtenerCompraPorId: async (id) => {
        const response = await api.get(`/Compra/${id}`);
        return response.data;
    },
    
    // POST: 
    generarPedido: async (pedido) => {
        const response = await api.post('/Compra', pedido);
        return response.data;
    },

    // PATCH: 
    cambiarEstado: async (id, nuevoEstado) => {
        const response = await api.patch(`/Compra/${id}/estado`, { estado: nuevoEstado });
        return response.data;
    }
};