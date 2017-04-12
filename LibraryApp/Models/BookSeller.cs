using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class BookSeller
    {
        public int BookSellerId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Libro")]
        [Index("BookSeller_BookId_Index", IsUnique = true)]
        public int BookId { get; set; }

        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        public virtual Book Book { get; set; }
    }
}