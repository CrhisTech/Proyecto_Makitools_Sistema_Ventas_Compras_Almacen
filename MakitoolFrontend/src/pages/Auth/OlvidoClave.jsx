import React,{useState} from 'react'
import { Link } from 'react-router-dom'
import { authService } from '../../services/authService'

function OlvidoClave() {
  const [correo, setCorreo] = useState('');
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setCargando(true);
    setMensaje(null);

    try{
        const res = await authService.solicitarCorreo(correo);
        setMensaje({ tipo: 'success', texto: res.message});
        setCorreo('');
    }catch(error){
        setMensaje({
            tipo: 'danger',
            texto: error.response?.data?.message || "Ocurrió un error al procesar tu solicitud."
        });
    }finally{
        setCargando(false);
    }

  }
    return (
    <div className="container d-flex justify-content-center align-items-center vh-100 bg-light">
            <div className="card shadow-lg border-0 rounded-lg p-4" style={{ maxWidth: '450px', width: '100%' }}>
                <div className="text-center mb-4">
                    <h2 className="h4 text-gray-900 font-weight-bold">Recuperar Acceso</h2>
                    <p className="text-muted small">Ingresa tu correo electrónico y te enviaremos un enlace para restablecer tu contraseña.</p>
                </div>

                {mensaje && (
                    <div className={`alert alert-${mensaje.tipo} text-center`} role="alert">
                        {mensaje.texto}
                    </div>
                )}

                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label className="form-label fw-bold">Correo Electrónico</label>
                        <input 
                            type="email" 
                            className="form-control" 
                            placeholder="ejemplo@makitools.com" 
                            value={correo}
                            onChange={(e) => setCorreo(e.target.value)}
                            required 
                            disabled={cargando}
                        />
                    </div>
                    <button 
                        type="submit" 
                        className="btn btn-primary w-100 fw-bold shadow-sm" 
                        disabled={cargando}
                    >
                        {cargando ? 'Enviando instrucciones...' : 'Enviar enlace de recuperación'}
                    </button>
                </form>

                <div className="text-center mt-4">
                    <Link to="/login" className="text-decoration-none text-primary fw-bold">
                        <i className="fa-solid fa-arrow-left me-2"></i>Volver al Login
                    </Link>
                </div>
            </div>
        </div>
  )
}

export default OlvidoClave
