import api from './api';

export const usuarioService = {
    // GET: /api/usuario
    listarUsuarios: async() => {
        const response = await api.get('/usuario');
        return response.data;
    },

    // POST: /api/usuario
    crearUsuario: async(usuario) => {
        const response = await api.post('/usuario', usuario);
    },

    // GET: /api/usuario/{id}
    obtenerUsuarioPorId: async(id) => {
        const response = await api.get(`/usuario/${id}`);
        return response.data
    },

    // PUT: /api/usuario/{id}
    actualizarUsuario: async(id, usuario)=> {
        const response = await api.put(`/usuario/${id}`, usuario);
        return response.data
    } ,

    // DELETE: /api/usuario/{id}
    eliminarUsuario: async (id) => {
        const response = await api.delete(`usuario/${id}`);
        return response.data
    }
}