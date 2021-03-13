using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class CustomerDto
    {
        /// <summary>
        /// Müşteri Id Bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Müşterinın adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Müşteri soyadı
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Müşteri adresi
        /// </summary>
        public string Address { get; set; }
    }
}
