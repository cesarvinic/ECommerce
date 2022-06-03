using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class ComboboxHelper : IDisposable
    {
        private static EcommerceContext db = new EcommerceContext();
        public static List<Departments> GetAllDepartments()
        {
            var departments = db.Departments.ToList();
            departments.Add(new Departments
            {
                DepartmentsId = 0,
                Name = "[ Selecione um Departamento ]"
            });
            return departments = departments.OrderBy(x => x.Name).ToList();

        }

        public static List<City> GetAllCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[ Selecione uma Cidade ]"
            });
            return cities = cities.OrderBy(x => x.Name).ToList();
        }

        public static List<City> GetCitiesByDepartment(int departmentsId)
        {
            var cities = db.Cities.Where(x => x.DepartmentsId == departmentsId).ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[ Selecione uma Cidade ]"
            });
            return cities = cities.OrderBy(x => x.Name).ToList();
        }

        public static List<Company> GetAllCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CompanyId = 0, 
                Name = "[ Selecione uma Empresa ]"
            });
            return companies = companies.OrderBy(x => x.Name).ToList();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            db.Dispose();
        }
    }
}