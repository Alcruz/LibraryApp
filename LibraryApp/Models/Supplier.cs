using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Display(Name = "Suplidor")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        [Index("Supplier_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string Phone { get; set; }

        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Index("Supplier_EMail_Index", IsUnique = true)]
        public string EMail { get; set; }

        [Display(Name = "Nombre de la persona encargada")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string PersonInCharge { get; set; }

        [Display(Name = "Teléfono de la persona encargada")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Debe ingresar un {0}.")]
        public string PersonInChargePhone { get; set; }

        [Display(Name = "Correo de la persona encargada")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres.", MinimumLength = 3)]
        public string PersonInChargeEMail { get; set; }
    }
}