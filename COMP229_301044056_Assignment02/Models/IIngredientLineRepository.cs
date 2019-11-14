using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public interface IIngredientLineRepository
    {
        IQueryable<IngredientLine> Lines { get; }

        void DeleteIngredientLine(int ingredientLineID);
    }
}
