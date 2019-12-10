using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheeses.Data;
using Cheeses.Models;
using Cheeses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cheeses.Controllers
{
    public class CheeseController : Controller
    {

        private CheeseDBContext context;

        public CheeseController(CheeseDBContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());

            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {

            if(ModelState.IsValid)
            {

                CheeseCategory newCheeseCategory =
                                    context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

                Cheese newCheese = addCheeseViewModel.CreateCheese(newCheeseCategory);

                context.Cheeses.Add(newCheese);

                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        [HttpPost]
        [Route("/Cheese")]
        public IActionResult RemoveCheese(string[] cheeses)
        {

            foreach(string cheeseId in cheeses)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == int.Parse(cheeseId));
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();            

            return Redirect("/Cheese");
        }

        public IActionResult Edit(int cheeseId)
        {
            Cheese editCheese = context.Cheeses.Single(c => c.ID == cheeseId);
            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel(editCheese, context.Categories.ToList());
            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            if (ModelState.IsValid)
            {

                Cheese editCheese = context.Cheeses.Single(c => c.ID == addEditCheeseViewModel.cheeseId);
                editCheese.Name = addEditCheeseViewModel.Name;
                editCheese.Description = addEditCheeseViewModel.Description;
                editCheese.CategoryID = addEditCheeseViewModel.CategoryID;
                editCheese.Raiting = addEditCheeseViewModel.Raiting;

                context.SaveChanges();

                return Redirect("/Cheese");
            }
           
            return View(addEditCheeseViewModel);
        }

        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                return Redirect("/Category");
            }

            CheeseCategory theCategory = context.Categories
                                                .Include(cat => cat.Cheeses)
                                                .Single(cat => cat.ID == id);
            return View("Index", theCategory.Cheeses);

        }

    }
}