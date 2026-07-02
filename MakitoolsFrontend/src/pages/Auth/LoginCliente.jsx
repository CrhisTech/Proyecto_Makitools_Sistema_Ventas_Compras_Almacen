import React, { useState } from 'react';
import '../../assets/css/ClienteLoginStyle.css';

export default function LoginCliente() {
    // ESTADO PARA LA ANIMACIÓN (Reemplaza al JavaScript tradicional)
    const [isRegisterActive, setIsRegisterActive] = useState(false);

    const handleSubmit = (e) => {
        e.preventDefault();
        alert("Integración de API de clientes pendiente...");
    };

    return (
        <div className="login-page-wrapper">
            
            {/* Si isRegisterActive es TRUE, inyecta la clase 'active' que dispara la animación CSS */}
            <div className={`login-container ${isRegisterActive ? 'active' : ''}`} id="container">

                {/* --- FORMULARIO DE LOGIN --- */}
                <div className="form-container sign-in">
                    <form className="login-form" onSubmit={handleSubmit}>
                        <h1>Iniciar Sesión</h1>
                        <span>Usa tu correo y contraseña</span>

                        <div className="container-input">
                            <ion-icon name="mail-outline"></ion-icon>
                            <input type="email" placeholder="Correo electrónico" required />
                        </div>

                        <div className="container-input">
                            <ion-icon name="lock-closed-outline"></ion-icon>
                            <input type="password" placeholder="Contraseña" required />
                        </div>

                        <a href="#">¿Olvidaste tu contraseña?</a>
                        <button type="submit">Iniciar Sesión</button>
                    </form>
                </div>

                {/* --- FORMULARIO DE REGISTRO --- */}
                <div className="form-container sign-up">
                    <form className="login-form" onSubmit={handleSubmit}>
                        <h1>Registrarse</h1>
                        <div className="container-input">
                            <ion-icon name="person-outline"></ion-icon>
                            <input type="text" placeholder="Nombre completo" required />
                        </div>
                        <div className="container-input">
                            <ion-icon name="mail-outline"></ion-icon>
                            <input type="email" placeholder="Correo electrónico" required />
                        </div>
                        <div className="container-input">
                            <ion-icon name="lock-closed-outline"></ion-icon>
                            <input type="password" placeholder="Contraseña" required />
                        </div>
                        <div className="container-input">
                            <ion-icon name="shield-checkmark-outline"></ion-icon>
                            <input type="password" placeholder="Confirmar contraseña" required />
                        </div>
                        
                        <button type="submit">Registrarse</button>
                    </form>
                </div>

                {/* --- PANEL DE ANIMACIÓN DESLIZANTE --- */}
                <div className="toggle-container">
                    <div className="toggle">
                        <div className="toggle-panel toggle-left">
                            <h1>¡Bienvenido de nuevo!</h1>
                            <p>Introduzca sus datos personales para utilizar todas las funciones del sitio.</p>
                            <button type="button" className="hidden-btn" onClick={() => setIsRegisterActive(false)}>
                                Iniciar Sesión
                            </button>
                        </div>

                        <div className="toggle-panel toggle-right">
                            <h1>¡Hola, amigo!</h1>
                            <p>Regístrese con sus datos personales para utilizar todas las funciones del sitio.</p>
                            <button type="button" className="hidden-btn" onClick={() => setIsRegisterActive(true)}>
                                Registrarse
                            </button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    );
}