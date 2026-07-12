import React from 'react'
import { categoriaService } from '../../../services/categoriaService';

export default function EditarCategoria() {
  const navigate = useNavigate();
  const { id } = useParams();
  const [cargando, setCargando] = useState(true);
  const [guardando, setGuardando] = useState(false);

  const [categoria, setCategoria] = useState({
    descripcion: "",
  });

  useEffect(() => {
    const cargarDatos = async () => {
      try {
        const categoria = await (id);
        setCategoria({
          descripcion: categoria.descripcion,
        });
      } catch (error) {
        alert("Error al cargar la informacion de la categoria.");
        navigate("/almacen/categorias");
      } finally {
        setCargando(false);
      }
    };
    cargarDatos();
  }, [id, navigate]);

  const handleChange = (e) =>
    setMarca({ ...categoria, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    setGuardando(true);
    try {
      await categoriaService.actualizarCategoria(id, categoria);
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
            Editar Categoria
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
                  value={categoria.descripcion}
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
  )
}
