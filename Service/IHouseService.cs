using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IHouseService
    {
        Task<PaginationItem<House>> GetHousesByPrice(float priceFrom, float priceTo, int size, string paginationToken);
        Task<IEnumerable<House>> GetHousesByPriceEF(float priceFrom, float priceTo, int size, string paginationToken);
    }
}
