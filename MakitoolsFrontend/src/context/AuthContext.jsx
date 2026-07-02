import React, {createContext, useState, useEffect} from 'react'
import { useNavigate } from 'react-router-dom'

export const AuthContext = createContext();

export const AuthProvider = ({children}) => {
    const [user, setUser] = useState(null);
    const [cargando, setCargando] = useState(true);
    const navigate = useNavigate();

    useEffect(()=>{
        const storedUser = localStorage.getItem('makitools_session');
        if(storedUser){
            setUser(JSON.parse(storedUser));
        }
        setCargando(false);
    }, []);

    const login = (userData) => {
        setUser(userData);
        localStorage.setItem('makitools_session', JSON.stringify(userData));
        navigate('/almacen/productos');
    };

    const logout = () => {
        setUser(null);
        localStorage.removeItem('makitools_session');
        navigate('/login');
    };

    if(cargando) return <div>Cargando sesion...</div>;

    return(
        <AuthContext.Provider value={{user, login, logout}}>
            {children}
        </AuthContext.Provider>
    )

}