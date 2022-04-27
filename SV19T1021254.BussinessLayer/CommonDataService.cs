using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1021254.DataLayer;
using SV19T1021254.DomainModel;
using System.Configuration;
using SV19T1021254.DataLayer.SQLServer;

namespace SV19T1021254.BussinessLayer
{
    /// <summary>
    /// Cung cấp các chức năng xử lí dữ liệu chung (từ điển dữ liệu)
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Country> countryDB;

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
                    supplierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                    shipperDB = new DataLayer.SQLServer.ShipperDAL(connectionString);
                    employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                    break;
                //tình huống nhiều loại DB -> thêm các case
                //TODO: xoá FakeDB, bổ sung báo lỗi cho trường hợp default?
                default:
                    //categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }

        /// <summary>
        /// Danh sách toàn bộ loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategory()
        {
            return categoryDB.List().ToList();
        }
        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// Thêm một mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin một loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        /// <summary>
        /// Kiểm tra xem loại hàng có mặt hàng nào không
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        /// <summary>
        /// Xoá một loại hàng khi nó không tồn tại một mặt hàng nào
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }

        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries(){
            return countryDB.List().ToList();
        }

        /// <summary>
        /// Danh sách toàn bộ khách hàng
        /// </summary>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers()
        {
            return customerDB.List().ToList();
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
        /// <summary>
        /// Lấy thông tin 1 khách hàng từ CSDL
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Thêm một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Kiểm tra xem khách hàng có đơn hàng không
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        /// <summary>
        /// Xoá khách hàng, nếu như đã tồn tại đơn hàng thì không cho phép xoá.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DelteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }

        /// <summary>
        /// Danh sách toàn bộ nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers()
        {
            return supplierDB.List().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)

        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 nhà cung cấp từ CSDL
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        /// <summary>
        /// Thêm một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        /// Kiểm tra xem nhà cung cấp có mặt hàng không
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        /// <summary>
        /// Xoá nhà cung cấp, nếu như đã tồn tại mặt hàng thì không cho phép xoá.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DelteSupplier(int supplierID)
        {
            if (supplierDB.InUsed(supplierID))
                return false;
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// Danh sách toàn bộ người giao hàng
        /// </summary>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers()
        {
            return shipperDB.List().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 người giao hàng từ CSDL
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        /// <summary>
        /// Thêm một người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        /// <summary>
        /// Kiểm tra xem người giao hàng có đơn hàng không
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        /// <summary>
        /// Xoá người giao hàng, nếu như đã tồn tại đơn hàng thì không cho phép xoá.
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DelteShipper(int shipperID)
        {
            if (shipperDB.InUsed(shipperID))
                return false;
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// Danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees()
        {
            return employeeDB.List().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 nhân viên từ CSDL
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        /// <summary>
        /// Thêm một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        /// <summary>
        /// Kiểm tra xem nhân viên có đơn hàng không
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        /// <summary>
        /// Xoá nhân viên, nếu như đã tồn tại đơn hàng thì không cho phép xoá.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DelteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }
    }
}
