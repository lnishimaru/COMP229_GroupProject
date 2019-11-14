using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP229_301044056_Assignment02.Models;
using COMP229_301044056_Assignment02.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace COMP229_301044056_Assignment02.Controllers
{
    public class BaseController : Controller
    {
        private readonly IIngredientRepository repository;
        private IRecipeRepository recipeRepo;
        private IIngredientLineRepository lineRepo;
        private IMeasureRepository measureRepo;

        public BaseController(IIngredientRepository repoService, IRecipeRepository repoRecipe, IIngredientLineRepository repoLine, IMeasureRepository repoMeasure)
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
   
        [HttpGet]
        public ActionResult InsertPage()
        {
            InsertPageViewModel model = new InsertPageViewModel
            {
                RecipeVM = new Recipe(),
                CollectLine = new List<IngredientLineDetail>() { new IngredientLineDetail(),
                                                                 new IngredientLineDetail(),
                                                                 new IngredientLineDetail(),
                                                                 new IngredientLineDetail(),
                                                                 new IngredientLineDetail()},
                Line = new IngredientLine(),
                Ingredient = new List<Ingredient>(),
                Measure = new List<Measure>()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult InsertPage(InsertPageViewModel recipe)
        {
            if (recipe.RecipeVM.Name != null)
            {
                recipe.RecipeVM.Comments = recipe.Comments;
                recipe.Photo = recipe.Photo;

                foreach (IngredientLineDetail x in recipe.CollectLine)
                {
                    System.Diagnostics.Debug.WriteLine(x.Ingredient.IngredientName);
                    recipe.Line = new IngredientLine();
                    if (x.Ingredient.IngredientName != null)
                    {
                        int ingrID = FindIngredient(x.Ingredient.IngredientName);
                        recipe.Line.IngredientID = ingrID;
                    }
                    if (x.Measure.MeasureDesc != null)
                    {
                        int measID = FindMeasure(x.Measure.MeasureDesc);
                        recipe.Line.MeasureID = measID;
                    }
                    if (recipe.Line.MeasureID != 0 && recipe.Line.IngredientID != 0)
                    {
                        recipe.Line.Quantity = x.Quantity;
                        System.Diagnostics.Debug.WriteLine("deletando: ", x.IngredientLineID);
                        lineRepo.DeleteIngredientLine(x.IngredientLineID);
                        recipe.RecipeVM.Lines.Add(new IngredientLine { IngredientID = recipe.Line.IngredientID, MeasureID = recipe.Line.MeasureID, Quantity = x.Quantity, RecipeID = 0, IngredientLineID = 0 });
                    }
                }

                System.Diagnostics.Debug.WriteLine("Insert Recipe");
                System.Diagnostics.Debug.WriteLine(recipe.RecipeVM.Name);
                System.Diagnostics.Debug.WriteLine(recipe.Line.Quantity);

                recipeRepo.SaveRecipe(recipe.RecipeVM);
                TempData["message"] = $"{recipe.RecipeVM.Name} has been saved";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = $"Please inform the Recipe's name";
                return View();
            }
        }
     
        public ViewResult UserPage(int ID)
        {
            InsertPageViewModel insertView = new InsertPageViewModel();
            insertView.RecipeVM = new Recipe
            {
                Lines = new List<IngredientLine>()
            };
            insertView.CollectLine = new List<IngredientLineDetail>();
            insertView.Line = new IngredientLine();
            insertView.Ingredient = new List<Ingredient>();
            insertView.Measure = new List<Measure>();
            insertView.Comments = "";
            insertView.Photo = "";

            Measure measure = new Measure();

            var query = from p in recipeRepo.Recipes
                        where p.ID == ID
                        orderby p.ID
                        select p;

            foreach (var recipe in query)
            {
                insertView.RecipeVM.Name = recipe.Name;
                insertView.RecipeVM.Instructions = recipe.Instructions;
                insertView.RecipeVM.ID = recipe.ID;
                insertView.RecipeVM.Category = recipe.Category;
                insertView.RecipeVM.Cuisine = recipe.Cuisine;
                insertView.Comments = recipe.Comments;
                insertView.Photo = recipe.Photo;
                insertView.CollectLine = new List<IngredientLineDetail>() { };

                var query2 = from q in lineRepo.Lines
                             where q.RecipeID == recipe.ID
                             orderby q.IngredientLineID
                             select q;

                foreach (var line in query2)
                {
                    var query4 = from s in measureRepo.Measures
                                 where s.MeasureID == line.MeasureID
                                 select s;

                    foreach (var o in query4)
                    {
                        measure = new Measure { MeasureID = o.MeasureID, MeasureDesc = o.MeasureDesc };
                    }
                    var query3 = from r in repository.Ingredients
                                 where r.IngredientID == line.IngredientID
                                 select r;

                    foreach (var m in query3)
                    {
                        insertView.CollectLine.Add(new IngredientLineDetail
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
            return View(insertView);
        }
        public IActionResult Delete(int ID)
        {
            System.Diagnostics.Debug.WriteLine("Delete Recipe", ID);
            Recipe deletedRecipe = recipeRepo.DeleteRecipe(ID);
            if (deletedRecipe != null)
            {
                TempData["message"] = $"{deletedRecipe.Name} was deleted";
            }
            return RedirectToAction("DataPage","Home");
        }
        public ViewResult Edit(int ID)
        {
            InsertPageViewModel insertView = new InsertPageViewModel();
            Measure measure = new Measure();

            var query = from p in recipeRepo.Recipes
                        where p.ID == ID
                        orderby p.ID
                        select p;

            foreach (var recipe in query)
            {
                insertView.RecipeVM.Name = recipe.Name;
                insertView.RecipeVM.Instructions = recipe.Instructions;
                insertView.RecipeVM.ID = recipe.ID;
                insertView.RecipeVM.Category = recipe.Category;
                insertView.RecipeVM.Cuisine = recipe.Cuisine;
                insertView.Comments = recipe.Comments;
                insertView.Photo = recipe.Photo;
                insertView.CollectLine = new List<IngredientLineDetail>() { };

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
                        insertView.CollectLine.Add(new IngredientLineDetail
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
            for (int i = insertView.CollectLine.Count; i < 5; i++)
            {
                insertView.CollectLine.Add(new IngredientLineDetail());
            }
            return View(insertView);
        }
    }
}