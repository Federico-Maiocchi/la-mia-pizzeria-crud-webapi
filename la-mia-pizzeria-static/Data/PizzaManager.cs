using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public static class PizzaManager
    {
        private static int id;

        public static int CountAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizze.Count();

        }

        //prendere tutte le pizze
        public static List<Pizza> GetAllPizzas()
        {
            using PizzaContext db = new PizzaContext();

            //return db.Pizze.ToList();

            return db.Pizze.Include(p => p.Category).ToList();
        }

        //prendere tutte le categorie
        public static List<Category> GetAllCategories()
        {
            using PizzaContext db = new PizzaContext();
            return db.Categorie.ToList();
        }

        //prendere tutti gli ingredienti
        public static List<Ingredient> GetAllIngredients()
        {
            using PizzaContext db = new PizzaContext();
            return db.Ingredienti.ToList();
        }

        //public static Pizza GetPizzaById(int id)
        //{
        //    using PizzaContext db = new PizzaContext();
        //    return db.Pizze.FirstOrDefault(p => p.Id == id);
        //}

        //prendere la pizza con ID e includere le categorie/ingredienti
        public static Pizza GetPizzaById(int id, bool includesReferences = true)
        {
            using PizzaContext db = new PizzaContext();
            
            if (includesReferences)
            {
                return db.Pizze.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
            }
            else
            {
                return db.Pizze.FirstOrDefault(p => p.Id == id);
            }
            
        }

        public static Pizza GetPizzaByName(string name)
        {
            using PizzaContext db = new PizzaContext();

            return db.Pizze.FirstOrDefault(p => p.Name == name);
        }

        public static Category GetCategoryById(int id)
        {
            using PizzaContext db = new PizzaContext();
            return db.Categorie.FirstOrDefault(c => c.Id == id);
        }

        public static Ingredient GetIngredientById(int id)
        {
            using PizzaContext db = new PizzaContext();
            return db.Ingredienti.FirstOrDefault(i => i.Id == id);
        }

        //Aggiungere una nuova pizza
        public static void InserPizza(Pizza pizza, List<string> SelectedIngredients = null)
        {
            using PizzaContext db = new PizzaContext();

            if (SelectedIngredients != null)
            {
                pizza.Ingredients = new List<Ingredient>();

                foreach (var ingredientId in SelectedIngredients)
                {
                    int idInt = int.Parse(ingredientId);
                    var ingredient = db.Ingredienti.FirstOrDefault(i => i.Id == idInt);
                    pizza.Ingredients.Add(ingredient);
                }
            }

            db.Pizze.Add(pizza);
            db.SaveChanges();
        }

        //Modificare una pizza
        public static bool UpdatePizza(int id, string name, string description, string image,
            decimal price, int? categoryId, List<string> ingredients)
        {
            using PizzaContext db = new PizzaContext();

            var pizza = db.Pizze.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizza == null)
            {
                return false;
            }

            pizza.Name = name;
            pizza.Description = description;
            pizza.Image = image;
            pizza.Price = price;
            pizza.CategoryId = categoryId;

            pizza.Ingredients.Clear();
            if (ingredients != null)
            {
                foreach (var ingredient in ingredients)
                {
                    int ingredientId = int.Parse(ingredient);
                    var ingredientFromDb = db.Ingredienti.FirstOrDefault(i => i.Id == ingredientId);
                    pizza.Ingredients.Add(ingredientFromDb);
                }
            }
            

            db.SaveChanges();

            return true;
        }

        //Cancella pizza
        public static bool DeletePizza(int id)
        {
            try
            {
                var idPizzaDelete = GetPizzaById(id, false);
                if (idPizzaDelete == null)
                {
                    return false;
                }
                else
                {
                    using PizzaContext db = new PizzaContext();
                    db.Remove(idPizzaDelete);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static void Seed()
        {
            using PizzaContext db = new PizzaContext();

            List<Pizza> pizze = new List<Pizza>()
            {
                new Pizza { Name = "Margherita", Description = "Pizza Margherita , normale", Image = "~/img/margherita.png", Price = 8.99m },
                new Pizza { Name = "Capricciosa", Description = "pizza capricciosa, molto capricciosa", Image = "~/img/capricciosa.png", Price = 9.99m },
                new Pizza { Name = "Quattro Stagioni", Description = "Pizza quattro stagioni, in primavera è buona", Image = "~/img/quattro-stagioni.png", Price = 10.99m },
                new Pizza { Name = "Diavola", Description = "Pizza diavola, è sempre arrabbiata", Image = "~/img/diavola.png", Price = 9.49m },
                new Pizza { Name = "Bufala", Description = "pizza bufala, è uno scherzo", Image = "~/img/bufala.png", Price = 11.49m },
            };

            db.Pizze.AddRange(pizze);
            db.SaveChanges();
        }
    }

    public class SelectedListIngredients
    {
    }
}
