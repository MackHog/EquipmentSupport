using ApplicationCore.Services;
using ApplicationCore.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace Tests.ApplicationCore.Services
{
    [TestClass]
    [TestCategory(TestCategory.Unit)]
    public class CrossCountrySkiCategoryServiceTests
    {
        private CrossCountrySkiCategoryService _crossCountrySkiCategoryService;
        private Mock<ICrossCountrySkiCategoryRepository> _crossCountrySkiCategoryRepository;
        private int _length;
        private int _age;
        private CrossCountrySkiType _type;
        private int _additionalLength;
        private int _maxLength;

        [TestInitialize]
        public void Init()
        {
            _crossCountrySkiCategoryRepository = new Mock<ICrossCountrySkiCategoryRepository>();
            _crossCountrySkiCategoryService = new CrossCountrySkiCategoryService(_crossCountrySkiCategoryRepository.Object);
        }

        [TestMethod]
        public async Task When_Categories_Are_Not_Found_Return_Null()
        {
            //act
            var result = await _crossCountrySkiCategoryService.GetCategoryAsync(_age, _type);

            //assert
            Assert.IsNull(result);
            _crossCountrySkiCategoryRepository.Verify(s => s.GetCategoriesAsync(), Times.Once);
        }

        [DataTestMethod]
        [DataRow(2, CrossCountrySkiType.Classic, "Toddler")]
        [DataRow(6, CrossCountrySkiType.Classic, "Child")]
        [DataRow(17, CrossCountrySkiType.Classic, "Classic")]
        [DataRow(17, CrossCountrySkiType.Freestyle, "Freestyle")]
        public async Task When_Categories_Are_Found_Return_Matched_Category(int age, CrossCountrySkiType type, string name)
        {
            //arrange
            var categories = GetCategories();
            _crossCountrySkiCategoryRepository.Setup(s => s.GetCategoriesAsync())
                .Returns(Task.FromResult(categories));

            //act
            var result = await _crossCountrySkiCategoryService.GetCategoryAsync(age, type);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, name);
            _crossCountrySkiCategoryRepository.Verify(s => s.GetCategoriesAsync(), Times.Once);
        }

        private IEnumerable<CrossCountrySkiCategory> GetCategories()
        {
            return new CrossCountrySkiCategory[]
            {
                new CrossCountrySkiCategory{
                    Name = "Toddler",
                    FromAge = 0,
                    ToAge = 4,
                    Style = null,
                    AdditionalLength = 0,
                    MaxLength = null,
                    AdditionalMinLength = null,
                    Info = string.Empty
                },
                new CrossCountrySkiCategory{
                    Name = "Child",
                    FromAge = 5,
                    ToAge = 8,
                    Style = null,
                    AdditionalLength = 10,
                    MaxLength = null,
                    AdditionalMinLength = null,
                    Info = string.Empty
                },
                new CrossCountrySkiCategory{
                    Name = "Classic",
                    FromAge = 9,
                    ToAge = null,
                    Style = "Classic",
                    AdditionalLength = 20,
                    MaxLength = 207,
                    AdditionalMinLength = null,
                    Info = "Klassiska skidor tillverkas bara till längder upp till 207cm"
                },
                new CrossCountrySkiCategory{
                    Name = "Freestyle",
                    FromAge = 9,
                    ToAge = null,
                    Style = "Freestyle",
                    AdditionalLength = 10,
                    MaxLength = null,
                    AdditionalMinLength = -10,
                    Info = "Enligt tävlingsreglerna får inte skidan understiga kroppslängden med mer än 10cm"
                }
            };
        }
    }
}
