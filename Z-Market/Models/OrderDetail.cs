using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(40, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Description { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public float Quantity { get; set; }


    }
}
