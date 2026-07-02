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
    }
}