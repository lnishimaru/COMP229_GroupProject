using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP229_301044056_Assignment02.Models;
using COMP229_301044056_Assignment02.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace COMP229_301044056_Assignment02.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIngredientRepository repository;
        private IRecipeRepository recipeRepo;
        private IIngredientLineRepository lineRepo;
        private IMeasureRepository measureRepo;

        public HomeController(IIngredientRepository repoService, IRecipeRepository repoRecipe, IIngredientLineRepository repoLine, IMeasureRepository repoMeasure)
        {
            repository = repoService;
            recipeRepo = repoRecipe;
            lineRepo = repoLine;
            measureRepo = repoMeasure;
        }
        public int FindIngredient(string ingredientName)
        {
            var query1 = from a in repository.Ingredients
                         where a.IngredientName == ingredientName
                         orderby a.IngredientName
                         select a.IngredientID;

            int result = query1.FirstOrDefault();

            if (result == 0)
            {
                repository.SaveIngredient(new Ingredient { IngredientName = ingredientName });
                var query3 = from a in repository.Ingredients
                             where a.IngredientName == ingredientName
                             orderby a.IngredientName
                             select a.IngredientID;

                result = query3.FirstOrDefault();
            }

            return result;
        }
        public int FindMeasure(string measureDesc)
        {
            var query2 = from b in measureRepo.Measures
                         where b.MeasureDesc == measureDesc
                         orderby b.MeasureDesc
                         select b.MeasureID;

            int result = query2.FirstOrDefault();

            if (result == 0)
            {
                measureRepo.SaveMeasure(new Measure { MeasureDesc = measureDesc });
                var query3 = from b in measureRepo.Measures
                             where b.MeasureDesc == measureDesc
                             orderby b.MeasureDesc
                             select b.MeasureID;

                result = query3.FirstOrDefault();
            }

            return result;
        }
        public ViewResult DisplayPage(int recipeID)
        {
            RecipeViewModel recipeView = new RecipeViewModel();
            recipeView.Line = new List<IngredientLineDetail>();
            Measure measure = new Measure();

            var query = from p in recipeRepo.Recipes
                        where p.ID == recipeID
                        orderby p.ID
                        select p;

            foreach (var recipe in query)
            {
                recipeView.Name = recipe.Name;
                recipeView.Instructions = recipe.Instructions;
                recipeView.ID = recipe.ID;
                recipeView.Category = recipe.Category;
                recipeView.Cuisine = recipe.Cuisine;
                recipeView.Comments = recipe.Comments;
                
                if (recipe.Photo == null)
                {
                    recipeView.Photo = "~/default.jpg";
                }
                else
                {
                    recipeView.Photo = recipe.Photo;
                };
                   

                System.Diagnostics.Debug.WriteLine(recipe.Name);
                var query2 = from q in lineRepo.Lines
                             where q.RecipeID == recipe.ID
                             orderby q.IngredientLineID
                             select q;

                foreach (var line in query2)
                {
                    System.Diagnostics.Debug.WriteLine(line.IngredientID);
                    var query4 = from s in measureRepo.Measures
                                 where s.MeasureID == line.MeasureID
                                 select s;

                    foreach (var o in query4)
                    {
                        System.Diagnostics.Debug.WriteLine(o.MeasureDesc);
                        measure = new Measure { MeasureID = o.MeasureID, MeasureDesc = o.MeasureDesc };
                    }
                    var query3 = from r in repository.Ingredients
                                 where r.IngredientID == line.IngredientID
                                 select r;

                    foreach (var m in query3)
                    {
                        System.Diagnostics.Debug.WriteLine(m.IngredientName);
                        recipeView.Line.Add(new IngredientLineDetail
                        {
                            IngredientLineID = line.IngredientLineID,
                            Quantity = line.Quantity,
                            Measure = new Measure { MeasureID = measure.MeasureID, MeasureDesc = measure.MeasureDesc },
                            RecipeID = line.RecipeID,
                            Ingredient = new Ingredient
                            {
                                IngredientID = line.IngredientID,
                                IngredientName = m.IngredientName
                            }
                        });
                    }
                }
            }
            return View(recipeView);
        }
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult DataPage() =>
            View(recipeRepo.Recipes.Where(o => o.ID > 0));
       
    }
}