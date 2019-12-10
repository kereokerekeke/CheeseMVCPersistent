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
    public class MenuController : Controller
    {

        private CheeseDBContext context;

        public MenuController(CheeseDBContext dBContext)
        {
            context = dBContext;
        }

        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();

            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = addMenuViewModel.CreateMenu();
                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect($"/Menu/ViewMenu/{newMenu.MenuID}");
            }

            return View(addMenuViewModel);
        }

        public IActionResult ViewMenu(int id)
        {
            Menu viewMenu = context.Menus.Single(c => c.MenuID == id);

            List<CheeseMenu> items = context.CheeseMenus
                                            .Include(item => item.Cheese)
                                            .Where(cm => cm.MenuID == id)
                                            .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel(viewMenu, items);

            return View(viewMenuViewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(c => c.MenuID == id);

            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, context.Cheeses.ToList());

            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {

                IList<CheeseMenu> existingItems = context.CheeseMenus
                                                         .Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseID)
                                                         .Where(cm => cm.MenuID == addMenuItemViewModel.MenuID).ToList();

                if(existingItems.Count == 0)
                {
                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        MenuID = addMenuItemViewModel.MenuID,
                        CheeseID = addMenuItemViewModel.CheeseID
                    };

                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();
                }

                return Redirect($"/Menu/ViewMenu/{addMenuItemViewModel.MenuID}");
            }

            return View(addMenuItemViewModel);
        }
    }
}