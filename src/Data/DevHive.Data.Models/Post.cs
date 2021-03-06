using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DevHive.Data.Models.Relational;

namespace DevHive.Data.Models
{
	[Table("Posts")]
	public class Post
	{
		public Guid Id { get; set; }

		public User Creator { get; set; }

		public string Message { get; set; }

		public DateTime TimeCreated { get; set; }

		public List<Comment> Comments { get; set; } = new();

		public List<Rating> Ratings { get; set; }

		public List<PostAttachments> Attachments { get; set; } = new();
	}
}
