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
    [EnableCors(origins: "http://127.0.0.1:65206", headers: "*", methods: "*")]
    public class UserRatingsController : ApiController
    {
        private IDesiReviewRepository _repo;
        public UserRatingsController(IDesiReviewRepository repo)
        {
            _repo = repo;
        }
    }
}
