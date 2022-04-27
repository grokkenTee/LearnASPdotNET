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
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
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
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [Route("edit/{employeeID}")]
        public ActionResult Edit(int employeeID)
        {
            Employee model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin nhân viên";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee model, string dateOfBirth)
        {
            ////TODO: xử lí đầu vào
            //DateTime birthday = DateTime.ParseExact(birthateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //model.BirthDate = birthday;
            ////TODO: xử lí ảnh
            //if (uploadPhoto != null)
            //{
            //    string path = Server.MapPath("~/images/employees");
            //    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
            //    string uploadFilePath = System.IO.Path.Combine(path+fileName);
            //    uploadPhoto.SaveAs(uploadFilePath);
            //    model.Photo = $"/images/employees/{fileName}";
            //}
            try
            {
                model.BirthDate = DateTime.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                ModelState.AddModelError("BirthDate", "Ngày sinh không hợp lệ");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Create", model);
            }

            return Json(string.Format("{0:dd/MM/yyyy}", model.BirthDate));



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
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