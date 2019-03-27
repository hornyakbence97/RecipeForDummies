using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models.Dto
{
    public class RecipeDto
    {
        public string Title { get; set; }
        public IFormFile Tumbnail { get; set; }
        public string SmallDescription { get; set; }
        public string InsturctionsJson { get; set; }
        public string IngredientsJson { get; set; }
        public List<IFormFile> Images { get; set; }
        public Complexity Complexity { get; set; }
        public string CategoryListJson { get; set; }
        public int ForHowManyPeople { get; set; }
    }
}
