using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class HouseService : IHouseService
    {

        public async Task<PaginationItem<House>> GetHousesByPrice(float priceFrom, float priceTo, int size, string paginationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<House>> GetHousesByPriceEF(float priceFrom, float priceTo, int size, string paginationToken)
        {
            using (var context = new HouseContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                var houses = context.Houses.Where(h => priceFrom < h.Price && h.Price < priceTo);
                return await Task.FromResult(houses);
            }
        }
    }
}
