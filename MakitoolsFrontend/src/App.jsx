import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";

{
  /* ALMACEN */
}
import Productos from "./pages/Almacen/Productos";
import NuevoProducto from "./pages/Almacen/NuevoProducto";
import Categorias from "./pages/Almacen/Categorias";
import Marcas from "./pages/Almacen/Marcas";
import MainLayout from "./components/MainLayout";
import EditarProducto from "./pages/Almacen/EditarProducto";

{
  /* COMPRAS */
}
import HistorialCompras from "./pages/Compras/HistorialCompras";
import NuevaCompra from "./pages/Compras/NuevaCompra";
import Proveedores from "./pages/Compras/Proveedores";
import EditarProveedor from "./pages/Compras/EditarProveedor";
import NuevoProveedor from "./pages/Compras/NuevoProveedor";

{
  /* VENTAS */
}
import Clientes from "./pages/Ventas/Clientes";
import NuevoCliente from "./pages/Ventas/NuevoCliente";
import EditarCliente from "./pages/Ventas/EditarCliente";

{
  /* SEGURIDAD - USUARIOS*/
}
import Usuarios from "./pages/Seguridad/Usuarios";
import NuevoUsuario from "./pages/Seguridad/NuevoUsuario";
import EditarUsuario from "./pages/Seguridad/EditarUsuario";

{
  /* AUTENTICACION*/
}
import { AuthProvider } from "./context/AuthContext";
import Login from "./pages/Auth/Login";
import LoginCliente from "./pages/Auth/LoginCliente";
import { ProtectedRoute } from "./components/ProtectedRoute";

function App() {
  return (
    <Router>
      <AuthProvider>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/cliente/login" element={<LoginCliente />} />

          <Route element={<ProtectedRoute />}>
            <Route element={<MainLayout />}>
              {/* Modulo de seguridad*/}
              <Route element={<ProtectedRoute allowedRoles={[1]} />}>
                <Route path="/usuarios" element={<Usuarios />} />
                <Route path="/usuarios/nuevo" element={<NuevoUsuario />} />
                <Route
                  path="/usuarios/editar/:id"
                  element={<EditarUsuario />}
                />
              </Route>
              {/* Modulo de almacen */}
              <Route path="/almacen/productos" element={<Productos />} />
              <Route
                path="/almacen/productos/nuevo"
                element={<NuevoProducto />}
              />
              <Route
                path="/almacen/productos/editar/:id"
                element={<EditarProducto />}
              />
              <Route path="/almacen/categorias" element={<Categorias />} />
              <Route path="/almacen/marcas" element={<Marcas />} />

              {/* Modulo de compras*/}
              <Route path="/compras/proveedores" element={<Proveedores />} />
              <Route
                path="/compras/proveedores/editar/:id"
                element={<EditarProveedor />}
              />
              <Route
                path="/compras/proveedores/nuevo"
                element={<NuevoProveedor />}
              />
              {/* Modulo de compras*/}
              <Route element={<ProtectedRoute allowedRoles={[1, 2]} />}>
                <Route path="/ventas/clientes" element={<Clientes />} />
                <Route
                  path="/ventas/clientes/nuevo"
                  element={<NuevoCliente />}
                />
                <Route element={<ProtectedRoute allowedRoles={[1, 4, 7]} />}>
                  <Route
                    path="/compras/historial"
                    element={<HistorialCompras />}
                  />
                </Route>
                <Route element={<ProtectedRoute allowedRoles={[1, 4]} />}>
                  <Route
                    path="/compras/nuevo-pedido"
                    element={<NuevaCompra />}
                  />
                </Route>
                <Route
                  path="/ventas/clientes/editar/:id"
                  element={<EditarCliente />}
                />
              </Route>
            </Route>
          </Route>

          <Route path="*" element={<Navigate to="/almacen/productos" />} />
        </Routes>
      </AuthProvider>
    </Router>
  );
}

export default App;
