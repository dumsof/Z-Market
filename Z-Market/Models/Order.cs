using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Pedido")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public DateTime DateOrder { get; set; }

        //realcion con el modelo customers
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        //fin relacion modelo customer

        [Display(Name = "Estado")]
        public OrderStatus OrderStatus { get; set; }

        //crear la relacion una orden puede tener muchos detalles o orderDetails
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
