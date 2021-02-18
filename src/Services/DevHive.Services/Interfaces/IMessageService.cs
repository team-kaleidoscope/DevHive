using System;
using System.Threading.Tasks;
using DevHive.Services.Models.Message;

namespace DevHive.Services.Interfaces
{
	public interface IMessageService
	{
		Task<Guid> CreateMessage(CreateMessageServiceModel createMessageServiceModel);

		Task<ReadMessageServiceModel> GetMessageById(Guid id);
	}
}
