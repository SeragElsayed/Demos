using FullCalenderDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Repos
{
    public interface IEventRepo
    {
        IEnumerable<Event> GetAll();

        Event GetById(int id);

        Task<Event> AddAsync(Event newEvent);

        Event Update(Event updatedEvent);

        Event Remove(int id);
    }
}
