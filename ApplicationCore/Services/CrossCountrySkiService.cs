using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CrossCountrySkiService : ICrossCountrySkiService
    {
        private readonly ICrossCountrySkiCategoryService _crossCountrySkiCategoryService;
        public CrossCountrySkiService(ICrossCountrySkiCategoryService crossCountrySkiCategoryService)
        {
            _crossCountrySkiCategoryService = crossCountrySkiCategoryService;
        }

        public async Task<CrossCountrySkiRecommendation> GetRecommendationAsync(int length, int age, CrossCountrySkiType type)
        {
            var category = await _crossCountrySkiCategoryService.GetCategoryAsync(age, type);
            if (category == null)
                return null;

            var recommendedLength = length + category.AdditionalLength;
            var skiLength = category.MaxLength.HasValue ?
                (recommendedLength > category.MaxLength.Value ? category.MaxLength.Value : recommendedLength) :
                recommendedLength;

            return new CrossCountrySkiRecommendation(skiLength, category.Info);
        }
    }
}
