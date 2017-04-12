using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class OrderStatus
    {
        [Key]
        public int WishStatusId { get; set; }

        [Index("OrderStatus_Description_Index", IsUnique = true)]
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(25, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}