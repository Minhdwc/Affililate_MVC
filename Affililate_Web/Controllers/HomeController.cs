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
            var topBuy = db.Products.Select(p => new
                {
                    p.id,
                    p.product_name,
                    p.description,
                    p.image_product,
                    totalBuy = p.Product_Click.Count(pc => pc.event_type == "buy")
                })
                .OrderByDescending(x => x.totalBuy)
                .Take(8)
                .ToList()
                .Select(p => new Product
                {
                    id = p.id,
                    product_name = p.product_name,
                    description = p.description,
                    image_product = p.image_product
                })
                .ToList();
            var topView = db.Products.Select(p => new
            {
                p.id,
                p.product_name,
                p.description,
                p.image_product,
                totalBuy = p.Product_Click.Count(pc => pc.event_type == "click")
            })
                .OrderByDescending(x => x.totalBuy)
                .Take(4)
                .ToList()
                .Select(p => new Product
                {
                    id = p.id,
                    product_name = p.product_name,
                    description = p.description,
                    image_product = p.image_product
                })
                .ToList(); ;
            ViewBag.topBuy = topBuy;
            ViewBag.topView = topView;
            return View();
        }
    }
}