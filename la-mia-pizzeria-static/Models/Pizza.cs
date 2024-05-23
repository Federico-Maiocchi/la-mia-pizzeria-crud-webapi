using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("Pizza")]
    
    [Index(nameof(Name), IsUnique = true)]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        //Validazioni
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(30, ErrorMessage = "Il nome non può avere più di 30 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(300, ErrorMessage = "Il nome non può avere più di 300 caratteri")]
        public string Description { get; set; }

        public string? Image { get; set; }

        //[Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }



        //relazione 1 : N con CATEGORY
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }


        //relazione N : N con INGREDIENT
        public List<Ingredient>? Ingredients {  get; set; }



        public Pizza() { }

        public Pizza(string name, string description, string image, decimal price)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }


        public string GetDisplayCategory()
        {
            if (Category == null)
            {
                return "Nessuna categoria";
            }
            else
            {
                return Category.Name;
            }
        }

        /*public string PriceString()
        {
            string priceStringConvert = $"{Price} €";

            return priceStringConvert;
        }*/
    }
}
