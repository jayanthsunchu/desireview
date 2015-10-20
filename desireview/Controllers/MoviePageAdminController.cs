using desireview.Data;
using desireview.Helpers;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace desireview.Controllers
{
    public class MoviePageAdminController : Controller
    {
        public IDesiReviewRepository _repo;

        public MoviePageAdminController(IDesiReviewRepository repo) {
            this._repo = repo;
        }
        // GET: MoviePageAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotAuthorized() {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Movie movieToAdd, HttpPostedFileBase file) {
            try
            {
                //var fileName = Path.GetFileName(file.FileName);
                //var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                //file.SaveAs(path);
                //movieToAdd.ImageName = file.FileName;
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("fsrimages");
                container.CreateIfNotExists();
                container.SetPermissions(
                new BlobContainerPermissions
                {
                PublicAccess =
                BlobContainerPublicAccessType.Blob
                });
                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);

                // Create or overwrite the "myblob" blob with contents from a local file.
                using (var fileStream = file.InputStream)
                {
                    blockBlob.UploadFromStream(fileStream);
                }
                movieToAdd.ImageName = blockBlob.Uri.AbsoluteUri;
                _repo.AddMovie(movieToAdd);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { Error = ex.Message });
            }
        }
    }
}