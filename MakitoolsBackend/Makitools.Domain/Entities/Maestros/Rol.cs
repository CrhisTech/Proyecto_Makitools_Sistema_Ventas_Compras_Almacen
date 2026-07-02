namespace Makitools.Domain.Entities.Maestros
{
    public class Rol
    {
        public int IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Activo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}
