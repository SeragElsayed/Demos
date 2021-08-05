using FullCalenderDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Repos.Interfaces
{
   public interface IServiceRepo
    {
        IEnumerable<Service> GetAll();

        Service GetById(int id);

        Task<Service> AddAsync(Service newService);

        Service Update(Service updatedService);

        Service Remove(int id);
    }
}
