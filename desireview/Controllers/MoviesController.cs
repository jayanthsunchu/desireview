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
    public class MoviesController : ApiController
    {
        private IDesiReviewRepository _repo;
        public MoviesController(IDesiReviewRepository repo) {
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<Movie> Get() {
            return _repo.GetMovies();
        }

        [HttpGet]
        public IEnumerable<Movie> GetByLanguage(string id)
        {
            return _repo.GetMoviesByLanguage(id);
        }

        [HttpPost]
        public bool AddMovie(Movie movieToAdd)
        {
            return _repo.AddMovie(movieToAdd);
        }
    }
}
