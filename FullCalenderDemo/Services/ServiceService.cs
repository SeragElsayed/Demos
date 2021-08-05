using FullCalenderDemo.Models;
using FullCalenderDemo.Repos.Interfaces;
using FullCalenderDemo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Services
{
    public class ServiceService: IServiceService
    {
        IServiceRepo _ServiceRepo;
        public ServiceService(IServiceRepo ServiceRepo)
        {
            _ServiceRepo = ServiceRepo;
        }
        public IEnumerable<Service> GetAll()
        {
            return _ServiceRepo.GetAll();
        }
        public Service GetById(int id)
        {
            return _ServiceRepo.GetById(id);
        }
        public async Task<Service> AddAsync(Service newService)
        {
            return await _ServiceRepo.AddAsync(newService);

        }
        public Service Update(Service updatedService)
        {
            return _ServiceRepo.Update(updatedService);

        }
        public Service Remove(int id)
        {
            return _ServiceRepo.Remove(id);
        }
    }
}
