using FullCalenderDemo.DBContext;
using FullCalenderDemo.Models;
using FullCalenderDemo.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Repos
{
    public class CustomerRepo:ICustomerRepo
    {
        CalendarDBCTX _ctx;
        public CustomerRepo(CalendarDBCTX ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _ctx.Customers.ToList();
        }
        public Customer GetById(int id)
        {
            return _ctx.Customers.FirstOrDefault(e => e.Id == id);
        }
        public async Task<Customer> AddAsync(Customer newCustomer)
        {
            await _ctx.Customers.AddAsync(newCustomer);
            await _ctx.SaveChangesAsync();
            return newCustomer;
        }
        public Customer Update(Customer updatedCustomer)
        {
            _ctx.Customers.Update(updatedCustomer);
            _ctx.SaveChanges();
            return updatedCustomer;
        }
        public Customer Remove(int id)
        {
            var CustomerToBeRemoved = _ctx.Customers.FirstOrDefault(e => e.Id == id);
            _ctx.Remove(CustomerToBeRemoved);
            _ctx.SaveChanges();
            return CustomerToBeRemoved;
        }
    }
}
