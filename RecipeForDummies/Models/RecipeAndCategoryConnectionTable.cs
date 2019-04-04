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
        public virtual int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        [ForeignKey("RecipeCategory")]
        public virtual int RecipeCategoryId { get; set; }
        public virtual RecipeCategory RecipeCategory { get; set; }
    }
}
