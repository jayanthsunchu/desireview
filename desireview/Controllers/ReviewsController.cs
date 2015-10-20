using desireview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace desireview.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReviewsController : ApiController
    {
        private IDesiReviewRepository _repo;

        public ReviewsController(IDesiReviewRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public Review GetReviewById(int movieId) {
            return _repo.GetReviewById(movieId);
        }

        [HttpPost]
        public bool AddReview(Review movieReview) {
            return _repo.AddReview(movieReview);
        }
    }
}
