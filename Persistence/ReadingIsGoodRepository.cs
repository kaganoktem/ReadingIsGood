using Microsoft.Extensions.Logging;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ReadingIsGood.Persistence
{
    public class ReadingIsGoodRepository : IReadingIsGoodRepository
    {
        private readonly ReadingIsGoodDbContext readingIsGoodDbContext;
        private readonly ILogger<ReadingIsGoodRepository> logger;

        public ReadingIsGoodRepository(ILogger<ReadingIsGoodRepository> logger,
            ReadingIsGoodDbContext readingIsGoodDbContext)
        {
            this.logger = logger;
            this.readingIsGoodDbContext = readingIsGoodDbContext;
        }

        public int Authenticate(string username, string password)
        {
            var query = from customers in this.readingIsGoodDbContext.Customers
                        where customers.Username.Equals(username) && customers.Password.Equals(password)
                        select customers;

            if (query.Any())
            {
                var userId = query.FirstOrDefault().Id;
                return userId;
            }
            else
            {
                return 0;
            }
        }

        public OrderDto GetOrderById(int orderId)
        {
            var query = from order in this.readingIsGoodDbContext.Orders
                        join book in readingIsGoodDbContext.BooksStocks on order.BookId equals book.Id into orderedBook
                        from book in orderedBook.DefaultIfEmpty()
                        where order.Id == orderId
                        select new OrderDto
                        {
                            Id = order.Id,
                            BookId = order.BookId,
                            BookName = book.Name,
                            CustomerId = order.CustomerId,
                            OrderDate = order.OrderDate
                        };

            return query.FirstOrDefault();
        }

        public BookDeliveryInformation GetBookDeliveryInformationByOrderId(int orderId)
        {
            var query = from bookDeliveryInfo in this.readingIsGoodDbContext.BookDeliveryInformations
                        where bookDeliveryInfo.OrderId == orderId
                        select bookDeliveryInfo;

            return query.FirstOrDefault();
        }

        public BooksStock GetBookFromBooksStockById(int bookId)
        {
            var query = from book in this.readingIsGoodDbContext.BooksStocks
                        where book.Id == bookId
                        select book;

            return query.FirstOrDefault();
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var query = from customers in this.readingIsGoodDbContext.Customers
                        select new CustomerDto
                        {
                            Id = customers.Id,
                            Address = customers.Address,
                            Name = customers.Name,
                            Surname = customers.Surname,
                            Username = customers.Username
                        };
            return query.ToList();
        }

        public bool HasExistingCustomer(string username) 
        {
            var query = from customer in this.readingIsGoodDbContext.Customers
                        where customer.Username.ToLower().Equals(username.ToLower())
                        select customer;
            
            return query.Any();
        }

        public bool HasExistingBook(string bookName)
        {
            var query = from book in this.readingIsGoodDbContext.BooksStocks
                        where book.Name.ToLower().Equals(bookName.ToLower())
                        select book;

            return query.Any();
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            var query = from order in this.readingIsGoodDbContext.Orders
                        join book in readingIsGoodDbContext.BooksStocks on order.BookId equals book.Id into orderedBook
                        from book in orderedBook.DefaultIfEmpty()
                        select new OrderDto
                        {
                            Id = order.Id,
                            BookId = order.BookId,
                            BookName = book.Name,
                            CustomerId = order.CustomerId,
                            OrderDate = order.OrderDate
                        };

            return query.ToList();
        }

        public BooksStock GetBookFromStockByBookName(string bookName)
        {
            var query = from book in this.readingIsGoodDbContext.BooksStocks
                        where book.Name.ToLower().Equals(bookName.ToLower())
                        select book;
            return query.FirstOrDefault();
        }

        public void CreateNewCustomer(Customer customer)
        {
            readingIsGoodDbContext.Customers.Add(customer);
        }

        public void CreateNewBooksStock(BooksStock booksStock)
        {
            readingIsGoodDbContext.BooksStocks.Add(booksStock);
        }

        public void UpdateDeliveryInformation(BookDeliveryInformation bookDeliveryInformation)
        {
            readingIsGoodDbContext.BookDeliveryInformations.Update(bookDeliveryInformation);           
        }

        public void UpdateBookAtBooksStock(BooksStock booksStock)
        {
            readingIsGoodDbContext.BooksStocks.Update(booksStock);
        }

        public void CreateNewOrder(Order order)
        {
            readingIsGoodDbContext.Orders.Add(order);
        }

        public void CreateNewDeliveryInformation(BookDeliveryInformation deliveryInfo)
        {
            readingIsGoodDbContext.BookDeliveryInformations.Add(deliveryInfo);
        }
    }
}
