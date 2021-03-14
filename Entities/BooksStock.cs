using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Entities
{
    /// <summary>
    /// Kitap stoklarının tutulduğu varlık
    /// </summary>
    public class BooksStock
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
        public string Name { get; set; }
    }
}
