using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
    public class BookView
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

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Editorial")]
        public int EditorialId { get; set; }

        [Display(Name = "Título")]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Title { get; set; }

        [Display(Name = "Edición")]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        public int Edition { get; set; }

        [Display(Name = "Argumento")]
        [StringLength(500, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 5)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        [DataType(DataType.MultilineText)]
        public string Plot { get; set; }

        [Display(Name = "Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        public DateTime DateOfRelease { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }

        [Display(Name = "Foto")]
        public HttpPostedFileBase PhotoFile { get; set; }
    }
}