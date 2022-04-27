using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV19T1021254.BussinessLayer;
using SV19T1021254.DomainModel;
using System.Web.Mvc;

namespace SV19T1021254.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn một quốc gia --"
            });
            foreach(var c in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = c.CountryName,
                    Text = c.CountryName
                });
            }
            return list;
        }
    }
}