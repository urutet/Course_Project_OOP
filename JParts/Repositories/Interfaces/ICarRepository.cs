using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JParts.Repositories.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        public Task<List<string>> GetAllManufacturers();
        public Task<List<string>> GetManufacturerModels(string manufacturer);
        public Task<List<int?>> GetModelsYears(string manufacturer, string model);
        public Car GetCar(string manufacturer, string model, int? year);

    }
}
