using RecipeForDummies.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class RecipeAndTheIngredientsList
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<Ingredtients> IngredientsList { get; set; }


        public bool ContainOnlyTheElementsListedHere(IEnumerable<string> elements)
        {
          
            foreach (var item in IngredientsList)
            {
                if (!elements.Contains(item.Name.ToUpper()))
                {
                   
                    return false;
                }
            }

            return true;
        }
    }
}
