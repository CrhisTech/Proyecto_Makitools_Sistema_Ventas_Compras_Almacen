import api from './api';

export const productoService = {
    // GET: /api/producto
    listarProductos: async() => {
        const response = await api.get('/producto');
        return response.data;
    },

    // POST: /api/prodcuto
    crearProducto: async (productoFormData) => {
        const response = await api.post('/producto', productoFormData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        return response.data;
    },

    // GET: /api/producto/{id}
    obtenerProductoPorId: async (id) => {
        const response = await api.get(`/producto/${id}`);
        return response.data;
    },

    // PUT: /api/producto/{id}
    actualizarProducto: async (id, productoFormData) => {
        const response = await api.put(`/producto/${id}`, productoFormData, {
            headers: {'Content-Type': 'multipart/form-data'}
        });
        return response.data;
    },

    // DELETE: /api/producto/{id}
    eliminarProducto: async(id) => {
        const response = await api.delete(`/producto/{id}`);
        return response.data;
    }
}