using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class ReadingIsGoodException : Exception
    {
        public int ExceptionCode { get; set; }

        public string ExceptionMessage { get; set; }

        public Exception Exception { get; set; }
    }
}
