using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public interface IIngredientRepository
    {
        IQueryable<Ingredient> Ingredients { get; }

        void SaveIngredient(Ingredient ingredient);
    }
}
