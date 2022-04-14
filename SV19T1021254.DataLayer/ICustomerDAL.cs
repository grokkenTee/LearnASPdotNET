using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1021254.DomainModel;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu liên quan đến khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Tim kiếm, hiển thị danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Danh sách khách hàng</returns>
        IList<Customer> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số khách hàng thoả điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Sô khách hàng</returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của 1 khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns>Đối tượng Customer</returns>
        Customer Get(int customerID);
        /// <summary>
        /// Bổ sung 1 khách hàng. Hàm trả về mã khách hàng bổ sung được
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);
        /// <summary>
        /// Cập nhật thông tin của một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// Xoá 1 khách hàng dựa vào mã của khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool Delete(int customerID);
        /// <summary>
        /// Kiểm tra xem thử 1 khách hàng hiện có dữ liệu nào liên quan không
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool InUsed(int customerID);
    }
}
