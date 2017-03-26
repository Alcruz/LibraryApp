using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class BookType
    {
        [Key]
        public int BookTypeId { get; set; }

        [Display(Name = "Género")]
        [StringLength(15, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        [Index("BookType_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}