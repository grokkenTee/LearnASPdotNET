using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace SV19T1021254.DataLayer.SQLServer
{
    /// <summary>
    /// Định nghĩa các phép xử lí dữ liệu liên quan đến khách hàng.
    /// </summary>
    public class CustomerDAL : _BaseDAL, ICommonDAL<Customer>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối đến CSDL</param>
        public CustomerDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một khách hàng vào DB
        /// </summary>
        /// <param name="data">Khách hàng</param>
        /// <returns>ID của khách hàng</returns>
        public int Add(Customer data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customers(CustomerName,ContactName,Address,City,PostalCode,Country)
                                    VALUES(@CustomerName,@ContactName,@Address,@City,@PostalCode,@Country);
                                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Truy vấn số lượng khách hàng thoả mãn chuỗi tìm kiếm. 
        /// </summary>
        /// <param name="searchValue">Chuỗi tìm kiếm, chuỗi rỗng nếu lấy tất cả</param>
        /// <returns>Số khách hàng thoả yêu cầu</returns>
        public int Count(string searchValue="")
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using(SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    COUNT(*)
                                    FROM    Customers
                                    WHERE    (@searchValue = N'')
                                        OR    (
                                                (CustomerName LIKE @searchValue)
                                                OR (ContactName LIKE @searchValue)
                                                OR (Address LIKE @searchValue)
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
        /// Xoá một khách hàng trong DB
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns>Đúng nếu xoá thành công, sai nếu ngược lại</returns>
        public bool Delete(int customerID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CustomerID", customerID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy thông tin khách hàng theo ID
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns></returns>
        public Customer Get(int customerID)
        {
            Customer result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Customers WHERE CustomerID=@CustomerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"])
                    };
                }
                dbReader.Close();
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Kiểm tra xem thử 1 khách hàng hiện có dữ liệu nào liên quan không
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns></returns>
        public bool InUsed(int customerID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM Orders WHERE CustomerID = @CustomerID) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CustomerID", customerID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Tim kiếm, hiển thị danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Số trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm (tương đối). Chuỗi rỗng nếu lấy toàn bộ</param>
        /// <returns>Danh sách khách hàng</returns>
        public IList<Customer> List(int page=1, int pageSize=0, string searchValue="")
        {
            List<Customer> data = new List<Customer>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM
	                                    (	SELECT	*, ROW_NUMBER() OVER(ORDER BY CustomerName) AS [RowNumber]
		                                    FROM	Customers
		                                    WHERE  (@searchValue = N'')
			                                    or	(
                                                        (CustomerName like @searchValue)
			                                        or	(ContactName like @searchValue)
			                                        or	(Address like @searchValue)
                                                    )
	                                    ) AS t
                                    WHERE	(@pageSize=0) or (t.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// Cập nhật thông tin của một khách hàng
        /// </summary>
        /// <param name="data">Khách hàng</param>
        /// <returns></returns>
        public bool Update(Customer data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Customers
                                    SET CustomerName = @CustomerName,
                                        ContactName = @ContactName,
                                        Address = @Address,
                                        City = @City,
                                        PostalCode = @PostalCode,
                                        Country = @Country
                                    WHERE CustomerID = @CustomerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
