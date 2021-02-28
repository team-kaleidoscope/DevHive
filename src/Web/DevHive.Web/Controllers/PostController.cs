using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using DevHive.Web.Models.Post;
using DevHive.Services.Models.Post;
using Microsoft.AspNetCore.Authorization;
using DevHive.Services.Interfaces;
using DevHive.Common.Jwt.Interfaces;

namespace DevHive.Web.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	[Authorize(Roles = "User,Admin")]
	public class PostController
	{
		private readonly IPostService _postService;
		private readonly IMapper _postMapper;
		private readonly IJwtService _jwtService;

		public PostController(IPostService postService, IMapper postMapper, IJwtService jwtService)
		{
			this._postService = postService;
			this._postMapper = postMapper;
			this._jwtService = jwtService;
		}

		#region Create
		[HttpPost]
		public async Task<IActionResult> Create(Guid userId, [FromForm] CreatePostWebModel createPostWebModel, [FromHeader] string authorization)
		{
			if (!this._jwtService.ValidateToken(userId, authorization))
				return new UnauthorizedResult();

			CreatePostServiceModel createPostServiceModel =
				this._postMapper.Map<CreatePostServiceModel>(createPostWebModel);
			createPostServiceModel.CreatorId = userId;

			Guid id = await this._postService.CreatePost(createPostServiceModel);

			return id == Guid.Empty ?
				new BadRequestObjectResult("Could not create post!") :
				new OkObjectResult(new { Id = id });
		}
		#endregion

		#region Read
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetById(Guid id)
		{
			ReadPostServiceModel postServiceModel = await this._postService.GetPostById(id);
			ReadPostWebModel postWebModel = this._postMapper.Map<ReadPostWebModel>(postServiceModel);

			return new OkObjectResult(postWebModel);
		}
		#endregion

		#region Update
		[HttpPut]
		public async Task<IActionResult> Update(Guid userId, [FromForm] UpdatePostWebModel updatePostWebModel, [FromHeader] string authorization)
		{
			if (!this._jwtService.ValidateToken(userId, authorization))
				return new UnauthorizedResult();

			if (!await this._postService.ValidateJwtForPost(updatePostWebModel.PostId, authorization))
				return new UnauthorizedResult();

			UpdatePostServiceModel updatePostServiceModel =
				this._postMapper.Map<UpdatePostServiceModel>(updatePostWebModel);
			updatePostServiceModel.CreatorId = userId;

			Guid id = await this._postService.UpdatePost(updatePostServiceModel);

			return id == Guid.Empty ?
				new BadRequestObjectResult("Could not update post!") :
				new OkObjectResult(new { Id = id });
		}
		#endregion

		#region Delete
		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id, [FromHeader] string authorization)
		{
			if (!await this._postService.ValidateJwtForPost(id, authorization))
				return new UnauthorizedResult();

			return await this._postService.DeletePost(id) ?
				new OkResult() :
				new BadRequestObjectResult("Could not delete Post");
		}
		#endregion
	}
}
