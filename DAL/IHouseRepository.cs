using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IHouseRepository
    {
        Task<PaginationItem<House>> GetHousesByPrice();
        Task<IEnumerable<House>> GetHousesByPriceEF(float priceFrom, float priceTo, int size, string paginationToken);
    }
}
