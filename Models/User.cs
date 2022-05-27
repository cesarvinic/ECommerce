using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Empresa")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O campo E-Mail é requerido!")]
        [MaxLength(250, ErrorMessage = "O campo E-Mail recebe no máximo 250 caracteres")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [Index("User_UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido!")]
        [MaxLength(50, ErrorMessage = "O campo Nome recebe no máximo 50 caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é requerido!")]
        [MaxLength(50, ErrorMessage = "O campo Sobrenome recebe no máximo 50 caracteres")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

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
        public string Photo { get; set; }

        [NotMapped] //Pegar propriedades do arquivo em geral
        public HttpPostedFileBase PhotoFile { get; set; }

        //Por ter propriedade somente de Get ele não envia para o banco de dados criá-lo. 
        [Display(Name = "Usuário")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [Required(ErrorMessage = "O campo Empresa é requerido!")]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required(ErrorMessage = "O campo Departamento é requerido!")]
        [Display(Name = "Departamento")]
        public int DepartmentsId { get; set; }
        public virtual Departments Department { get; set; }

        [Required(ErrorMessage = "O campo Cidade é requerido!")]
        [Display(Name = "Cidade")]
        public int CityId { get; set; }
        public virtual City Cities { get; set; }
    }
}