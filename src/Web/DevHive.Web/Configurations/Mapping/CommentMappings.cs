using AutoMapper;
using DevHive.Services.Models.Comment;
using DevHive.Web.Models.Comment;

namespace DevHive.Web.Configurations.Mapping
{
	public class CommentMappings : Profile
	{
		public CommentMappings()
		{
			CreateMap<CreateCommentWebModel, CreateCommentServiceModel>();
			CreateMap<UpdateCommentWebModel, UpdateCommentServiceModel>();

			CreateMap<ReadCommentServiceModel, ReadCommentWebModel>();
		}
	}
}
