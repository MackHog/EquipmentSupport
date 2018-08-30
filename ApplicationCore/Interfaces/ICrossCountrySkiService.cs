using ApplicationCore.Models;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICrossCountrySkiService
    {
        Task<CrossCountrySkiRecommendation> GetRecommendationAsync(int length, int age, CrossCountrySkiType type);
    }
}
