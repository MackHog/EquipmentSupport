using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICrossCountrySkiCategoryRepository
    {
        Task<IEnumerable<CrossCountrySkiCategory>> GetCategoriesAsync();
    }
}
