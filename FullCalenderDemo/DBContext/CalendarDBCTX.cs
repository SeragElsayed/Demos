using FullCalenderDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.DBContext
{
    public class CalendarDBCTX: DbContext
    {
        public CalendarDBCTX(DbContextOptions<CalendarDBCTX> options)
           : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
