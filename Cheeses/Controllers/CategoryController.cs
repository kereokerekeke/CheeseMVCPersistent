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
    public class CategoryController : Controller
    {

        private readonly CheeseDBContext context;

        public CategoryController(CheeseDBContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {

            List<CheeseCategory> Categories = context.Categories.ToList();

            return View(Categories);
        }

        public IActionResult Add()
        {

            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();

            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {

            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = addCategoryViewModel.CreateCategory();

                context.Categories.Add(newCategory);

                context.SaveChanges();

                return Redirect("/Category");
            }

            return View(addCategoryViewModel);
        }

    }
}