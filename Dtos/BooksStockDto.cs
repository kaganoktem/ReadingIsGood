using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class BooksStockDto
    {
        /// <summary>
        /// Kitap Id bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Stokta kaç tane kitap olduğu
        /// </summary>
        public int NumberofBooks { get; set; }

        /// <summary>
        /// Kitabın adı
        /// </summary>
        public int Name { get; set; }
    }
}
