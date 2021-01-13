using System;
using System.Threading.Tasks;
using DevHive.Data.Models;
using DevHive.Data.Repositories.Interfaces;

namespace DevHive.Data.Interfaces
{
	public interface ILanguageRepository : IRepository<Language>
	{
		Task<bool> DoesLanguageExistAsync(Guid id);
		Task<bool> DoesLanguageNameExistAsync(string languageName);
	}
}