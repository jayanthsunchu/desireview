using desireview.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace desireview.Controllers
{
    public class UsersController : ApiController
    {
        private IDesiReviewRepository _repo;
        public UsersController(IDesiReviewRepository repo)
        {
            _repo = repo;
        }
        public class IsUser {
            public IsUser(bool _flag) {
                this.flag = _flag;
            }
            public bool flag { get; set; }
        }
        [HttpGet]
        public IsUser IsUsernameAvailable(string id)
        {
            return new IsUser(_repo.IsUsernameAvailable(id));
        }

        [HttpPost]
        public UserAccessToken RegisterNewUser(User newUser)
        {
            return _repo.RegisterNewUser(newUser);
        }

        [HttpPost]
        public UserAccessToken ValidateExistingUser(User existingUser)
        {
            return _repo.ValidateExistingUser(existingUser);
        }

        [HttpPost]
        public bool SendPasswordResetLink(User existingUser)
        {
            return _repo.SendPasswordResetLink(existingUser);
        }

        [HttpPost]
        public bool UpdatePassword(PasswordUpdate passwordUpdate)
        {
            return _repo.UpdatePassword(passwordUpdate);
        }
    }
}
