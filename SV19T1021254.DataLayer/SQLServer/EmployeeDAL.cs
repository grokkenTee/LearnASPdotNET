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
    /// 
    /// </summary>
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Employee data)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
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
            }
            return count;
        }

        public bool Delete(int employeeID)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int employeeID)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	*
                                    FROM
	                                    (	SELECT	*, ROW_NUMBER() OVER(ORDER BY SupplierName) AS [RowNumber]
		                                    FROM	Employees
		                                    WHERE  (@searchValue = N'')
			                                    OR	(
                                                        (LastName LIKE @searchValue)
                                                    OR (FirstName LIKE @searchValue)
                                                    OR (Email LIKE @searchValue)
                                                )
	                                    ) AS t
                                    WHERE	t.RowNumber BETWEEN (@page-1)*@pageSize+1 AND @page*@pageSize";
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
                        //SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        //SupplierName = Convert.ToString(dbReader["SupplierName"]),
                        //ContactName = Convert.ToString(dbReader["ContactName"]),
                        //Address = Convert.ToString(dbReader["Address"]),
                        //City = Convert.ToString(dbReader["City"]),
                        //PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        //Country = Convert.ToString(dbReader["Country"]),
                        //Phone = Convert.ToString(dbReader["Phone"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        public bool Update(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}
