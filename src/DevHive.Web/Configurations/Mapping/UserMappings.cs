using DevHive.Data.Models;
using AutoMapper;
using DevHive.Services.Models.Identity;

namespace DevHive.Web.Configurations.Mapping
{
	public class UserMappings : Profile
	{
		public UserMappings()
		{
			CreateMap<UserServiceModel, User>();
			CreateMap<RegisterServiceModel, User>();
			CreateMap<UpdateUserServiceModel, User>();
		}
	} 
}