using SV19T1021254.BussinessLayer;
using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV19T1021254.Web.Controllers
{
    // TODO: Chưa code, còn chỗ photo path chưa biết làm
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);
            Models.EmployeePaginationResult model = new Models.EmployeePaginationResult()
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
            Employee model = new Employee()
            {
                EmployeeID = 0
            };

            ViewBag.Title = "Bổ sung nhân viên";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("edit/{employeeID}")]
        public ActionResult Edit(int employeeID)
        {
            Employee model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin nhân viên";
            return View("Create");
        }
        /// <summary>
        /// Lưu thông tin nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //TODO: sử dụng cấu trúc model thì tự động ráp tham số vào
        public ActionResult Save(Employee model, string birthateString, HttpPostedFileBase uploadPhoto)
        {
            //TODO: xử lí đầu vào
            DateTime birthday = DateTime.ParseExact(birthateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            model.BirthDate = birthday;
            //TODO: xử lí ảnh
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/images/employees");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string uploadFilePath = System.IO.Path.Combine(path+fileName);
                uploadPhoto.SaveAs(uploadFilePath);
                model.Photo = $"/images/employees/{fileName}";
            }

            return Json(model);

            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int employeeID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DelteCustomer(employeeID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(employeeID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}