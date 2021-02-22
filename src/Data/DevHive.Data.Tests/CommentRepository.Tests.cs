using System;
using System.Threading.Tasks;
using DevHive.Data.Models;
using DevHive.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DevHive.Data.Tests
{
	[TestFixture]
	public class CommentRepositoryTests
	{
		private const string COMMENT_MESSAGE = "Comment message";

		protected DevHiveContext Context { get; set; }

		protected CommentRepository CommentRepository { get; set; }

		#region Setups
		[SetUp]
		public void Setup()
		{
			DbContextOptionsBuilder<DevHiveContext> optionsBuilder = new DbContextOptionsBuilder<DevHiveContext>()
				.UseInMemoryDatabase(databaseName: "DevHive_Test_Database");

			this.Context = new DevHiveContext(optionsBuilder.Options);

			this.CommentRepository = new CommentRepository(this.Context);
		}

		[TearDown]
		public void TearDown()
		{
			_ = this.Context.Database.EnsureDeleted();
		}
		#endregion

		#region GetCommentByIssuerAndTimeCreatedAsync
		[Test]
		public async Task GetCommentByCreatorAndTimeCreatedAsync_ReturnsTheCorrectComment_IfItExists()
		{
			Comment comment = await this.AddEntity();

			Comment resultComment = await this.CommentRepository.GetCommentByIssuerAndTimeCreatedAsync(comment.Creator.Id, comment.TimeCreated);

			Assert.AreEqual(comment.Id, resultComment.Id, "GetCommentByIssuerAndTimeCreatedAsync does not return the corect comment when it exists");
		}

		[Test]
		public async Task GetPostByCreatorAndTimeCreatedAsync_ReturnsNull_IfThePostDoesNotExist()
		{
			_ = await this.AddEntity();

			Comment resultComment = await this.CommentRepository.GetCommentByIssuerAndTimeCreatedAsync(Guid.Empty, DateTime.Now);

			Assert.IsNull(resultComment, "GetCommentByIssuerAndTimeCreatedAsync does not return null when the comment does not exist");
		}
		#endregion

		#region DoesCommentExist
		[Test]
		public async Task DoesCommentExist_ReturnsTrue_WhenTheCommentExists()
		{
			Comment comment = await this.AddEntity();

			bool result = await this.CommentRepository.DoesCommentExist(comment.Id);

			Assert.IsTrue(result, "DoesCommentExist does not return true whenm the Comment exists");
		}

		[Test]
		public async Task DoesCommentExist_ReturnsFalse_WhenTheCommentDoesNotExist()
		{
			bool result = await this.CommentRepository.DoesCommentExist(Guid.Empty);

			Assert.IsFalse(result, "DoesCommentExist does not return false whenm the Comment" +
				" does not exist");
		}
		#endregion

		#region HelperMethods
		private async Task<Comment> AddEntity()
		{
			User creator = new() { Id = Guid.NewGuid() };
			Comment comment = new()
			{
				Message = COMMENT_MESSAGE,
				Creator = creator,
				TimeCreated = DateTime.Now
			};

			_ = this.Context.Comments.Add(comment);
			_ = await this.Context.SaveChangesAsync();

			return comment;
		}
		#endregion
	}
}
