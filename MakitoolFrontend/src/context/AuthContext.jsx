import React, {createContext, useState, useEffect} from 'react'
import { useNavigate } from 'react-router-dom'

export const AuthContext = createContext();

export const AuthProvider = ({children}) => {
    const [cargando, setCargando] = useState(true);
    const navigate = useNavigate();

    const [user, setUser] = useState(()=> {
        const usuarioGuardado = localStorage.getItem('makitools_session');
        return usuarioGuardado ? JSON.parse(usuarioGuardado) : null;
    });

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
        navigate('/');
    };

    const logout = () => {
        const confirmar = window.confirm("¿Estás seguro de que deseas cerrar sesión?")
        if(confirmar){
            setUser(null);
            localStorage.removeItem('makitools_session');
            navigate('/login');
        }
        
    };

    if(cargando) return (
            <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
                <div className="text-primary fw-bold fs-4">Cargando sesión de Makitools...</div>
            </div>
    );
    
    return(
        <AuthContext.Provider value={{user, login, logout}}>
            {children}
        </AuthContext.Provider>
    )

}