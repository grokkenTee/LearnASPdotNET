using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1021254.DataLayer;
using SV19T1021254.DomainModel;
using System.Configuration;

namespace SV19T1021254.BussinessLayer
{
    /// <summary>
    /// Cung cấp các chức năng xử lí dữ liệu chung (từ điển dữ liệu)
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICategoryDAL categoryDB;
        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    break;
                //tình huống nhiều loại DB -> thêm các case

                default:
                    categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }

        public static List<Category> CategoryList()
        {
            return categoryDB.List().ToList();
        }
    }
}
