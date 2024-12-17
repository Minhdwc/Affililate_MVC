using Affililate_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace Affililate_Web.Controllers
{
    public class ProductController : Controller
    {
        private db_affiliate_webEntities db = new db_affiliate_webEntities();
        public ActionResult Index(int? page, int? pageSize)
        {
            var topBuy = db.Products.Select(p => new
            {
                p.id,
                p.product_name,
                p.description,
                p.image_product,
                p.price,
                totalBuy = p.Product_Click.Count(pc => pc.event_type == "buy")
            }).OrderByDescending(x => x.totalBuy).Take(4).ToList().Select(p => new Product
            {
                id = p.id,
                product_name = p.product_name,
                description = p.description,
                image_product = p.image_product,
                price = p.price,
            }).ToList();
            List<Category> lstCate = db.Categories.ToList();
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 8;
            }
            var lstProduct = db.Products.OrderBy(p => p.id).ToPagedList(page.Value, pageSize.Value);
            ViewBag.lstCate = lstCate;
            ViewBag.topBuy = topBuy;
            return View(lstProduct);
        }
        public ActionResult detail(int id)
        {
            var product = db.Products.Where(p => p.id == id).FirstOrDefault();
            var countClick = db.Product_Click.Where(p => p.id_product == id && p.event_type == "click").Count();
            var countBuy = db.Product_Click.Where(p => p.id_product == id && p.event_type == "buy").Count();
            var randomProduct = db.Products.Where(p => p.id != id).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            ViewBag.counClick = countClick;
            ViewBag.countBuy = countBuy;
            ViewBag.randomProduct = randomProduct;
            return View(product);
        }
        [HttpPost]
        public ActionResult Click(int productId, string type)
        {
            try
            {
                string userIp = Request.UserHostAddress;
                var NewClick = new Product_Click
                {
                    id_product = productId,
                    click_time = DateTime.Now,
                    event_type = type,
                    user_ip = userIp
                };
                db.Product_Click.Add(NewClick);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}