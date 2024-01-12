using System.ComponentModel.DataAnnotations;

namespace Comercio.Modelos
{
    public class MarcaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El Nombre debe contener 60 caracteres cómo máximo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        [MaxLength(60, ErrorMessage = "La Descripción debe contener 100 caracteres cómo máximo")]
        public string Descripcion { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}
