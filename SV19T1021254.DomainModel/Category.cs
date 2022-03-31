using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DomainModel
{   
    /// <summary>
    /// Loai hang
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Ma loai hang
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Ten loai hang
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Mo ta
        /// </summary>
        public string Description { get; set; }

    }
}
