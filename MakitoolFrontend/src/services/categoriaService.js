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
    },

    // PUT: /api/categoria/{id}
    actualizarCategoria: async (id, categoria) => {
        const response = await api.put(`/categoria/${id}`, categoria);
        return response.data;
    },

    // DELETE: /api/categoria/{id}
    eliminarCategoria: async (id) => {
        const response = await api.delete(`/categoria/${id}`);
        return response.data;  
    }
}