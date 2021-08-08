using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBDemo.Models;
using MongoDBDemo.Repos;
using MongoDBDemo.ViewModels;
using MongoDBDemo.Wrappers;
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
            //throw new Exception("test exception");
            var allProjects = _projectRepo.GetAll();
            return Ok(new OperationResult<IEnumerable<Project>>(allProjects));
        }
        [HttpGet]

        // GET: ProjectController/Details/5
        public ActionResult Details(string id)
        {
            var project = _projectRepo.GetById(id);
            return Ok(new OperationResult<Project>( project));
        }


        // POST: ProjectController/Create

        [HttpPost]
        public ActionResult Create(Project newProject)
        {
            try
            {
                var createdProject = _projectRepo.Create(newProject);
                return Ok(new OperationResult<Project>(createdProject));
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
                var updatedProject = _projectRepo.Update(editedProject);
                return Ok(new OperationResult<Project>(updatedProject));
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
                var deletedProject = _projectRepo.Delete(id);
                return Ok(new OperationResult<Project>(deletedProject));
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
            return Ok(new OperationResult<IEnumerable<Project>>(projects));
        }
        [HttpPost("~/GetProjectBySkillsNames")]
        public ActionResult GetProjectBySkillsNames(IEnumerable<string> skillsNames)
        {
            var projects = _projectRepo.GetProjectBySkillsNames( skillsNames);
            return Ok(new OperationResult<IEnumerable<Project>>(projects));
        }
        [HttpPost("~/Search")]
        public ActionResult Search(ProjectSearchVM searchObj)
        {
            if (searchObj.SkillsNamesOperator == LogicalOperators.STATS)
            {
                var skills = _projectRepo.GetSkillsStats(searchObj);
                return Ok(new OperationResult<IEnumerable<SkillsStatsVM>>(skills));
            }
            var projects = _projectRepo.Search(searchObj);
            return Ok(new OperationResult<IEnumerable<Project>>(projects));
        }
    }
}
