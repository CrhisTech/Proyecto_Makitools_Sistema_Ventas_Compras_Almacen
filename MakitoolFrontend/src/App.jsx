import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";

/* ALMACEN */
import Productos from "./pages/Almacen/Producto/Productos";
import NuevoProducto from "./pages/Almacen/Producto/NuevoProducto";
import Categorias from "./pages/Almacen/Categoria/Categorias";
import EditarCategoria from "./pages/Almacen/Categoria/EditarCategoria"
import Marcas from "./pages/Almacen/Marca/Marcas";
import EditarMarca from "./pages/Almacen/Marca/EditarMarca";
import EditarProducto from "./pages/Almacen/Producto/EditarProducto";

/* COMPRAS */
import HistorialCompras from "./pages/Compras/Compra/HistorialCompras";
import NuevaCompra from "./pages/Compras/Compra/NuevaCompra";
import Proveedores from "./pages/Compras/Proveedor/Proveedores";
import EditarProveedor from "./pages/Compras/Proveedor/EditarProveedor";
import NuevoProveedor from "./pages/Compras/Proveedor/NuevoProveedor";

/* VENTAS */
import Clientes from "./pages/Ventas/Cliente/Clientes";
import NuevoCliente from "./pages/Ventas/Cliente/NuevoCliente";
import EditarCliente from "./pages/Ventas/Cliente/EditarCliente";

/* SEGURIDAD - USUARIOS*/
import Usuarios from "./pages/Seguridad/Usuario/Usuarios";
import NuevoUsuario from "./pages/Seguridad/Usuario/NuevoUsuario";
import EditarUsuario from "./pages/Seguridad/Usuario/EditarUsuario";

/* AUTENTICACION & LAYOUT */
import { AuthProvider } from "./context/AuthContext";
import Login from "./pages/Auth/Login";
import LoginCliente from "./pages/Auth/LoginCliente";
import { ProtectedRoute } from "./components/ProtectedRoute";
import MainLayout from "./components/MainLayout";
import OlvidoClave from "./pages/Auth/OlvidoClave";
import RestablecerClave from "./pages/Auth/RestablecerClave";

function App() {
  return (
    <Router>
      <AuthProvider>
        <Routes>
          {/* RUTAS PÚBLICAS (Sin protección) */}
          <Route path="/login" element={<Login />} />
          <Route path="/olvido-clave" element={<OlvidoClave/>}/>
          <Route path="/reset-password" element={<RestablecerClave/>}/>
          <Route path="/cliente/login" element={<LoginCliente />} />

          {/* RUTAS PRIVADAS (Requieren estar logueado) */}
          <Route element={<ProtectedRoute />}>
            <Route  element={<MainLayout/>}>

              {/* 1. MÓDULO DE SEGURIDAD (Solo Admin General: 1) */}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/usuarios" element={<Usuarios />} />
                <Route path="/usuarios/nuevo" element={<NuevoUsuario />} />
                <Route path="/usuarios/editar/:id" element={<EditarUsuario />} />
              </Route>

              {/* 2. MÓDULO DE ALMACÉN (Admin: 1 | Almacén: 7, 8, 9) */}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/almacen/productos" element={<Productos />} />
                <Route path="/almacen/productos/nuevo" element={<NuevoProducto />} />
                <Route path="/almacen/productos/editar/:id" element={<EditarProducto />} />
                <Route path="/almacen/categorias" element={<Categorias />} />
                <Route path="/almacen/categorias/editar/:id" element={<EditarCategoria />}/>
                <Route path="/almacen/marcas" element={<Marcas />} />
                <Route path="/almacen/marcas/editar/:id" element={<EditarMarca/>}/>
              </Route>

              {/* 3. MÓDULO DE COMPRAS - PROVEEDORES (Admin: 1 | Compras: 4, 5, 6) */}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/compras/proveedores" element={<Proveedores />} />
                <Route path="/compras/proveedores/nuevo" element={<NuevoProveedor />} />
                <Route path="/compras/proveedores/editar/:id" element={<EditarProveedor />} />
              </Route>

              {/* 4. MÓDULO DE VENTAS - CLIENTES (Admin: 1 | Ventas: 2, 3) */}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/ventas/clientes" element={<Clientes />} />
                <Route path="/ventas/clientes/nuevo" element={<NuevoCliente />} />
                <Route path="/ventas/clientes/editar/:id" element={<EditarCliente />} />
              </Route>

              {/* 5. MÓDULO DE ABASTECIMIENTO - ÓRDENES DE COMPRA */}
              {/* Historial: Visible para Admin(1), Compras(4) y Almacén(7) */}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/compras/historial" element={<HistorialCompras />} />
              </Route>

              {/* Generar Pedido: Solo Admin(1) y Compras(4) pueden crear nuevas órdenes */}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/compras/nuevo-pedido" element={<NuevaCompra />} />
              </Route>

            </Route>
          </Route>

          {/* RUTA POR DEFECTO (Fallback para URLs no válidas) */}
          
          <Route path="*" element={<Navigate to="/almacen/productos" />} />
        </Routes>
      </AuthProvider>
    </Router>
  );
}

export default App;