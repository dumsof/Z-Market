using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }

        [Display(Name = "Documento")]
        public string Description { get; set; }

        //con esto se hace la relacion para indicar que la tabla DocumentType 
        //Maneja una realacion de uno a varios con la tabla Empleados
        //esto se lee un tipo documento tiene muchos empleados.
        public virtual ICollection<Employee> Employees { get; set; }

        //ralacion de uno a varios con la tabla clientes customers
        public virtual ICollection<Customer> Customer { get; set; }

    }
}
