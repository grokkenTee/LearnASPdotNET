using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy ra danh sách tất cả quốc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
