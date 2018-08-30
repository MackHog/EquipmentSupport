using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfConfig
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options)
        {
        }

        public DbSet<CrossCountrySkiCategoryEntity> CrossCountrySkiCategory { get; set; }
    }
}
