using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Entities
{
    /// <summary>
    /// Müşteri Varlığı
    /// </summary>
    public class Customer
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
        /// Müşteri şifresi
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Müşteri adresi
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Müşteri siparişleri
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}
