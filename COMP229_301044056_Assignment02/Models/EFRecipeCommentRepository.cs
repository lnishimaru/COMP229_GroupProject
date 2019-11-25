using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP229_301044056_Assignment02.Models;

namespace COMP229_301044056_Assignment02.Models
{
    public class EFRecipeCommentRepository: IRecipeCommentRepository
    {
        private ApplicationDbContext context;

        public EFRecipeCommentRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<RecipeComment> RecipeComments => context.RecipeComments;

        public void SaveRecipeComment(RecipeComment recipeComment)
        {
            if (recipeComment.RecipeCommentId == 0)
            {
                context.RecipeComments.Add(recipeComment);
                System.Diagnostics.Debug.WriteLine("Adding Recipe Comment");
            }
            else
            {
                RecipeComment dbEntry = context.RecipeComments
                .FirstOrDefault(p => p.RecipeCommentId == recipeComment.RecipeCommentId);
                if (dbEntry != null)
                {
                    System.Diagnostics.Debug.WriteLine("Editing Recipe");
                    dbEntry.RecipeCommentId = recipeComment.RecipeCommentId;
                    dbEntry.Comments = recipeComment.Comments;
                    dbEntry.UserId = recipeComment.UserId;
                    dbEntry.CommentDateTime = DateTime.Today;
                }
            }
            context.SaveChanges();
        }
        public RecipeComment DeleteRecipeComment(int recipeCommentId)
        {
            RecipeComment dbEntry = context.RecipeComments
            .FirstOrDefault(p => p.RecipeCommentId == recipeCommentId);
            if (dbEntry != null)
            {
                context.RecipeComments.Remove(dbEntry);
                //context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
