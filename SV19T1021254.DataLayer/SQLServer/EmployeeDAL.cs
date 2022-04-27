using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DataLayer.SQLServer
{
    /// <summary>
    /// SQL Server implementation for IEmployeeDAL
    /// </summary>
    public class EmployeeDAL : _BaseDAL, ICommonDAL<Employee>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một nhân viên vào DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Employee data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees(LastName,FirstName,BirthDate,Photo,Notes,Email)
                                    VALUES(@LastName,@FirstName,@BirthDate,@Photo,@Notes,@Email);
                                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Truy vấn số lượng nhân viên thoả mãn chuỗi tìm kiếm. 
        /// </summary>
        /// <param name="searchValue">Chuỗi tìm kiếm, chuỗi rỗng nếu lấy tất cả</param>
        /// <returns>Số nhân viên thoả yêu cầu</returns>
        public int Count(string searchValue="")
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    COUNT(*)
                                    FROM    Employees
                                    WHERE    (@searchValue = N'')
                                        OR    (
                                                (LastName LIKE @searchValue)
                                                OR (FirstName LIKE @searchValue)
                                                OR (Email LIKE @searchValue)
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
        /// <param name="employeeID">Mã nhân viên </param>
        /// <returns>Đúng nếu xoá thành công, sai nếu ngược lại</returns>
        public bool Delete(int employeeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy thông tin khách hàng theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee Get(int employeeID)
        {
            Employee result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeID=@EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        LastName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["FirstName"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Email = Convert.ToString(dbReader["Email"])
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
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool InUsed(int employeeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Employee> List(int page=1, int pageSize=0, string searchValue="")
        {
            List<Employee> data = new List<Employee>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM
	                                    (	SELECT	*, ROW_NUMBER() OVER(ORDER BY FirstName) AS [RowNumber]
		                                    FROM	Employees
		                                    WHERE  (@searchValue = N'')
			                                    OR	(
                                                        (LastName LIKE @searchValue)
                                                    OR (FirstName LIKE @searchValue)
                                                    OR (Email LIKE @searchValue)
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
                    data.Add(new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        public bool Update(Employee data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName,
                                        FirstName = @FirstName, 
                                        BirthDate = @BirthDate,
                                        Photo = @Photo,
                                        Notes = @Notes,
                                        Email = @Email
                                    WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
