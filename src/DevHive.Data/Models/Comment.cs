using System;
namespace DevHive.Data.Models
{
	public class Comment : IModel
	{
		public Guid Id { get; set; }
		public Guid IssuerId { get; set; }
		public string Message { get; set; }
		public DateTime TimeCreated { get; set; }
	}
}