using desireview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace desireview.Controllers
{
    [EnableCors(origins: "http://firstshowresponse.com", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private IDesiReviewRepository _repo;
        public MoviesController(IDesiReviewRepository repo) {
            _repo = repo;
        }
        [HttpPost]
        public IEnumerable<Movie> Get(UserAccessToken user) {
            return _repo.GetMovies(user);
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
