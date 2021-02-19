using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevHive.Services.Interfaces;
using DevHive.Services.Models.Technology;
using DevHive.Web.Models.Technology;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevHive.Web.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class TechnologyController
	{
		private readonly ITechnologyService _technologyService;
		private readonly IMapper _technologyMapper;

		public TechnologyController(ITechnologyService technologyService, IMapper technologyMapper)
		{
			this._technologyService = technologyService;
			this._technologyMapper = technologyMapper;
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromBody] CreateTechnologyWebModel createTechnologyWebModel)
		{
			CreateTechnologyServiceModel technologyServiceModel = this._technologyMapper.Map<CreateTechnologyServiceModel>(createTechnologyWebModel);

			Guid id = await this._technologyService.CreateTechnology(technologyServiceModel);

			return id == Guid.Empty ?
				new BadRequestObjectResult($"Could not create technology {createTechnologyWebModel.Name}") :
				new OkObjectResult(new { Id = id });
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetById(Guid id)
		{
			ReadTechnologyServiceModel readTechnologyServiceModel = await this._technologyService.GetTechnologyById(id);
			ReadTechnologyWebModel readTechnologyWebModel = this._technologyMapper.Map<ReadTechnologyWebModel>(readTechnologyServiceModel);

			return new OkObjectResult(readTechnologyWebModel);
		}

		[HttpGet]
		[Route("GetTechnologies")]
		[Authorize(Roles = "User,Admin")]
		public IActionResult GetAllExistingTechnologies()
		{
			HashSet<ReadTechnologyServiceModel> technologyServiceModels = this._technologyService.GetTechnologies();
			HashSet<ReadTechnologyWebModel> languageWebModels = this._technologyMapper.Map<HashSet<ReadTechnologyWebModel>>(technologyServiceModels);

			return new OkObjectResult(languageWebModels);
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTechnologyWebModel updateModel)
		{
			UpdateTechnologyServiceModel updateTechnologyServiceModel = this._technologyMapper.Map<UpdateTechnologyServiceModel>(updateModel);
			updateTechnologyServiceModel.Id = id;

			bool result = await this._technologyService.UpdateTechnology(updateTechnologyServiceModel);

			if (!result)
				return new BadRequestObjectResult("Could not update Technology");

			return new OkResult();
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool result = await this._technologyService.DeleteTechnology(id);

			if (!result)
				return new BadRequestObjectResult("Could not delete Technology");

			return new OkResult();
		}
	}
}