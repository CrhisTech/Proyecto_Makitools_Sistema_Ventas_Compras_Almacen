import React, { useEffect, useState } from "react";
import { categoriaService } from "../../services/categoriaService";

export default function Categorias() {
  const [categorias, setCategorias] = useState([]);
  const [cargando, setCargando] = useState(true);

  const [descripcion, setDescripcion] = useState("");
  const [guardando, setGuardando] = useState(false);

  useEffect(() => {
    cargarCategorias();
  }, []);

  const cargarCategorias = async () => {
    try {
      const data = await categoriaService.listarCategorias();
      setCategorias(data);
    } catch (err) {
      console.error("Error al cargar las categorias: ", err);
    } finally {
      setCargando(false);
    }
  };

  const handleCrearCategoria = async (e) => {
    e.preventDefault();
    if (!descripcion.trim()) return;

    setGuardando(true);
    try {
      const nuevaCat = await categoriaService.crearCategoria({ descripcion });
      setCategorias([...categorias, nuevaCat]);
      setDescripcion("");
    } catch (err) {
      alert("Error al crear la categoria. Revisa la consola.");
      console.error(err);
    } finally {
      setGuardando(false);
    }
  };

  if (cargando) return <div className="p-4">Cargando categorías...</div>;

  return (
    <div className="container-fluid">
      <h2 className="h3 mb-4 text-gray-800">Gestión de Almacén - Categorías</h2>
      
      <div className="card shadow mb-4">
        <div className="card-header py-3">
          <h6 className="m-0 font-weight-bold text-primary">Nueva Categoría</h6>
        </div>
        <div className="card-body">
          <form onSubmit={handleCrearCategoria}>
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
                {guardando ? "Guardando..." : "Guardar Categoría"}
              </span>
            </button>
          </form>
        </div>
      </div>

      <div className="card shadow mb-4">
        <div className="card-header py-3">
          <h6 className="m-0 font-weight-bold text-primary">Lista de Categorías</h6>
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
                {categorias.length === 0 ? (
                  <tr>
                    <td colSpan="3" className="text-center">No hay categorías registradas.</td>
                  </tr>
                ) : (
                  categorias.map((cat) => (
                    <tr key={cat.idCategoria}>
                      <td className="fw-bold">{cat.idCategoria}</td>
                      <td>{cat.descripcion}</td>
                      <td>
                        <span className={`badge ${cat.activo ? 'bg-success' : 'bg-danger'} rounded-pill`}>
                          {cat.activo ? "Activo" : "Inactivo"}
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