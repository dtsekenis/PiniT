using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiniT.Models;

namespace PiniT.Managers
{
    public class ImageManager
    {
        public ICollection<Image> GetRestaurantsImages(string restId)
        {
            ICollection<Image> images;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                images = db.Images.Include("Restaurant")
                                  .Where(x => x.RestaurantId == restId)
                                  .ToList();
            }
            return images;
        }
        public Image GetImage(int id)
        {
            Image img;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                img = db.Images.Find(id);
            }
            return img;
        }
        public bool SaveImage(Image img)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (img != null)
                {
                    db.Images.Add(img);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public bool SetProfileImage(int id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Image img = db.Images.Find(id);
                if (img !=null)
                {
                    var restImages = db.Images.Where(x => x.RestaurantId == img.RestaurantId);
                    if (restImages != null)
                    {
                        foreach (Image image in restImages)
                        {
                            image.isProfileImage = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                    img.isProfileImage = true;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}