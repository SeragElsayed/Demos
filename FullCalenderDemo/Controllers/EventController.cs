using FullCalenderDemo.Models;
using FullCalenderDemo.Services;
using FullCalenderDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FullCalenderDemo.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        IEventService _eventService;
        ICustomerService _customerService;
        IServiceService _serviceService;
        public EventController(IEventService eventService,ICustomerService customerService,IServiceService serviceService)
        {
            _eventService = eventService;
            _customerService = customerService;
            _serviceService = serviceService;
        }
        public ActionResult GetAllEvents([FromQuery]bool returnList=false)
        {
            var allEvnets = _eventService.GetAll();
            if (returnList == false)
                return View(allEvnets);
            else
                return Ok(allEvnets);
        }
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        // GET: Calendar/Create
        public ActionResult Create()
        {
            ViewBag.Customers = _customerService.GetAll();
            ViewBag.Services = _serviceService.GetAll();
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        // POST: Calendar/Create
        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent)
        {
            try
            {
                var currentUserName = User.FindFirst(ClaimTypes.Name).Value;
                newEvent.CreatedBy = currentUserName;
                var createdEvent = await _eventService.AddAsync(newEvent);
                return RedirectToAction(nameof(GetAllEvents)); 
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        // GET: Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            var eventToBeEdited = _eventService.GetById(id);
            ViewBag.Customers = _customerService.GetAll();
            ViewBag.Services = _serviceService.GetAll();
            return View(eventToBeEdited);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        // POST: Calendar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Event editedEvent)
        {
            try
            {
                
                _eventService.Update(editedEvent);
                return RedirectToAction(nameof(GetAllEvents));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Calendar/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}
        [Authorize(Roles = "Admin,SuperAdmin")]
        // POST: Calendar/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _eventService.Remove(id);
                return RedirectToAction(nameof(GetAllEvents));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public ActionResult IsEndTimeValid(DateTime endTime, DateTime startTime)
        {
            return Ok(endTime>startTime);
        }
    }
}
