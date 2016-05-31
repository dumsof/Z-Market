
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Z_Market.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(40, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Description { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Compra")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public DateTime LastBuy { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public float Stock { get; set; }

        [Display(Name = "Comentario")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        //inicio realacion entre la tabla proveedor y ProveedorProductos relacion muchos a muchos
        //indica que el proveedor tiene muchos registro en la tabla Proveedor Producto
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        //crear la relacion un producto puede estar en muchos detalle de ordenes
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
