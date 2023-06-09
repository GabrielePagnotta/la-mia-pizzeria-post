﻿using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new DbPizzaContext();
            var pizza= ctx.Pizzas.ToArray();
            return View(pizza);
        }

        public IActionResult Details(int id)
        {
            using (var ctx = new DbPizzaContext()) 
            {
                Pizza singleuser = ctx.Pizzas.SingleOrDefault(h => h.Id == id);

                if (singleuser == null) 
                {
                    return NotFound($"l'id numero {id} non è stato trovato");
                }

                return View(singleuser);
                    
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",data);
            }
            using (DbPizzaContext context = new DbPizzaContext())
            {
                Pizza newpizza = new Pizza();
                newpizza.Name = data.Name;
                newpizza.Description = data.Description;
                newpizza.Price = data.Price;
                newpizza.Image = data.Image;
                context.Add(newpizza);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View("Create");
        }
    }
}
