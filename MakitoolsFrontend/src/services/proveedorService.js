import api from './api';

export const proveedorService = {
    // GET: /api/marca
    listarProveedores: async() => {
        const response = await api.get('/proveedor');
        return response.data;
    },
    // POST: /api/marca
    crearProveedor: async (proveedor) => {
        const response = await api.post('/proveedor', proveedor);
        return response.data;
    },
    // GET: /api/marca/{id} 
    obtenerProveedorPorId: async (id) => {
        const response = await api.get(`/proveedor/${id}`);
        return response.data;
    },

    // PUT: /api/producto/{id}
    actualizarProveedor: async(id, proveedor) => {
        const response = await api.put(`/proveedor/${id}`, proveedor);
        return response.data;
    },

    // DELETE: /api/producto/{id}
    eliminarProveedor: async(id) => {
        const response = await api.delete(`/proveedor/${id}`);
        return response.data;
    }
}