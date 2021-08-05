using FullCalenderDemo.Models;
using FullCalenderDemo.Services.Interfaces;
using FullCalenderDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class CustomerController : Controller
    {
        ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            var customers = _customerService.GetAll();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        public async Task<ActionResult> Create(Customer newCustomer)
        {
            try
            {
                await _customerService.AddAsync(newCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer=_customerService.GetById(id);
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer editedCustomer)
        {
            try
            {
                _customerService.Update(editedCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: CustomerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CustomerController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _customerService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
