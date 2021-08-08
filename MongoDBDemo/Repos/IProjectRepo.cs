using MongoDBDemo.Models;
using MongoDBDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Repos
{
    public interface IProjectRepo
    {
        IEnumerable<Project> GetAll();
        Project GetById(string id);
        Project Create(Project newProject);
        Project Update(Project updatedProject);
        Project Delete(string id);
        IEnumerable<Project> GetProjectBySkillsNames(IEnumerable<string> skillsNames);
        IEnumerable<Project> GetProjectByRateValue(decimal minValue, decimal maxValue);
        IEnumerable<Project> Search(ProjectSearchVM searchObj);
        IEnumerable<SkillsStatsVM> GetSkillsStats(ProjectSearchVM searchObj);
    }
}
