using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using WebApp.Controllers;

namespace Tests.WebApp.Controllers
{
    [TestClass]
    [TestCategory(TestCategory.Unit)]
    public class CrossCountrySkiControllerTests
    {
        private CrossCountrySkiController _crossCountrySkiController;
        private Mock<ICrossCountrySkiService> _crossCountrySkiService;
        private int _length;
        private int _age;
        private CrossCountrySkiType _type;

        [TestInitialize]
        public void Init()
        {
            _crossCountrySkiService = new Mock<ICrossCountrySkiService>();
            _crossCountrySkiController = new CrossCountrySkiController(_crossCountrySkiService.Object);
        }

        [TestMethod]
        public async Task When_Length_Is_Invalid_Return_Bad_Request()
        {
            //arrange
            _length = 0;
            _age = 1;

            //act
            var result = await _crossCountrySkiController.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            _crossCountrySkiService.Verify(s => s.GetRecommendationAsync(
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Never);
        }

        [TestMethod]
        public async Task When_Age_Is_Invalid_Return_Bad_Request()
        {
            //arrange
            _length = 1;
            _age = 0;

            //act
            var result = await _crossCountrySkiController.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            _crossCountrySkiService.Verify(s => s.GetRecommendationAsync(
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Never);
        }

        [TestMethod]
        public async Task When_No_Recommendations_Are_Found_Return_Not_Found()
        {
            //arrange
            _length = 1;
            _age = 1;
            _type = CrossCountrySkiType.Classic;
            _crossCountrySkiService.Setup(s => s.GetRecommendationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()))
                .Returns(Task.FromResult((CrossCountrySkiRecommendation)null));

            //act
            var result = await _crossCountrySkiController.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            _crossCountrySkiService.Verify(s => s.GetRecommendationAsync(
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Once);
        }

        [TestMethod]
        public async Task When_Recommendations_Are_Found_Return_Values()
        {
            //arrange
            _length = 1;
            _age = 1;
            _type = CrossCountrySkiType.Classic;
            var expectedObj = new CrossCountrySkiRecommendation(_length, string.Empty);
            _crossCountrySkiService.Setup(s => s.GetRecommendationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()))
                .Returns(Task.FromResult(expectedObj));

            //act
            var result = await _crossCountrySkiController.GetRecommendationAsync(_length, _age, _type);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            _crossCountrySkiService.Verify(s => s.GetRecommendationAsync(
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CrossCountrySkiType>()), Times.Once);
            var okObj = (OkObjectResult)result;
            Assert.IsInstanceOfType(okObj.Value, typeof(CrossCountrySkiRecommendation));
            var recom = (CrossCountrySkiRecommendation)okObj.Value;
            Assert.AreEqual(recom.SkiLength, _length);
        }
    }
}
