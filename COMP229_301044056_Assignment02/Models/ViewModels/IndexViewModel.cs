using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public int Count { get; set; }
        public string FigName { get; set; }
    }
}
