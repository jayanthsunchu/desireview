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
    [EnableCors(origins: "http://firstshowresponse.com", headers: "*", methods: "*")]
    public class UserRatingsController : ApiController
    {
        private IDesiReviewRepository _repo;
        public UserRatingsController(IDesiReviewRepository repo)
        {
            _repo = repo;
        }
        [HttpPost]
        public UserRating AddUserRating(UserRating rating) {
            return _repo.AddUserRating(rating);
        }
    }
}
