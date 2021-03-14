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
        /// Sipariş bilgisini getiren metod
        /// </summary>
        /// <param name="orderId">sipariş id bilgisi</param>
        /// <returns>sipariş bilgisi</returns>
        OrderDto GetOrderById(int orderId);

        /// <summary>
        /// Kitap teslimat bilgisi döner
        /// </summary>
        /// <param name="orderId">sipariş id bilgisi</param>
        /// <returns>teslimat bilgisi</returns>
        BookDeliveryInformation GetBookDeliveryInformationByOrderId(int orderId);

        /// <summary>
        /// Kitap'ı bookstock'dan çeker
        /// </summary>
        /// <param name="bookId">kitap bilgisi</param>
        /// <returns>kitap bilgisi</returns>
        BooksStock GetBookFromBooksStockById(int bookId);

        /// <summary>
        /// Tüm müşterileri çeker
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerDto> GetAllCustomers();

        /// <summary>
        /// Aynı kullanıcı ismine ait kullanıcı var mı sonucu döner
        /// </summary>
        /// <param name="username">kullanıcı adı</param>
        /// <returns>var/yok</returns>
        bool HasExistingCustomer(string username);

        /// <summary>
        /// Aynı isme ait kitap var mı sonucu döner
        /// </summary>
        /// <param name="bookName">kitap adı</param>
        /// <returns>var/yok</returns>
        bool HasExistingBook(string bookName);

        /// <summary>
        /// Tüm siparişleri getiren metod
        /// </summary>
        /// <returns>sipaşler</returns>
        IEnumerable<OrderDto> GetAllOrders();

        /// <summary>
        /// Kitabı isminden bulan ve dönen metod
        /// </summary>
        /// <param name="bookName">kitap adı</param>
        /// <returns>kitap bilgisi</returns>
        BooksStock GetBookFromStockByBookName(string bookName);

        /// <summary>
        /// Yeni bir kullanıcı ekler
        /// </summary>
        /// <param name="customer">kullanıcı bilgisi</param>
        void CreateNewCustomer(Customer customer);

        /// <summary>
        /// Yeni bir kitap ekler stoklara
        /// </summary>
        /// <param name="booksStock">kitap</param>
        void CreateNewBooksStock(BooksStock booksStock);

        /// <summary>
        /// Siparişin teslimat bilgisini günceller.
        /// </summary>
        /// <param name="bookDeliveryInformation">teslimat bilgisi</param>
        void UpdateDeliveryInformation(BookDeliveryInformation bookDeliveryInformation);

        /// <summary>
        /// Kitapları güncelle kitap store'undaki
        /// </summary>
        /// <param name="booksStock">kitap</param>
        void UpdateBookAtBooksStock(BooksStock booksStock);

        /// <summary>
        /// Yeni kitap siparişi
        /// </summary>
        /// <param name="order">şipariş</param>
        void CreateNewOrder(Order order);

        /// <summary>
        /// kitap teslimat bilgisi ekler
        /// </summary>
        /// <param name="deliveryInfo">teslimat bilgisi</param>
        void CreateNewDeliveryInformation(BookDeliveryInformation deliveryInfo);
    }
}
