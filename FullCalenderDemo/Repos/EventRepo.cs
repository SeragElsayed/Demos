using FullCalenderDemo.DBContext;
using FullCalenderDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Repos
{
    public class EventRepo: IEventRepo
    {
        CalendarDBCTX _ctx;
        public EventRepo(CalendarDBCTX ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Event> GetAll()
        {
           return _ctx.Events.Include(c=>c.Customers).Include(s=>s.Services).ToList();
        }
        public Event GetById(int id)
        {
            return _ctx.Events.Include(c => c.Customers).Include(s => s.Services).FirstOrDefault(e=>e.Id==id);
        }
        public async Task<Event> AddAsync(Event newEvent)
        {
           await _ctx.Events.AddAsync(newEvent);
            await _ctx.SaveChangesAsync();
            return newEvent;
        }
        public Event Update(Event updatedEvent)
        {
            var oldEvent = _ctx.Events.AsNoTrackingWithIdentityResolution().FirstOrDefault(e => e.Id == updatedEvent.Id);
            updatedEvent.CreatedBy = oldEvent.CreatedBy;
            _ctx.Events.Update(updatedEvent);
            _ctx.SaveChanges();
            return updatedEvent;
        }
        public Event Remove(int id)
        {
            var eventToBeRemoved=_ctx.Events.FirstOrDefault(e=>e.Id==id);
            _ctx.Remove(eventToBeRemoved);
            _ctx.SaveChanges();
            return eventToBeRemoved;
        }
    }
}
