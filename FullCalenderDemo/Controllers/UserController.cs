using AutoMapper;
using FullCalenderDemo.Areas.Identity.Data;
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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserController : Controller
    {
        IUserService _userService;
        IMapper _autoMapper;
        public UserController(IUserService userService,IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _userService = userService;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var allUsers = _userService.GetAll();
            IEnumerable<UserVM> allUserVM = _autoMapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserVM>>(allUsers);
            if (allUserVM.Count() == allUsers.Count())
            {
                for (int i = 0; i < allUsers.Count(); i++)
                {
                    await _userService.MapUserVMRoles(allUsers.ElementAt(i), allUserVM.ElementAt(i));
                }
            }
            return View(allUserVM);
        }
       
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            var createUserVM = new CreateUserVM();
            createUserVM.Roles=Enum.GetValues<Roles>().ToList();
            return View(createUserVM);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserVM newUser)
        {
            try
            {
                await _userService.AddAsync(newUser);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userService.GetById(id);
            var vm = _autoMapper.Map<UserVM>(user);
            await _userService.MapUserVMRoles(user, vm);
            ViewBag.Roles = Enum.GetValues<Roles>().ToList();
            return View(vm);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserVM userVM)
        {
            try
            {
                await _userService.Update(userVM);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
