import api from './api';

export const categoriaService = {

    // GET: /api/categoria
    listarCategorias: async () => {
        const response = await api.get('/categoria');
        return response.data;
    },

    // POST: /api/categoria
    crearCategoria: async (categoria) => {
        const response = await api.post('/categoria', categoria);
        return response.data;
    },

    // GET: /api/categoria/{id}
    obtenerCategoriaPorId: async (id) => {
        const response = await api.get(`/categoria/${id}`);
        return response.data;
    }
}