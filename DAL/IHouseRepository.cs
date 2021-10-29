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
        IEnumerable<House> GetHouses();
        House GetHouseById(string id);
        IEnumerable<House> GetHousesByPriceRange(int priceFrom, int priceTo);
        Task<House> AddHouse(House house);
    }
}
