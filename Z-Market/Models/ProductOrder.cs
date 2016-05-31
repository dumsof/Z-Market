using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    public class ProductOrder:Product
    {

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public float Quantity { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Value { get { return Price * (decimal)Quantity; } }
    }
}
