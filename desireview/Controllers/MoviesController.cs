﻿using desireview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace desireview.Controllers
{
    public class MoviesController : ApiController
    {
        private IDesiReviewRepository _repo;
        public MoviesController(IDesiReviewRepository repo) {
            _repo = repo;
        }

        public IEnumerable<Movie> Get() {
            return _repo.GetMovies();
        }
    }
}