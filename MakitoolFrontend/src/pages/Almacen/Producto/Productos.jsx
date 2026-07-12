import React, { useEffect, useState } from "react";
import { productoService } from "../../../services/productoService";
import { Link, useNavigate } from "react-router-dom";

export default function Productos() {
  const [productos, setProductos] = useState([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  useEffect(() => {
    const cargarProductos = async () => {
      try {
        const data = await productoService.listarProductos();
        setProductos(data);
      } catch (err) {
        setError(
          "No se pudieron cargar los productos. Validar que el backend este encendido.",
        );
        console.error(err);
      } finally {
        setCargando(false);
      }
    };
    cargarProductos();
  }, []);

  const handleEliminar = async (id, nombre) => {
    const confirmar = window.confirm(
      `¿Estás seguro de que deseas eliminar el producto "${nombre}"?`,
    );
    if (confirmar) {
      try {
        await productoService.eliminarProducto(id);
        setProductos(productos.filter((prod) => prod.idProducto !== id));
        alert("Producto eliminado correctamente.");
      } catch (err) {
        alert("Error al eliminar el producto.");
        console.error(err);
      }
    }
  };

  // Igualamos los estilos de carga y error a los de Proveedores
  if (cargando) return <div className="p-4">Cargando catálogo...</div>;
  if (error) return <div className="p-4 text-danger">Error: {error}</div>;

  return (
    <div className="container-fluid">
      {/* 1. Título actualizado al estilo h3 de Proveedores */}
      <h2 className="h3 mb-4 text-gray-800">
        Gestión de Almacén - Catálogo de Productos
      </h2>

      <div className="card shadow mb-4">
        {/* 2. Cabecera alineada con Flexbox en lugar de Rows/Cols */}
        <div className="card-header py-3 d-flex flex-row align-items-center justify-content-between">
          <h6 className="m-0 font-weight-bold text-primary">
            Lista de Productos
          </h6>
          <button
            onClick={() => navigate("/almacen/productos/nuevo")}
            className="btn btn-success btn-sm shadow-sm"
          >
            <i className="fa-solid fa-plus fa-sm text-white-50 me-2"></i> Nuevo
            producto
          </button>
        </div>

        <div className="card-body">
          <div className="table-responsive">
            {/* 3. Tabla con efecto hover */}
            <table className="table table-hover" cellSpacing="0" width="100%">
              {/* 4. Cabecera gris clara (table-light) */}
              <thead className="table-light">
                <tr>
                  <th scope="col">SKU</th>
                  <th scope="col">Nombre</th>
                  <th scope="col">Categoria</th>
                  <th scope="col">Marca</th>
                  <th scope="col">Precio(S)</th>
                  <th scope="col">Stock</th>
                  <th scope="col">Estado</th>
                  <th scope="col">
                    <i className="fa-solid fa-gears text-primary"></i>
                  </th>
                </tr>
              </thead>
              <tbody>
                {productos.length === 0 ? (
                  <tr>
                    <td colSpan="8" className="text-center">
                      No hay productos registrados en la base de datos.
                    </td>
                  </tr>
                ) : (
                  productos.map((prod) => (
                    <tr key={prod.idProducto}>
                      <td className="fw-bold">{prod.sku}</td>
                      <td>{prod.nombre}</td>
                      <td>{prod.categoria}</td>
                      <td>{prod.marca}</td>
                      <td>{prod.precioVenta.toFixed(2)}</td>
                      <td>{prod.stock}</td>
                      <td>
                        <span
                          className={`badge ${prod.activo ? "bg-success" : "bg-danger"} rounded-pill`}
                        >
                          {prod.activo ? "Activo" : "Inactivo"}
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
                                to={`/almacen/productos/editar/${prod.idProducto}`}
                              >
                                <i className="fa-regular fa-pen-to-square text-primary me-2"></i>{" "}
                                Editar
                              </Link>
                            </li>
                            <li>
                              <button
                                className="dropdown-item"
                                onClick={() =>
                                  handleEliminar(prod.idProducto, prod.nombre)
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
