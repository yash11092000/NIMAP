using Microsoft.AspNetCore.Mvc;
using NIMAP.Models;
using NIMAP.Services;
using System.Web.Helpers;


namespace NIMAP.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IProductServices _productServices;
        private readonly IViewHelper _iviewhelper;


        public HomeController(IProductServices productServices, IViewHelper iviewhelper)
        {

            _productServices = productServices;
            _iviewhelper = iviewhelper;
        }


        #region Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GeAllProducts(int current_page = 1, int pageSize = 5)
        {
            var ds = _productServices.GetAllProducts(current_page, pageSize);
            string result = await _iviewhelper.RenderPartialViewToString(this, "_ProductList", ds);
            int TotalRec = (int)ds.Tables[1].Rows[0]["TotalRecords"];
            return Json(new { data = result, TotalRecords = TotalRec });
        }


        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            int response = await _productServices.AddProduct(product);
            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(int NewCategoryId, string CategoryName)
        {
            int response = await _productServices.AddCategory(NewCategoryId, CategoryName);
            return Json(response);
        }

        [HttpGet]

        public async Task<ActionResult> GetCategoryDropDown()
        {
            var response = await _productServices.GetCategoryDropDown();
            return Json(response);
        }


        [HttpGet]

        public async Task<ActionResult> GetProductDetails(int ProductId)
        {
            var response = await _productServices.GetProductDetails(ProductId);
            return Json(response);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteProducts(int ProductId)
        {
            var response = await _productServices.DeleteProducts(ProductId);
            return Json(response);
        }






        #endregion


        #region Category

        [HttpGet]
        public async Task<ActionResult> GetCategoryList()
        {
            var ds = _productServices.GetCategoryList();
            string result = await _iviewhelper.RenderPartialViewToString(this, "_CategoryList", ds);
            return Json(result);
        }

        [HttpGet]

        public async Task<ActionResult> EditCategory(int CategoryId)
        {
            var response = await _productServices.EditCategory(CategoryId);
            return Json(response);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(int CategoryId)
        {
            var response = await _productServices.DeleteCategory(CategoryId);
            return Json(response);
        }


        public async Task<ActionResult> CheckCategoryUsed(int CategoryId)
        {
            var response = await _productServices.CheckCategoryUsed(CategoryId);
            return Json(response);
        }


        #endregion







    }
}
