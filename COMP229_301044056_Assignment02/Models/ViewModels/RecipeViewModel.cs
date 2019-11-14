using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models.ViewModels
{
    public class RecipeViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Cuisine { get; set; }
        public string Instructions { get; set; }
        public string Comments { get; set; }
        public string Photo { get; set; }
        public List<IngredientLineDetail> Line { get; set; }
    }
}
