using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    //tabla clientes
    public class Customer
    {
        private string _fullName = "-Seleccione un cliente-";
        [Key]
        public int CustomerID { get; set; }

        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(15, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 8)]
        public string Document { get; set; }

        //inicio relacion para el tipo de documento con el modelo DocumentType
        //relacion de uno a muchos en el cliente
        [Display(Name = "Tipo Documento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        //fin de la relacion entre tipo documento y clientes

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(12, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Phone { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Addrres { get; set; }

        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [NotMapped]
        public string FullName { get { _fullName = string.Format("{0} {1}", FirstName, LastName); return _fullName; } set { _fullName = value; } }

        //indicar relacion que un cliente puede tener varias orders o pedidos
        public virtual ICollection<Order> Orders { get; set; }
    }
}
