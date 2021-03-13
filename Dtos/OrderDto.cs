using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class OrderDto
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
        /// İstenen kitabın adı
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// foreign key - bookStock Sipariş verilen kitabın bilgisi
        /// </summary>
        public int BookId { get; set; }
    }
}
