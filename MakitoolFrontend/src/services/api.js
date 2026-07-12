import axios from 'axios';

const api = axios.create({
    baseURL:'http://localhost:5259/api',
    headers: {
        'Content-Type': 'application/json'
    }
});

api.interceptors.request.use(
    (config)=>{
        const storedUser = localStorage.getItem('makitools_session');
        if(storedUser){
            const user = JSON.parse(storedUser);

            if(user.token){
                config.headers['Authorization'] = `Bearer ${user.token}`;
            }
        }
        return config;
    },
    (error)=>{
        return Promise.reject(error);
    }
)

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response && error.response.status === 401){
            console.error("Acceso denegado o sesion expirada.")
            localStorage.removeItem('maitools_session');
            window.location.href='/login';
        }
        return Promise.reject(error);
    }
);

export default api;