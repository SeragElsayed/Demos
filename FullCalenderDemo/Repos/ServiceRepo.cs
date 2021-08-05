using FullCalenderDemo.DBContext;
using FullCalenderDemo.Models;
using FullCalenderDemo.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Repos
{
    public class ServiceRepo:IServiceRepo
    {
        CalendarDBCTX _ctx;
        public ServiceRepo(CalendarDBCTX ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Service> GetAll()
        {
            return _ctx.Services.ToList();
        }
        public Service GetById(int id)
        {
            return _ctx.Services.FirstOrDefault(e => e.Id == id);
        }
        public async Task<Service> AddAsync(Service newService)
        {
            await _ctx.Services.AddAsync(newService);
            await _ctx.SaveChangesAsync();
            return newService;
        }
        public Service Update(Service updatedService)
        {
            _ctx.Services.Update(updatedService);
            _ctx.SaveChanges();
            return updatedService;
        }
        public Service Remove(int id)
        {
            var ServiceToBeRemoved = _ctx.Services.FirstOrDefault(e => e.Id == id);
            _ctx.Remove(ServiceToBeRemoved);
            _ctx.SaveChanges();
            return ServiceToBeRemoved;
        }
    }
}
