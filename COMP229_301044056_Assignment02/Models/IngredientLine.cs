using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public class IngredientLine
    {
        public int IngredientLineID { get; set; }
        public int IngredientID { get; set; }
        public int Quantity { get; set; }
        public int MeasureID { get; set; }
        public int RecipeID { get; set; }
    }
}
