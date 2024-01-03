using System.ComponentModel.DataAnnotations;

namespace Comercio.Modelos
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El nombre debe tener un máximo de 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatorio")]
        [MaxLength(60, ErrorMessage = "La descrioción debe tener un máximo de 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }

    }
}
