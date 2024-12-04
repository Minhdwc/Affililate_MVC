using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Affililate_Web.Models;

namespace Affililate_Web.Controllers
{
    public class HomeController : Controller
    {
        private db_affiliate_webEntities db = new db_affiliate_webEntities();
        public ActionResult Index()
        {
            var lstProduct = db.Products.ToList();
            return View(lstProduct);
        }
    }
}