using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    /// <summary>
    /// Lớp cơ sở (lớp cha) cho các lớp lưu trữ các dữ liệu liên quan điến truy vấn phân trang
    /// </summary>
    public abstract class BasePaginationResult
    {
        /// <summary>
        /// Trang cần xem
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Giá trị tìm kiếm
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// Tổng số dòng
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// Số trang
        /// </summary>
        public int PageCount {
            get
            {
                int p = (RowCount / PageSize);
                if(RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }
    }
}