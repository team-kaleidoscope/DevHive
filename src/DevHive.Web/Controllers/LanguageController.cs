using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevHive.Services.Interfaces;
using DevHive.Services.Models.Language;
using DevHive.Web.Models.Language;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevHive.Web.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class LanguageController
	{
		private readonly ILanguageService _languageService;
		private readonly IMapper _languageMapper;

		public LanguageController(ILanguageService languageService, IMapper mapper)
		{
			this._languageService = languageService;
			this._languageMapper = mapper;
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromBody] CreateLanguageWebModel createLanguageWebModel)
		{
			CreateLanguageServiceModel languageServiceModel = this._languageMapper.Map<CreateLanguageServiceModel>(createLanguageWebModel);

			Guid id = await this._languageService.CreateLanguage(languageServiceModel);

			return id == Guid.Empty ?
				new BadRequestObjectResult($"Could not create language {createLanguageWebModel.Name}") :
				new OkObjectResult(new { Id = id });
		}

		[HttpGet]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> GetById(Guid id)
		{
			ReadLanguageServiceModel languageServiceModel = await this._languageService.GetLanguageById(id);
			ReadLanguageWebModel languageWebModel = this._languageMapper.Map<ReadLanguageWebModel>(languageServiceModel);

			return new OkObjectResult(languageWebModel);
		}

		[HttpGet]
		[Route("GetLanguages")]
		[Authorize(Roles = "User,Admin")]
		public IActionResult GetAllExistingLanguages()
		{
			HashSet<ReadLanguageServiceModel> languageServiceModels = this._languageService.GetLanguages();
			HashSet<ReadLanguageWebModel> languageWebModels = this._languageMapper.Map<HashSet<ReadLanguageWebModel>>(languageServiceModels);

			return new OkObjectResult(languageWebModels);
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLanguageWebModel updateModel)
		{
			UpdateLanguageServiceModel updatelanguageServiceModel = this._languageMapper.Map<UpdateLanguageServiceModel>(updateModel);
			updatelanguageServiceModel.Id = id;

			bool result = await this._languageService.UpdateLanguage(updatelanguageServiceModel);

			if (!result)
				return new BadRequestObjectResult("Could not update Language");

			return new OkResult();
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool result = await this._languageService.DeleteLanguage(id);

			if (!result)
				return new BadRequestObjectResult("Could not delete Language");

			return new OkResult();
		}
	}
}
