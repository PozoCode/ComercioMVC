using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Comercio.Modelos
{
    public class BodegaModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo Nombre es obligatorio")]
        [MaxLength(60, ErrorMessage ="El campo Nombre debe tener 60 caracteres cómo máximo")]
        //[Remote(action: "ValidarNombre", controller: "Bodega", AdditionalFields = nameof(Id), ErrorMessage = "El nombre de la Bodega ya está en uso")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo Descripcion debe tener 100 caracteres cómo máximo")]
        public string Descripcion { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}
