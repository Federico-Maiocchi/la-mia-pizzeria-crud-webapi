using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        public PizzaContext db = new PizzaContext();

        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Index()
        {
           
            //PizzaManager.Seed();

            return View(PizzaManager.GetAllPizzas());
        }

        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Show(int id)
        {
         
            var pizza = PizzaManager.GetPizzaById(id, true);
            if (pizza != null)
            {
                return View(pizza);
            } 
            else
            {
                return NotFound();
            } 
        }


        //Sezione CREATE
        //GET
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            PizzaFormModel model = new PizzaFormModel();
            model.Pizza = new Pizza();
            model.Categories = PizzaManager.GetAllCategories();
            model.CreateIngredients();
           
            return View(model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories();
                data.CreateIngredients();

                return View("Create", data);
            }

            //using (PizzaContext db = new PizzaContext())
            //{
            //    var pizzaToCreate = new Pizza(data.Name, data.Description, data.Image, data.Price);

            // impostiamo lo sport preferito dell'utente
            //    pizzaToCreate.CategoryId = data.Pizza.CategoryId;


            //    db.Pizze.Add(pizzaToCreate);
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}
            


            PizzaManager.InserPizza(data.Pizza, data.SelectedIngredients);
            return RedirectToAction("Index");

        }


        //sezione UPDATE
        //GET
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {

            //Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);

            //if (pizzaToEdit == null)
            //{
            //    return NotFound();
            //}
            //else
            //{

            //    return View(pizzaToEdit);
            //}

            Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = pizzaToEdit;
                model.Categories = PizzaManager.GetAllCategories();
                model.CreateIngredients();

                return View(model);
            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = PizzaManager.GetAllCategories();
                data.Categories = categories;
                data.CreateIngredients();

                return View("Update", data);
            }

            //using (PizzaContext db = new PizzaContext())
            //{
            //    Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);

            //    if (pizzaToEdit != null)
            //    {
            //        pizzaToEdit.Name = data.Name;
            //        pizzaToEdit.Description = data.Description;
            //        pizzaToEdit.Image = data.Image;
            //        pizzaToEdit.Price = data.Price;

            //        db.SaveChanges();

            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }
            //}

            if (PizzaManager.UpdatePizza(id, data.Pizza.Name, data.Pizza.Description, data.Pizza.Image, data.Pizza.Price, data.Pizza.CategoryId, data.SelectedIngredients))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }   
        }


        //sezione DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToDelete = PizzaManager.GetPizzaById(id);

                if (pizzaToDelete != null)
                {
                    db.Pizze.Remove(pizzaToDelete);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            
        } 
    }
}
