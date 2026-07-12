import React, { useState, useEffect } from "react";
import { productoService } from "../../../services/productoService";
import { categoriaService } from "../../../services/categoriaService";
import { marcaService } from "../../../services/marcaService";
import { useNavigate } from "react-router-dom";

export default function NuevoProducto() {
  const navigate = useNavigate();

  const [categorias, setCategorias] = useState([]);
  const [marcas, setMarcas] = useState([]);

  const [producto, setProducto] = useState({
    sku: "",
    nombre: "",
    descripcion: "",
    idCategoria: "",
    idMarca: "",
    precioVenta: "",
  });

  const [archivoImagen, setArchivoImagen] = useState(null);
  const [imagenPreview, setImagenPreview] = useState(null);

  const [guardando, setGuardando] = useState(false);

  useEffect(() => {
    const cargarListas = async () => {
      try {
        const [dataCategorias, dataMarcas] = await Promise.all([
          categoriaService.listarCategorias(),
          marcaService.listarMarcas(),
        ]);
        setCategorias(dataCategorias);
        setMarcas(dataMarcas);
      } catch (err) {
        console.error("Error al cargar las listas: ", err);
        alert(
          "Error al conectar con el servidor para cargar marcas y categorias.",
        );
      }
    };
    cargarListas();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProducto({ ...producto, [name]: value });
  };

  const handleImagenChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      setArchivoImagen(file);
      setImagenPreview(URL.createObjectURL(file));
    } else {
      setArchivoImagen(null);
      setImagenPreview(null);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setGuardando(true);

    try {
      const formDataToSend = new FormData();
      formDataToSend.append('sku', producto.sku);
      formDataToSend.append('nombre', producto.nombre);
      formDataToSend.append('descripcion', producto.descripcion);
      formDataToSend.append('idCategoria', producto.idCategoria);
      formDataToSend.append('idMarca', producto.idMarca);
      formDataToSend.append('precioVenta', producto.precioVenta);

      if (archivoImagen){
        formDataToSend.append("Imagen", archivoImagen);
      }
      await productoService.crearProducto(formDataToSend);

      alert("Producto registrado!");
      navigate("/almacen/productos");
    } catch (err) {
      console.error("Error al guardar el producto: ", err);
    } finally {
      setGuardando(false);
    }
  };

  return (
    <div className="container">
      <div className="card shadow mb-4">
        <div className="card-header py-3">
          <h6 className="m-0 font-weight-bold text-primary">
            Registrar Nuevo Producto
          </h6>
        </div>
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label">SKU (Código único)</label>
              <input
                type="text"
                name="sku"
                className="form-control"
                id="text"
                value={producto.sku}
                onChange={handleChange}
                placeholder="Ej. MAQ-SOL-001"
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Nombre del Producto</label>
              <input
                type="text"
                name="nombre"
                className="form-control"
                id="text"
                value={producto.nombre}
                onChange={handleChange}
                placeholder="Ej. Máquina de soldar inversora"
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Descripción Técnica</label>
              <textarea
                type="text"
                name="descripcion"
                className="form-control"
                id="text"
                value={producto.descripcion}
                onChange={handleChange}
                placeholder="Detalles, potencia, accesorios incluidos..."
                style={{ height: "80px" }}
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Categoría</label>
              <select
                className="form-select"
                name="idCategoria"
                value={producto.idCategoria}
                onChange={handleChange}
                required
              >
                <option value="">-- Seleccione una categoría --</option>
                {categorias.map((cat) => (
                  <option key={cat.idCategoria} value={cat.idCategoria}>
                    {cat.descripcion}
                  </option>
                ))}
              </select>
            </div>
            <div className="mb-3">
              <label className="form-label">Marca</label>
              <select
                className="form-select"
                name="idMarca"
                value={producto.idMarca}
                onChange={handleChange}
                required
              >
                <option value="">-- Seleccione una marca --</option>
                {marcas.map((marca) => (
                  <option key={marca.idMarca} value={marca.idMarca}>
                    {marca.descripcion}
                  </option>
                ))}
              </select>
            </div>
            <div className="mb-3">
              <label className="form-label">Precio de Venta (S/.)</label>
              <input
                type="number"
                step="0.01"
                min="0.01"
                name="precioVenta"
                className="form-control"
                id="text"
                value={producto.precioVenta}
                onChange={handleChange}
                placeholder="0.00"
                required
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Imagen del producto</label>
              {imagenPreview ? (
                <img
                  src={imagenPreview}
                  id="img_producto"
                  height="197"
                  width="200"
                  className="border rounded mx-auto d-block img-fluid"
                  style={{ objectFit: "cover" }}
                  alt="Vista previa"
                />
              ) : (
                <div
                  className="border rounded mx-auto d-block"
                  style={{
                    height: "197px",
                    width: "200px",
                    backgroundColor: "#f8f9fa",
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    color: "#6c757d",
                  }}
                >
                  Sin imagen
                </div>
              )}
            </div>
            <div className="mb-2">
              <input
                className="form-control"
                type="file"
                id="fileProducto"
                accept="image/png, image/jpg, image/jpeg, image/webp"
                onChange={handleImagenChange}
              />
            </div>
            <div className="row">
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
                    {guardando ? "Guardando..." : "Guardar"}
                  </span>
                </button>
              </div>
              <div className="col-auto">
                <button
                  disabled={guardando}
                  onClick={() => navigate("/almacen/productos")}
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
