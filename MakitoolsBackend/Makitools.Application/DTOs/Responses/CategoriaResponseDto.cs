
namespace Makitools.Application.DTOs.Responses
{
    public class CategoriaResponseDto
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = null!;

        public bool? Activo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
