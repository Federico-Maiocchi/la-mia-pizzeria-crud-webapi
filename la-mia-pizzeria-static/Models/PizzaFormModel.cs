using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        //Pizza
        public Pizza Pizza {  get; set; }

        //Categorie
        public List<Category>? Categories {  get; set; }

        //Ingredienti
        //elecon ingredienti selezionabili
        public List<SelectListItem>? Ingredients { get; set; }
        //elenco degli ID degli ingredienti selezionati
        public List<string>? SelectedIngredients { get; set; }
        

        public PizzaFormModel() { }

        public PizzaFormModel(Pizza pizza, List<Category>? categories)
        {
            Pizza = pizza;
            Categories = categories;
        }

        public void CreateIngredients()
        {
            this.Ingredients = new List<SelectListItem>();
            var ingredientsFromDb = PizzaManager.GetAllIngredients();
            foreach (var ingredient in ingredientsFromDb)
            {
                this.Ingredients.Add(new SelectListItem()
                {
                    Text = ingredient.Name,
                    Value = ingredient.Id.ToString(),
                    Selected = this.Pizza.Ingredients?.Any(i => i.Id == ingredient.Id) == true,
                });
            }
        }
    }
}
