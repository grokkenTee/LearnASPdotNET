using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm, phân trang của khách hàng
    /// </summary>
    public class CustomerPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }

    }
}