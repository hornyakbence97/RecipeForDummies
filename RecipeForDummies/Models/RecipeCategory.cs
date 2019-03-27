using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class RecipeCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RecipeCategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

       
    }
}
