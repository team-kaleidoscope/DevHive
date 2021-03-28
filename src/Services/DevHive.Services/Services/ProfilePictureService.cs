using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevHive.Data.Interfaces;
using DevHive.Data.Models;
using DevHive.Services.Interfaces;
using DevHive.Services.Models.ProfilePicture;
using Microsoft.AspNetCore.Http;

namespace DevHive.Services.Services
{
	public class ProfilePictureService : IProfilePictureService
	{
		private readonly IUserRepository _userRepository;
		private readonly IProfilePictureRepository _profilePictureRepository;
		private readonly ICloudService _cloudinaryService;

		public ProfilePictureService(IUserRepository userRepository, IProfilePictureRepository profilePictureRepository, ICloudService cloudinaryService)
		{
			this._userRepository = userRepository;
			this._profilePictureRepository = profilePictureRepository;
			this._cloudinaryService = cloudinaryService;
		}

		public async Task<string> InsertProfilePicture(ProfilePictureServiceModel profilePictureServiceModel)
		{
			ValidateProfPic(profilePictureServiceModel.ProfilePictureFormFile);
			await ValidateUserExistsAsync(profilePictureServiceModel.UserId);

			return await SaveProfilePictureInDatabase(profilePictureServiceModel);
		}

		public async Task<string> GetProfilePictureById(Guid id)
		{
			return (await this._profilePictureRepository.GetByIdAsync(id)).PictureURL;
		}

		public async Task<string> UpdateProfilePicture(ProfilePictureServiceModel profilePictureServiceModel)
		{
			ValidateProfPic(profilePictureServiceModel.ProfilePictureFormFile);
			await ValidateUserExistsAsync(profilePictureServiceModel.UserId);

			User user = await this._userRepository.GetByIdAsync(profilePictureServiceModel.UserId);
			// if (user.ProfilePicture.PictureURL != ProfilePicture.DefaultURL)
			if (user.ProfilePicture.Id != Guid.Empty)
			{
				List<string> file = new() { user.ProfilePicture.PictureURL };
				bool removed = await this._cloudinaryService.RemoveFilesFromCloud(file);

				if (!removed)
					throw new ArgumentException("Cannot delete old picture");
			}

			return await SaveProfilePictureInDatabase(profilePictureServiceModel);
		}

		public async Task<bool> DeleteProfilePicture(Guid id)
		{
			ProfilePicture profilePic = await this._profilePictureRepository.GetByIdAsync(id) ??
				throw new ArgumentException("Such picture doesn't exist!");

			bool removedFromDb = await this._profilePictureRepository.DeleteAsync(profilePic);
			if (!removedFromDb)
				throw new ArgumentException("Cannot delete picture from database!");

			List<string> file = new() { profilePic.PictureURL };
			bool removedFromCloud = await this._cloudinaryService.RemoveFilesFromCloud(file);
			if (!removedFromCloud)
				throw new ArgumentException("Cannot delete picture from cloud!");

			return true;
		}

		private async Task<string> SaveProfilePictureInDatabase(ProfilePictureServiceModel profilePictureServiceModel)
		{
			List<IFormFile> file = new() { profilePictureServiceModel.ProfilePictureFormFile };
			string picUrl = (await this._cloudinaryService.UploadFilesToCloud(file))[0];
			ProfilePicture profilePic = new() { PictureURL = picUrl };

			User user = await this._userRepository.GetByIdAsync(profilePictureServiceModel.UserId);
			profilePic.UserId = user.Id;
			profilePic.User = user;

			bool success = await this._profilePictureRepository.AddAsync(profilePic);
			if (!success)
				throw new ArgumentException("Unable to upload picture!");

			user.ProfilePicture = profilePic;
			bool userProfilePicAlter = await this._userRepository.EditAsync(user.Id, user);

			if (!userProfilePicAlter)
				throw new ArgumentException("Unable to alter user's profile picture");

			return picUrl;
		}

		private static void ValidateProfPic(IFormFile profilePictureFormFile)
		{
			if (profilePictureFormFile.Length == 0)
				throw new ArgumentException("Picture cannot be null");
		}

		private async Task ValidateUserExistsAsync(Guid userId)
		{
			if (!await this._userRepository.DoesUserExistAsync(userId))
				throw new ArgumentException("User does not exist!");
		}
	}
}
