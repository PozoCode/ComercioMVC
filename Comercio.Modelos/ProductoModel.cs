using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comercio.Modelos
{
    public class ProductoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de serie es obligatorio")]
        [MaxLength(60)]
        public string NumeroSerie  { get; set; }

        [Required(ErrorMessage = "La descripción es obligatorio")]
        [MaxLength(60)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio")]
        public double Costo { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }

        #region RelacionCategoria
        [Required(ErrorMessage = "La categoria es obligatorio")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public CategoriaModel Categoria { get; set; }

        #endregion RelacionCategoria

        #region RelacionMarca
        [Required(ErrorMessage = "La marca es obligatorio")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public MarcaModel Marca { get; set; }

        #endregion RelacionMarca

        #region Recursividad
        public int? PadreId { get; set; }

        public virtual ProductoModel Padre { get; set; }

        #endregion Recursividad

    }
}
