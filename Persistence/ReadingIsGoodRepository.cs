using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Dtos;
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

        public bool Authenticate(string username, string password)
        {
            using (readingIsGoodDbContext)
            {
                var query = from customers in this.readingIsGoodDbContext.Customers
                            where customers.Surname.Equals(username) && customers.Password.Equals(password)
                            select customers;
                return query.Any();
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
    }
}
