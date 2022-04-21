using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu liên quan đến nhân viên.
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Tim kiếm, hiển thị danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Danh sách nhân viên</returns>
        IList<Employee> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số nhân viên thoả điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Sô lượng nhân viên</returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        Employee Get(int employeeID);
        /// <summary>
        /// Bổ sung thêm 1 nhân viên mới. Hàm trả về mã của nhân viên được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// Cập nhật thông tin của nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// Xoá một nhân viên dựa vào mã nhân viên
        /// Không được xoá nhân viên mà đã có đơn hàng liên quan
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool Delete(int employeeID);
        /// <summary>
        /// Kiểm tra xem thử 1 nhân viên hiện có dữ liệu nào liên quan không (đơn hàng)
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool InUsed(int employeeID);
    }
}
