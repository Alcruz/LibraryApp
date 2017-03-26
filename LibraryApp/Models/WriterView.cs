using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LibraryApp.Models
{
    public class WriterView
    {
        public int WriterId { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Name { get; set; }

        [Display(Name = "Biografía")]
        [StringLength(750, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 5)]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }

        [Display(Name = "Foto")]
        public HttpPostedFileBase PhotoFile { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}