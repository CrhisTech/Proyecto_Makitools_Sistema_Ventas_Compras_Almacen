import React, { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { compraService } from '../../../services/compraService';
import { AuthContext } from '../../../context/AuthContext';
import { proveedorService } from '../../../services/proveedorService';
import { productoService } from '../../../services/productoService';

export default function NuevaCompra() {
    const navigate = useNavigate();
    const { user } = useContext(AuthContext); 
    const [guardando, setGuardando] = useState(false);
    const [cargandoCatalogos, setCargandoCatalogos]= useState(true);

    const [compra, setCompra] = useState({
        idProveedor: '',
        tipoDocumento: 'Orden de Compra',
        moneda: 'PEN',
        condicionesPago: 'Contado',
        fechaEntregaEsperada: '',
        lugarEntrega: 'Almacén Principal - Makitools',
        observaciones: ''
    });

    const [productoSeleccionado, setProductoSeleccionado] = useState({
        idProducto: '',
        nombre: '',
        cantidad: 1,
        costoUnitario: ''
    });

    const [detallesCompra, setDetallesCompra] = useState([]);

    const [proveedores, setProveedores] = useState([]);
    const [productosCatalago, setProductosCatalogo] = useState([]);

    useEffect(()=>{
        const cargarCatalogos = async () => {
            try{
                const [listaProveedores, listaProductos] = await Promise.all([
                    proveedorService.listarProveedores(),
                    productoService.listarProductos()
                ]);

                setProveedores(listaProveedores);

                setProductosCatalogo(listaProductos)
            }catch (error){
                console.error("Error al cargar catalogos:", error);
                alert("Error al conectar con el servidor para cargar proveedores y productos");
            }finally{
                setCargandoCatalogos(false);
            }
        };
        cargarCatalogos();
    },[]);

    const handleCompraChange = (e) => {
        setCompra({ ...compra, [e.target.name]: e.target.value });
    };

    const handleProductoChange = (e) => {
        if (e.target.name === 'idProducto') {
            const prod = productosCatalago.find(p => p.idProducto === parseInt(e.target.value));
            setProductoSeleccionado({ 
                ...productoSeleccionado, 
                idProducto: e.target.value, 
                nombre: prod ? prod.nombre : '' 
            });
        } else {
            setProductoSeleccionado({ ...productoSeleccionado, [e.target.name]: e.target.value });
        }
    };

    const agregarAlCarrito = () => {
        if (!productoSeleccionado.idProducto || productoSeleccionado.cantidad <= 0 || productoSeleccionado.costoUnitario <= 0) {
            alert("Por favor, selecciona un producto e ingresa una cantidad y costo válidos.");
            return;
        }

        const existe = detallesCompra.find(d => d.idProducto === parseInt(productoSeleccionado.idProducto));
        if (existe) {
            alert("Este producto ya está en el detalle. Elimínalo y vuelve a agregarlo si deseas cambiar la cantidad.");
            return;
        }

        const nuevoDetalle = {
            idProducto: parseInt(productoSeleccionado.idProducto),
            nombre: productoSeleccionado.nombre,
            cantidad: parseInt(productoSeleccionado.cantidad),
            costoUnitario: parseFloat(productoSeleccionado.costoUnitario),
            subtotal: parseInt(productoSeleccionado.cantidad) * parseFloat(productoSeleccionado.costoUnitario)
        };

        setDetallesCompra([...detallesCompra, nuevoDetalle]);
        
        setProductoSeleccionado({ idProducto: '', nombre: '', cantidad: 1, costoUnitario: '' });
    };

    const eliminarDelCarrito = (idProducto) => {
        setDetallesCompra(detallesCompra.filter(d => d.idProducto !== idProducto));
    };

    const montoTotalGeneral = detallesCompra.reduce((acumulador, item) => acumulador + item.subtotal, 0);

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (detallesCompra.length === 0) {
            alert("No puedes generar una Orden de Compra sin productos.");
            return;
        }

        if (!compra.idProveedor || !compra.fechaEntregaEsperada) {
            alert("Por favor, completa los datos del proveedor y la fecha de entrega.");
            return;
        }

        setGuardando(true);

        const payload = {
            idProveedor: parseInt(compra.idProveedor),
            idUsuario: user?.idUsuario || 1, 
            tipoDocumento: compra.tipoDocumento,
            moneda: compra.moneda,
            condicionesPago: compra.condicionesPago,
            fechaEntregaEsperada: compra.fechaEntregaEsperada,
            lugarEntrega: compra.lugarEntrega,
            observaciones: compra.observaciones,
            detalles: detallesCompra.map(d => ({
                idProducto: d.idProducto,
                cantidad: d.cantidad,
                costoUnitario: d.costoUnitario
            }))
        };

        try {
            await compraService.generarPedido(payload);
            alert("¡Orden de Compra generada y guardada con éxito!");
            navigate('/compras/historial'); 
        } catch (error) {
            alert(error.response?.data?.message || "Ocurrió un error al generar el pedido.");
        } finally {
            setGuardando(false);
        }
    };

    if(cargandoCatalogos){
        return <div className="p-4 text-center fw-bold text-primary">Cargando catálogos de Proveedores y Productos...</div>;
    }

    return (
        <div className="container-fluid">
            <h2 className="h3 mb-4 text-gray-800">Generar Orden de Compra</h2>

            <form onSubmit={handleSubmit}>
                <div className="card shadow mb-4 border-left-primary">
                    <div className="card-header py-3">
                        <h6 className="m-0 font-weight-bold text-primary">Datos del Proveedor y Condiciones Comerciales</h6>
                    </div>
                    <div className="card-body">
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label fw-bold">Proveedor <span className="text-danger">*</span></label>
                                <select name="idProveedor" value={compra.idProveedor} onChange={handleCompraChange} className="form-select" required>
                                    <option value="">Seleccione un proveedor...</option>
                                    {proveedores.map(p => (
                                        <option key={p.idProveedor} value={p.idProveedor}>{p.razonSocial}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="col-md-3">
                                <label className="form-label fw-bold">Moneda</label>
                                <select name="moneda" value={compra.moneda} onChange={handleCompraChange} className="form-select">
                                    <option value="PEN">Soles (PEN)</option>
                                    <option value="USD">Dólares (USD)</option>
                                </select>
                            </div>
                            <div className="col-md-3">
                                <label className="form-label fw-bold">Condición de Pago</label>
                                <select name="condicionesPago" value={compra.condicionesPago} onChange={handleCompraChange} className="form-select">
                                    <option value="Contado">Al Contado</option>
                                    <option value="Crédito 15 días">Crédito 15 días</option>
                                    <option value="Crédito 30 días">Crédito 30 días</option>
                                </select>
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col-md-4">
                                <label className="form-label fw-bold">Fecha de Entrega Esperada <span className="text-danger">*</span></label>
                                <input type="date" name="fechaEntregaEsperada" value={compra.fechaEntregaEsperada} onChange={handleCompraChange} className="form-control" required />
                            </div>
                            <div className="col-md-8">
                                <label className="form-label fw-bold">Lugar de Entrega</label>
                                <input type="text" name="lugarEntrega" value={compra.lugarEntrega} onChange={handleCompraChange} className="form-control" placeholder="Ej: Almacén Lurigancho" required />
                            </div>
                        </div>
                    </div>
                </div>

                <div className="card shadow mb-4 border-left-info">
                    <div className="card-body bg-light">
                        <div className="row align-items-end">
                            <div className="col-md-5">
                                <label className="form-label fw-bold text-info">Producto a solicitar</label>
                                <select name="idProducto" value={productoSeleccionado.idProducto} onChange={handleProductoChange} className="form-select">
                                    <option value="">Buscar en catálogo...</option>
                                    {productosCatalago.map(p => (
                                        <option key={p.idProducto} value={p.idProducto}>{p.nombre}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="col-md-2">
                                <label className="form-label fw-bold text-info">Cantidad</label>
                                <input type="number" name="cantidad" value={productoSeleccionado.cantidad} onChange={handleProductoChange} className="form-control" min="1" />
                            </div>
                            <div className="col-md-3">
                                <label className="form-label fw-bold text-info">Costo Unit. ({compra.moneda === 'PEN' ? 'S/' : '$'})</label>
                                <input type="number" name="costoUnitario" value={productoSeleccionado.costoUnitario} onChange={handleProductoChange} className="form-control" step="0.01" min="0.01" />
                            </div>
                            <div className="col-md-2 d-grid">
                                <button type="button" onClick={agregarAlCarrito} className="btn btn-info text-white shadow-sm">
                                    <i className="fa-solid fa-plus me-2"></i>Agregar
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="card shadow mb-4">
                    <div className="card-header py-3">
                        <h6 className="m-0 font-weight-bold text-primary">Detalle de Mercancía</h6>
                    </div>
                    <div className="card-body p-0">
                        <div className="table-responsive">
                            <table className="table table-hover mb-0">
                                <thead className="table-dark">
                                    <tr>
                                        <th className="px-4">Producto</th>
                                        <th className="text-center">Cantidad</th>
                                        <th className="text-end">Costo Unit.</th>
                                        <th className="text-end">Subtotal</th>
                                        <th className="text-center px-4">Acción</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {detallesCompra.length === 0 ? (
                                        <tr>
                                            <td colSpan="5" className="text-center py-4 text-muted">
                                                <i className="fa-solid fa-box-open fa-2x mb-2 d-block"></i>
                                                Aún no has agregado productos a la orden.
                                            </td>
                                        </tr>
                                    ) : (
                                        detallesCompra.map((item, index) => (
                                            <tr key={index} className="align-middle">
                                                <td className="px-4">{item.nombre}</td>
                                                <td className="text-center fw-bold">{item.cantidad}</td>
                                                <td className="text-end">{item.costoUnitario.toFixed(2)}</td>
                                                <td className="text-end fw-bold text-primary">
                                                    {item.subtotal.toFixed(2)}
                                                </td>
                                                <td className="text-center px-4">
                                                    <button type="button" onClick={() => eliminarDelCarrito(item.idProducto)} className="btn btn-outline-danger btn-sm rounded-circle">
                                                        <i className="fa-solid fa-trash-can"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    )}
                                </tbody>
                                {detallesCompra.length > 0 && (
                                    <tfoot className="bg-light">
                                        <tr>
                                            <td colSpan="3" className="text-end fw-bold h5 mb-0 py-3">MONTO TOTAL:</td>
                                            <td className="text-end fw-bold h5 mb-0 py-3 text-success">
                                                {compra.moneda === 'PEN' ? 'S/ ' : '$ '} 
                                                {montoTotalGeneral.toFixed(2)}
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tfoot>
                                )}
                            </table>
                        </div>
                    </div>
                </div>

                <div className="row mb-4">
                    <div className="col-12">
                        <label className="form-label fw-bold">Observaciones / Notas adicionales</label>
                        <textarea name="observaciones" value={compra.observaciones} onChange={handleCompraChange} className="form-control" rows="2" placeholder="Ej: Entregar mercadería en horario de mañana..."></textarea>
                    </div>
                </div>

                <hr/>
                <div className="row mt-4 mb-5 pb-5">
                    <div className="col-auto">
                        <button type="submit" disabled={guardando || detallesCompra.length === 0} className="btn btn-primary btn-icon-split shadow-sm">
                            <span className="icon text-white-50"><i className="fa-solid fa-file-signature"></i></span>
                            <span className="text">{guardando ? "Generando y Procesando..." : "Emitir Orden de Compra"}</span>
                        </button>
                    </div>
                    <div className="col-auto">
                        <button type="button" disabled={guardando} onClick={() => navigate('/compras/historial')} className="btn btn-danger btn-icon-split shadow-sm">
                            <span className="icon text-white-50"><i className="fa-solid fa-xmark"></i></span>
                            <span className="text">Cancelar</span>
                        </button>
                    </div>
                </div>

            </form>
        </div>
    );
}