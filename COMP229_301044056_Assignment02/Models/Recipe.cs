using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Cuisine { get; set; }
        public string Instructions { get; set; }
        public string Photo { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public List<IngredientLine> Lines { get; set; }
    }
}
