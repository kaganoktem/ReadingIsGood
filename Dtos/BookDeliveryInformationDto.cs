using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class BookDeliveryInformationDto
    {
        /// <summary>
        /// Kitap teslim kaydı Id Bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key - kullanıcı id - siparişi veren kullanıcı
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// Foreign key - sipariş edilen kitap id bilgisi
        /// </summary>
        public int bookId { get; set; }

        /// <summary>
        /// Kitabın sipariş verilme zamanı
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Kitabın teslim edilme zamanı
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Teslim edildi bilgisi
        /// </summary>
        public bool? IsDelivered { get; set; }
    }
}
