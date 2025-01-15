using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E12 RID: 7698
	public static class IMessengerExtensions
	{
		// Token: 0x0600BDE7 RID: 48615 RVA: 0x00266ECB File Offset: 0x002650CB
		public static void AddHandler<T>(this IMessenger messenger, Action<IMessageChannel, T> handler) where T : Message
		{
			messenger.Handlers.AddHandler<T>(handler);
		}

		// Token: 0x0600BDE8 RID: 48616 RVA: 0x00266ED9 File Offset: 0x002650D9
		public static void RemoveHandler<T>(this IMessenger messenger) where T : Message
		{
			messenger.Handlers.RemoveHandler<T>();
		}

		// Token: 0x0600BDE9 RID: 48617 RVA: 0x00266EE8 File Offset: 0x002650E8
		public static void WaitFor<T>(this IMessenger messenger, Action<IMessageChannel, T> handler) where T : Message
		{
			IMessageHandlers handlers = messenger.Handlers;
			bool handled = false;
			handlers.AddHandler<T>(delegate(IMessageChannel c, T m)
			{
				handler(c, m);
				handled = true;
			});
			try
			{
				using (IMessageChannel messageChannel = messenger.CreateChannel())
				{
					while (!handled)
					{
						handlers.Dispatch(messageChannel, messageChannel.Read());
					}
				}
			}
			finally
			{
				handlers.RemoveHandler<T>();
			}
		}

		// Token: 0x0600BDEA RID: 48618 RVA: 0x00266F70 File Offset: 0x00265170
		public static T WaitFor<T>(this IMessenger messenger) where T : Message
		{
			T message = default(T);
			messenger.WaitFor(delegate(IMessageChannel c, T m)
			{
				message = m;
			});
			return message;
		}
	}
}
