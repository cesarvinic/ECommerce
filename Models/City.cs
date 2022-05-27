using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class City
    {
        [Key]
        [Display(Name = "Cidade")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido!")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requerido!")]
        [Display(Name = "Departamento")]
        [Range(1, double.MaxValue, ErrorMessage = "Selecione um Departamento")]
        public int DepartmentsId { get; set; }

        public virtual Departments Department { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}