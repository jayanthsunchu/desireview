﻿using desireview.Data;
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
        // GET: ReviewPageAdmin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Review model) {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59545");
                    var result = client.PostAsJsonAsync("/api/reviews/addreview", model).Result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Error = ex.Message });
            }
        }
    }
}