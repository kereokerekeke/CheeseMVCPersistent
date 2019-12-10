using Cheeses.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cheeses.ViewModels
{
    public class AddMenuViewModel
    {
        [Required]
        [Display(Name="Menu name")]
        public string Name { get; set; }

        public AddMenuViewModel()
        {

        }

        public Menu CreateMenu()
        {
            return new Menu
            {
                Name = this.Name
            };
        }

    }

}
