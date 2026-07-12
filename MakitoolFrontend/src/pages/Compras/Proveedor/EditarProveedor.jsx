import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { proveedorService } from '../../../services/proveedorService';

export default function EditarProveedor() {
    const navigate = useNavigate();
    const { id } = useParams();
    const [cargando, setCargando] = useState(true);
    const [guardando, setGuardando] = useState(false);
    
    const [proveedor, setProveedor] = useState({
        ruc: '', razonSocial: '', contacto: '', telefono: '', correo: ''
    });

    useEffect(() => {
        const cargarDatos = async () => {
            try {
                const proveedor = await proveedorService.obtenerProveedorPorId(id);
                setProveedor({
                    ruc: proveedor.ruc,
                    razonSocial: proveedor.razonSocial,
                    contacto: proveedor.contacto || '',
                    telefono: proveedor.telefono || '',
                    correo: proveedor.correo || ''
                });
            } catch (error) {
                alert("Error al cargar la información del proveedor.");
                navigate('/compras/proveedores');
            } finally {
                setCargando(false);
            }
        };
        cargarDatos();
    }, [id, navigate]);

    const handleChange = (e) => setProveedor({ ...proveedor, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        setGuardando(true);
        try {
            await proveedorService.actualizarProveedor(id, proveedor);
            alert("¡Proveedor actualizado con éxito!");
            navigate('/compras/proveedores'); 
        } catch (error) {
            alert(error.response?.data?.message || "Ocurrió un error al actualizar.");
        } finally {
            setGuardando(false);
        }
    };

    if (cargando) return <div className="p-4">Cargando datos...</div>;

    return (
        <div className="container">
            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">
                        Editar Proveedor
                    </h6>
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="row mb-3">
                            <div className="col-md-4">
                                <label className="form-label">RUC</label>
                                <input type="text" name="ruc" value={proveedor.ruc} onChange={handleChange} className="form-control" maxLength="11" pattern="\d{11}" required />
                            </div>
                            <div className="col-md-8">
                                <label className="form-label">Razón Social</label>
                                <input type="text" name="razonSocial" value={proveedor.razonSocial} onChange={handleChange} className="form-control" required />
                            </div>
                        </div>
                        <div className="row mb-4">
                            <div className="col-md-4">
                                <label className="form-label">Teléfono</label>
                                <input type="text" name="telefono" value={proveedor.telefono} onChange={handleChange} className="form-control" />
                            </div>
                            <div className="col-md-4">
                                <label className="form-label">Contacto</label>
                                <input type="text" name="contacto" value={proveedor.contacto} onChange={handleChange} className="form-control" />
                            </div>
                            <div className="col-md-4">
                                <label className="form-label">Correo</label>
                                <input type="email" name="correo" value={proveedor.correo} onChange={handleChange} className="form-control" />
                            </div>
                        </div>
                        
                        <div className="row mt-4">
                            <div className="col-auto">
                                <button
                                type="submit"
                                disabled={guardando}
                                style={{ cursor: guardando ? "wait" : "pointer" }}
                                className="btn btn-primary btn-icon-split"
                                >
                                <span className="icon text-white-50">
                                    <i className="fa-regular fa-floppy-disk"></i>
                                </span>
                                <span className="text">
                                    {guardando ? "Guardando..." : "Actualizar"}
                                </span>
                                </button>
                            </div>
                            <div className="col-auto">
                                <button
                                type="button"
                                disabled={guardando}
                                onClick={() => navigate('/compras/proveedores')}
                                className="btn btn-danger btn-icon-split"
                                >
                                <span className="icon text-white-50">
                                    <i className="fa-solid fa-rotate-left"></i>
                                </span>
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