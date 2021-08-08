using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo.DataBaseContext;
using MongoDBDemo.Models;
using MongoDBDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.Repos
{
    public class ProjectRepo: IProjectRepo
    {
        private MongoCollectionBase<Project> _projectCollection { get; set; }
        private ProjectSearchVM _searchObj { get; set; }
        public ProjectRepo(IDBContext db)
        {
           
            _projectCollection = (MongoCollectionBase<Project>)db.DBCTX.GetCollection<Project>(Project.CollectionName);
        }
        public IEnumerable<Project> GetAll( )
        {
            return _projectCollection.Find<Project>(c=>true).ToList();
        }
        public Project GetById(string id)
        {
            return _projectCollection.Find<Project>(p => p.Id==id).SingleOrDefault<Project>();
        }
        public Project Create(Project newProject)
        {
            _projectCollection.InsertOne(newProject);
            return newProject;
        }
        public Project Update(Project updatedProject)
        {
            var result = _projectCollection.ReplaceOne(p => p.Id == updatedProject.Id, updatedProject);
            return updatedProject;
        }
        public Project Delete(string id)
        {
            return _projectCollection.FindOneAndDelete(p => p.Id == id);
            
        }
        public IEnumerable<Project> GetProjectBySkillsNames(IEnumerable<string> skillsNames)
        {
            return _projectCollection.Find(Builders<Project>.Filter.ElemMatch(p => p.Skills,s => skillsNames.Contains(s.Name))).ToList();
        }
        public IEnumerable<Project> GetProjectByRateValue(decimal minValue,decimal maxValue)
        {
            return _projectCollection.Find(Builders<Project>.Filter.ElemMatch(p => p.Skills, s => s.Rate<=maxValue && s.Rate>=minValue)).ToList();
        }

        public IEnumerable<Project> Search(ProjectSearchVM searchObj)
        {
           
            var projectFilter = PrepareSearchFilter(searchObj);

            return _projectCollection.Find(projectFilter).ToList();

        }
        public IEnumerable<SkillsStatsVM> GetSkillsStats(ProjectSearchVM searchObj)
        {
            var pipeline = _projectCollection
                                        .Aggregate()
                                        .Unwind<Project, UnwindedProject>(p => p.Skills)
                                        .Group(
                                                p => p.Skills.Name,
                                                s =>
                                                    new SkillsStatsVM
                                                    {
                                                        Name = s.Key,
                                                        Count = s.Count(),
                                                        Min = s.Min(ss => ss.Skills.Rate),
                                                        Max = s.Max(ss => ss.Skills.Rate),
                                                        Average = s.Average(ss => ss.Skills.Rate),
                                                    }
                                              );
            if (searchObj.SkillsNames==null)
            {

                return pipeline.ToList<SkillsStatsVM>();
            }
            else
            {
                return pipeline.Match(p=>searchObj.SkillsNames.Contains(p.Name)).ToList<SkillsStatsVM>();
            }
        }

        private FilterDefinition<Project> PrepareSearchFilter(ProjectSearchVM searchObj)
        {
                var skillFilterList = new List<FilterDefinition<Skill>>();
                if (searchObj.SkillsNamesOperator == LogicalOperators.OR)
                {
                    skillFilterList.Add(Builders<Skill>.Filter.In(s => s.Name, searchObj.SkillsNames));
                }
                else if (searchObj.SkillsNamesOperator == LogicalOperators.STATS)
                {

                }
                else
                {
                    foreach (var skillName in searchObj.SkillsNames)
                    {
                        skillFilterList.Add(Builders<Skill>.Filter.Eq(s => s.Name, skillName));
                    }
                    if (searchObj.SkillsNamesOperator == LogicalOperators.ONLY)
                    {
                        var skillNameFilter = Builders<Skill>.Filter.And(skillFilterList);
                        skillFilterList = new List<FilterDefinition<Skill>> { skillNameFilter };
                    }

                }

                var skillMaxRateFilter = Builders<Skill>.Filter.Lte(s => s.Rate, searchObj.MaxRate);
                skillFilterList.Add(skillMaxRateFilter);
                var skillMinRateFilter = Builders<Skill>.Filter.Gte(s => s.Rate, searchObj.MinRate);
                skillFilterList.Add(skillMinRateFilter);

                var skillFiltersWithAnd = Builders<Skill>.Filter.And(skillFilterList);

                 var projectFilter = Builders<Project>.Filter.ElemMatch(p => p.Skills, skillFiltersWithAnd);
                if (searchObj.SkillsNamesOperator == LogicalOperators.ONLY)
                {
                    //projectFilter=projectFilter & Builders<Project>.Filter.Size(p => p.Skill, skillFiltersWithAnd);
                }
            return projectFilter;
        }
       
    }
}
