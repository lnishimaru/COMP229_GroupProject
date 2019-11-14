using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using COMP229_301044056_Assignment02.Models;

namespace COMP229_301044056_Assignment02.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
            .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Measures.Any())
            {
                context.Measures.AddRange(

                new Measure
                {
                    MeasureDesc = "cup"
                }, 
                new Measure
                {
                    MeasureDesc = "tablespoon"
                }, 
                new Measure
                {
                    MeasureDesc = "teaspoon"
                },
                new Measure
                {
                    MeasureDesc = "pinch"
                },
                new Measure
                {
                    MeasureDesc = "gr"
                },
                new Measure
                {
                    MeasureDesc = "kg"
                },
                new Measure
                {
                    MeasureDesc = "unit"
                },
                new Measure
                {
                    MeasureDesc = "package"
                },
                new Measure
                {
                    MeasureDesc = "litre"
                },
                new Measure
                {
                    MeasureDesc = "to taste"
                }
                );
                context.SaveChanges();
            }
            if (!context.Ingredients.Any())
            {
                context.Ingredients.AddRange(
                
                new Ingredient
                {
                    IngredientName = "Milk"
                }, new Ingredient
                {
                    IngredientName = "Wheat flour"
                }, new Ingredient
                {
                    IngredientName = "Sugar"
                }, new Ingredient
                {
                    IngredientName = "Salt"
                }, new Ingredient
                {
                    IngredientName = "Pepper"
                }, new Ingredient
                {
                    IngredientName = "Baking powder"
                }, new Ingredient
                {
                    IngredientName = "Onion"
                }, new Ingredient
                {
                    IngredientName = "Butternut Squash"
                }, new Ingredient
                {
                    IngredientName = "Chicken Stock"
                }, new Ingredient
                {
                    IngredientName = "Celery"
                }, new Ingredient
                {
                    IngredientName = "Heavy Cream"
                }, new Ingredient
                {
                    IngredientName = "Vanilla Extract"
                }, new Ingredient
                {
                    IngredientName = "Unflavored Gelatin"
                }, new Ingredient
                {
                    IngredientName = "Beef"
                }, new Ingredient
                {
                    IngredientName = "Cumin"
                }, new Ingredient
                {
                    IngredientName = "Tomatoes"
                }
                );
                context.SaveChanges();
            }
            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                new Recipe
                {
                    Name = "Autumn Soup",
                    Category = "Appetizers",
                    Cuisine = "American",
                    Instructions = "Melt the butter in a large pot, and cook the onion, celery and squash for 5 minutes. Add the chicken stock to cover vegetables. Bring to a boil. Reduce heat to low, cover pot, and simmer 40 minutes, or until all vegetables are tender. Transfer the soup to a blender and blend until smooth.Return to pot and season with salt and pepper.",
                    Photo = "~/AutumnSoup.jpg"
                },
                new Recipe
                {
                    Name = "Panna Cotta",
                    Category = "Desserts",
                    Cuisine = "Italian",
                    Instructions = "Mix the and gelatin powder in a bowl. Set aside. In a saucepan, stir together the heavy cream and sugar, and set over medium heat. Bring to a full boil, watching carefully, as the cream will quickly rise to the top of the pan. Pour the gelatin and milk into the cream, stirring until completely dissolved. Cook for one minute, stirring constantly. Remove from heat, stir in the vanilla and pour into six individual ramekin dishes. Cool the ramekins uncovered at room temperature. Refrigerate for at least 4 hours",
                    Photo = "~/PannaCotta.jpg"
                }, new Recipe
                {
                    Name = "Beef Taco",
                    Category = "Entrees",
                    Cuisine = "Mexican",
                    Instructions = "Grill the beef in a skillet over medium-high heat. Meanwhile, wrap the flour tortillas in foil and warm in the oven for 20 to 25 minutes. Fill the tortilas with beef, tomatos, grated cheese and tomatos.",
                    Photo = "~/BeefTaco.jpg"
                });
                context.SaveChanges();
            }
            if (!context.IngredientLine.Any())
            {
                context.IngredientLine.AddRange(

                    new IngredientLine
                    {
                        IngredientID = 4,
                        Quantity = 1,
                        MeasureID = 4,
                        RecipeID = 1
                    },
                    new IngredientLine
                    {
                        IngredientID = 7,
                        Quantity = 1,
                        MeasureID = 7,
                        RecipeID = 1
                    },
                    new IngredientLine
                    {
                        IngredientID = 8,
                        Quantity = 4,
                        MeasureID = 1,
                        RecipeID = 1
                    },
                    new IngredientLine
                    {
                        IngredientID = 9,
                        Quantity = 5,
                        MeasureID = 1,
                        RecipeID = 1
                    },
                    new IngredientLine
                    {
                        IngredientID = 10,
                        Quantity = 1,
                        MeasureID = 1,
                        RecipeID = 1
                    }, 
                    new IngredientLine
                    {
                        IngredientID = 3,
                        Quantity = 1,
                        MeasureID = 1,
                        RecipeID = 2
                    }, new IngredientLine
                    {
                        IngredientID = 1,
                        Quantity = 1,
                        MeasureID = 9,
                        RecipeID = 2
                    }, new IngredientLine
                    {
                        IngredientID = 12,
                        Quantity = 1,
                        MeasureID = 3,
                        RecipeID = 2
                    }, new IngredientLine
                    {
                        IngredientID = 13,
                        Quantity = 1,
                        MeasureID = 8,
                        RecipeID = 2
                    }, new IngredientLine
                    {
                        IngredientID = 7,
                        Quantity = 1,
                        MeasureID = 1,
                        RecipeID = 3
                    }, new IngredientLine
                    {
                        IngredientID = 14,
                        Quantity = 500,
                        MeasureID = 5,
                        RecipeID = 3
                    }, new IngredientLine
                    {
                        IngredientID = 15,
                        Quantity = 1,
                        MeasureID = 3,
                        RecipeID = 3
                    }, new IngredientLine
                    {
                        IngredientID = 16,
                        Quantity = 1,
                        MeasureID = 1,
                        RecipeID = 3
                    }
                );
                context.SaveChanges();
            } 
        }
    }
}
