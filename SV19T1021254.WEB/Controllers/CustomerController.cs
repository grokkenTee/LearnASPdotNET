using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1021254.WEB.Controllers
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
        public ActionResult Index()
        {
            return View();
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