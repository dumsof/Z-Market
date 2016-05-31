using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    //proveedores
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Primer Contacto")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string ContactFirstName { get; set; }

        [Display(Name = "Segundo Contacto")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string ContactLastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public string Phone { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Addrres { get; set; }

        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //inicio realacion entre la tabla proveedor y ProveedorProductos relacion muchos a muchos
        //indica que el proveedor tiene muchos registro en la tabla Proveedor Producto
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

    }
}
