using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Writer
    {
        [Key]
        public int WriterId { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        [Index("DocumentType_Name_Index", IsUnique = true)]
        public string Name { get; set; }
    }
}