using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1021254.DataLayer;
using SV19T1021254.DomainModel;
using System.Configuration;

namespace SV19T1021254.BussinessLayer
{
    /// <summary>
    /// Cung cấp các chức năng xử lí dữ liệu chung (từ điển dữ liệu)
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICategoryDAL categoryDB;
        private static readonly ICustomerDAL customerDB;
        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                    break;
                //tình huống nhiều loại DB -> thêm các case
                //TODO: xoá FakeDB, bổ sung báo lỗi cho trường hợp default?
                default:
                    categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }
        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategories()
        {
            return categoryDB.List().ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount) 
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
    }
}
