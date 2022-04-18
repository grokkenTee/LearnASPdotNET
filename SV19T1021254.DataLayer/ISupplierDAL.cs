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
        /// Lấy danh sách các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        IList<Supplier> List();
        /// <summary>
        /// Lấy thông tin 1 nhà cung cấp theo mã nhà cung cấp
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
