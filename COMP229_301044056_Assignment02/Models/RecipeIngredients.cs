using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace COMP229_301044056_Assignment02.Models
{
    // not sure we will structure this way
    public class RecipeIngredients
    {
        private List<IngredientLine> lineCollection = new List<IngredientLine>();
        public virtual void AddItem(Ingredient ingredient, int quantity)
        {
            IngredientLine line = lineCollection
            .Where(p => p.IngredientID == ingredient.IngredientID)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new IngredientLine
                {
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Ingredient ingredient) =>
        lineCollection.RemoveAll(l => l.IngredientID == ingredient.IngredientID);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<IngredientLine> Lines => lineCollection;
    }
    
}
