using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E15 RID: 7701
	public static class IMessageChannelExtensions
	{
		// Token: 0x0600BDEF RID: 48623 RVA: 0x00266FC8 File Offset: 0x002651C8
		public static T WaitFor<T>(this IMessageChannel channel) where T : Message
		{
			IMessageHandlers handlers = channel.Messenger.Handlers;
			T t;
			for (;;)
			{
				Message message = channel.Read();
				t = message as T;
				if (t != null)
				{
					break;
				}
				handlers.Dispatch(channel, message);
			}
			return t;
		}
	}
}
