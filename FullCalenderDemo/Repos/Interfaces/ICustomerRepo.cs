using FullCalenderDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Repos.Interfaces
{
    public interface ICustomerRepo
    {
        IEnumerable<Customer> GetAll();

        Customer GetById(int id);

        Task<Customer> AddAsync(Customer newCustomer);

        Customer Update(Customer updatedCustomer);

        Customer Remove(int id);
    }
}
