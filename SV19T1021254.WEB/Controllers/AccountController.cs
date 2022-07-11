using SV19T1021254.BussinessLayer;
using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SVT19T1021254.WEB.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View(new Account());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login(Account model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email hiện trống");
            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "Password hiện trống");
            if (ModelState.IsValid)
            {
                switch (AccountService.Login(model))
                {
                    case AccountService.StatusCodes.Success:
                        System.Web.Security.FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("Index", "Home");

                    case AccountService.StatusCodes.WrongEmail:
                        ModelState.AddModelError("Email", "Email không tồn tại");
                        break;

                    case AccountService.StatusCodes.WrongPassword:
                        ModelState.AddModelError("Password", "Password không đúng");
                        break;

                    case AccountService.StatusCodes.Undefined:
                        ModelState.AddModelError("Failed", "Lỗi không xác định");
                        break;

                    default:
                        break;
                }
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("changepassword")]
        public ActionResult ChangePassword(string oldPassword = "", string newPassword = "")
        {
            switch (AccountService.ChangePassword(User.Identity.Name, oldPassword, newPassword))
            {
                case AccountService.StatusCodes.WrongEmail:
                    return RedirectToAction("Index");

                case AccountService.StatusCodes.WrongPassword:
                    ModelState.AddModelError("Failed", "Sai mật khẩu cũ");
                    break;

                case AccountService.StatusCodes.Success:
                    ModelState.AddModelError("Success", "Thay đổi mật khẩu thành công");
                    break;
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("changepassword")]
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}