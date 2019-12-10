using Cheeses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeses.ViewModels
{
    public class ViewMenuViewModel
    {

        public Menu Menu { get; set; }
        public IList<CheeseMenu> Items { get; set; }

        public ViewMenuViewModel()
        {
               
        }

        public ViewMenuViewModel(Menu menu, List<CheeseMenu> items)
        {
            Menu = menu;
            Items = items;
        }

    }
}
