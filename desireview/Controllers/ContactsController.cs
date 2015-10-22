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
    public class ContactsController : ApiController
    {
        private IDesiReviewRepository _repo;
        public ContactsController(IDesiReviewRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Contact> GetInfo() {
            return _repo.GetContacts();
        }

        [HttpPost]
        public void SubmitContact(Contact c) {
            _repo.SubmitContact(c);
        }
    }
}
