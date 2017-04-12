using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Index("Book_Title_Index", 1, IsUnique = true)]
        [Display(Name = "Título")]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        [Index("Book_ISBN_Index", IsUnique = true)]
        [MaxLength(13)]
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

        [Index("Book_Title_Index", 2, IsUnique = true)]
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

        public virtual Writer Writer { get; set; }

        public virtual BookType BookType { get; set; }

        public virtual Editorial Editorial { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }

        public virtual ICollection<BookSeller> BookSellers { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}