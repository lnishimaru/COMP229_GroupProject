using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public interface IRecipeCommentRepository
    {
        IQueryable<RecipeComment> RecipeComments { get; }
        void SaveRecipeComment(RecipeComment comments);
        RecipeComment DeleteRecipeComment(int recipeCommentId);
    }
}
