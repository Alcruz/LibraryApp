using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar una {0}.")]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Cantidad")]
        public int Queantity { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Libro")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Suplidor")]
        public int SupplierId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}