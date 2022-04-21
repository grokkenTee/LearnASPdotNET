using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Giao diện, định nghĩa các phép xử lý dữ liệu liên quan đến người giao hàng
    /// </summary>
    public interface IShipperDAL
    {
        /// <summary>
        /// Tim kiếm, hiển thị danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Danh sách người giao hàng</returns>
        IList<Shipper> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số người giao hàng thoả điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Sô lượng người giao hàng</returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của người giao hàng theo mã người giao hàng
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        Shipper Get(int shipperID);
        /// <summary>
        /// Bổ sung thêm 1 người giao hàng mới. Hàm trả về mã của người giao hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);
        /// <summary>
        /// Cập nhật thông tin của người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);
        /// <summary>
        /// Xoá một người giao hàng dựa vào mã người giao hàng
        /// Không được xoá người giao hàng mà đã có đơn hàng liên quan
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        bool Delete(int shipperID);
        /// <summary>
        /// Kiểm tra xem thử 1 người giao hàng hiện có dữ liệu nào liên quan không (đơn hàng)
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        bool InUsed(int shipperID);
    }
}
