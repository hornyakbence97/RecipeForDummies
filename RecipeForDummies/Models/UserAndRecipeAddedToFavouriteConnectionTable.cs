using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class UserAndRecipeAddedToFavouriteConnectionTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserAndRecipeAddedToFavouriteConnectionTableId { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
