using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBDemo.Models;
using MongoDBDemo.Repos;
using MongoDBDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        public IProjectRepo _projectRepo { get; set; }
        public ProjectController(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }
        [HttpGet("~/Index")]
        public ActionResult Index()
        {
            var allProjects = _projectRepo.GetAll();
            return Ok(allProjects);
        }
        [HttpGet]

        // GET: ProjectController/Details/5
        public ActionResult Details(string id)
        {
            var project = _projectRepo.GetById(id);
            return Ok(project);
        }


        // POST: ProjectController/Create

        [HttpPost]
        public ActionResult Create(Project newProject)
        {
            try
            {
                var createdProject = _projectRepo.Create(newProject);
                return Created("Project", createdProject);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }


        // POST: ProjectController/Edit/5
        [HttpPut]
        public ActionResult Edit(Project editedProject)
        {
            try
            {
                return Ok(_projectRepo.Update(editedProject));
            }
            catch
            {
                return NotFound();
            }
        }

      
        // POST: ProjectController/Delete/5
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            try
            {
                return Ok(_projectRepo.Delete(id));
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("~/GetProjectBySkillsRate")]
        public ActionResult GetProjectBySkillsRate(decimal minValue, decimal maxValue)
        {
            var projects = _projectRepo.GetProjectByRateValue(minValue,maxValue);
            return Ok(projects);
        }
        [HttpPost("~/GetProjectBySkillsNames")]
        public ActionResult GetProjectBySkillsNames(IEnumerable<string> skillsNames)
        {
            var projects = _projectRepo.GetProjectBySkillsNames( skillsNames);
            return Ok(projects);
        }
        [HttpPost("~/Search")]
        public ActionResult Search(ProjectSearchVM searchObj)
        {
            var projects = _projectRepo.Search(searchObj);
            return Ok(projects);
        }
    }
}
