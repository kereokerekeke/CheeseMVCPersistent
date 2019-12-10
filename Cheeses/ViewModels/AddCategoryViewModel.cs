using Cheeses.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeses.ViewModels
{
    public class AddCategoryViewModel
    {

        public int ID { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; }


        public Models.CheeseCategory CreateCategory()
        {
            CheeseCategory newCategory = new CheeseCategory
            {
                Name = this.Name
            };

            return newCategory;
        }

    }
}
