using System;
using System.Threading.Tasks;
using AutoMapper;
using DevHive.Data.Repositories;
using DevHive.Services.Models.Language;
using DevHive.Services.Services;
using DevHive.Web.Models.Language;
using Microsoft.AspNetCore.Mvc;

namespace DevHive.Web.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class LanguageController
	{
		private readonly LanguageService _languageService;
		private readonly IMapper _languageMapper;

		public LanguageController(DevHiveContext context, IMapper mapper)
		{
			this._languageService = new LanguageService(context, mapper);
			this._languageMapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] LanguageWebModel languageWebModel)
		{
			LanguageServiceModel languageServiceModel = this._languageMapper.Map<LanguageServiceModel>(languageWebModel);

			bool result = await this._languageService.CreateLanguage(languageServiceModel);

			if(!result)
				return new BadRequestObjectResult("Could not create Language");

			return new OkResult();
		}

		[HttpGet]
		public async Task<IActionResult> GetById(Guid id)
		{
			LanguageServiceModel languageServiceModel = await this._languageService.GetLanguageById(id);
			LanguageWebModel languageWebModel = this._languageMapper.Map<LanguageWebModel>(languageServiceModel);

			return new OkObjectResult(languageWebModel);
		}

		[HttpPut]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLanguageWebModel updateModel)
		{
			UpdateLanguageServiceModel updatelanguageServiceModel = this._languageMapper.Map<UpdateLanguageServiceModel>(updateModel);
			updatelanguageServiceModel.Id = id;

			bool result = await this._languageService.UpdateLanguage(updatelanguageServiceModel);

			if(!result)
				return new BadRequestObjectResult("Could not update Language");

			return new OkResult();
		}
	
		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool result = await this._languageService.DeleteLanguage(id);
			
			if(!result)
				return new BadRequestObjectResult("Could not delete Language");

			return new OkResult();
		}
	}
}