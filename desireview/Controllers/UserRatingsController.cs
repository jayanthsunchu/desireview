using desireview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace desireview.Controllers
{
    public class UserRatingsController : ApiController
    {
        private IDesiReviewRepository _repo;
        public UserRatingsController(IDesiReviewRepository repo)
        {
            _repo = repo;
        }
    }
}
