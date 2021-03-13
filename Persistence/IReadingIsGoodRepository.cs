using ReadingIsGood.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Persistence
{
    public interface IReadingIsGoodRepository
    {
        /// <summary>
        /// Müşteri girişi
        /// </summary>
        /// <param name="username">kullanıcı adı</param>
        /// <param name="password">şifre</param>
        /// <returns></returns>
        bool Authenticate(string username, string password);

        /// <summary>
        /// Tüm müşterileri çeker
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerDto> GetAllCustomers();
    }
}
