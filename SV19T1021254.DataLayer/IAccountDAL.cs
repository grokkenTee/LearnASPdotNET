using SV19T1021254.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1021254.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Account Get(string email);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean Update(Account data);
    }
}
