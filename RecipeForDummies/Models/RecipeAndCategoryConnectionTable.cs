using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class RecipeAndCategoryConnectionTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RecipeAndCategoryConnectionTableId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        [ForeignKey("RecipeCategory")]
        public int RecipeCategoryId { get; set; }
        public RecipeCategory RecipeCategory { get; set; }
    }
}
