using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV19T1021254.BussinessLayer;
using SV19T1021254.DomainModel;

namespace SV19T1021254.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("customer")]
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
            var data = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);
            Models.CustomerPaginationResult model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
            };
            return View(model);
            //int pageSize = 10;
            //int rowCount = 0;
            //var model = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);
            //int pageCount = rowCount / pageSize + (rowCount % pageSize > 0 ? 1 : 0);
            //ViewBag.RowCount = rowCount;
            //ViewBag.PageCount = pageCount;
            //ViewBag.SearchValue = searchValue;
            //ViewBag.CurrentPage = page;
            //return View(model);
        }
        /// <summary>
        /// Giao diện bổ sung khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Customer model = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Bổ sung khách hàng";
            return View(model);
        }
        /// <summary>
        /// Giao diện chỉnh sửa khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{customerID}")]
        public ActionResult Edit(int customerID)
        {
            Customer model = CommonDataService.GetCustomer(customerID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin khách hàng";
            return View("Create", model);
        }
        /// <summary>
        /// Lưu thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Customer model)
        {
            //kiểm tra dữ liệu đầu vào k dc null
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                ModelState.AddModelError("CustomerName", "Tên khách hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Phải chọn quốc gia");
            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CustomerID == 0 ? "Bổ sung khách hàng" : "Thay đổi thông tin";
                return View("Create", model);
            }

            //lưu dữ liệu
            if (model.CustomerID == 0)
            {
                CommonDataService.AddCustomer(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateCustomer(model);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xác nhận xoá 
        /// </summary>
        /// <returns></returns>
        [Route("delete/{customerID}")]
        public ActionResult Delete(int customerID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DelteCustomer(customerID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(customerID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}