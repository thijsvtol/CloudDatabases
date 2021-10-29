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
        IEnumerable<House> GetHouses();
        House GetHouseById(string id);
        IEnumerable<House> GetHousesBetweenPrice(int priceFrom, int priceTo);
        Task<House> AddHouse(House house);
    }
}
