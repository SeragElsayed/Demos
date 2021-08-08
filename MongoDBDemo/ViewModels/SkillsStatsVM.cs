using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.ViewModels
{
    public class SkillsStatsVM
    {
        public string Name { get; set; }
        public decimal Average { get; set; }
        public int Count { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
    }
}
