using Cheeses.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeses.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required(ErrorMessage = "Где имя сыра, мудак ебаный?")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; }
        [Range(1,5)]
        public int Raiting { get; set; }

        public AddCheeseViewModel()
        {

        }

        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories)
        {
            Categories = new List<SelectListItem>();
            foreach(CheeseCategory category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = Convert.ToInt32(category.ID).ToString(),
                    Text = category.Name.ToString()
                });
            }
        }

        public Cheese CreateCheese(CheeseCategory newCheeseCategory)
        {
            Cheese newCheese = new Cheese
            {
                Name = this.Name,
                Description = this.Description,
                CategoryID = newCheeseCategory.ID,
                Raiting = this.Raiting
            };

            return newCheese;
        }

    }
}
