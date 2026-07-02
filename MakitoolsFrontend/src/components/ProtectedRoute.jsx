import React, {useContext} from "react";
import { Navigate, Outlet } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

export const ProtectedRoute = ({allowedRoles}) => {
    const {user} = useContext(AuthContext);

    if(!user){
        return <Navigate to="/login" replace/>
    }

    if (allowedRoles && !allowedRoles.includes(user.idRol)){
        alet("No tienes permisos para acceder a este modulo.");
        return <Navigate to="/almacen/prodcutos" replace/>
    }

    return <Outlet/>;

}
