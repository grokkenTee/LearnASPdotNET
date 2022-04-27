using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SV19T1021254.DataLayer.SQLServer
{
    /// <summary>
    /// SQL Server implementation for ICategoryDAL
    /// </summary>
    public class CategoryDAL : _BaseDAL, ICommonDAL<Category>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một loại hàng vào DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Categories(CategoryName,Description)
                                    VALUES(@CategoryName,@Description);
                                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Truy vấn số lượng loại hàng thoả mãn chuỗi tìm kiếm. 
        /// </summary>
        /// <param name="searchValue">Chuỗi tìm kiếm, chuỗi rỗng nếu lấy tất cả</param>
        /// <returns>Số loại hàng thoả yêu cầu</returns>
        public int Count(string searchValue = "")
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    COUNT(*)
                                    FROM    Categories
                                    WHERE    (@searchValue = N'')
                                        OR    (
                                                (CategoryName LIKE @searchValue)
                                                OR (Description LIKE @searchValue)
                                            )";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Connection = cn;

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }
        /// <summary>
        /// Xoá một loại hàng trong DB
        /// </summary>
        /// <param name="categoryID">Mã loại hàng</param>
        /// <returns>Đúng nếu xoá thành công, sai nếu ngược lại</returns>
        public bool Delete(int categoryID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Categories WHERE CategoryID= @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy thông tin loại hàng theo ID
        /// </summary>
        /// <param name="categoryID">mã loại hàng</param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            Category result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Categories WHERE CategoryID=@CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Category()
                    {
                        CategoryId = Convert.ToInt32(dbReader["CategoryId"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),
                    };
                }
                dbReader.Close();
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool InUsed(int categoryID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM Products WHERE CategoryID = @CategoryID) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Category> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Category> data = new List<Category>();

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM
	                                    (	SELECT	*, ROW_NUMBER() OVER(ORDER BY CategoryName) AS [RowNumber]
		                                    FROM	Categories
		                                    WHERE  (@searchValue = N'')
			                                    or (
                                                        (CategoryName LIKE @searchValue)
                                                    OR  (Description LIKE @searchValue)
                                                   )
	                                    ) AS t
                                    WHERE	(@pageSize=0) or (t.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Category()
                    {
                        CategoryId = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"])
                    });

                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Categories
                                    SET CategoryName = @CategoryName,
                                        Description = @Description
                                    WHERE CategoryID= @CategoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

    }
}
