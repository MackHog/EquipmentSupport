using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.EfConfig;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CrossCountrySkiCategoryRepository : RepositoryBase<CrossCountrySkiCategoryEntity>, 
        ICrossCountrySkiCategoryRepository
    {
        public CrossCountrySkiCategoryRepository(EquipmentContext equipmentContext) :
            base(equipmentContext)
        { }

        public async Task<IEnumerable<CrossCountrySkiCategory>> GetCategoriesAsync() => 
            (await GetAll().ToListAsync()).ToModels();
    }
}
