using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LibraryApp.Models
{
    public class EditorialView
    {
        public int EditorialId { get; set; }

        [Display(Name = "Editorial")]
        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        public string Description { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }

        [Display(Name = "Foto")]
        public HttpPostedFileBase PhotoFile { get; set; }
    }
}