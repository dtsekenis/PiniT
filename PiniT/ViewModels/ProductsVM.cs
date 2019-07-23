using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiniT.ViewModels
{
   
    public class IndexProductsVM
    {
        public IEnumerable<Product> Products { get; set; }
        public string Category { get; set; }
        public string Search { get; set; }

        public Product Product { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
        
    }

    public class CreateProductsVM
    {
        public Product product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}