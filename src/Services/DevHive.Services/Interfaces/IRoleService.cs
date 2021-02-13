using System;
using System.Threading.Tasks;
using DevHive.Services.Models.Role;

namespace DevHive.Services.Interfaces
{
	public interface IRoleService
	{
		Task<Guid> CreateRole(CreateRoleServiceModel roleServiceModel);

		Task<RoleServiceModel> GetRoleById(Guid id);

		Task<bool> UpdateRole(UpdateRoleServiceModel roleServiceModel);

		Task<bool> DeleteRole(Guid id);
	}
}
