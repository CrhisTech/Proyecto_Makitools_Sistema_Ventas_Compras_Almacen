import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { usuarioService } from '../../services/usuarioService';

export default function NuevoUsuario() {
    const navigate = useNavigate();
    const [guardando, setGuardando] = useState(false);
    const [formData, setFormData] = useState({
        nombres: '', apellidos: '', correo: '', clave: '', idRol: ''
    });

    const handleChange = (e) => setFormData({ ...formData, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        setGuardando(true);
        try {
            await usuarioService.crearUsuario({
                ...formData,
                idRol: parseInt(formData.idRol)
            }); 
            alert("¡Usuario registrado con éxito!");
            navigate('/usuarios'); 
        } catch (error) {
            alert(error.response?.data?.message || "Ocurrió un error al registrar el usuario.");
        } finally {
            setGuardando(false);
        }
    };

    return (
        <div className="container-fluid" style={{ maxWidth: '900px' }}>
            <h2 className="h3 mb-4 text-gray-800">Registrar Nuevo Usuario</h2>

            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Datos del Usuario</h6>
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Nombres</label>
                                <input type="text" name="nombres" value={formData.nombres} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Apellidos</label>
                                <input type="text" name="apellidos" value={formData.apellidos} onChange={handleChange} className="form-control" required />
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Correo Electrónico</label>
                                <input type="email" name="correo" value={formData.correo} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Rol en el Sistema</label>
                                <select name="idRol" value={formData.idRol} onChange={handleChange} className="form-select" required>
                                    <option value="">-- Seleccione un Rol --</option>
                                    <option value="1">Administrador General</option>
                                    <option value="2">Vendedor</option>
                                    <option value="4">Compras</option>
                                    <option value="7">Almacén</option>
                                </select>
                            </div>
                        </div>
                        <div className="row mb-4">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Contraseña Temporal</label>
                                <input type="password" name="clave" value={formData.clave} onChange={handleChange} className="form-control" minLength="6" required />
                                <small className="text-muted">Mínimo 6 caracteres.</small>
                            </div>
                        </div>
                        
                        <hr/>
                        {/* BOTONERA UNIFICADA */}
                        <div className="row mt-4">
                            <div className="col-auto">
                                <button type="submit" disabled={guardando} style={{ cursor: guardando ? "wait" : "pointer" }} className="btn btn-primary btn-icon-split">
                                    <span className="icon text-white-50"><i className="fa-regular fa-floppy-disk"></i></span>
                                    <span className="text">{guardando ? "Guardando..." : "Guardar Usuario"}</span>
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