﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeses.Models
{
    public class Menu
    {

        public int MenuID { get; set; }
        public string Name { get; set; }
        public IList<CheeseMenu> CheeseMenus { get; set; }

        public Menu()
        {

        }
    }
}
