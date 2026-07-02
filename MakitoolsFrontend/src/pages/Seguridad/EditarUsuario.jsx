import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { usuarioService } from '../../services/usuarioService';

export default function EditarUsuario() {
    const navigate = useNavigate();
    const { id } = useParams();
    const [cargando, setCargando] = useState(true);
    const [guardando, setGuardando] = useState(false);
    
    const [usuario, setUsuario] = useState({
        nombres: '', apellidos: '', correo: '', clave: '', idRol: ''
    });

    useEffect(() => {
        const cargarDatos = async () => {
            try {
                const usuario = await usuarioService.obtenerUsuarioPorId(id);
                setUsuario({
                    nombres: usuario.nombres,
                    apellidos: usuario.apellidos,
                    correo: usuario.correo,
                    clave: '',
                    idRol: usuario.idRol
                });

            } catch (error) {
                
                console.error("Error exacto del backend:", error.response || error);
                alert("Error al cargar la información del usuario.");
                navigate('/usuarios');
            } finally {
                setCargando(false);
            }
        };
        cargarDatos();
    }, [id, navigate]);

    const handleChange = (e) => setUsuario({ ...usuario, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        setGuardando(true);
        try {
            await usuarioService.actualizarUsuario(id, {
                ...usuario,
                idRol: parseInt(usuario.idRol)
            });
            alert("¡Usuario actualizado con éxito!");
            navigate('/usuarios'); 
        } catch (error) {
            alert(error.response?.data?.message || "Ocurrió un error al actualizar.");
        } finally {
            setGuardando(false);
        }
    };

    if (cargando) return <div className="p-4">Cargando datos...</div>;

    return (
        <div className="container-fluid" style={{ maxWidth: '900px' }}>
            <h2 className="h3 mb-4 text-gray-800">Editar Usuario</h2>

            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Modificar Datos</h6>
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Nombres</label>
                                <input type="text" name="nombres" value={usuario.nombres} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Apellidos</label>
                                <input type="text" name="apellidos" value={usuario.apellidos} onChange={handleChange} className="form-control" required />
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Correo Electrónico</label>
                                <input type="email" name="correo" value={usuario.correo} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Rol en el Sistema</label>
                                <select name="idRol" value={usuario.idRol} onChange={handleChange} className="form-select" required>
                                    <option value="">-- Seleccione un Rol --</option>
                                    <option value="1">Administrador General</option>
                                    <option value="2">Vendedor</option>
                                    <option value="3">Jefe de ventas</option>
                                    <option value="4">Auxiliar de Compras</option>
                                    <option value="5">Asistente de Compras</option>
                                    <option value="6">Jefe de Compras</option>
                                    <option value="7">Auxiliar de Almacén</option>
                                    <option value="8">Asistente de Almacén</option>
                                    <option value="9">Jefe de Almacén</option>
                                </select>
                            </div>
                        </div>
                        <div className="row mb-4">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Nueva Contraseña</label>
                                <input type="password" name="clave" value={usuario.clave} onChange={handleChange} className="form-control" minLength="6" />
                                <small className="text-danger fw-bold">Déjalo en blanco si NO deseas cambiar la contraseña actual.</small>
                            </div>
                        </div>
                        
                        <hr/>
                        <div className="row mt-4">
                            <div className="col-auto">
                                <button type="submit" disabled={guardando} style={{ cursor: guardando ? "wait" : "pointer" }} className="btn btn-primary btn-icon-split">
                                    <span className="icon text-white-50"><i className="fa-regular fa-floppy-disk"></i></span>
                                    <span className="text">{guardando ? "Guardando..." : "Actualizar Usuario"}</span>
                                </button>
                            </div>
                            <div className="col-auto">
                                <button type="button" disabled={guardando} onClick={() => navigate('/usuarios')} className="btn btn-danger btn-icon-split">
                                    <span className="icon text-white-50"><i className="fa-solid fa-rotate-left"></i></span>
                                    <span className="text">Volver</span>
                                </button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}