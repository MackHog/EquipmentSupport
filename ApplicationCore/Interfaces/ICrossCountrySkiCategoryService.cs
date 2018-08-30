using ApplicationCore.Models;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICrossCountrySkiCategoryService
    {
        Task<CrossCountrySkiCategory> GetCategoryAsync(int age, CrossCountrySkiType type);
    }
}
