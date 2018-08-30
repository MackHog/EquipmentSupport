using Infrastructure.Entities;
using Infrastructure.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Infrastructure.Mappers
{
    [TestClass]
    [TestCategory(TestCategory.Unit)]
    public class CrossCountrySkiMapperTests
    {
        [TestMethod]
        public void When_Entity_Is_Provided_Map_To_Model()
        {
            //arrange
            var entities = new List<CrossCountrySkiCategoryEntity>
            {
                new CrossCountrySkiCategoryEntity{
                    Name = "Toddler",
                    FromAge = 0,
                    ToAge = 4,
                    Style = null,
                    AdditionalLength = 0,
                    MaxLength = null,
                    AdditionalMinLength = null,
                    Info = string.Empty
                }
            };

            //act
            var result = entities.ToModels();

            //assert
            Assert.IsTrue(result.Any());
            Assert.AreEqual(entities[0].Name, "Toddler");
            Assert.AreEqual(entities[0].FromAge, 0);
            Assert.AreEqual(entities[0].ToAge, 4);
            Assert.IsNull(entities[0].Style);
            Assert.AreEqual(entities[0].AdditionalLength, 0);
            Assert.IsNull(entities[0].MaxLength);
            Assert.IsNull(entities[0].AdditionalMinLength);
            Assert.AreEqual(entities[0].Info, string.Empty);

        }
    }
}
