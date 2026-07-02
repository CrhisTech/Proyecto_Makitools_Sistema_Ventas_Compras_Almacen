import React, { useState, useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import "../../assets/css/UsuarioLoginStyle.css"
import { authService } from "../../services/authService";

export default function Login() {
  const { login } = useContext(AuthContext);
  const [credenciales, setCredenciales] = useState({ correo: "", clave: "" });
  const [cargando, setCargando] = useState(false);

  const handleChange = (e) => {
    setCredenciales({ ...credenciales, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setCargando(true);

    try {
      const userData = await authService.login(credenciales);
      login(userData);
    } catch (error) {
      const mensajeError =
        error.response?.data?.message || "Error al conectarse con el servidor";
      alert(mensajeError);
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="login-page-wrapper">
      <div className="login-container-usuario">
        <div className="form-container-usuario sign-in">
          <form className="login-form" onSubmit={handleSubmit}>
            <h1>Iniciar Sesión</h1>
            <span>Ingresa con tu correo y contraseña</span>

            <div className="container-input">
              <i className="fa-regular fa-envelope"></i>
              <input
                type="email"
                name="correo"
                value={credenciales.correo}
                onChange={handleChange}
                placeholder="Correo electrónico"
                required
              />
            </div>

            <div className="container-input">
              <i className="fa-solid fa-lock"></i>
              <input
                type="password"
                name="clave"
                value={credenciales.clave}
                onChange={handleChange}
                placeholder="Contraseña"
                required
              />
            </div>

            <a href="#">¿Olvidaste tu contraseña?</a>

            <button type="submit" disabled={cargando}>
              {cargando ? "Verificando..." : "Iniciar Sesión"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
