using AutoMapper;
using DevHive.Services.Models.Identity.User;
using DevHive.Web.Models.Identity.User;
using DevHive.Common.Models.Identity;
using DevHive.Web.Models.Language;
using DevHive.Web.Models.Technology;

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

			CreateMap<FriendWebModel, FriendServiceModel>();
			CreateMap<FriendServiceModel, FriendWebModel>();

			CreateMap<FriendWebModel, UpdateUserCollectionServiceModel>()
				.ForMember(f => f.Name, u => u.MapFrom(src => src.UserName));
			CreateMap<UpdateLanguageWebModel, UpdateUserCollectionServiceModel>();
			CreateMap<UpdateTechnologyWebModel, UpdateUserCollectionServiceModel>();
		}
	}
}
