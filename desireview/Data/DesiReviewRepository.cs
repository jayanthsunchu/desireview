using desireview.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace desireview.Data
{
    class DesiReviewRepository : IDesiReviewRepository
    {
        DesiReviewContext _ctx;

        public void SubmitContact(Contact contact) {
            try {
                _ctx.Contacts.Add(contact);
                _ctx.SaveChanges();
            }
            catch (DbUpdateException) { }
        }

        public IQueryable<Contact> GetContacts() {
            return _ctx.Contacts.ToList().AsQueryable();
        }

        public UserRating AddUserRating(UserRating rating) {
            try
            {
                //Testing GitHub
                if (validateUserToken(rating.UserName, rating.UserAccessToken)) {
                    rating.UserId = _ctx.Users.Single(x => x.UserName == rating.UserName).Id;
                    if (_ctx.UserRatings.Where(x => (x.UserId == rating.UserId && x.MovieId == rating.MovieId)).Count() > 0)
                    {
                        var itemToUpdate = _ctx.UserRatings.Single(x => (x.UserId == rating.UserId && x.MovieId == rating.MovieId));
                        
                        itemToUpdate.Rating = rating.Rating;
                        if (rating.VideoReviewThumb != null && rating.VideoReviewUrl != null)
                        {
                            itemToUpdate.VideoReviewUrl = rating.VideoReviewUrl;
                            itemToUpdate.VideoReviewThumb = rating.VideoReviewThumb;
                        }
                    }
                    else
                    _ctx.UserRatings.Add(rating);
                    if (_ctx.SaveChanges() > 0) {
                        UpdateAverageRating(rating.MovieId);
                        return rating;
                    }
                    else
                    {
                        var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("There was an error adding the rating.")),
                            ReasonPhrase = "There was an error adding the rating."
                        };
                        throw new HttpResponseException(res);
                    }
                }
                else {
                    var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("User not validated.")),
                            ReasonPhrase = "User not validated."
                    };
                        throw new HttpResponseException(res);
                }
                
            }
            catch (DbUpdateException) {
                var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("There was an error adding the rating.")),
                    ReasonPhrase = "There was an error adding the rating."
                };
                throw new HttpResponseException(res);
            }
        }

        private bool validateUserToken(string UserName, string UserAccessToken) {
            var expirationDate = _ctx.UserAccessTokens.Single(x => (x.UserName == UserName && x.AccessToken == UserAccessToken)).ExpirationDate;
            return expirationDate >= DateTime.Now;
        }

        public IEnumerable<SelectListItem> GetMovieDropdown() {
            return _ctx.Movies.Select(x =>
                        new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = x.Title
                        });
        }

        private void UpdateAverageRating(int MovieId) {
            try
            {
                decimal AverageRating = _ctx.UserRatings.Where(x => x.MovieId == MovieId).Sum(x => x.Rating) / _ctx.UserRatings.Where(x => x.MovieId == MovieId).Count();
                var itemToUpdate = _ctx.Movies.Single(x => x.Id == MovieId);
                itemToUpdate.AverageRating = AverageRating;

                _ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }
            catch (InvalidOperationException) { }

        }

        public Review GetReviewById(int movieId)
        {
            try
            {
                return _ctx.Reviews.Single(x => x.MovieId == movieId);
            }
            catch (InvalidOperationException)
            {
                var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Movie Review not found.")),
                    ReasonPhrase = "Movie Review not found."
                };
                throw new HttpResponseException(res);
            }
        }
        public bool AddReview(Review review)
        {
            try
            {
                if (_ctx.Reviews.Where(x => x.MovieId == review.MovieId).Count() > 0)
                {
                    var reviewToUpdate = _ctx.Reviews.Single(x => x.MovieId == review.MovieId);
                    reviewToUpdate.ReviewContent = review.ReviewContent;
                }
                else
                _ctx.Reviews.Add(review);
                return _ctx.SaveChanges() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public DesiReviewRepository(DesiReviewContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Movie> GetMoviesByLanguage(string Language)
        {
            return Language.ToLower() != "all" ?_ctx.Movies.Where(x => x.MovieLanguage.ToLower() == Language.ToLower()).OrderByDescending(x => x.ReleaseDate)
                : _ctx.Movies.OrderByDescending(x => x.ReleaseDate);
        }
        public IQueryable<Movie> GetMovies(UserAccessToken user)
        {
            var result = _ctx.Movies.OrderByDescending(x => x.ReleaseDate).Take(10);
            
            if (user.UserName != null && user.UserName != "") {
                foreach (var item in result)
                {
                    int userId = _ctx.Users.Single(x => x.UserName == user.UserName).Id;
                    if (_ctx.UserRatings.Where(x => (x.UserId == userId && x.MovieId == item.Id)).Count() > 0) {
                        var userRating = _ctx.UserRatings.Single(x => (x.UserId == userId && x.MovieId == item.Id));
                        if (userRating != null)
                        {
                            item.UserRating = userRating.Rating;
                        }
                    }
                    
                }
            }
            return result;
        }

        public bool AddMovie(Movie movieToAdd)
        {
            try
            {
                _ctx.Movies.Add(movieToAdd);
                return _ctx.SaveChanges() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        } 

        public bool IsUsernameAvailable(string userName)
        {
            return !(_ctx.Users.Where(x => x.UserName == userName).Count() > 0);
        }

        public UserAccessToken ValidateExistingUser(User newUser)
        {
            try
            {
                bool IsUserValidated = false;

                User existingUser = _ctx.Users.Single(x => x.UserName == newUser.UserName);
                using (MD5 md5Hash = MD5.Create())
                {
                    IsUserValidated = HelperClass.VerifyMd5Hash(md5Hash, newUser.Password + existingUser.UserGuid, existingUser.Password);
                }

                if (IsUserValidated) {
                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    UserAccessToken newUserAccessToken = new UserAccessToken();
                    newUserAccessToken.UserName = newUser.UserName;
                    newUserAccessToken.AccessToken = token;
                    newUserAccessToken.ExpirationDate = DateTime.Now.AddYears(1);
                    _ctx.UserAccessTokens.Add(newUserAccessToken);
                    if (_ctx.SaveChanges() > 0)
                        return newUserAccessToken;
                    else
                    {
                        var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("There was an error.")),
                            ReasonPhrase = "There was an error"
                        };
                        throw new HttpResponseException(res);
                    }
                }
                else
                {
                    var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Invalid Username/Password Combination.")),
                        ReasonPhrase = "Invalid Username and Password"
                    };
                    throw new HttpResponseException(res);
                }
            }
            catch (InvalidOperationException)
            {
                var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("User not found.")),
                    ReasonPhrase = "User not found."
                };
                throw new HttpResponseException(res);
            }
        }

        public UserAccessToken RegisterNewUser(User newUser)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                newUser.UserGuid = Guid.NewGuid();
                newUser.Password = HelperClass.GetMd5Hash(md5Hash, newUser.Password + newUser.UserGuid.ToString());
            }

            try
            {
                _ctx.Users.Add(newUser);
                if (_ctx.SaveChanges() > 0) {
                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    UserAccessToken newUserAccessToken = new UserAccessToken();
                    newUserAccessToken.UserName = newUser.UserName;
                    newUserAccessToken.AccessToken = token;
                    newUserAccessToken.ExpirationDate = DateTime.Now.AddYears(1);
                    _ctx.UserAccessTokens.Add(newUserAccessToken);
                    if (_ctx.SaveChanges() > 0)
                        return newUserAccessToken;
                    else
                    {
                        var res = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("There was an error.")),
                            ReasonPhrase = "There was an error"
                        };
                        throw new HttpResponseException(res);
                    }
                }
                else
                    throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
            catch (DbUpdateException) { throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest); }
        }

        public bool UpdatePassword(PasswordUpdate newPassword) {
            try {
                var userRequest = _ctx.UserPasswordRequests.Single(x => x.PasswordToken == newPassword.Token);
                if(userRequest != null)
                {
                    if (DateTime.Compare(DateTime.Now, userRequest.ExpirationDate) <= 0) {
                        
                        var updateP = _ctx.Users.Single(x => x.UserName == userRequest.UserName);
                        if(updateP != null)
                        {
                            using (MD5 md5Hash = MD5.Create())
                            {
                                updateP.Password = HelperClass.GetMd5Hash(md5Hash, newPassword.Password + updateP.UserGuid.ToString());
                            }
                            userRequest.ExpirationDate = DateTime.Now.AddDays(-1);
                            if (_ctx.SaveChanges() > 0) {
                                return true;

                            }
                            else
                            {
                                var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                                {
                                    Content = new StringContent(string.Format("Something went wrong. DB.")),
                                    ReasonPhrase = "Something went wrong. DB."
                                };
                                throw new HttpResponseException(res);
                            }
                        }
                        else
                        {
                            var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                            {
                                Content = new StringContent(string.Format("Something went wrong. DB.")),
                                ReasonPhrase = "Something went wrong. DB."
                            };
                            throw new HttpResponseException(res);
                        }
                    }
                    else {
                        var res = new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                        {
                            Content = new StringContent(string.Format("Something went wrong. Timeout.")),
                            ReasonPhrase = "Something went wrong. Timeout."
                        };
                        throw new HttpResponseException(res);
                    }
                }
                else
                {
                    var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(string.Format("Something went wrong. Timeout.")),
                        ReasonPhrase = "Something went wrong. Timeout."
                    };
                    throw new HttpResponseException(res);
                }
            }
            catch (InvalidOperationException) {
                var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Something went wrong.")),
                    ReasonPhrase = "Something went wrong."
                };
                throw new HttpResponseException(res);
            }
            catch (DbUpdateException)
            {
                var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Something went wrong. DB.")),
                    ReasonPhrase = "Something went wrong. DB."
                };
                throw new HttpResponseException(res);
            }
        }

        public bool SendPasswordResetLink(User existinguser)
        {
            try {
                User checkUser = _ctx.Users.Single(x => x.Email == existinguser.Email);
                if (checkUser != null)
                {
                    byte[] token = new byte[10];
                    UserPasswordRequest req = new UserPasswordRequest();
                    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(token);
                        req.PasswordToken = HttpServerUtility.UrlTokenEncode(token);
                    };

                    req.UserName = checkUser.UserName;
                    req.ExpirationDate = DateTime.Now.AddMinutes(30);

                    _ctx.UserPasswordRequests.Add(req);
                    if (_ctx.SaveChanges() > 0)
                        return true;
                    else
                    {
                        var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(string.Format("Something went wrong.")),
                            ReasonPhrase = "Something went wrong."
                        };
                        throw new HttpResponseException(res);
                    }
                }
                else
                {
                    var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(string.Format("Something went wrong.")),
                        ReasonPhrase = "Something went wrong."
                    };
                    throw new HttpResponseException(res);
                }
            }
            catch (InvalidOperationException) {
                var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Something went wrong.")),
                    ReasonPhrase = "Something went wrong."
                };
                throw new HttpResponseException(res);
            }
            catch (DbUpdateException)
            {
                var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Something went wrong.")),
                    ReasonPhrase = "Something went wrong."
                };
                throw new HttpResponseException(res);
            }

        }
    }
}
