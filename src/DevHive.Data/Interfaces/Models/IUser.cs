using System.Collections.Generic;
using DevHive.Data.Models;
using DevHive.Data.RelationModels;

namespace DevHive.Data.Interfaces.Models
{
	public interface IUser : IModel
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		string ProfilePictureUrl { get; set; }
		HashSet<Language> Languages { get; set; }
		HashSet<Technology> Technologies { get; set; }
		HashSet<Role> Roles { get; set; }
		HashSet<User> Friends { get; set; }
	}
}
