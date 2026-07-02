import React, { useEffect, useState } from "react";
import { marcaService } from "../../services/marcaService";

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
          <h6 className="m-0 font-weight-bold text-primary">Nueva Marca</h6>
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
                  <th scope="col">ID</th>
                  <th scope="col">Descripción</th>
                  <th scope="col">Estado</th>
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
                      <td className="fw-bold">{marca.idMarca}</td>
                      <td>{marca.descripcion}</td>
                      <td>
                        <span className={`badge ${marca.activo ? 'bg-success' : 'bg-danger'} rounded-pill`}>
                          {marca.activo ? "Activo" : "Inactivo"}
                        </span>
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