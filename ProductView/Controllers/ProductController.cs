using ProductView.Models;
using ProductView.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductView.Controllers
{
    public class ProductController : Controller
    {
        [NonAction]
        public List<CategoryModel> CategoryList()
        {
            ProductDbContext productDbContext = new ProductDbContext();
            List<CategoryModel> cmodel = productDbContext.categoryModels.ToList();
            ViewBag.CategoryList = cmodel;
            return cmodel;
        }
        [NonAction]
        public List<ListProducts> FetchAllProducts()
        {
            ProductDbContext productDbContext = new ProductDbContext();
            List<Products> prod = productDbContext.ProductsLists.ToList();
            List<CategoryModel> cmodel = CategoryList();
            List<ListProducts> Productdetails = prod.Join(cmodel, pr => pr.CategoryId, cm => cm.Id, (pr, cm) => new ListProducts
            {
                ProductName = pr.ProductName,
                ProductId = pr.ProductId,
                CategoryName = cm.CategoryName,
                CategoryId = cm.Id
            }).ToList();
            return Productdetails;
        }

        // GET: Product
        [HttpGet]
        public ActionResult ViewProducts()
        {
            List<ListProducts> Productdetails = FetchAllProducts();
            return View(Productdetails);
        }
        [HttpGet]
        public ActionResult CreateNewProduct()
        {
            CategoryList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewProduct(Products products)
        {
            ProductDbContext dbContext = new ProductDbContext();
            dbContext.ProductsLists.Add(products);
            dbContext.SaveChanges();
            return RedirectToAction("ViewProducts");
        }
        [HttpGet]
        public ActionResult UpdateProduct(int productID)
        {
            CategoryList();
            ListProducts Productdetails = FetchAllProducts().Where(e => e.ProductId == productID).FirstOrDefault();
            return View(Productdetails);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Products products)
        {
            ProductDbContext dbContext = new ProductDbContext();
            Products product = new Products();
            Products Productdetails = (from s in dbContext.ProductsLists where s.ProductId == products.ProductId select s).FirstOrDefault();
            Productdetails.ProductName = products.ProductName;
            Productdetails.CategoryId = products.CategoryId;
            dbContext.SaveChanges();
            return RedirectToAction("ViewProducts");
        }

        public ActionResult DeleteProduct(int productId)
        {
            ProductDbContext dbContext = new ProductDbContext();
            Products prod = dbContext.ProductsLists.ToList().Where(a => a.ProductId == productId).FirstOrDefault();
            dbContext.ProductsLists.Remove(prod);
            dbContext.SaveChanges();
            return RedirectToAction("ViewProducts");
        }
    }
}