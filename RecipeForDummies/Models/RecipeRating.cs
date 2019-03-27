using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class RecipeRating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RecipeRatingId { get; set; }
        [Required]
        public int RateValue { get; set; }

        [ForeignKey("Recipe")]
        public int ReceipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
