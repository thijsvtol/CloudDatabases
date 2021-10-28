using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class HouseRepository : IHouseRepository
    {

        private HouseContext _hContext;

        public HouseRepository(HouseContext hContext)
        {
            _hContext = hContext;
        }

        public async Task<IEnumerable<House>> GetHousesPaginated(float priceFrom, float priceTo)
        {
            await _hContext.Database.EnsureDeletedAsync();
            await _hContext.Database.EnsureCreatedAsync();

            var houses = _hContext.Houses.Where(h => priceFrom < h.Price && h.Price < priceTo);
            return houses;
        }
    }
}
