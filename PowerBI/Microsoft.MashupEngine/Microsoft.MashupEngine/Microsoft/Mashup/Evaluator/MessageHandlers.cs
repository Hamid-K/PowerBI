using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CF5 RID: 7413
	internal class MessageHandlers : IMessageHandlers
	{
		// Token: 0x0600B90F RID: 47375 RVA: 0x00258291 File Offset: 0x00256491
		public MessageHandlers()
		{
			this.handlers = new Dictionary<string, Action<IMessageChannel, Message>>();
		}

		// Token: 0x0600B910 RID: 47376 RVA: 0x002582B0 File Offset: 0x002564B0
		public void AddHandler<T>(Action<IMessageChannel, T> handler) where T : Message
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.handlers.Add(typeof(T).FullName, delegate(IMessageChannel channel, Message message)
				{
					handler(channel, (T)((object)message));
				});
			}
		}

		// Token: 0x0600B911 RID: 47377 RVA: 0x00258320 File Offset: 0x00256520
		public void RemoveHandler<T>() where T : Message
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.handlers.Remove(typeof(T).FullName);
			}
		}

		// Token: 0x0600B912 RID: 47378 RVA: 0x00258378 File Offset: 0x00256578
		public void Dispatch(IMessageChannel channel, Message message)
		{
			if (!this.TryDispatch(channel, message))
			{
				throw new InvalidOperationException(Strings.Messenger_NoHandler(message.GetType().FullName));
			}
		}

		// Token: 0x0600B913 RID: 47379 RVA: 0x0025839C File Offset: 0x0025659C
		protected virtual bool TryDispatch(IMessageChannel channel, Message message)
		{
			object obj = this.syncRoot;
			Action<IMessageChannel, Message> action;
			lock (obj)
			{
				if (!this.handlers.TryGetValue(message.GetType().FullName, out action))
				{
					action = null;
				}
			}
			if (action != null)
			{
				action(channel, message);
				return true;
			}
			return false;
		}

		// Token: 0x04005E36 RID: 24118
		private readonly object syncRoot = new object();

		// Token: 0x04005E37 RID: 24119
		private readonly Dictionary<string, Action<IMessageChannel, Message>> handlers;
	}
}
