import React, { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { compraService } from "../../../services/compraService";
import { AuthContext } from "../../../context/AuthContext";

export default function HistorialCompras() {
  const [compras, setCompras] = useState([]);
  const [cargando, setCargando] = useState(true);
  const navigate = useNavigate();
  
  const { user } = useContext(AuthContext);
  const idRolUsuario = user?.idRol; 

  const estadosPermitidos = [
    "Pendiente / Emitida", 
    "Aceptada / Confirmada", 
    "Rechazada", 
    "Despachada / En tránsito", 
    "Recibida / Entregada", 
    "Facturada", 
    "Cerrada / Pagada"
  ];

  useEffect(() => {
    cargarHistorial();
  }, []);

  const cargarHistorial = async () => {
    try {
      const data = await compraService.listarCompras();
      setCompras(data);
    } catch (err) {
      console.error(err);
      alert("Error al cargar el historial de compras.");
    } finally {
      setCargando(false);
    }
  };

  const handleCambiarEstado = async (idCompra, nuevoEstado) => {
    const confirmar = window.confirm(`¿Seguro que deseas mover la orden a "${nuevoEstado}"?`);
    if (!confirmar) return;

    try {
      await compraService.cambiarEstado(idCompra, nuevoEstado);
      setCompras(compras.map(c => c.idCompra === idCompra ? { ...c, estado: nuevoEstado } : c));
    } catch (error) {
      alert(error.response?.data?.message || "Error al actualizar el estado.");
    }
  };

  {/* colores de estado*/}
  const obtenerColorBadge = (estado) => {
    switch (estado) {
      case "Pendiente / Emitida": return "bg-warning text-dark";
      case "Aceptada / Confirmada": return "bg-primary";
      case "Rechazada": return "bg-danger";
      case "Despachada / En tránsito": return "bg-info text-dark";
      case "Recibida / Entregada": return "bg-success";
      case "Facturada": return "bg-secondary";
      case "Cerrada / Pagada": return "bg-dark";
      default: return "bg-secondary";
    }
  };

  if (cargando) return <div className="p-4">Cargando el panel de compras...</div>;

  return (
    <div className="container-fluid">
      <h2 className="h3 mb-4 text-gray-800">Compras - Historial de Órdenes</h2>
      
      <div className="card shadow mb-4 border-bottom-primary">
        <div className="card-header py-3 d-flex flex-row align-items-center justify-content-between">
          <h6 className="m-0 font-weight-bold text-primary">Seguimiento de Pedidos</h6>
          
          {/* SOLO ADMIN(1) O COMPRAS(4) PUEDEN GENERAR NUEVAS ÓRDENES */}
          {idRolUsuario === 1 && (
             <button onClick={() => navigate("/compras/nuevo-pedido")} className="btn btn-success btn-sm shadow-sm">
               <i className="fa-solid fa-plus fa-sm text-white-50 me-2"></i> Generar Orden de Compra
             </button>
          )}
        </div>
        <div className="card-body p-0">
          <div className="table-responsive">
            <table className="table table-hover align-middle mb-0" width="100%" cellSpacing="0">
              <thead className="table-light">
                <tr>
                  <th className="px-4">N° Orden</th>
                  <th>Proveedor</th>
                  <th>Emisión</th>
                  <th>Llegada Estimada</th>
                  <th className="text-end">Monto Total</th>
                  <th className="text-center">Estado Actual</th>
                  <th className="text-center px-4"><i className="fa-solid fa-gears text-primary"></i></th>
                </tr>
              </thead>
              <tbody>
                {compras.length === 0 ? (
                  <tr>
                    <td colSpan="7" className="text-center py-5 text-muted">
                      <i className="fa-solid fa-folder-open fa-2x mb-3 d-block"></i>
                      No hay órdenes de compra registradas en el sistema.
                    </td>
                  </tr>
                ) : (
                  compras.map((c) => (
                    <tr key={c.idCompra}>
                      <td className="fw-bold text-primary px-4">{c.numeroOrdenCompra}</td>
                      <td className="fw-bold text-dark">{c.proveedor}</td>
                      <td>{new Date(c.fechaCompra).toLocaleDateString()}</td>
                      <td className={new Date(c.fechaEntregaEsperada) < new Date() && c.estado !== "Recibida / Entregada" && c.estado !== "Cerrada / Pagada" ? "text-danger fw-bold" : ""}>
                        {new Date(c.fechaEntregaEsperada).toLocaleDateString()}
                      </td>
                      <td className="text-end fw-bold">
                         {c.moneda === "PEN" ? "S/ " : "$ "} {c.montoTotal.toFixed(2)}
                      </td>
                      <td className="text-center">
                        <span className={`badge ${obtenerColorBadge(c.estado)} px-3 py-2 rounded-pill shadow-sm`}>
                          {c.estado}
                        </span>
                      </td>
                      <td className="text-center px-4">
                        <div className="dropdown no-arrow">
                          <a className="dropdown-toggle text-dark" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <i className="fa-solid fa-ellipsis-vertical px-2 fs-5"></i>
                          </a>
                          <ul className="dropdown-menu dropdown-menu-end shadow">
                            
                            <li>
                              <button className="dropdown-item" onClick={() => alert("Vista de Detalle en desarrollo...")}>
                                <i className="fa-solid fa-eye text-primary me-2"></i> Ver Documento
                              </button>
                            </li>
                            {idRolUsuario === 1  && (
                              <>
                                <li><hr className="dropdown-divider" /></li>
                                <li><h6 className="dropdown-header">Mover estado a:</h6></li>
                                {estadosPermitidos.map(estadoItem => (
                                  estadoItem !== c.estado && (
                                    <li key={estadoItem}>
                                      <button className="dropdown-item" onClick={() => handleCambiarEstado(c.idCompra, estadoItem)}>
                                        <i className="fa-solid fa-arrow-right-long text-muted me-2"></i> {estadoItem}
                                      </button>
                                    </li>
                                  )
                                ))}
                              </>
                            )}
                          </ul>
                        </div>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}