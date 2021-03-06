using AutoMapper;
using DevHive.Services.Models.User;
using DevHive.Web.Models.User;
using DevHive.Common.Models.Identity;

namespace DevHive.Web.Configurations.Mapping
{
	public class UserMappings : Profile
	{
		public UserMappings()
		{
			CreateMap<LoginWebModel, LoginServiceModel>();
			CreateMap<RegisterWebModel, RegisterServiceModel>();
			CreateMap<UserWebModel, UserServiceModel>();
			CreateMap<UpdateUserWebModel, UpdateUserServiceModel>();

			CreateMap<UserServiceModel, UserWebModel>();

			CreateMap<TokenModel, TokenWebModel>();

			//Update
			CreateMap<UpdateUserWebModel, UpdateUserServiceModel>();
			CreateMap<UsernameWebModel, FriendServiceModel>();
			CreateMap<UsernameWebModel, UpdateFriendServiceModel>();

			CreateMap<UpdateUserServiceModel, UpdateUserWebModel>();
			CreateMap<FriendServiceModel, UsernameWebModel>();
		}
	}
}
