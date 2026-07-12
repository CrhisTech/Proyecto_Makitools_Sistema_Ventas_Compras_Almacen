import React, { useEffect, useState } from "react";
import { marcaService } from "../../../services/marcaService";
import { Link } from "react-router-dom";

export default function Marcas() {
  const [marcas, setMarcas] = useState([]);
  const [cargando, setCargando] = useState(true);

  const [descripcion, setDescripcion] = useState("");
  const [guardando, setGuardando] = useState(false);

  useEffect(() => {
    cargarMarcas();
  }, []);

  const cargarMarcas = async () => {
    try {
      const data = await marcaService.listarMarcas();
      setMarcas(data);
    } catch (err) {
      console.error("Error al cargar las marcas: ", err);
    } finally {
      setCargando(false);
    }
  };

    const handleEliminar = async(id, descripcion) =>{
      const confirmar = window.confirm(`¿Estás seguro de que deseas eliminar la marca "${descripcion}"?`);
      if(confirmar){
        try{
          await marcaService.eliminarMarca(id);
          setMarcas(marcas.filter((marca)=> marca.idCategoria !== id));
          alert("Marca eliminada correctamente.");
        }catch(error){
          alert("Error al eliminar la categoria.");
          console.error(error);
        }
        
      }
    }

  const handleCrearMarca = async (e) => {
    e.preventDefault();
    if (!descripcion.trim()) return;

    setGuardando(true);
    try {
      const nuevaMarca = await marcaService.crearMarca({ descripcion });
      setMarcas([...marcas, nuevaMarca]);
      setDescripcion("");
    } catch (err) {
      alert("Error al crear la marca. Revisa la consola.");
      console.error(err);
    } finally {
      setGuardando(false);
    }
  };

  if (cargando) return <div className="p-4">Cargando marcas...</div>;

  return (
    <div className="container-fluid">
      <h2 className="h3 mb-4 text-gray-800">Gestión de Almacén - Marcas</h2>
      
      <div className="card shadow mb-4">
        <div className="card-header py-3">
          <h6 className="m-0 font-weight-bold text-success">+ Nueva Marca</h6>
        </div>
        <div className="card-body">
          <form onSubmit={handleCrearMarca}>
            <div className="mb-3">
              <label htmlFor="descripcion" className="form-label fw-bold">
                Descripción
              </label>
              <input
                type="text"
                className="form-control"
                id="descripcion"
                value={descripcion}
                onChange={(e) => setDescripcion(e.target.value)}
                required
              />
            </div>
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
                {guardando ? "Guardando..." : "Guardar Marca"}
              </span>
            </button>
          </form>
        </div>
      </div>

      <div className="card shadow mb-4">
        <div className="card-header py-3">
          <h6 className="m-0 font-weight-bold text-primary">Lista de Marcas</h6>
        </div>
        <div className="card-body">
          <div className="table-responsive">
            <table className="table table-hover" width="100%" cellSpacing="0">
              <thead className="table-light">
                <tr>
                  <th scope="col">Descripción</th>
                  <th scope="col">Estado</th>
                  <th scope="col">
                    <i className="fa-solid fa-gears text-primary"></i>
                  </th>
                </tr>
              </thead>
              <tbody>
                {marcas.length === 0 ? (
                  <tr>
                    <td colSpan="3" className="text-center">No hay marcas registradas.</td>
                  </tr>
                ) : (
                  marcas.map((marca) => (
                    <tr key={marca.idMarca}>
                      <td>{marca.descripcion}</td>
                      <td>
                        <span className={`badge ${marca.activo ? 'bg-success' : 'bg-danger'} rounded-pill`}>
                          {marca.activo ? "Activo" : "Inactivo"}
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
                                to={`/almacen/marcas/editar/${marca.idMarca}`}
                              >
                                <i className="fa-regular fa-pen-to-square text-primary me-2"></i>{" "}
                                Editar
                              </Link>
                            </li>
                            <li>
                              <button
                                className="dropdown-item"
                                onClick={() =>
                                  handleEliminar(marca.idMarca, marca.descripcion)
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