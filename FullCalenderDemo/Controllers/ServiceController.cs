using FullCalenderDemo.Models;
using FullCalenderDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ServiceController : Controller
    {
        IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        // GET: ServiceController
        public ActionResult Index()
        {
            var services=_serviceService.GetAll();
            return View(services);
        }

        // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceController/Create
        [HttpPost]
        public async Task<ActionResult> Create(Service newService)
        {
            try
            {
                await _serviceService.AddAsync(newService);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceController/Edit/5
        public ActionResult Edit(int id)
        {
            var service = _serviceService.GetById(id);
            return View(service);
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service editedService)
        {
            try
            {
                _serviceService.Update(editedService);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: ServiceController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ServiceController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _serviceService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
