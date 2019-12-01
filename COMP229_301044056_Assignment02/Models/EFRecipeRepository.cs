using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP229_301044056_Assignment02.Models
{
    public class EFRecipeRepository : IRecipeRepository
    {
        private ApplicationDbContext context;

        public EFRecipeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Recipe> Recipes => context.Recipes;

        public void SaveRecipe(Recipe recipe)
        {
            if (recipe.RecipeID == 0)
            {
                context.Recipes.Add(recipe);
                System.Diagnostics.Debug.WriteLine("Adding Recipe");
            }
            else
            {
                Recipe dbEntry = context.Recipes
                .FirstOrDefault(p => p.RecipeID == recipe.RecipeID);
                if (dbEntry != null)
                {
                    System.Diagnostics.Debug.WriteLine("Editing Recipe");
                    dbEntry.RecipeID = recipe.RecipeID;
                    dbEntry.Name = recipe.Name;
                    dbEntry.Category = recipe.Category;
                    dbEntry.Cuisine = recipe.Cuisine;
                    dbEntry.Instructions = recipe.Instructions;
                    dbEntry.Photo = recipe.Photo;
                    dbEntry.UserId = recipe.UserId;
                    dbEntry.Date = DateTime.Now;
                    dbEntry.Lines = recipe.Lines;
                }
            }
            context.SaveChanges();
        }
        public Recipe DeleteRecipe(int recipeID)
        {
            Recipe dbEntry = context.Recipes
            .FirstOrDefault(p => p.RecipeID == recipeID);
            if (dbEntry != null)
            {
                context.Recipes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
