using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public class IngredientLineDetail
    {
        public int IngredientLineID { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
        public Measure Measure { get; set; }
        public int RecipeID { get; set; }
    }
}
