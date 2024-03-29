﻿using Microsoft.AspNetCore.Http;
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
        public Complexity Complexity { get; set; }
        [Required]
        public int ForHowManyPeople { get; set; }


        public virtual ICollection<RecipeComment> RecipeComments { get; set; }
        public virtual ICollection<RecipeRating> RecipeRatings { get; set; }
        public virtual ICollection<ImageAndRecipeConnection> ImageAndRecipeConnections { get; set; }

        public virtual string UserId { get; set; }
        public virtual IdentityUser User { get; set; }


    }
}
