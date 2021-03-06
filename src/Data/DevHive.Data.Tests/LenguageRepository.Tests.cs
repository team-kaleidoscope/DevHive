using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHive.Data.Models;
using DevHive.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DevHive.Data.Tests
{
	[TestFixture]
	public class LenguageRepositoryTests
	{
		private const string LANGUAGE_NAME = "Language test name";
		private DevHiveContext _context;
		private LanguageRepository _languageRepository;

		#region Setups
		[SetUp]
		public void Setup()
		{
			DbContextOptionsBuilder<DevHiveContext> optionsBuilder = new DbContextOptionsBuilder<DevHiveContext>()
				.UseInMemoryDatabase(databaseName: "DevHive_Test_Database");

			this._context = new DevHiveContext(optionsBuilder.Options);

			this._languageRepository = new LanguageRepository(this._context);
		}

		[TearDown]
		public void TearDown()
		{
			this._context.Database.EnsureDeleted();
		}
		#endregion

		#region GetByNameAsync
		[Test]
		public async Task GetByNameAsync_ReturnsTheCorrectLanguage_IfItExists()
		{
			await this.AddEntity();

			Language language = this._context.Languages.Where(x => x.Name == LANGUAGE_NAME).AsEnumerable().FirstOrDefault();

			Language languageResult = await this._languageRepository.GetByNameAsync(LANGUAGE_NAME);

			Assert.AreEqual(language.Id, languageResult.Id);
		}

		[Test]
		public async Task GetByNameAsync_ReturnsNull_IfTechnologyDoesNotExists()
		{
			Language languageResult = await this._languageRepository.GetByNameAsync(LANGUAGE_NAME);

			Assert.IsNull(languageResult);
		}
		#endregion

		#region GetLanguages
		[Test]
		public async Task GetLanguages_ReturnsAllLanguages()
		{
			await this.AddEntity();
			await this.AddEntity("secondLanguage");
			await this.AddEntity("thirdLanguage");

			HashSet<Language> languages = this._languageRepository.GetLanguages();

			Assert.GreaterOrEqual(languages.Count, 3, "GetLanguages does not get all Languages");
		}
		#endregion

		#region DoesLanguageExistAsync
		[Test]
		public async Task DoesLanguageExist_ReturnsTrue_IfIdExists()
		{
			await this.AddEntity();
			Language language = this._context.Languages.Where(x => x.Name == LANGUAGE_NAME).AsEnumerable().FirstOrDefault();

			Guid id = language.Id;

			bool result = await this._languageRepository.DoesLanguageExistAsync(id);

			Assert.IsTrue(result, "DoesLanguageExistAsync returns flase when language exists");
		}

		[Test]
		public async Task DoesLanguageExist_ReturnsFalse_IfIdDoesNotExists()
		{
			Guid id = Guid.NewGuid();

			bool result = await this._languageRepository.DoesLanguageExistAsync(id);

			Assert.IsFalse(result, "DoesLanguageExistAsync returns true when language does not exist");
		}
		#endregion

		#region DoesTechnologyNameExistAsync
		[Test]
		public async Task DoesLanguageNameExist_ReturnsTrue_IfLanguageExists()
		{
			await this.AddEntity();

			bool result = await this._languageRepository.DoesLanguageNameExistAsync(LANGUAGE_NAME);

			Assert.IsTrue(result, "DoesLanguageNameExists returns true when language name does not exist");
		}

		[Test]
		public async Task DoesLanguageNameExist_ReturnsFalse_IfLanguageDoesNotExists()
		{
			bool result = await this._languageRepository.DoesLanguageNameExistAsync(LANGUAGE_NAME);

			Assert.False(result, "DoesTechnologyNameExistAsync returns true when language name does not exist");
		}
		#endregion

		#region HelperMethods
		private async Task AddEntity(string name = LANGUAGE_NAME)
		{
			Language language = new()
			{
				Id = Guid.NewGuid(),
				Name = name
			};

			await this._context.Languages.AddAsync(language);
			await this._context.SaveChangesAsync();
		}
		#endregion
	}
}
