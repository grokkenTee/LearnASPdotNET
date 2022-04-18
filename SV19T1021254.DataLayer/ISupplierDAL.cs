using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Giao diện, định nghĩa các phép xử lý dữ liệu liên quan đến nhà cung cấp
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Tim kiếm, hiển thị danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Danh sách nhà cung cấp</returns>
        IList<Supplier> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số nhà cung cấp thoả điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Sô lượng nhà cung cấp</returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của nhà cung cấp theo mã nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Bổ sung thêm 1 nhà cung cấp mới. Hàm trả về mã của nhà cung cấp được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);
        /// <summary>
        /// Cập nhật thông tin của nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// Xoá một nhà cung cấp dựa vào mã nhà cung cấp
        /// Không được xoá nhà cung cấp mà đã có mặt hàng liên quan
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool Delete(int supplierID);
    }
}
