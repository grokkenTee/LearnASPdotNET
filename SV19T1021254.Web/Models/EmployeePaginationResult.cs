using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeePaginationResult:BasePaginationResult
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data{ get; set; }
    }
}