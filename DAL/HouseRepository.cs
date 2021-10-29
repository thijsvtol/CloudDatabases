using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class HouseRepository : IHouseRepository
    {

        private HouseContext _HouseContext;

        public HouseRepository(HouseContext userContext)
        {
            _HouseContext = userContext;
        }

        public IEnumerable<House> GetHouses()
        {
            var users = _HouseContext.Houses.ToList();
            return users;
        }

        public House GetHouseById(string user)
        {
            return _HouseContext.Houses.Find(user);
        }

        public IEnumerable<House> GetHousesByPriceRange(int priceFrom, int priceTo)
        {
            return _HouseContext.Houses.Where(h => priceFrom < h.Price && h.Price < priceTo).ToList();
        }

        public async Task<House> AddHouse(House house)
        {
            _HouseContext.Add<House>(house);
            await _HouseContext.SaveChangesAsync();
            return house;
        }
    }
}
