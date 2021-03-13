using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.DomainInterfaces
{
    public interface ICacheService
    {
        void AddItem(string key, object value);

        object GetItem(string key);

        void DeleteItem(string key);
    }
}
