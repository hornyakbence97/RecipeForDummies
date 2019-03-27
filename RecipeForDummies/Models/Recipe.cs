using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RecipeId { get; set; }
        [Required]
        public string Title { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Uploaded { get; set; }
        [Required]
        public byte[] Tumbnail { get; set; }
        [Required]
        public string SmallDescription { get; set; }
        [Required]
        public string InsturctionsJson { get; set; }
        [Required]
        public string IngredientsJson { get; set; }
        [Required]
        public string ImageUrlListJson { get; set; }
       

        public ICollection<RecipeComment> RecipeComments { get; set; }
        public ICollection<RecipeRating> RecipeRatings { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }


    }
}
