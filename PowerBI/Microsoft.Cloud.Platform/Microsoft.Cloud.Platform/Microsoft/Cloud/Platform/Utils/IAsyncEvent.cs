using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000184 RID: 388
	public interface IAsyncEvent<TEventArgs> : IIdentifiable where TEventArgs : EventArgs
	{
		// Token: 0x06000A05 RID: 2565
		void Subscribe(IIdentifiable subscriber, WorkTicket subscriberTicket, EventHandler<TEventArgs> callback, AsyncEventSubscriptionOptions options);

		// Token: 0x06000A06 RID: 2566
		void Subscribe(IIdentifiable subscriber, WorkTicket subscriberTicket, EventHandlerWithContext<TEventArgs> callback, object context, AsyncEventSubscriptionOptions options);

		// Token: 0x06000A07 RID: 2567
		void Unsubscribe(IIdentifiable subscriber);
	}
}
