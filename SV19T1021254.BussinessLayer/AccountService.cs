using SV19T1021254.DataLayer;
using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.BussinessLayer
{
    /// <summary>
    /// 
    /// </summary>
    public static class AccountService
    {
        private static IAccountDAL accountDB;
        public enum StatusCodes
        {
            Success = 1,
            WrongEmail = -1,
            WrongPassword = -2,
            Undefined = 0
        }
        /// <summary>
        /// Ctor
        /// </summary>
        static AccountService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    accountDB = new DataLayer.SQLServer.AccountDAL(connectionString);
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Status code: 1-Success; -2 Wrong password; -1 Wrong username; 0 Undefined</returns>
        public static StatusCodes Login(Account data)
        {
            StatusCodes result = StatusCodes.Undefined;
            Account validate = accountDB.Get(data.Email);
            if (validate == null)
                result = StatusCodes.WrongEmail;
            else if (validate.Password != data.Password)
                result = StatusCodes.WrongPassword;
            else
                result = StatusCodes.Success;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StatusCodes ChangePassword(string email = "", string oldPass = "", string newPass = "")
        {
            if (string.IsNullOrWhiteSpace(email))
                return StatusCodes.WrongEmail;

            var account = accountDB.Get(email);

            if (account.Password != oldPass)
                return StatusCodes.WrongPassword;

            account.Password = newPass;

            if (accountDB.Update(account))
                return StatusCodes.Success;

            return StatusCodes.Undefined;
        }
    }
}
