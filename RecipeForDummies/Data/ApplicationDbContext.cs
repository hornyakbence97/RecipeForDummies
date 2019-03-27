using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeForDummies.Models;

namespace RecipeForDummies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeComment> RecipeComment { get; set; }
        public DbSet<RecipeRating> RecipeRating { get; set; }
        public DbSet<RecipeAndCategoryConnectionTable> RecipeAndCategoryConnectionTable { get; set; }
        public DbSet<UserAndRecipeAddedToFavouriteConnectionTable> UserAndRecipeAddedToFavouriteConnectionTable { get; set; }
    }
}
