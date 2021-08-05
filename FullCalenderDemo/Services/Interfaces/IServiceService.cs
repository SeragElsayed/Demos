using FullCalenderDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Services.Interfaces
{
    public interface IServiceService
    {
        IEnumerable<Service> GetAll();

        Service GetById(int id);

        Task<Service> AddAsync(Service newService);

        Service Update(Service updatedService);

        Service Remove(int id);
    }
}
