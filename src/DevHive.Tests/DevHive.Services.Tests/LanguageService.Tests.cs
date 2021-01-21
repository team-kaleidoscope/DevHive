using System;
using System.Threading.Tasks;
using AutoMapper;
using DevHive.Data.Interfaces.Repositories;
using DevHive.Data.Models;
using DevHive.Services.Models.Language;
using DevHive.Services.Services;
using Moq;
using NUnit.Framework;

namespace DevHive.Services.Tests
{
	[TestFixture]
	public class LanguageServiceTests
	{
		private Mock<ILanguageRepository> LanguageRepositoryMock { get; set; }
		private Mock<IMapper> MapperMock { get; set; }
		private LanguageService LanguageService { get; set; }

		[SetUp]
		public void SetUp()
		{
			this.LanguageRepositoryMock = new Mock<ILanguageRepository>();
			this.MapperMock = new Mock<IMapper>();
			this.LanguageService = new LanguageService(this.LanguageRepositoryMock.Object, this.MapperMock.Object);
		}

		#region Create
		[Test]
		public async Task CreateLanguage_ReturnsNonEmptyGuid_WhenEntityIsAddedSuccessfully()
		{
			string technologyName = "Gosho Trapov";
			Guid id = Guid.NewGuid();
			CreateLanguageServiceModel createLanguageServiceModel = new CreateLanguageServiceModel
			{
				Name = technologyName
			};
			Language language = new Language
			{
				Name = technologyName,
				Id = id
			};

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageNameExistAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
			this.LanguageRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Language>())).Returns(Task.FromResult(true));
			this.LanguageRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(language));
			this.MapperMock.Setup(p => p.Map<Language>(It.IsAny<CreateLanguageServiceModel>())).Returns(language);

			Guid result = await this.LanguageService.CreateLanguage(createLanguageServiceModel);

			Assert.AreEqual(id, result);
		}

		[Test]
		public async Task CreateLanguage_ReturnsEmptyGuid_WhenEntityIsNotAddedSuccessfully()
		{
			string languageName = "Gosho Trapov";

			CreateLanguageServiceModel createLanguageServiceModel = new CreateLanguageServiceModel
			{
				Name = languageName
			};
			Language language = new Language
			{
				Name = languageName
			};

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageNameExistAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
			this.LanguageRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Language>())).Returns(Task.FromResult(false));
			this.MapperMock.Setup(p => p.Map<Language>(It.IsAny<CreateLanguageServiceModel>())).Returns(language);

			Guid result = await this.LanguageService.CreateLanguage(createLanguageServiceModel);

			Assert.IsTrue(result == Guid.Empty);

		}

		[Test]
		public void CreateLanguage_ThrowsArgumentException_WhenEntityAlreadyExists()
		{
			string exceptionMessage = "Language already exists!";
			string languageName = "Gosho Trapov";

			CreateLanguageServiceModel createLanguageServiceModel = new CreateLanguageServiceModel
			{
				Name = languageName
			};
			Language language = new Language
			{
				Name = languageName
			};

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageNameExistAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

			Exception ex = Assert.ThrowsAsync<ArgumentException>(() => this.LanguageService.CreateLanguage(createLanguageServiceModel));

			Assert.AreEqual(exceptionMessage, ex.Message, "Incorecct exception message");
		}
		#endregion

		#region Read
		[Test]
		public async Task GetLanguageById_ReturnsTheLanguage_WhenItExists()
		{
			Guid id = new Guid();
			string name = "Gosho Trapov";
			Language language = new Language()
			{
				Name = name
			};
			ReadLanguageServiceModel readLanguageServiceModel = new ReadLanguageServiceModel
			{
				Name = name
			};

			this.LanguageRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(language));
			this.MapperMock.Setup(p => p.Map<ReadLanguageServiceModel>(It.IsAny<Language>())).Returns(readLanguageServiceModel);

			ReadLanguageServiceModel result = await this.LanguageService.GetLanguageById(id);

			Assert.AreEqual(name, result.Name);
		}

		[Test]
		public void GetLanguageById_ThrowsException_WhenLanguageDoesNotExist()
		{
			string exceptionMessage = "The language does not exist";
			Guid id = new Guid();
			this.LanguageRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Language>(null));

			Exception ex = Assert.ThrowsAsync<ArgumentException>(() => this.LanguageService.GetLanguageById(id));

			Assert.AreEqual(exceptionMessage, ex.Message, "Incorecct exception message");
		}
		#endregion

		#region Update
		[Test]
		[TestCase(true)]
		[TestCase(false)]
		public async Task UpdateLanguage_ReturnsIfUpdateIsSuccessfull_WhenLanguageExistsy(bool shouldPass)
		{
			string name = "Gosho Trapov";
			Language language = new Language
			{
				Name = name
			};
			UpdateLanguageServiceModel updateLanguageServiceModel = new UpdateLanguageServiceModel
			{
				Name = name,
			};

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageExistAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));
			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageNameExistAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
			this.LanguageRepositoryMock.Setup(p => p.EditAsync(It.IsAny<Language>())).Returns(Task.FromResult(shouldPass));
			this.MapperMock.Setup(p => p.Map<Language>(It.IsAny<UpdateLanguageServiceModel>())).Returns(language);

			bool result = await this.LanguageService.UpdateLanguage(updateLanguageServiceModel);

			Assert.AreEqual(shouldPass, result);
		}

		[Test]
		public void UpdateLanguage_ReturnsIfUpdateIsSuccessfull_WhenLanguageExistsy()
		{
			string exceptionMessage = "Language does not exist!";
			UpdateLanguageServiceModel updateTechnologyServiceModel = new UpdateLanguageServiceModel
			{
			};

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageExistAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

			Exception ex = Assert.ThrowsAsync<ArgumentException>(() => this.LanguageService.UpdateLanguage(updateTechnologyServiceModel));

			Assert.AreEqual(exceptionMessage, ex.Message, "Incorecct exception message");
		}

		[Test]
		public void UpdateLanguage_ThrowsException_WhenLanguageNameAlreadyExists()
		{
			string exceptionMessage = "Language name already exists in our data base!";
			UpdateLanguageServiceModel updateTechnologyServiceModel = new UpdateLanguageServiceModel
			{
			};

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageExistAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));
			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageNameExistAsync(It.IsAny<string>())).Returns(Task.FromResult(true));

			Exception ex = Assert.ThrowsAsync<ArgumentException>(() => this.LanguageService.UpdateLanguage(updateTechnologyServiceModel));

			Assert.AreEqual(exceptionMessage, ex.Message, "Incorecct exception message");
		}
		#endregion

		#region Delete
		[Test]
		[TestCase(true)]
		[TestCase(false)]
		public async Task DeleteLanguage_ShouldReturnIfDeletionIsSuccessfull_WhenLanguageExists(bool shouldPass)
		{
			Guid id = new Guid();
			Language language = new Language();

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageExistAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));
			this.LanguageRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(language));
			this.LanguageRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<Language>())).Returns(Task.FromResult(shouldPass));

			bool result = await this.LanguageService.DeleteLanguage(id);

			Assert.AreEqual(shouldPass, result);
		}

		[Test]
		public void DeleteLanguage_ThrowsException_WhenLanguageDoesNotExist()
		{
			string exceptionMessage = "Language does not exist!";
			Guid id = new Guid();

			this.LanguageRepositoryMock.Setup(p => p.DoesLanguageExistAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

			Exception ex = Assert.ThrowsAsync<ArgumentException>(() => this.LanguageService.DeleteLanguage(id));

			Assert.AreEqual(exceptionMessage, ex.Message, "Incorecct exception message");
		}
		#endregion
		//Task.Run(async () =>
		//{

		//}).GetAwaiter().GetResult();
	}
}
