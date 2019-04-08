using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return RedirectToAction("Browse");
        }

        [Authorize]
        [HttpGet]
        public IActionResult UploadRecipe()
        {
            
            return View(dbContext.RecipeCategory.OrderBy(x => x.CategoryName));
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
               ForHowManyPeople = recipeDto.ForHowManyPeople
               
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




        [HttpGet]
        public IActionResult Search()
        {
            return View(dbContext.RecipeCategory.OrderBy(x => x.CategoryName));
        }


        [HttpPost]
        public IActionResult Search(SerachDto dto)
        {
            if (dto.search != null && dto.search.Length > 0)
            {
                var se = dbContext.Recipe.Where(x => x.Title.ToLower().Contains(dto.search.ToLower()) || x.SmallDescription.ToLower().Contains(dto.search.ToLower()));
                List<Recipe> recipesList = new List<Recipe>();
                if (dto.Category != null && dto.Category.Count() > 0)
                {
                    foreach (var item in se)
                    {
                        var temp = dbContext.RecipeAndCategoryConnectionTable.Where(x => x.RecipeId == item.RecipeId && dto.Category.Contains(x.RecipeCategory.CategoryName));
                        foreach (var okItem in temp)
                        {
                            var kk = okItem.Recipe;
                            if (!recipesList.Contains(kk))
                            {
                                recipesList.Add(kk);
                            }
                        }
                    }
                    //------
                    if (dto.Complexity != null && dto.Complexity.Count() > 0)
                    {
                        var finalList = new List<Recipe>();
                        foreach (var item in recipesList)
                        {
                            if (dto.Complexity.Contains(item.Complexity.ToString()))
                            {
                                Recipe r = item;
                                finalList.Add(r);
                            }
                        }
                        recipesList = finalList;
                    }
                    //-----
                    return View("Browse", recipesList);
                }
                else
                {
                    //------
                    if (dto.Complexity != null && dto.Complexity.Count() > 0)
                    {
                        var finalList = new List<Recipe>();
                        foreach (var item in se)
                        {
                            if (dto.Complexity.Contains(item.Complexity.ToString()))
                            {
                                Recipe r = item;
                                finalList.Add(r);
                            }
                        }
                        se = finalList.AsQueryable();
                    }
                    //-----
                    return View("Browse", se);
                }
            }
            else 
            {
                List<Recipe> recipesList = new List<Recipe>();
                if (dto.Category != null && dto.Category.Count() > 0)
                {
                    foreach (var item in dbContext.Recipe)
                    {
                        var temp = dbContext.RecipeAndCategoryConnectionTable.Where(x => x.RecipeId == item.RecipeId && dto.Category.Contains(x.RecipeCategory.CategoryName));
                        foreach (var okItem in temp)
                        {
                            var kk = okItem.Recipe;
                            if (!recipesList.Contains(kk))
                            {
                                recipesList.Add(kk);
                            }
                        }
                    }
                    //------
                    if (dto.Complexity != null && dto.Complexity.Count() > 0)
                    {
                        var finalList = new List<Recipe>();
                        foreach (var item in recipesList)
                        {
                            if (dto.Complexity.Contains(item.Complexity.ToString()))
                            {
                                Recipe r = item;
                                finalList.Add(r);
                            }
                        }
                        recipesList = finalList;
                    }
                    //-----
                    return View("Browse", recipesList);
                }
                else if (dto.Complexity != null && dto.Complexity.Count() > 0)
                {
                    //------
                    
                    
                        var finalList = new List<Recipe>();
                        foreach (var item in dbContext.Recipe)
                        {
                            if (dto.Complexity.Contains(item.Complexity.ToString()))
                            {
                                Recipe r = item;
                                finalList.Add(r);
                            }
                        }
                        recipesList = finalList;
                    
                    //-----
                    return View("Browse", recipesList);
                }
            }
            return RedirectToAction("Search");
        }


        [HttpGet]
        public IActionResult Browse()
        {

            //vissza az 50 legújabb receptet értékelés szerint sorba rendezve

            var ordered = dbContext.Recipe.OrderBy(x => x.Uploaded);
            var firstordered = ordered.Take(50);
           
            ordered = firstordered.OrderByDescending(x => (x.RecipeRatings.Count>0) ? x.RecipeRatings.Average(z => z.RateValue) : 0);

            return View(ordered);
        }
        [HttpGet]
        public IActionResult Recipe(int id)
        {
            Recipe recipe = dbContext.Recipe.Where(x => x.RecipeId == id).FirstOrDefault();
            ViewData["userid"] = userManager.GetUserId(this.User);
            ViewData["category"] = dbContext.RecipeAndCategoryConnectionTable.Where(x => x.RecipeId == id);
            ViewData["fav"] = dbContext.UserAndRecipeAddedToFavouriteConnectionTable.Where(x => x.RecipeId == id && x.UserId == userManager.GetUserId(User)).Count() > 0 ? true : false;
            ViewData["isModerator"] = this.User.IsInRole("Moderator");
            return View(recipe);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RateIt(string rate, string recipeId, string userId)
        {
            var user = userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user != null)
            {
                int rateN = int.Parse(rate);
                int recipeIdNum = int.Parse(recipeId);

                var already = dbContext.RecipeRating.Where(x => x.UserId == userId && x.ReceipeId == recipeIdNum).FirstOrDefault();
                if (already != null)
                {
                    dbContext.RecipeRating.Remove(already);
                }


                dbContext.RecipeRating.Add(new RecipeRating() {RateValue = rateN, ReceipeId = recipeIdNum, UserId = userId });
                dbContext.SaveChanges();
            }

            return Ok();
        }

        public IActionResult SendComment(string recipeId, string userId, string comment)
        {
            if (comment != null && comment.Length > 0)
            {
                var user = userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
                RecipeComment recipeComment = new RecipeComment() { CommentText = comment, ReceipeId = int.Parse(recipeId), UserId = userId, UploadedOrModified = DateTime.Now };
                dbContext.RecipeComment.Add(recipeComment);

                dbContext.SaveChanges();
            }
            return Ok();
        }


        public IActionResult RenderImage(int id, int num, bool isTumbnail)
        {
            Recipe recipe = dbContext.Recipe.Where(x => x.RecipeId == id).FirstOrDefault();
            byte[] photoBack = null;
            if (isTumbnail)
            {
                photoBack = recipe.Tumbnail;
            }
            else
            {
                int i = 0;
                
                foreach (var item in recipe.ImageAndRecipeConnections)
                {
                    if (num == i)
                    {
                        photoBack = item.Image;
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }


            }

            return File(photoBack, "image/png");
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateCategory(string Category)
        {
            if (Category != null && Category.Length > 0)
            {
                if (dbContext.RecipeCategory.Where(x => x.CategoryName == Category).Count() == 0)
                {
                    dbContext.RecipeCategory.Add(new RecipeCategory() { CategoryName = Category });
                    dbContext.SaveChanges();
                }
            }

            return View((object)Category);
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddFavourite(string id, string userid)
        {
            var c = dbContext.UserAndRecipeAddedToFavouriteConnectionTable.Where(x => x.RecipeId == int.Parse(id) && x.UserId == userid);
            if (c.Count() == 0)
            {
                dbContext.UserAndRecipeAddedToFavouriteConnectionTable.Add(new UserAndRecipeAddedToFavouriteConnectionTable() { RecipeId = int.Parse(id), UserId = userid });
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.UserAndRecipeAddedToFavouriteConnectionTable.Remove(c.FirstOrDefault());
                dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult MyFavourites()
        {
            return View("Browse", dbContext.UserAndRecipeAddedToFavouriteConnectionTable.Select(x => x.Recipe));
        }

        [HttpGet]
        public IActionResult LookUpByIngredients()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllIngredientsList()
        {
            var k = dbContext.Recipe.Select(x => x.IngredientsJson);
            List<string> ingred = new List<string>();
            foreach (var item in k)
            {
                var obj = JsonConvert.DeserializeObject<List<Ingredtients>>(item);
                foreach (var ob in obj)
                {
                    Ingredtients ing = ob;
                    ing.Name = ing.Name.ToUpper();
                    if (!ingred.Contains(ing.Name))
                    {
                        ingred.Add(ing.Name);
                    }
                }
            }

          

            return Json(ingred);
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