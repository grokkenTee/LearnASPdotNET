using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm phân trang của nhà cung cấp
    /// </summary>
    public class SupplierPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}