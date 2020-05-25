using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vkrS.Models;
using System.Data.Entity;

namespace vkrS.Controllers
{
    public class ImageController : Controller
    {
        private VKRDbContext db = new VKRDbContext();

        // GET: Image
        public ActionResult GetImages()
        {
            var images = db.Images;
            ViewBag.Images = images;
            return View();
        }

        [HttpPost]
        public void AddImage(string imageInput, string description)
        {
            if (imageInput != null)
            {
                db.Images.Add(new Image { ImageId = Guid.NewGuid(), Link = imageInput });
                db.SaveChanges();
            }
        }
    }
}