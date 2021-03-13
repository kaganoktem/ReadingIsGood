using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Dtos;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            using (readingIsGoodDbContext)
            {
                var query = from customers in this.readingIsGoodDbContext.Customers
                            where customers.Surname.Equals(username) && customers.Password.Equals(password)
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
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            using (readingIsGoodDbContext)
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
        }

        public BooksStockDto GetBookFromStockByBookName(string bookName)
        {
            using (readingIsGoodDbContext)
            {
                var query = from book in this.readingIsGoodDbContext.BooksStocks
                            select new BooksStockDto
                            {
                                Id = book.Id,
                                Name = book.Name,
                                NumberofBooks = book.NumberofBooks
                            };

                return query.FirstOrDefault();
            }
        }

        public int CreateNewCustomer(Customer customer)
        {
            using (readingIsGoodDbContext)
            {
                readingIsGoodDbContext.Customers.Add(customer);
                readingIsGoodDbContext.SaveChanges();
            }

            return customer.Id;
        }

        public int CreateNewOrder(Order order)
        {
            using (readingIsGoodDbContext)
            {
                readingIsGoodDbContext.Orders.Add(order);
                readingIsGoodDbContext.SaveChanges();
            }

            return order.Id;
        }

        public int CreateNewDeliveryInformation(BookDeliveryInformation deliveryInfo)
        {
            using (readingIsGoodDbContext)
            {
                readingIsGoodDbContext.BookDeliveryInformations.Add(deliveryInfo);
                readingIsGoodDbContext.SaveChanges();
            }

            return deliveryInfo.Id;
        }
    }
}
