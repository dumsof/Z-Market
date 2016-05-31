using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Market.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener  entre {2} y {1} caracteres", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Salario")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Salary { get; set; }

        [Display(Name = "Porcentaje")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Hora Entrada")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }


        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        //campo calculado que no se crea en la base de datos
        [NotMapped]
        [Display(Name = "Edad")]
        public int Age
        {
            get
            {
                DateTime nacimiento = new DateTime(DateOfBirth.Year, DateOfBirth.Month, DateOfBirth.Day); //Fecha de nacimiento
                int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
                return edad;
            }

        }

        //inicio
        //para crear la relacion lado varios entre DocumentType y Employee
        //campo en la tabla employee que se realaciona con la tabla DocumentType
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public int DocumentTypeID { get; set; }
        //indica que la tabla empleados se realizaciona con la tabla tipo de documento DocumentType uno a mucho.
        public virtual DocumentType DocumentType { get; set; }
        //Fin crear relacion

    }
}
