using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        //Pizza
        public DbSet<Pizza> Pizze { get; set; }

        //Categorie
        public DbSet<Category> Categorie { get; set; }

        //Ingredienti
        public DbSet<Ingredient> Ingredienti { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=DbPizze;");
        }
    }
}
