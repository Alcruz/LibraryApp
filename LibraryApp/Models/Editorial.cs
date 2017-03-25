using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Editorial
    {
        [Key]
        public int EditorialId { get; set; }

        [Display(Name = "Editorial")]
        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        [Index("Editorial_Description_Index", IsUnique = true)]
        public string Description { get; set; }
    }
}