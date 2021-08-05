using FullCalenderDemo.Models;
using FullCalenderDemo.Repos.Interfaces;
using FullCalenderDemo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Services
{
    public class CustomerService:ICustomerService
    {
        ICustomerRepo _CustomerRepo;
        public CustomerService(ICustomerRepo CustomerRepo)
        {
            _CustomerRepo = CustomerRepo;
        }
        public IEnumerable<Customer> GetAll()
        {
            return _CustomerRepo.GetAll();
        }
        public Customer GetById(int id)
        {
            return _CustomerRepo.GetById(id);
        }
        public async Task<Customer> AddAsync(Customer newCustomer)
        {
            return await _CustomerRepo.AddAsync(newCustomer);

        }
        public Customer Update(Customer updatedCustomer)
        {
            return _CustomerRepo.Update(updatedCustomer);

        }
        public Customer Remove(int id)
        {
            return _CustomerRepo.Remove(id);
        }
    }
}
