import React, { useContext } from "react";
import { Link, Outlet, useLocation } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

export default function MainLayout() {
  const location = useLocation();

  const { user, logout } = useContext(AuthContext);

  const idRolUsuario = user?.idRol;
  const obtenerNombreRol = (idRol) => {
    const roles = {
      1: "Administrador General",
      2: "Vendedor",
      3: "Jefe de Ventas",
      4: "Auxiliar de Compras",
      5: "Asistente de Compras",
      6: "Jefe de Compras",
      7: "Auxiliar de Almacén",
      8: "Asistente de Almacén",
      9: "Jefe de Almacén",
    };
    return roles[idRol] || "Usuario Invitado";
  };

  return (
    <div id="wrapper" className="d-flex" style={{ minHeight: "100vh" }}>
      {/* 2. SIDEBAR (Panel de Mantenimiento a la Izquierda)*/}
      <ul
        className="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"
        id="accordionSidebar"
      >
        <Link
          className="sidebar-brand d-flex align-items-center justify-content-center text-decoration-none"
          to="/almacen/productos"
        >
          <div className="sidebar-brand-text">
            <h4 className="m-0 font-weight-bold">Makitools</h4>
          </div>
        </Link>

        <hr className="sidebar-divider my-0" />

        {/* DASHBOARD: Solo lo ven el Admin (1) o los Jefes (3, 6, 9) */}
        {idRolUsuario === 1  && (
          <>
            <div className="sidebar-heading mt-3">RESUMEN</div>
            <li
              className={`nav-item ${location.pathname === "/dashboard" ? "active" : ""}`}
            >
              <Link className="nav-link" to="/dashboard">
                <i className="fas fa-fw fa-tachometer-alt"></i>
                <span className="mx-2">Dashboard</span>
              </Link>
            </li>
            <hr className="sidebar-divider" />
          </>
        )}

        {/* USUARIOS: Seguridad estricta, solo el Administrador General (1) */}
        {idRolUsuario === 1 && (
          <>
            <div className="sidebar-heading">Seguridad</div>
            <li
              className={`nav-item ${location.pathname === "/usuarios" ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/usuarios">
                <i className="fas fa-fw fa-users"></i>
                <span className="mx-2">Usuarios</span>
              </Link>
            </li>
            <hr className="sidebar-divider" />
          </>
        )}

        {/* MANTENIMIENTO: Lo ve el Admin (1) y el área de Almacén (7,8,9) //[1, 2, 3, 7, 8, 9].includes(idRolUsuario) */}
          {idRolUsuario === 1  && (
          <>
            <div className="sidebar-heading">Mantenimiento</div>
            <li
              className={`nav-item ${location.pathname.includes("/marcas") ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/almacen/marcas">
                <i className="fas fa-fw fa-tags"></i>
                <span className="mx-2">Marcas</span>
              </Link>
            </li>
            <li
              className={`nav-item ${location.pathname.includes("/categorias") ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/almacen/categorias">
                <i className="fas fa-fw fa-list"></i>
                <span className="mx-2">Categorias</span>
              </Link>
            </li>
            <li
              className={`nav-item ${location.pathname.includes("/productos") ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/almacen/productos">
                <i className="fas fa-fw fa-box"></i>
                <span className="mx-2">Productos</span>
              </Link>
            </li>
          </>
        )}
        {/* MÓDULO DE VENTAS (Admin: 1 | Vendedor: 2) */}
        {idRolUsuario === 1  && (
          <>
            <div className="sidebar-heading mt-3">Ventas</div>
            <li
              className={`nav-item ${location.pathname.includes("/clientes") ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/ventas/clientes">
                <i className="fas fa-fw fa-users"></i>
                <span className="mx-2">Clientes</span>
              </Link>
            </li>
            <hr className="sidebar-divider" />
          </>
        )}
        {/* MODULO DE COMPRAS*/}
        {idRolUsuario === 1 && (
          <>
            <div className="sidebar-heading mt-3">Compras</div>
            <li
              className={`nav-item ${location.pathname.includes("/proveedores") ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/compras/proveedores">
                <i className="fas fa-fw fa-truck"></i>
                <span className="mx-2">Proveedores</span>
              </Link>
            </li>

            <li
              className={`nav-item ${location.pathname.includes("/historial") ? "active" : ""}`}
            >
              <Link className="nav-link collapsed" to="/compras/historial">
                <i className="fa-solid fa-cart-flatbed"></i>
                <span className="mx-2">Órdenes de Compra</span>
              </Link>
            </li>

            <hr className="sidebar-divider d-none d-md-block" />
          </>
        )}
      </ul>

      <div id="content-wrapper" className="d-flex flex-column w-100 bg-light">
        <div id="content">
          {/* ======================= TOPBAR ======================= */}
          <nav className="navbar navbar-expand-lg navbar-light bg-white topbar mb-4 static-top shadow px-4">
            <div className="d-flex align-items-center">
              {/* MÓDULO VENTAS: Admin(1) y Ventas(2,3) */}
              {idRolUsuario === 1  && (
                <Link className="navbar-brand text-dark fs-6" to="/ventas">
                  Ventas{" "}
                  <i className="mx-2 fa-solid fa-dollar-sign text-success"></i>
                </Link>
              )}

              {/* MÓDULO COMPRAS: Admin(1) y Compras(4,5,6) */}
              {idRolUsuario === 1  && (
                <Link className="navbar-brand text-dark fs-6" to="/compras">
                  Compras{" "}
                  <i className="mx-2 fa-solid fa-bag-shopping text-warning"></i>
                </Link>
              )}

              {/* MÓDULO ALMACÉN: Admin(1) y Almacén(7,8,9) */}
              {idRolUsuario === 1  && (
                <Link
                  className="navbar-brand text-dark fs-6"
                  to="/almacen/productos"
                >
                  Almacén{" "}
                  <i className="mx-2 fa-solid fa-warehouse text-primary"></i>
                </Link>
              )}
            </div>

            {/* Perfil de Usuario */}
            <ul className="navbar-nav ms-auto">
              <li className="nav-item dropdown no-arrow">
                <a
                  className="nav-link dropdown-toggle text-dark d-flex align-items-center"
                  href="#"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  <span className="me-3 d-none d-lg-inline text-gray-600 small fw-bold">
                    {user ? `${user.correo}` : "Cargando..."}{" "}
                    <br />
                    <small className="text-primary">
                      {obtenerNombreRol(idRolUsuario)}
                    </small>
                  </span>
                  <i className="fa-solid fa-circle-user fs-2 text-primary"></i>
                </a>
                <ul className="dropdown-menu dropdown-menu-end shadow animated--grow-in">
                  <li>
                    <button className="dropdown-item" onClick={logout}>
                      Cerrar Sesión{" "}
                      <i className="fa-solid fa-arrow-right-from-bracket ms-2"></i>
                    </button>
                  </li>
                </ul>
              </li>
            </ul>
          </nav>

          <div className="container-fluid">
            <Outlet />
          </div>
        </div>
      </div>
    </div>
  );
}
