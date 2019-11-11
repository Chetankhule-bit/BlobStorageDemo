using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobStorageDemo.Controllers
{
    public class ImageController : Controller
    {
        ImageService imageService = new ImageService();

        // GET: Image
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo)
        {
            var imageUrl = await imageService.UploadImageAsync(photo);
            TempData["LatestImage"] = imageUrl.ToString();
            return RedirectToAction("LatestImage");
        }

        public ActionResult LatestImage()
        {
            var latestImage = string.Empty;
            if (TempData["LatestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
            }

            return View();
        }

        public  string GetExcelFile()
        {

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=ckstorage271289;AccountKey=yKkX79VhvG3prDB/9iuHQ3zcfzq3vqs+Z2e8v5y1A1MH06K0AJXn1NJ5hBnTWSOm6OOfQpoOSH/Ssagyack72Q==;EndpointSuffix=core.windows.net");
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference("democontainerblockblob");
            //return container.GetBlobReference("HelloWorld.png");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=ckstorage271289;AccountKey=yKkX79VhvG3prDB/9iuHQ3zcfzq3vqs+Z2e8v5y1A1MH06K0AJXn1NJ5hBnTWSOm6OOfQpoOSH/Ssagyack72Q==;EndpointSuffix=core.windows.net");
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("democontainerblockblob");
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference("HelloWorld.png");
   
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\P10493178\Downloads\Test\Blobtest.PNG"))            {
                blob.DownloadToStream(fileStream);
            }
            return "success";
        }
    }
}
