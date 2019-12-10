using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheeses.Models;

namespace Cheeses.ViewModels
{
    public class AddEditCheeseViewModel : AddCheeseViewModel
    {
        public int cheeseId { get; set; }

        public AddEditCheeseViewModel()
        {

        }

        public AddEditCheeseViewModel(Cheese editCheese, IEnumerable<CheeseCategory> categories): base(categories)
        {
            this.cheeseId = cheeseId;
            this.Name = editCheese.Name;
            this.Description = editCheese.Description;
            this.CategoryID = editCheese.CategoryID;
            this.Raiting = editCheese.Raiting;

        } 

    }
}
