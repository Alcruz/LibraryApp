using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de la órden")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inicio de la órden")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Final de la órden")]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Estado de órden")]
        public int WishStatusId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}.")]
        [Display(Name = "Libro")]
        public int BookId { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual Book Book { get; set; }
    }
}