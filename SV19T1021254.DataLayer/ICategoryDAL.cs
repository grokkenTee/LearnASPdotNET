using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1021254.DomainModel;

//TODO: làm list() chưa chuẩn, thêm count()
namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Giao diện, định nghĩa các phép xử lý dữ liệu liên quan đến loại hàng
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Tìm kiếm, hiển thị danh sách loại hàng dưới dạng phân trang.
        /// </summary>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Số dòng mỗi trang</param>
        /// <param name="searchValue">Tên loại hàng cần tìm (tương đối). Chuối rỗng nếu lấy toàn bộ.</param>
        /// <returns></returns>
        IList<Category> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số loại hàng dựa theo kết quả tìm kiếm
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        int Count(string categoryID);
        /// <summary>
        /// Lấy thông tin 1 loại hàng theo mã loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// Bổ sung thêm 1 loại hàng mới. Hàm trả về mã của loại hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// Cập nhật thông tin của loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// Xoá một loại hàng dựa vào mã loại hàng
        /// Không được xoá loại hàng mà đã có mặt hàng liên quan
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool Delete(int categoryID);
    }
}
