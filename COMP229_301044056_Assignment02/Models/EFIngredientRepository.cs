using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace COMP229_301044056_Assignment02.Models
{
    public class EFIngredientRepository: IIngredientRepository
    {
        private ApplicationDbContext context;

        public EFIngredientRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Ingredient> Ingredients => context.Ingredients;
        public void SaveIngredient(Ingredient ingredient)
        {
            System.Diagnostics.Debug.WriteLine("Save Ingredient");

            if (ingredient.IngredientID == 0)
            {
                context.Ingredients.Add(ingredient);
            }
            context.SaveChanges();
        }
    }
}
