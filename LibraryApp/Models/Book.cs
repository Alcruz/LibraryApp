using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [StringLength(13, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 10)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Escritor")]
        public int WriterId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Género")]
        public int BookTypeId { get; set; }

        [Index("Book_Title_Index", 1, IsUnique = true)]
        [Display(Name = "Título")]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Title { get; set; }

        [Index("Book_Title_Index", 2, IsUnique = true)]
        [Display(Name = "Edición")]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        public int Edition { get; set; }

        [Display(Name = "Argumento")]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Plot { get; set; }

        [Display(Name = "Fecgha de lanzamiento")]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        public DateTime DateOfRelease { get; set; }

        public virtual Writer Writer { get; set; }

        public virtual BookType BookType { get; set; }
    }
}