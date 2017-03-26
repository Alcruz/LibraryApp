using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Cantidad")]
        public int Queantity { get; set; }

        //public virtual Province Province { get; set; }

    }
}