using ApplicationCore.Models;
using Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Mappers
{
    public static class CrossCountrySkiMapper
    {
        public static IEnumerable<CrossCountrySkiCategory> ToModels(this List<CrossCountrySkiCategoryEntity> entities)
        {
            return entities.Select(e => new CrossCountrySkiCategory
            {
                Name = e.Name,
                AdditionalLength = e.AdditionalLength,
                AdditionalMinLength = e.AdditionalMinLength,
                FromAge = e.FromAge,
                ToAge = e.ToAge,
                Info = e.Info,
                MaxLength = e.MaxLength,
                Style = e.Style
            });
        }
    }
}
