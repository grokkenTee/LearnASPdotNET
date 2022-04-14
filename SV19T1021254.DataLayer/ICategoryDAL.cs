using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1021254.DomainModel;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Giao diện, định nghĩa các phép xử lý dữ liệu liên quan đến loại hàng
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Lấy danh sách các loại hàng
        /// </summary>
        /// <returns></returns>
        IList<Category> List();
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
