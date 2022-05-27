using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Departments
    {
        [Key]
        [Display(Name = "Departamento")]
        public int DepartmentsId { get; set; }

        //[Required(ErrorMessage = "O campo {0} é requerido!")]
        [Required(ErrorMessage = "O campo Nome é requerido!")]
        [MaxLength(50, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Nome")]
        [Index("Department_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities {get; set;}
        public virtual ICollection<Company> Companies { get; set; }
    }
}