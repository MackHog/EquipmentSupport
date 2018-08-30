using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace WebApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class CrossCountrySkiController : ControllerBase
    {
        private readonly ICrossCountrySkiService _crossCountrySkiService;

        public CrossCountrySkiController(ICrossCountrySkiService crossCountrySkiService)
        {
            _crossCountrySkiService = crossCountrySkiService;
        }

        /// <summary>
        /// Get cross country ski length recommendation 
        /// </summary>
        /// <remarks>This API will return ski length recommendation</remarks>
        /// <param name="length">Body length in centimeters</param>
        /// <param name="age">Age in years</param>
        /// <param name="type">Cross country ski style</param>
        /// <returns>CrossCountrySkiRecommendation response model</returns>
        [HttpGet("{length}/{age}/{type}")]
        public async Task<IActionResult> GetRecommendationAsync([FromRoute]int length, [FromRoute]int age, [FromRoute]CrossCountrySkiType type)
        {
            if (!IsGreaterThanZero(length))
                return BadRequest(NegativeNumberMsg(nameof(length)));
            else if (!IsGreaterThanZero(age))
                return BadRequest(NegativeNumberMsg(nameof(length)));

            var result = await _crossCountrySkiService.GetRecommendationAsync(length, age, type);
            if (result == null)
                return NotFound("No recommendations found for given values");

            return Ok(result);
        }

        private bool IsGreaterThanZero(int value) => value > 0;
        private string NegativeNumberMsg(string type) => $"{type} is not valid. {type} needs to be greater than zero";
    }
}
