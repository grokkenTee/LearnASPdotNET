using SV19T1021254.BussinessLayer;
using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1021254.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(page, pageSize, searchValue, out rowCount);
            Models.CategoryPaginationResult model = new Models.CategoryPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Category model = new Category()
            {
                CategoryId = 0
            };

            ViewBag.Title = "Bổ sung loại hàng";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [Route("edit/{categoryID}")]
        public ActionResult Edit(int categoryID)
        {
            Category model = CommonDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin loại hàng";
            return View("Create");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [Route("delete/{categoryID}")]
        public ActionResult Delete(int categoryID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCategory(categoryID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}