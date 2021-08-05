using FullCalenderDemo.Models;
using FullCalenderDemo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Services
{
    public class EventService: IEventService
    {
        IEventRepo _eventRepo;
        public EventService(IEventRepo eventRepo)
        {
            _eventRepo = eventRepo;
        }
        public IEnumerable<Event> GetAll()
        {
            return _eventRepo.GetAll();
        }
        public Event GetById(int id)
        {
            return _eventRepo.GetById( id);
        }
        public async Task<Event> AddAsync(Event newEvent)
        {
            return await _eventRepo.AddAsync(newEvent);

        }
        public Event Update(Event updatedEvent)
        {
            return _eventRepo.Update(updatedEvent);

        }
        public Event Remove(int id)
        {
            return _eventRepo.Remove(id);
        }
    }
}
