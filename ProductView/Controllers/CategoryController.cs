using ProductView.Models;
using ProductView.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProductView.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult GetAllCategories()
        {
            ProductDbContext productDbContext = new ProductDbContext();
            List<CategoryModel> cmodel  = productDbContext.categoryModels.ToList();
            return View(cmodel);
        }
        [HttpGet]
        public ActionResult AddNewCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewCategory(CategoryModel category)
        {
            ProductDbContext productDbContext = new ProductDbContext();
            productDbContext.categoryModels.Add(category);
            productDbContext.SaveChanges();
            return RedirectToAction("GetAllCategories");
        }

        public ActionResult UpdateCategory(int categoyId)
        {
            CategoryModel model = new CategoryModel();
            ProductDbContext productDbContext = new ProductDbContext();
            CategoryModel result = (from s in productDbContext.categoryModels where s.Id == categoyId select s).FirstOrDefault();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory(CategoryModel category)
        {
            try
            {
                ProductDbContext productDbContext = new ProductDbContext();
                CategoryModel result = (from s in productDbContext.categoryModels where s.Id == category.Id select s).FirstOrDefault();
                result.CategoryName = category.CategoryName;
                productDbContext.SaveChanges();
                return RedirectToAction("GetAllCategories");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteCategory(int categoyId)
        {
            ProductDbContext productDbContext = new ProductDbContext();
            CategoryModel result = (from s in productDbContext.categoryModels where s.Id == categoyId select s).FirstOrDefault();
            productDbContext.categoryModels.Remove(result);
            productDbContext.SaveChanges();
            return RedirectToAction("GetAllCategories");
        }
    }
}