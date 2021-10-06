using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiLambda.Helpers
{
   public interface IListRepository
    {
        Task<List<string>> GetListOfData(string word);
    }
}
