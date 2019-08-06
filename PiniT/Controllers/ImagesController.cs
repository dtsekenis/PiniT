using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;

namespace PiniT.Controllers
{
    [Authorize(Roles ="Manager")]
    public class ImagesController : Controller
    {
        private ImageManager imgDb = new ImageManager();

        [HttpGet]
        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(Image img)
        {
            string fileName = Path.GetFileNameWithoutExtension(img.ImageFile.FileName);
            string extension = Path.GetExtension(img.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            img.ImagePath = "~/Content/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
            img.ImageFile.SaveAs(fileName);
            img.RestaurantId = User.Identity.GetUserId();
            imgDb.SaveImage(img);

            return View();
        }

        public ActionResult SetProfileImage()
        {
            var images = imgDb.GetRestaurantsImages(User.Identity.GetUserId());
            return View(images);
        }

        [HttpPost]
        public JsonResult SetProfileImage(int id)
        {
            bool result = imgDb.SetProfileImage(id);
            return Json(result);
        }
    }
}