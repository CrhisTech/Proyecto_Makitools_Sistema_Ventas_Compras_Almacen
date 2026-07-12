import React, { useState, useEffect, use } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { marcaService } from "../../../services/marcaService";

export default function EditarMarca() {
  const navigate = useNavigate();
  const { id } = useParams();
  const [cargando, setCargando] = useState(true);
  const [guardando, setGuardando] = useState(false);

  const [marca, setMarca] = useState({
    descripcion: "",
  });

  useEffect(() => {
    const cargarDatos = async () => {
      try {
        const marca = await marcaService.obtenerMarcaPorId(id);
        setMarca({
          descripcion: marca.descripcion,
        });
      } catch (error) {
        alert("Error al cargar la informacion de la marca.");
        navigate("/almacen/marcas");
      } finally {
        setCargando(false);
      }
    };
    cargarDatos();
  }, [id, navigate]);

  const handleChange = (e) =>
    setMarca({ ...marca, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    setGuardando(true);
    try {
      await marcaService.actualizarMarca(id, marca);
      alert("¡Marca actualizada con éxito!");
      navigate("/almacen/marcas");
    } catch (error) {
      alert(error.response?.data?.message || "Ocurrio un error al actualizar.");
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
            Editar Marca
          </h6>
        </div>
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="row mb-3">
              <div className="col-md-4">
                <label className="form-label">Descripcion</label>
                <input
                  type="text"
                  name="descripcion"
                  value={marca.descripcion}
                  onChange={handleChange}
                  className="form-control"
                  required
                />
              </div>
              <div/>
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
                  onClick={() => navigate("/almacen/marcas")}
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
