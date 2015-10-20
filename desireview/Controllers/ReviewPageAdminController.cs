using desireview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace desireview.Controllers
{
    public class ReviewPageAdminController : Controller
    {
        public ReviewPageAdminController(IDesiReviewRepository repo) {
            this.desi = repo;
        }
        // GET: ReviewPageAdmin
        public ActionResult Index()
        {
            Review model = new Review();
            model.ExistingMovies = desi.GetMovieDropdown();
            return View(model);
        }
        public IDesiReviewRepository desi;
        [HttpPost]
        public ActionResult Index(Review model) {
            try
            {
                desi.AddReview(model);
               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Error = ex.Message });
            }
        }
    }
}