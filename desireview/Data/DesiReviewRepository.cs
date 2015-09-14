﻿using desireview.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace desireview.Data
{
    class DesiReviewRepository : IDesiReviewRepository
    {
        DesiReviewContext _ctx;
        public DesiReviewRepository(DesiReviewContext ctx) {
            _ctx = ctx;
        }
        public IQueryable<Movie> GetMovies()
        {
            return _ctx.Movies.OrderByDescending(x => x.ReleaseDate);
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

        public User ValidateExistingUser(User newUser)
        {
            try
            {
                bool IsUserValidated = false;

                User existingUser = _ctx.Users.Where(x => x.UserName == newUser.UserName).Single();
                using (MD5 md5Hash = MD5.Create())
                {
                    IsUserValidated = HelperClass.VerifyMd5Hash(md5Hash, newUser.Password + existingUser.UserGuid, existingUser.Password);
                }

                if (IsUserValidated)
                    return existingUser;
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

        public User RegisterNewUser(User newUser)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                newUser.UserGuid = Guid.NewGuid();
                newUser.Password = HelperClass.GetMd5Hash(md5Hash, newUser.Password + newUser.UserGuid.ToString());
            }

            try
            {
                _ctx.Users.Add(newUser);
                if (_ctx.SaveChanges() > 0)
                    return newUser;
                else
                    throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
            catch (DbUpdateException) { throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest); }
        }

        public bool UpdatePassword(PasswordUpdate newPassword) {
            try {
                UserPasswordRequest userRequest = _ctx.UserPasswordRequests.Where(x => x.PasswordToken == newPassword.Token).Single();
                if(userRequest != null)
                {
                    if (DateTime.Compare(DateTime.Now, userRequest.ExpirationDate) > 0) {
                        var updateP = _ctx.Users.SingleOrDefault(x => x.UserName == userRequest.UserName);
                        if(updateP != null)
                        {
                            using (MD5 md5Hash = MD5.Create())
                            {
                                updateP.Password = HelperClass.GetMd5Hash(md5Hash, newPassword.Password + updateP.UserGuid.ToString());
                            }

                            if (_ctx.SaveChanges() > 0)
                                return true;
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
                        var res = new HttpResponseMessage(HttpStatusCode.BadRequest)
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
                User checkUser = _ctx.Users.Where(x => x.Email == existinguser.Email).Single();
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
