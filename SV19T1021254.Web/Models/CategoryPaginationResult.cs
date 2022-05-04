using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang của loại hàng
    /// </summary>
    public class CategoryPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách loại hàng
        /// </summary>
        public IList<Category> Data { get; set; }
    }
}