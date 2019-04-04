using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models.Dto
{
    public class SerachDto
    {
        public IEnumerable<string> Category { get; set; }
        public string search { get; set; }
    }
}
