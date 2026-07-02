import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { clienteService } from '../../services/clienteService';

export default function EditarCliente() {
    const navigate = useNavigate();
    const { id } = useParams();
    const [cargando, setCargando] = useState(true);
    const [guardando, setGuardando] = useState(false);
    
    const [cliente, setCliente] = useState({
        tipoDocumento: '', numeroDocumento: '', nombres: '', apellidos: '', correo: '', telefono: '', clave: ''
    });

    useEffect(() => {
        const cargarDatos = async () => {
            try {
                const cliente = await clienteService.obtenerClientePorId(id);
                setCliente({
                    tipoDocumento: cliente.tipoDocumento,
                    numeroDocumento: cliente.numeroDocumento,
                    nombres: cliente.nombres,
                    apellidos: cliente.apellidos || '',
                    correo: cliente.correo || '',
                    telefono: cliente.telefono || '',
                    clave: '' // Siempre vacío por seguridad
                });
            } catch (error) {
                alert("Error al cargar la información del cliente.");
                navigate('/ventas/clientes');
            } finally {
                setCargando(false);
            }
        };
        cargarDatos();
    }, [id, navigate]);

    const handleChange = (e) => setCliente({ ...cliente, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        setGuardando(true);
        try {
            await clienteService.actualizarCliente(id, cliente);
            alert("¡Cliente actualizado con éxito!");
            navigate('/ventas/clientes'); 
        } catch (error) {
            alert(error.response?.data?.message || "Ocurrió un error al actualizar.");
        } finally {
            setGuardando(false);
        }
    };

    if (cargando) return <div className="p-4">Cargando datos del cliente...</div>;

    return (
        <div className="container-fluid" style={{ maxWidth: '900px' }}>
            <h2 className="h3 mb-4 text-gray-800">Editar Cliente</h2>

            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">Modificar Datos</h6>
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="row mb-3">
                            <div className="col-md-4">
                                <label className="form-label fw-bold">Tipo Doc.</label>
                                <select name="tipoDocumento" value={cliente.tipoDocumento} onChange={handleChange} className="form-select" required>
                                    <option value="">Seleccione</option>
                                    <option value="DNI">DNI</option>
                                    <option value="RUC">RUC</option>
                                    <option value="CE">Carnet de Extranjería</option>
                                    <option value="Pasaporte">Pasaporte</option>
                                </select>
                            </div>
                            <div className="col-md-8">
                                <label className="form-label fw-bold">Número de Documento</label>
                                <input type="text" name="numeroDocumento" value={cliente.numeroDocumento} onChange={handleChange} className="form-control" maxLength="20" required />
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Nombres / Razón Social</label>
                                <input type="text" name="nombres" value={cliente.nombres} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Apellidos (Opcional)</label>
                                <input type="text" name="apellidos" value={cliente.apellidos} onChange={handleChange} className="form-control" />
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Teléfono</label>
                                <input type="text" name="telefono" value={cliente.telefono} onChange={handleChange} className="form-control" />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Correo Electrónico</label>
                                <input type="email" name="correo" value={cliente.correo} onChange={handleChange} className="form-control" />
                            </div>
                        </div>
                        
                        <div className="row mb-4">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Nueva Contraseña Web</label>
                                <input type="password" name="clave" value={cliente.clave} onChange={handleChange} className="form-control" minLength="6" />
                                <small className="text-danger fw-bold">Déjalo en blanco si NO deseas cambiar la contraseña de acceso web del cliente.</small>
                            </div>
                        </div>
                        
                        <hr/>
                        {/* BOTONERA UNIFICADA */}
                        <div className="row mt-4">
                            <div className="col-auto">
                                <button type="submit" disabled={guardando} style={{ cursor: guardando ? "wait" : "pointer" }} className="btn btn-primary btn-icon-split">
                                    <span className="icon text-white-50"><i className="fa-regular fa-floppy-disk"></i></span>
                                    <span className="text">{guardando ? "Guardando..." : "Actualizar Cliente"}</span>
                                </button>
                            </div>
                            <div className="col-auto">
                                <button type="button" disabled={guardando} onClick={() => navigate('/ventas/clientes')} className="btn btn-danger btn-icon-split">
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