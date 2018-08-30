using Infrastructure.Entities;
using System.Linq;

namespace Infrastructure.EfConfig
{
    public static class DbInitializer
    {
        public static void Initialize(EquipmentContext context)
        {
            context.Database.EnsureCreated();

            if (context.CrossCountrySkiCategory.Any())
                return;

            var categories = new CrossCountrySkiCategoryEntity[]
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
                },
                new CrossCountrySkiCategoryEntity{
                    Name = "Child",
                    FromAge = 5,
                    ToAge = 8,
                    Style = null,
                    AdditionalLength = 10,
                    MaxLength = null,
                    AdditionalMinLength = null,
                    Info = string.Empty
                },
                new CrossCountrySkiCategoryEntity{
                    Name = "Classic",
                    FromAge = 9,
                    ToAge = null,
                    Style = "Classic",
                    AdditionalLength = 20,
                    MaxLength = 207,
                    AdditionalMinLength = null,
                    Info = "Klassiska skidor tillverkas bara till längder upp till 207cm"
                },
                new CrossCountrySkiCategoryEntity{
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

            context.CrossCountrySkiCategory.AddRange(categories);
            context.SaveChanges();
        }
    }
}
