import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { proveedorService } from "../../services/proveedorService";

export default function Proveedores() {
  const [proveedores, setProveedores] = useState([]);
  const [cargando, setCargando] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    cargarProveedores();
  }, []);

  const cargarProveedores = async () => {
    try {
      const data = await proveedorService.listarProveedores();
      setProveedores(data);
    } catch (err) {
      console.error(err);
      alert("Error al cargar los proveedores.");
    } finally {
      setCargando(false);
    }
  };

  const handleEliminar = async (id, razonSocial) => {
    const confirmar = window.confirm(`¿Estás seguro de que deseas eliminar al proveedor "${razonSocial}"?`);
    if (confirmar) {
      try {
        await proveedorService.eliminarProveedor(id);
        setProveedores(proveedores.filter((prov) => prov.idProveedor !== id));
        alert("Proveedor eliminado correctamente.");
      } catch (err) {
        alert("Error al eliminar el proveedor.");
        console.error(err);
      }
    }
  };

  if (cargando) return <div className="p-4">Cargando directorio de proveedores...</div>;

  return (
    <div className="container-fluid">
      <h2 className="h3 mb-4 text-gray-800">Directorio de Proveedores</h2>
      
      <div className="card shadow mb-4">
        <div className="card-header py-3 d-flex flex-row align-items-center justify-content-between">
          <h6 className="m-0 font-weight-bold text-primary">Lista de Proveedores</h6>
          <button onClick={() => navigate("/compras/proveedores/nuevo")} className="btn btn-success btn-sm shadow-sm">
            <i className="fa-solid fa-plus fa-sm text-white-50 me-2"></i> Nuevo Proveedor
          </button>
        </div>
        <div className="card-body">
          <div className="table-responsive">
            <table className="table table-hover" width="100%" cellSpacing="0">
              <thead className="table-light">
                <tr>
                  <th>RUC</th>
                  <th>Razón Social</th>
                  <th>Contacto</th>
                  <th>Teléfono</th>
                  <th>Correo</th>
                  <th>Estado</th>
                  <th><i className="fa-solid fa-gears text-primary"></i></th>
                </tr>
              </thead>
              <tbody>
                {proveedores.length === 0 ? (
                  <tr><td colSpan="7" className="text-center">No hay proveedores registrados.</td></tr>
                ) : (
                  proveedores.map((prov) => (
                    <tr key={prov.idProveedor}>
                      <td className="fw-bold">{prov.ruc}</td>
                      <td>{prov.razonSocial}</td>
                      <td>{prov.contacto || '-'}</td>
                      <td>{prov.telefono || '-'}</td>
                      <td>{prov.correo || '-'}</td>
                      <td>
                        <span className={`badge ${prov.activo ? 'bg-success' : 'bg-danger'} rounded-pill`}>
                          {prov.activo ? "Activo" : "Inactivo"}
                        </span>
                      </td>
                      <td>
                        <div className="dropdown no-arrow">
                          <a className="dropdown-toggle text-dark" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <i className="fa-solid fa-ellipsis-vertical"></i>
                          </a>
                          <ul className="dropdown-menu shadow">
                            <li>
                              <Link className="dropdown-item" to={`/compras/proveedores/editar/${prov.idProveedor}`}>
                                <i className="fa-regular fa-pen-to-square text-primary me-2"></i> Editar
                              </Link>
                            </li>
                            <li>
                              <button className="dropdown-item" onClick={() => handleEliminar(prov.idProveedor, prov.razonSocial)}>
                                <i className="fa-regular fa-trash-can text-danger me-2"></i> Eliminar
                              </button>
                            </li>
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