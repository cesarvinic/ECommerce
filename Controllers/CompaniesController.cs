using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Classes;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class CompaniesController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        // GET: Companies
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.Cities).Include(c => c.Department);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name");
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name");
            return View();
        }

        // POST: Companies/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                //var pic = string.Empty;
                //var folder = "~/Content/Logos";

                //Upload da imagem com nome próprio
                //if (company.LogoFile != null)
                //{
                //    //pic = FilesHelper.UploadPhoto(company.LogoFile, folder);
                //    //pic = string.Format("{0}/{1}", folder, pic);
                //    var response = FilesHelper.UploadPhoto(company.LogoFile, folder);
                //}
                //company.Logo = pic;

                db.Companies.Add(company);
                db.SaveChanges();

                //Upload da imagem com o nome igual ao Id do registro. 
                if (company.LogoFile != null)
                {
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}.jpg", company.CompanyId);

                    var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                    if (response == true)
                    {
                        string pic = string.Format("{0}/{1}", folder, file);
                        company.Logo = pic;
                    }
                }

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name", company.CityId);
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name", company.DepartmentsId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name", company.CityId);
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name", company.DepartmentsId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.LogoFile != null)
                {
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}.jpg", company.CompanyId);

                    var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                    if (response == true)
                    {
                        string pic = string.Format("{0}/{1}", folder, file);
                        company.Logo = pic;
                    }
                }

                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name", company.CityId);
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name", company.DepartmentsId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region [Métodos Auxiliares]
        //CONTROLE DE LIST VIEW EM CASCATA
        public JsonResult GetCities(int departmentId)
        {
            if (departmentId != 0)
            {
                db.Configuration.ProxyCreationEnabled = false;
                var cities = db.Cities.Where(x => x.DepartmentsId == departmentId);
                return Json(cities);
            }
            else
            {
                db.Configuration.ProxyCreationEnabled = false;
                var cities = db.Cities.ToList();
                return Json(cities);
            }
        }

        //UPLOAD DE IMAGENS

        #endregion
    }
}
