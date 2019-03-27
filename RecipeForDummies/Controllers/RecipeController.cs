using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeForDummies.Data;
using RecipeForDummies.Models;
using RecipeForDummies.Models.Dto;

namespace RecipeForDummies.Controllers
{
    public class RecipeController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext dbContext;
        private UserManager<IdentityUser> userManager;

        public RecipeController(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.CreateRoles();
        }
        public IActionResult Index()
        {
            return RedirectToAction("UploadRecipe");
        }

        [Authorize]
        [HttpGet]
        public IActionResult UploadRecipe()
        {
            
            return View(dbContext.RecipeCategory);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UploadRecipe(RecipeDto recipeDto)
        {

            byte[] dataThumbnail = new byte[recipeDto.Tumbnail.Length];

            using (var stream = recipeDto.Tumbnail.OpenReadStream())
            {
                stream.Read(dataThumbnail, 0, (int)recipeDto.Tumbnail.Length);
            }

            List<byte[]> uploadedImages = new List<byte[]>();
            for (int i = 0; i < recipeDto.Images.Count; i++)
            {
                byte[] tempData = new byte[recipeDto.Images[i].Length];

                using (var stream = recipeDto.Images[i].OpenReadStream())
                {
                    stream.Read(tempData, 0, (int)recipeDto.Images[i].Length);
                }
                uploadedImages.Add(tempData);
            }



            Recipe recipe = new Recipe()
            {
                Complexity = recipeDto.Complexity,
                
                IngredientsJson = recipeDto.IngredientsJson,
                InsturctionsJson = recipeDto.InsturctionsJson,
                SmallDescription = recipeDto.SmallDescription,
                Title = recipeDto.Title,
                UserId = userManager.GetUserId(this.User),
                Tumbnail = dataThumbnail,
               Uploaded = DateTime.Now,
               
            };
            dbContext.Recipe.Add(recipe);
            dbContext.SaveChanges();


            foreach (var item in uploadedImages)
            {
                dbContext.ImageAndRecipeConnection.Add(new ImageAndRecipeConnection {Image = item, RecipeId = recipe.RecipeId });
            }
            dbContext.SaveChanges();

            var listOfCategories = JsonConvert.DeserializeObject<List<string>>(recipeDto.CategoryListJson);
            foreach (var item in listOfCategories)
            {
                dbContext.RecipeAndCategoryConnectionTable.Add(new RecipeAndCategoryConnectionTable {RecipeId= recipe.RecipeId, RecipeCategoryId = dbContext.RecipeCategory.Where(xx => xx.CategoryName == item).FirstOrDefault().RecipeCategoryId });
            }
            dbContext.SaveChanges();


            return View(dbContext.RecipeCategory);
        }


        private void CreateRoles()
        {
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Moderator"));
            }
        }
    }
}