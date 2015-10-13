using desireview.Data;
using desireview.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace desireview.Controllers
{
    public class MoviePageAdminController : Controller
    {
        // GET: MoviePageAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotAuthorized() {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Movie movieToAdd, HttpPostedFileBase file) {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(path);
                movieToAdd.ImageName = file.FileName;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59545");
                    var result = client.PostAsJsonAsync("/api/movies/addmovie", movieToAdd).Result;
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { Error = ex.Message });
            }
        }
    }
}