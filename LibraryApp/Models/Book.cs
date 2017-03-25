using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [StringLength(13, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 10)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string ISBN { get; set; }

        public string Title { get; set; }

        public int Edition { get; set; }


        [Display(Name = "Argumento")]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Plot { get; set; }

    }
}