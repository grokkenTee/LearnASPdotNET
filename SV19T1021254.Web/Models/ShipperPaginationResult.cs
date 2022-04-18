using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV19T1021254.Web.Models
{
    public class ShipperPaginationResult : BasePaginationResult
    {
        public List<Shipper> Data{ get; set; }
    }
}