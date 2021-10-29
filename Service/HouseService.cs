using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _HouseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _HouseRepository = houseRepository;
        }

        public IEnumerable<House> GetHouses()
        {
            return _HouseRepository.GetHouses();
        }

        public House GetHouseById(string id)
        {
            return _HouseRepository.GetHouseById(id);
        }

        public IEnumerable<House> GetHousesBetweenPrice(int priceFrom, int priceTo)
        {
            return _HouseRepository.GetHousesByPriceRange(priceFrom, priceTo);
        }

        public async Task<House> AddHouse(House house)
        {
            house.id = Guid.NewGuid().ToString();
            return await _HouseRepository.AddHouse(house);
        }
    }
}
