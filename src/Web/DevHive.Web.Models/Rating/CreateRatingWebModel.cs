using System;

namespace DevHive.Web.Models.Rating
{
	public class CreateRatingWebModel
	{
		public Guid PostId { get; set; }

		public bool IsLike { get; set; }
	}
}
