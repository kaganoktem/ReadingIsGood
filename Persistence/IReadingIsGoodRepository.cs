using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
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
        /// <returns>müşteri id bilgisi</returns>
        int Authenticate(string username, string password);

        /// <summary>
        /// Tüm müşterileri çeker
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerDto> GetAllCustomers();

        /// <summary>
        /// Kitabı isminden bulan ve dönen metod
        /// </summary>
        /// <param name="bookName">kitap adı</param>
        /// <returns>kitap bilgisi</returns>
        BooksStockDto GetBookFromStockByBookName(string bookName);

        /// <summary>
        /// Yeni bir kullanıcı ekler
        /// </summary>
        /// <param name="customer">kullanıcı bilgisi</param>
        /// <returns>müşteri id bilgisi</returns>
        int CreateNewCustomer(Customer customer);

        /// <summary>
        /// Yeni kitap siparişi
        /// </summary>
        /// <param name="order">şipariş</param>
        /// <returns>sipariş id bilgisi</returns>
        int CreateNewOrder(Order order);

        /// <summary>
        /// kitap teslimat bilgisi ekler
        /// </summary>
        /// <param name="deliveryInfo">teslimat bilgisi</param>
        /// <returns>teslimat bilgisi id</returns>
        int CreateNewDeliveryInformation(BookDeliveryInformation deliveryInfo);
    }
}
