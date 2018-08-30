using ApplicationCore.Services;
using ApplicationCore.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace Tests.ApplicationCore.Services
{
    [TestClass]
    [TestCategory(TestCategory.Unit)]
    public class CrossCountrySkiServiceTests
    {
        private CrossCountrySkiService _crossCountrySkiService;
        private Mock<ICrossCountrySkiCategoryService> _crossCountrySkiCategoryService;
        private int _length;
        private int _age;
        private CrossCountrySkiType _type;
        private int _additionalLength;
        private int _maxLength;

        [TestInitialize]
        public void Init()
        {
            _crossCountrySkiCategoryService = new Mock<ICrossCountrySkiCategoryService>();
            _crossCountrySkiService = new CrossCountrySkiService(_crossCountrySkiCategoryService.Object);
        }

        [TestMethod]
        public async Task When_Category_Is_Not_Found_Return_Null()
        {
            //act
            var result = await _crossCountrySkiService.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsNull(result);
            _crossCountrySkiCategoryService.Verify(s => s.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Once);
        }

        [TestMethod]
        public async Task When_Category_Is_Found_Calculate_Length()
        {
            //arrange
            _length = 150;
            _additionalLength = 10;
            _crossCountrySkiCategoryService.Setup(s => s.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()))
                .Returns(Task.FromResult(new CrossCountrySkiCategory { AdditionalLength = _additionalLength, MaxLength = null }));

            //act
            var result = await _crossCountrySkiService.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SkiLength == (_length + _additionalLength));
            _crossCountrySkiCategoryService.Verify(s => s.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Once);
        }

        [TestMethod]
        public async Task When_Calculated_Length_Violates_MaxLength_Return_MaxLength()
        {
            //arrange
            _length = 150;
            _additionalLength = 10;
            _maxLength = 155;
            _crossCountrySkiCategoryService.Setup(s => s.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()))
                .Returns(Task.FromResult(new CrossCountrySkiCategory { AdditionalLength = _additionalLength, MaxLength = _maxLength }));

            //act
            var result = await _crossCountrySkiService.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SkiLength == _maxLength);
            _crossCountrySkiCategoryService.Verify(s => s.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Once);
        }
    }
}
