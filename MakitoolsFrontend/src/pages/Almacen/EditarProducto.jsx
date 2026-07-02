import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { productoService } from '../../services/productoService';
import { categoriaService } from '../../services/categoriaService';
import { marcaService } from '../../services/marcaService';

const EditarProducto = () => {
    const navigate = useNavigate();
    const { id } = useParams(); 

    const [categorias, setCategorias] = useState([]);
    const [marcas, setMarcas] = useState([]);
    const [cargando, setCargando] = useState(true);
    
    const [producto, setproducto] = useState({
        sku: '',
        nombre: '',
        descripcion: '',
        idCategoria: '',
        idMarca: '',
        precioVenta: ''
    });

    const [archivoImagen, setArchivoImagen] = useState(null);
    const [imagenPreview, setImagenPreview] = useState(null);
    const [guardando, setGuardando] = useState(false);

    useEffect(() => {
        const cargarDatos = async () => {
            try {
                const [dataCategorias, dataMarcas] = await Promise.all([
                    categoriaService.listarCategorias(),
                    marcaService.listarMarcas()
                ]);
                setCategorias(dataCategorias);
                setMarcas(dataMarcas);

                const producto = await productoService.obtenerProductoPorId(id);
                
                setproducto({
                    sku: producto.sku,
                    nombre: producto.nombre,
                    descripcion: producto.descripcion,
                    idCategoria: producto.idCategoria,
                    idMarca: producto.marca, // Asegúrate de que el Backend envíe el ID o mapearlo correctamente
                    precioVenta: producto.precioVenta
                });

                if (producto.rutaImagen) {
                    setImagenPreview(`http://localhost:5259${producto.rutaImagen}`);
                }

            } catch (error) {
                console.error("Error al cargar datos:", error);
                alert("Error al cargar la información del producto.");
                navigate('/almacen/productos');
            } finally {
                setCargando(false);
            }
        };
        cargarDatos();
    }, [id, navigate]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setproducto({ ...producto, [name]: value });
    };

    const handleImagenChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            setArchivoImagen(file);
            setImagenPreview(URL.createObjectURL(file));
        } else {
            setArchivoImagen(null);
            setImagenPreview(null);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setGuardando(true);

        try {
            const formDataToSend = new FormData();
            formDataToSend.append('sku', producto.sku);
            formDataToSend.append('nombre', producto.nombre);
            formDataToSend.append('descripcion', producto.descripcion);
            formDataToSend.append('idCategoria', producto.idCategoria);
            formDataToSend.append('idMarca', producto.idMarca);
            formDataToSend.append('precioVenta', producto.precioVenta);

            if (archivoImagen) {
                formDataToSend.append('Imagen', archivoImagen); 
            }

            await productoService.actualizarProducto(id, formDataToSend);
            
            alert("¡Producto actualizado con éxito!");
            navigate('/almacen/productos'); 
            
        } catch (error) {
            console.error("Error al actualizar:", error);
            alert(error.response?.data?.message || "Ocurrió un error al actualizar el producto.");
        } finally {
            setGuardando(false);
        }
    };

    if (cargando) return <div className="container mt-4">Cargando información del producto...</div>;

    return (
        <div className="container">
            <div className="card shadow mb-4">
                <div className="card-header py-3">
                    <h6 className="m-0 font-weight-bold text-primary">
                        Editar Producto
                    </h6>
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label">SKU (Código único)</label>
                                <input type="text" name="sku" value={producto.sku} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label">Nombre del Producto</label>
                                <input type="text" name="nombre" value={producto.nombre} onChange={handleChange} className="form-control" required />
                            </div>
                        </div>

                        <div className="mb-3">
                            <label className="form-label">Descripción Técnica</label>
                            <textarea name="descripcion" value={producto.descripcion} onChange={handleChange} className="form-control" style={{ height: "80px" }} required />
                        </div>

                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label">Categoría</label>
                                <select name="idCategoria" value={producto.idCategoria} onChange={handleChange} className="form-select" required>
                                    <option value="">-- Seleccione --</option>
                                    {categorias.map(cat => (
                                        <option key={cat.idCategoria} value={cat.idCategoria}>{cat.descripcion}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="col-md-6">
                                <label className="form-label">Marca</label>
                                <select name="idMarca" value={producto.idMarca} onChange={handleChange} className="form-select" required>
                                    <option value="">-- Seleccione --</option>
                                    {marcas.map(marca => (
                                        <option key={marca.idMarca} value={marca.idMarca}>{marca.descripcion}</option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="row mb-4">
                            <div className="col-md-6">
                                <label className="form-label">Precio de Venta (S/.)</label>
                                <input type="number" step="0.01" min="0.01" name="precioVenta" value={producto.precioVenta} onChange={handleChange} className="form-control" required />
                            </div>
                            <div className="col-md-6 text-center">
                                <label className="form-label d-block text-start">Imagen del Producto</label>
                                {imagenPreview ? (
                                    <img src={imagenPreview} alt="Vista previa" className="img-fluid border rounded mb-2" style={{ height: '150px', objectFit: 'cover' }} />
                                ) : (
                                    <div className="border rounded bg-light mb-2 d-flex align-items-center justify-content-center" style={{ height: '150px', color: '#6c757d' }}>Sin imagen</div>
                                )}
                                <input className="form-control" type="file" accept="image/png, image/jpg, image/jpeg" onChange={handleImagenChange} />
                                <small className="text-muted text-start d-block mt-1">Sube una nueva foto solo si deseas reemplazar la actual.</small>
                            </div>
                        </div>

                        {/* NUEVA BOTONERA UNIFICADA */}
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
                                onClick={() => navigate("/almacen/productos")}
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
};

export default EditarProducto;