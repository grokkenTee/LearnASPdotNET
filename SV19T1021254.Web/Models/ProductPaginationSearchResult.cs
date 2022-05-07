using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductPaginationSearchResult : PaginationSearchResult
    {
        /// <summary>
        /// Loại hàng
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Mặt hàng
        /// </summary>
        public int SupplierID { get; set; }
    }
}