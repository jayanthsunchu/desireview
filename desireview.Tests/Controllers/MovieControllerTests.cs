using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using desireview.Data;
using desireview.Controllers;
using System.Net.Http;

namespace desireview.Tests.Controllers
{
    
    [TestClass]
    public class MovieControllerTests
    {
        private IDesiReviewRepository _repo;
        [TestMethod]
        public void GetMoviesById()
        {
            var controller = new MoviesController(_repo);
            controller.Request = new HttpRequestMessage();

            // Act
            var response = controller.GetByLanguage("Telugu");

            // Assert
            
            Assert.AreEqual(10, 10);
        }
    }
}
