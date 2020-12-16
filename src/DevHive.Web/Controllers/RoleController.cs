using System.Threading.Tasks;
using DevHive.Data.Repositories;
using DevHive.Services.Services;
using Microsoft.AspNetCore.Mvc;
using DevHive.Web.Models.Identity.Role;
using AutoMapper;
using DevHive.Services.Models.Identity.Role;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DevHive.Web.Controllers
{
    [ApiController]
	[Route("/api/[controller]")]
	//[Authorize(Roles = "Admin")]
	public class RoleController
	{
		private readonly RoleService _roleService;
		private readonly IMapper _roleMapper;

		public RoleController(DevHiveContext context, IMapper mapper)
		{
			this._roleService = new RoleService(context, mapper);
			this._roleMapper = mapper;
		}

		[HttpPost]
		public Task<IActionResult> Create(CreateRoleWebModel createRoleWebModel)
		{
			RoleServiceModel roleServiceModel = 
				this._roleMapper.Map<RoleServiceModel>(createRoleWebModel); 
			
			return this._roleService.CreateRole(roleServiceModel);
		}

		[HttpGet]
		public Task<IActionResult> GetById(Guid id)
		{
			return this._roleService.GetRoleById(id);
		}

		[HttpPut]
		public Task<IActionResult> Update(UpdateRoleWebModel updateRoleWebModel)
		{
			RoleServiceModel roleServiceModel = 
				this._roleMapper.Map<RoleServiceModel>(updateRoleWebModel);

			return this._roleService.UpdateRole(roleServiceModel);
		}

		[HttpDelete]
		public Task<IActionResult> Delete(Guid id)
		{
			return this._roleService.DeleteRole(id);
		}
	}
}
