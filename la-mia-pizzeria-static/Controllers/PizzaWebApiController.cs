using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaWebApiController : ControllerBase
    {
        //ricerca di tutte le pizze
        [HttpGet("{filterName?}")]
        public IActionResult GetAllPizzas(string? filterName = "")
        {

            
            if (string.IsNullOrWhiteSpace(filterName))
            {
                return Ok(PizzaManager.GetAllPizzas());
            }
            else
            {
                return Ok(PizzaManager.GetPizzasByName(filterName));
            }
            
                

            //    var allPizzasFilter = allPizzas.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
            //    return Ok(allPizzasFilter);
            //}
            //else
            //{
            //    return NotFound();
            //}
            
        }

        //ricerca per id pizza
        [HttpGet]
        public IActionResult GetPizzaById(int id) // query parma https: //.. /api/PizzaWebApi/GetPizzaById?id=1
        {
            var onePizza = PizzaManager.GetPizzaById(id, true);
            if(onePizza != null)
            {
                return Ok(onePizza);
            }
            else
            {
                return NotFound();
            }
        }

        //ricerca pizza per nome 
        [HttpGet("{name}")]
        public IActionResult GetPizzaByName(string nameFilter) // path param https: //.. /api/PizzaWebApi/GetPizzaByName/diavola
        {
            var pizzaFilterName = PizzaManager.GetPizzaByName(nameFilter);
            if (pizzaFilterName != null)
            {
                return Ok(pizzaFilterName);
            }
            else
            {
                return NotFound();
            }
        }

        //Creazione pizza
        [HttpPost]
        public IActionResult CreatePizza([FromBody] Pizza data)
        {
            PizzaManager.InserPizza(data, null); 
            {
                return Ok();
            }
        }

        //Modifica pizza
        [HttpPut("{id}")]
        public IActionResult UpdatePizza(int id, [FromBody] Pizza data)
        {
            var oldPizza = PizzaManager.GetPizzaById(id);
            if (oldPizza == null)
            {
                return NotFound();
            }

            //var pizzaDaModificare = PizzaManager.UpdatePizza(id, data, null);
            //return Ok();

            data.Id = id;
            if (PizzaManager.UpdatePizza(id, data.Name, data.Description, data.Image, data.Price, data.CategoryId, null))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
           
        }

        //cancella pizza
        [HttpDelete("{id}")]
        public IActionResult DeletePizza(int id)
        {
            bool pizzaDelete = PizzaManager.DeletePizza(id);
            if (pizzaDelete)
            {
                return Ok();

            }
            else
            {
                return NotFound();
            }
        }



    }
}
