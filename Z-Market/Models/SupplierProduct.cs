using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    //modelo que sirve para contener la relacion muchos a muchos entre proveedor y productos.
    public class SupplierProduct
    {
        [Key]
        public int SupplierProductID { get; set; }


        //inicio realacion de muchos a muchos
        public int ProductID { get; set; }
        public int SupplierID { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }

        //fin relacion de muchos a muchos
    }
}
