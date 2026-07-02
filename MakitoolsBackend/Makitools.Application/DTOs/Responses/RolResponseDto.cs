namespace Makitools.Application.DTOs.Responses
{
    public class RolResponseDto
    {
        public int IdRol { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Activo { get; set; }
    }
}
