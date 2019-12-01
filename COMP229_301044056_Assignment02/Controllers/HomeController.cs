using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP229_301044056_Assignment02.Models;
using COMP229_301044056_Assignment02.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP229_301044056_Assignment02.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIngredientRepository repository;
        private IRecipeRepository recipeRepo;
        private IIngredientLineRepository lineRepo;
        private IMeasureRepository measureRepo;
        private IRecipeCommentRepository commentRepo;

        public HomeController(IIngredientRepository repoService, IRecipeRepository repoRecipe, IIngredientLineRepository repoLine, IMeasureRepository repoMeasure,
            IRecipeCommentRepository repoComment)
        {
            repository = repoService;
            recipeRepo = repoRecipe;
            lineRepo = repoLine;
            measureRepo = repoMeasure;
            commentRepo = repoComment;
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
        public ViewResult DisplayPage(int RecipeID)
        {
            RecipeViewModel recipeView = new RecipeViewModel();
            recipeView.Line = new List<IngredientLineDetail>();
            Measure measure = new Measure();
            recipeView.Comments = new List<RecipeComment>();

            var query = from p in recipeRepo.Recipes
                        where p.RecipeID == RecipeID
                        orderby p.RecipeID
                        select p;

            foreach (var recipe in query)
            {
                recipeView.Name = recipe.Name;
                recipeView.Instructions = recipe.Instructions;
                recipeView.RecipeID = recipe.RecipeID;
                recipeView.Category = recipe.Category;
                recipeView.Cuisine = recipe.Cuisine;

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
                             where q.RecipeID == recipe.RecipeID
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
                var query5 = from s in commentRepo.RecipeComments
                             where s.RecipeID == RecipeID
                             orderby s.RecipeID
                             select s;
                foreach (RecipeComment s in query5)
                {
                    recipeView.Comments.Add(s);
                }
            }
            return View(recipeView);
        }
        //public ViewResult Index() =>
        //    View(recipeRepo.Recipes.Where(o => o.RecipeID > 0).OrderByDescending(o => o.Date).Take(3));
        public ViewResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.Recipes = new List<Recipe>();
            model.Count = 0;
            model.FigName = "";

            var query = (from p in recipeRepo.Recipes
                         where p.RecipeID > 0
                         orderby p.Date descending
                         select p).Take(3);

            foreach (var recipe in query)
            {
                model.Recipes.Add(new Recipe
                {
                    Name = recipe.Name,
                    Instructions = recipe.Instructions,
                    RecipeID = recipe.RecipeID,
                    Category = recipe.Category,
                    Cuisine = recipe.Cuisine,
                    Photo = recipe.Photo
                });
            }
         
            return View("Index", model);
        }

        public IActionResult DataPage(string searchBy, string search, string Category)
        {
         
            if (Category != null)
            {
                return View(recipeRepo.Recipes.Where(o => o.RecipeID > 0).Where(x => x.Category == Category));
            }
            
            if (searchBy == "Ingredient" && !string.IsNullOrEmpty(search))
            {
                var list = search.Split(" ");

                var recipe = from r in recipeRepo.Recipes
                             join n in lineRepo.Lines on r.RecipeID equals n.RecipeID
                             join i in repository.Ingredients on n.IngredientID equals i.IngredientID
                             where list.Any(a => i.IngredientName.ToLower().Contains(a.ToLower()))
                             select r;

                return View(recipe.Distinct());

            }
            else if (searchBy == "Name" && !string.IsNullOrEmpty(search))
            {
                return View(recipeRepo.Recipes.Where(x => x.Name.ToLower().Contains(search.ToLower())));
            }
            else
            {
                return View(recipeRepo.Recipes.Where(o => o.RecipeID > 0));
            }




        }
    }
}