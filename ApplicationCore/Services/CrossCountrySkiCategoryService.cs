using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CrossCountrySkiCategoryService : ICrossCountrySkiCategoryService
    {
        private readonly ICrossCountrySkiCategoryRepository _crossCountrySkiCategoryRepository;
        public CrossCountrySkiCategoryService(ICrossCountrySkiCategoryRepository crossCountrySkiCategoryRepository)
        {
            _crossCountrySkiCategoryRepository = crossCountrySkiCategoryRepository;
        }
        public async Task<CrossCountrySkiCategory> GetCategoryAsync(int age, CrossCountrySkiType type)
        {
            var categories = await _crossCountrySkiCategoryRepository.GetCategoriesAsync();
            if (!categories.Any())
                return null;

            var ageFilteredCategories = categories.Where(c =>
                c.ToAge.HasValue ? c.FromAge <= age && c.ToAge.Value >= age : c.FromAge <= age).ToList();

            return ageFilteredCategories.FirstOrDefault(c => string.IsNullOrEmpty(c.Style) ? true :
                    Enum.TryParse(c.Style, out CrossCountrySkiType currType) ? currType == type : false);
        }
    }
}
