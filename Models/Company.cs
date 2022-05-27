using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ECommerce.Models
{
    public class Company
    {
        [Key]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido!")]
        [MaxLength(50, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Nome")]
        [Index("Department_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Endereço é requerido!")]
        [MaxLength(200, ErrorMessage = "O campo Endereço recebe no máximo 200 caracteres")]
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [MaxLength(20, ErrorMessage = "O campo Telefone recebe no máximo 20 caracteres")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped] //Pegar propriedades do arquivo em geral
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requerido!")]
        [Display(Name = "Departamento")]
        public int DepartmentsId { get; set; }

        [Required(ErrorMessage = "O campo Cidade é requerido!")]
        [Display(Name = "Cidade")]
        public int CityId { get; set; }

        public virtual City Cities { get; set; }
        public virtual Departments Department { get; set; }
    }
}