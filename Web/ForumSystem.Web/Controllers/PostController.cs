namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PostController : Controller
    {
        private readonly Services.Data.IPostsService postService;
        private readonly ICategoriesService categoryService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostController(
            IPostsService postService,
            ICategoriesService categoryService,
            UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IActionResult ById(int id)
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = this.categoryService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new PostCreateInputModel();
            viewModel.Categories = categories;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var postId = await this.postService.CreateAsync(input.Title, input.Content, input.CategoryId, user.Id);

            return this.RedirectToAction("ById", new { id = postId});
        }
    }
}
