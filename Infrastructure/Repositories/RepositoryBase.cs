using Infrastructure.EfConfig;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        private readonly EquipmentContext _equipmentContext;
        public RepositoryBase(EquipmentContext equipmentContext)
        {
            _equipmentContext = equipmentContext;
        }
        public IQueryable<T> GetAll()
        {
            return _equipmentContext.Set<T>().AsNoTracking();
        }
    }
}
