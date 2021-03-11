using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Entities
{
    /// <summary>
    /// Sipariş bilgilerinin tutulduğu sınıf
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Sipariş Id bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// foreign key -> Müşteri bilgisi
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Sipariş veriliş zamanı
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// foreign key - bookStock Sipariş verilen kitabın bilgisi
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public Customer Customer;
    }
}
