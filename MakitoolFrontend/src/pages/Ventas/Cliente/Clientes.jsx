import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { clienteService } from "../../../services/clienteService";

export default function Clientes() {
  const [cliente, setCliente] = useState([]);
  const [cargando, setCargando] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    cargarClientes();
  }, []);

  const cargarClientes = async () => {
    try {
      const data = await clienteService.listarClientes();
      setCliente(data);
    } catch (err) {
      console.error(err);
      alert("Error al cargar el directorio de clientes.");
    } finally {
      setCargando(false);
    }
  };

  const handleEliminar = async (id, documento, nombres) => {
    const confirmar = window.confirm(
      `¿Estás seguro de que deseas eliminar (desactivar) al cliente ${nombres} (${documento})?`,
    );
    if (confirmar) {
      try {
        await clienteService.eliminarCliente(id);
        setCliente(clientes.filter((c) => c.idCliente !== id));
        alert("Cliente eliminado correctamente.");
      } catch (err) {
        alert("Error al eliminar el cliente.");
        console.error(err);
      }
    }
  };

  if (cargando)
    return <div className="p-4">Cargando cartera de clientes...</div>;

  return (
    <div className="container-fluid">
      <h2 className="h3 mb-4 text-gray-800">Ventas - Gestión de Clientes</h2>

      <div className="card shadow mb-4">
        <div className="card-header py-3 d-flex flex-row align-items-center justify-content-between">
          <h6 className="m-0 font-weight-bold text-primary">
            Cartera de Clientes
          </h6>
          <button
            onClick={() => navigate("/ventas/clientes/nuevo")}
            className="btn btn-success btn-sm shadow-sm"
          >
            <i className="fa-solid fa-plus fa-sm text-white-50 me-2"></i> Nuevo
            Cliente
          </button>
        </div>
        <div className="card-body">
          <div className="table-responsive">
            <table className="table table-hover" width="100%" cellSpacing="0">
              <thead className="table-light">
                <tr>
                  <th>Documento</th>
                  <th>Nombres / Razón Social</th>
                  <th>Apellidos</th>
                  <th>Correo</th>
                  <th>Teléfono</th>
                  <th>Estado</th>
                  <th>
                    <i className="fa-solid fa-gears text-primary"></i>
                  </th>
                </tr>
              </thead>
              <tbody>
                {cliente.length === 0 ? (
                  <tr>
                    <td colSpan="7" className="text-center">
                      No hay clientes registrados.
                    </td>
                  </tr>
                ) : (
                  cliente.map((c) => (
                    <tr key={c.idCliente}>
                      <td className="fw-bold">
                        {c.tipoDocumento}: {c.numeroDocumento}
                      </td>
                      <td>{c.nombres}</td>
                      <td>{c.apellidos || "-"}</td>
                      <td>{c.correo || "-"}</td>
                      <td>{c.telefono || "-"}</td>
                      <td>
                        <span
                          className={`badge ${c.activo ? "bg-success" : "bg-danger"} rounded-pill`}
                        >
                          {c.activo ? "Activo" : "Inactivo"}
                        </span>
                      </td>
                      <td>
                        <div className="dropdown no-arrow">
                          <a
                            className="dropdown-toggle text-dark"
                            href="#"
                            role="button"
                            data-bs-toggle="dropdown"
                            aria-expanded="false"
                          >
                            <i className="fa-solid fa-ellipsis-vertical"></i>
                          </a>
                          <ul className="dropdown-menu shadow">
                            <li>
                              <Link
                                className="dropdown-item"
                                to={`/ventas/clientes/editar/${c.idCliente}`}
                              >
                                <i className="fa-regular fa-pen-to-square text-primary me-2"></i>{" "}
                                Editar
                              </Link>
                            </li>
                            <li>
                              <button
                                className="dropdown-item"
                                onClick={() =>
                                  handleEliminar(
                                    c.idCliente,
                                    c.numeroDocumento,
                                    c.nombres,
                                  )
                                }
                              >
                                <i className="fa-regular fa-trash-can text-danger me-2"></i>{" "}
                                Eliminar
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
