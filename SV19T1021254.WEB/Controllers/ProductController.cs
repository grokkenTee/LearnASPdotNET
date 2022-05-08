using SV19T1021254.BussinessLayer;
using SV19T1021254.DomainModel;
using SV19T1021254.Web.Models;
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
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? categoryID, int? supplierID)
        {
            Models.ProductPaginationSearchResult model = Session["PRODUCT_SEARCH"] as Models.ProductPaginationSearchResult;
            if (model == null)
            {
                model = new Models.ProductPaginationSearchResult()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = "",
                    CategoryID = categoryID ?? 0,
                    SupplierID = supplierID ?? 0
                };
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.ProductPaginationSearchResult input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfProducts(input.Page, input.PageSize, input.SearchValue, input.CategoryID, input.SupplierID, out rowCount);
            Models.ProductPaginationResult model = new Models.ProductPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                CategoryID = input.CategoryID,
                SupplierID = input.SupplierID,
                RowCount = rowCount,
                Data = data
            };
            Session["PRODUCT_SEARCH"] = input;
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Product model = new Product()
            {
                ProductID = 0
            };

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID)
        {
            Product product = CommonDataService.GetProduct(productID);
            if (product == null)
                return RedirectToAction("Index");
            ProductDetailResult model = new ProductDetailResult()
            {
                ProductObj = product,
                ListPhoto = CommonDataService.ListOfProductPhotos(productID),
                ListAttribute = CommonDataService.ListOfProductAttributes(productID)
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Tên mặt hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");
            //if (string.IsNullOrWhiteSpace(model.Price))
            //    ModelState.AddModelError("Price", "Giá không được để trống");
            //if (string.IsNullOrWhiteSpace(model.))
            //    model.Notes = "";

            if (!ModelState.IsValid)
            {
                if (model.ProductID == 0)
                    return View("Create", model);
                else
                    return View("Edit", model);
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/images/products");
                string fileName = $"{DateTime.Now.Ticks}-{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = fileName;
            }

            if (model.ProductID == 0)
            {
                CommonDataService.AddProduct(model);
                Session["PRODUCT_SEARCH"] = new Models.ProductPaginationSearchResult()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = model.ProductName,
                    CategoryID = model.CategoryID,
                    SupplierID = model.SupplierID
                };
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateProduct(model);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DelteProduct(productID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetProduct(productID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, int productID, int? photoID, ProductPhoto data)
        {
            ProductPhoto model = new ProductPhoto() { PhotoID = 0 };
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    break;

                case "edit":
                    model = CommonDataService.GetProductPhoto(photoID ?? 0);
                    if (model == null)
                        return RedirectToAction("Edit", new { productID = productID });
                    ViewBag.Title = "Thay đổi ảnh";
                    break;

                case "save":
                    if (Request.HttpMethod == "POST")
                    {
                        if (data.PhotoID == 0)
                            CommonDataService.AddProductPhoto(model);
                        else
                            CommonDataService.UpdateProductPhoto(model);
                        return RedirectToAction("Edit", new { productID = productID });
                    }
                    break;

                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });

                default:
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    break;
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}