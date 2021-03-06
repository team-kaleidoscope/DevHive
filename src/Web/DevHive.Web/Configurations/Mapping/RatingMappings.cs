using AutoMapper;
using DevHive.Services.Models.Rating;
using DevHive.Web.Models.Rating;

namespace DevHive.Web.Configurations.Mapping
{
	public class RatingMappings : Profile
	{
		public RatingMappings()
		{
			CreateMap<CreateRatingWebModel, CreateRatingServiceModel>();

			CreateMap<ReadRatingServiceModel, ReadRatingWebModel>();

			CreateMap<UpdateRatingWebModel, UpdateRatingServiceModel>();
		}
	}
}
