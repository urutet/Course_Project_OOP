using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JParts.Repositories.Implementations
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await JpartsContext.Cars.Distinct().ToListAsync();
        }

        public async Task<List<string>> GetAllManufacturers()
        {
            return await JpartsContext.Cars.Select(c => c.Manufacturer).Distinct().ToListAsync();
        }

        public Car GetCar(string manufacturer, string model, int? year)
        {
            return JpartsContext.Cars.AsNoTracking().First(c => c.Manufacturer == manufacturer && c.Model == model && c.Year == year);
        }

        public async Task<List<string>> GetManufacturerModels(string manufacturer)
        {
            return await JpartsContext.Cars.Where(c => c.Manufacturer == manufacturer).Select(c => c.Model).Distinct().ToListAsync();
        }

        public async Task<List<int?>> GetModelsYears(string manufacturer, string model)
        {
            return await JpartsContext.Cars.Where(c => c.Manufacturer == manufacturer && c.Model == model).Select(c => c.Year).ToListAsync();
        }
    }
}
