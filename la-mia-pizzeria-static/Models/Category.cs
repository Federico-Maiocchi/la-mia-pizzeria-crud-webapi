﻿using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        [Key]
        public int Id {get; set;}
        public string Name { get; set;}

        public List<Pizza> Pizze { get; set;}   

        public Category() { }

    }
}
