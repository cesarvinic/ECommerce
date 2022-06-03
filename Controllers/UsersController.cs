using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ECommerce.Classes;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class UsersController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Cities).Include(u => u.Company).Include(u => u.Department);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name");
            ViewBag.CompanyId = new SelectList(ComboboxHelper.GetAllCompanies(), "CompanyId", "Name");
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name");
            return View();
        }

        // POST: Users/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                //Upload da imagem com o nome igual ao Id do registro. 
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}.jpg", user.UserId);

                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response == true)
                    {
                        string pic = string.Format("{0}/{1}", folder, file);
                        user.Photo = pic;
                    }
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(ComboboxHelper.GetAllCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name", user.DepartmentsId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(ComboboxHelper.GetAllCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name", user.DepartmentsId);
            return View(user);
        }

        // POST: Users/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                //Upload da imagem com o nome igual ao Id do registro. 
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Logos";
                    var file = string.Format("{0}.jpg", user.UserId);

                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response == true)
                    {
                        string pic = string.Format("{0}/{1}", folder, file);
                        user.Photo = pic;
                    }
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(ComboboxHelper.GetAllCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(ComboboxHelper.GetAllCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartmentsId = new SelectList(ComboboxHelper.GetAllDepartments(), "DepartmentsId", "Name", user.DepartmentsId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        #endregion
    }
}
