using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV19T1021254.BussinessLayer;

namespace SV19T1021254.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Tìm kiếm, hiển thị
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var model = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);
            int pageCount = rowCount / pageSize + (rowCount % pageSize > 0 ? 1 : 0);
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;
            ViewBag.CurrentPage = page;
            return View(model);
        }
        /// <summary>
        /// Giao diện bổ sung khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            return View();
        }
        /// <summary>
        /// Giao diện chỉnh sửa khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.Title = "Thay đổi thông tin khách hàng";
            return View("Create");
        }
        /// <summary>
        /// Xác nhận xoá 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return View();
        }
    }
}