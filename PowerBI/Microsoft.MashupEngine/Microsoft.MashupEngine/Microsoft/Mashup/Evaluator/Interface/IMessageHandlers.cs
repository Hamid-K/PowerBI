using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E0F RID: 7695
	public interface IMessageHandlers
	{
		// Token: 0x0600BDDC RID: 48604
		void AddHandler<T>(Action<IMessageChannel, T> handler) where T : Message;

		// Token: 0x0600BDDD RID: 48605
		void RemoveHandler<T>() where T : Message;

		// Token: 0x0600BDDE RID: 48606
		void Dispatch(IMessageChannel channel, Message message);
	}
}
