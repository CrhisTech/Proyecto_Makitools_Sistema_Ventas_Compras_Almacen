import api from './api';

export const marcaService = {
    // GET: /api/marca
    listarMarcas: async () => {
        const response = await api.get('/marca');
        return response.data;
    },

    // POST: /api/marca
    crearMarca: async (marca) => {
        const response = await api.post('/marca', marca);
        return response.data;
    },

    // GET: /api/marca/{id} 
    obtenerMarcaPorId: async (id) => {
        const response = await api.get(`/marca/${id}`);
        return response.data;
    },

    // PUT: /api/marca/{id}
    actualizarMarca: async (id, marca) => {
        const response = await api.put(`/marca/${id}`, marca);
        return response.data;
    },

    // DELETE: /api/marca/{id}
    eliminarMarca: async (id) => {
        const response = await api.delete(`/marca/${id}`);
        return response.data;  
    }
}