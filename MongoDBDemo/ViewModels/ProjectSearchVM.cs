using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.ViewModels
{
    public class ProjectSearchVM
    {
        public List<string> SkillsNames { get; set; }
        public LogicalOperators SkillsNamesOperator { get; set; }
        public decimal? MaxRate { get; set; }
        public decimal? MinRate { get; set; }
    }
    public enum LogicalOperators
    {
        AND,OR,ONLY,STATS
    }
}
