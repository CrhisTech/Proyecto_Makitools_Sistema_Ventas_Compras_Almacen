import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { usuarioService } from "../../services/usuarioService";

export default function Usuarios() {
  const [usuarios, setUsuarios] = useState([]);
  const [cargando, setCargando] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    cargarUsuarios();
  }, []);

  const cargarUsuarios = async () => {
    try {
      const data = await usuarioService.listarUsuarios();
      setUsuarios(data);
    } catch (err) {
      console.error(err);
      alert("Error al cargar los usuarios del sistema.");
    } finally {
      setCargando(false);
    }
  };

  const handleEliminar = async (id, nombres, apellidos) => {
    const confirmar = window.confirm(`¿Estás seguro de que deseas eliminar (desactivar) al usuario "${nombres} ${apellidos}"?`);
    if (confirmar) {
      try {
        await usuarioService.eliminarUsuario(id);
        setUsuarios(usuarios.filter((u) => u.idUsuario !== id));
        alert("Usuario eliminado correctamente.");
      } catch (err) {
        alert("Error al eliminar el usuario.");
        console.error(err);
      }
    }
  };

  if (cargando) return <div className="p-4">Cargando directorio de usuarios...</div>;

  return (
    <div className="container-fluid">
      <h2 className="h3 mb-4 text-gray-800">Seguridad - Gestión de Usuarios</h2>
      
      <div className="card shadow mb-4">
        <div className="card-header py-3 d-flex flex-row align-items-center justify-content-between">
          <h6 className="m-0 font-weight-bold text-primary">Lista de Usuarios</h6>
          <button onClick={() => navigate("/usuarios/nuevo")} className="btn btn-success btn-sm shadow-sm">
            <i className="fa-solid fa-plus fa-sm text-white-50 me-2"></i> Nuevo Usuario
          </button>
        </div>
        <div className="card-body">
          <div className="table-responsive">
            <table className="table table-hover" width="100%" cellSpacing="0">
              <thead className="table-light">
                <tr>
                  <th>Nombres</th>
                  <th>Apellidos</th>
                  <th>Correo</th>
                  <th>Rol</th>
                  <th>Fecha Registro</th>
                  <th>Estado</th>
                  <th><i className="fa-solid fa-gears text-primary"></i></th>
                </tr>
              </thead>
              <tbody>
                {usuarios.length === 0 ? (
                  <tr><td colSpan="7" className="text-center">No hay usuarios registrados.</td></tr>
                ) : (
                  usuarios.map((u) => (
                    <tr key={u.idUsuario}>
                      <td className="fw-bold">{u.nombres}</td>
                      <td>{u.apellidos}</td>
                      <td>{u.correo}</td>
                      <td>
                        <span className="badge bg-secondary rounded-pill px-3 py-2">
                           {u.rol}
                        </span>
                      </td>
                      <td>{new Date(u.fechaRegistro).toLocaleDateString()}</td>
                      <td>
                        <span className={`badge ${u.activo ? 'bg-success' : 'bg-danger'} rounded-pill`}>
                          {u.activo ? "Activo" : "Inactivo"}
                        </span>
                      </td>
                      <td>
                        <div className="dropdown no-arrow">
                          <a className="dropdown-toggle text-dark" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <i className="fa-solid fa-ellipsis-vertical"></i>
                          </a>
                          <ul className="dropdown-menu shadow">
                            <li>
                              <Link className="dropdown-item" to={`/usuarios/editar/${u.idUsuario}`}>
                                <i className="fa-regular fa-pen-to-square text-primary me-2"></i> Editar
                              </Link>
                            </li>
                            <li>
                              <button className="dropdown-item" onClick={() => handleEliminar(u.idUsuario, u.nombres, u.apellidos)}>
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