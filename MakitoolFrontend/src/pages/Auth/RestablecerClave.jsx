import React, {useState} from 'react'
import { useNavigate, useSearchParams, Link } from 'react-router-dom'
import { authService } from '../../services/authService'

function RestablecerClave() {

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();
    const token = searchParams.get('token');

    const [claves, setClaves] = useState({nuevaClave: '', confirmarClave:''});
    const [cargando, setCargando] = useState(false);
    const [mensaje, setMensaje] = useState(null);

    const handleChange = (e) => {
        setClaves({...claves, [e.target.name]: e.target.value});
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if(claves.nuevaClave !== claves.confirmarClave){
            setMensaje({ tipo: 'danger', texto: 'Las contraseñas no coinciden.'});
            return;
        }
        if(claves.nuevaClave.length < 6){
            setMensaje({tipo: 'danger', texto: 'La contraseña debe tener al menos 6 caracteres.'});
            return;
        }

        setCargando(true);
        setMensaje(null);

        try{
            await authService.restablecerPassword(token, claves.nuevaClave);
            alert("Contraseña restablecida con éxito! Ya puedes iniciar sesión.");
            navigate('/login');
        }catch(error){
            setMensaje({
                tipo: 'danger',
                texto: error.response?.data?.message || "El enlace es inválido o ha caducado."
            });
        }finally{
            setCargando(false);
        }
    };

    if(!token){
        <div className="container d-flex justify-content-center align-items-center vh-100 bg-light">
                <div className="alert alert-danger shadow-sm text-center">
                    <h4><i className="fa-solid fa-triangle-exclamation me-2"></i>Enlace inválido</h4>
                    <Link to="/login" className="btn btn-outline-danger mt-2">Ir al Login</Link>
                </div>
            </div>
    }

  return (
    <div className="container d-flex justify-content-center align-items-center vh-100 bg-light">
            <div className="card shadow-lg border-0 rounded-lg p-4" style={{ maxWidth: '450px', width: '100%' }}>
                <div className="text-center mb-4">
                    <h2 className="h4 text-gray-900 font-weight-bold">Nueva Contraseña</h2>
                    <p className="text-muted small">Crea una nueva contraseña segura para tu cuenta.</p>
                </div>

                {mensaje && (
                    <div className={`alert alert-${mensaje.tipo} text-center`} role="alert">
                        {mensaje.texto}
                    </div>
                )}

                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label className="form-label fw-bold">Nueva Contraseña</label>
                        <input 
                            type="password" 
                            name="nuevaClave"
                            className="form-control" 
                            value={claves.nuevaClave}
                            onChange={handleChange}
                            required 
                            disabled={cargando}
                        />
                    </div>
                    <div className="mb-4">
                        <label className="form-label fw-bold">Confirmar Contraseña</label>
                        <input 
                            type="password" 
                            name="confirmarClave"
                            className="form-control" 
                            value={claves.confirmarClave}
                            onChange={handleChange}
                            required 
                            disabled={cargando}
                        />
                    </div>
                    <button 
                        type="submit" 
                        className="btn btn-success w-100 fw-bold shadow-sm" 
                        disabled={cargando}
                    >
                        {cargando ? 'Guardando...' : 'Guardar nueva contraseña'}
                    </button>
                </form>
            </div>
        </div>
  )
}

export default RestablecerClave
