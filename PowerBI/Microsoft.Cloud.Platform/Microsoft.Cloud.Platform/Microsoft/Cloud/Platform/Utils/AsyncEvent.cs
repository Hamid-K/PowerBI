using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000185 RID: 389
	public class AsyncEvent<TEventArgs> : IAsyncEvent<TEventArgs>, IIdentifiable where TEventArgs : EventArgs
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x000228B3 File Offset: 0x00020AB3
		public AsyncEvent([NotNull] string identity, AsyncEventSubscriptionOptions options)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(identity, "identity");
			this.m_locker = new ReaderWriterLockSlim();
			this.m_name = identity;
			this.m_options = options;
			this.m_subscriptions = new List<AsyncEvent<TEventArgs>.Subscription>();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000228EC File Offset: 0x00020AEC
		public void FireEvent(object sender, TEventArgs args)
		{
			List<AsyncEvent<TEventArgs>.Subscription> subscriptions;
			using (new ReaderLock(this.m_locker))
			{
				subscriptions = this.m_subscriptions;
			}
			bool flag = (this.m_options & AsyncEventSubscriptionOptions.AsyncCallback) > AsyncEventSubscriptionOptions.None;
			using (List<AsyncEvent<TEventArgs>.Subscription>.Enumerator enumerator = subscriptions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AsyncEvent<TEventArgs>.Subscription subscription = enumerator.Current;
					if (flag || subscription.IsAsync)
					{
						AsyncInvoker.InvokeMethodAsynchronously(delegate
						{
							subscription.InvokeCallback(sender, args);
						}, WaitOrNot.DontWait, this.Name);
					}
					else
					{
						subscription.InvokeCallback(sender, args);
					}
				}
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000229EC File Offset: 0x00020BEC
		public void Subscribe(IIdentifiable subscriber, WorkTicket subscriberTicket, EventHandler<TEventArgs> callback, AsyncEventSubscriptionOptions options)
		{
			AsyncEvent<TEventArgs>.Subscription subscription = new AsyncEvent<TEventArgs>.Subscription(subscriber, subscriberTicket, callback, options);
			this.SubscribeImpl(subscription);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00022A0C File Offset: 0x00020C0C
		public void Subscribe(IIdentifiable subscriber, WorkTicket subscriberTicket, EventHandlerWithContext<TEventArgs> callback, object context, AsyncEventSubscriptionOptions options)
		{
			AsyncEvent<TEventArgs>.Subscription subscription = new AsyncEvent<TEventArgs>.Subscription(subscriber, subscriberTicket, callback, context, options);
			this.SubscribeImpl(subscription);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00022A30 File Offset: 0x00020C30
		public void Unsubscribe([NotNull] IIdentifiable subscriber)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
			AsyncEvent<TEventArgs>.Subscription subscription = null;
			using (new WriterLock(this.m_locker))
			{
				subscription = this.m_subscriptions.Find((AsyncEvent<TEventArgs>.Subscription s) => s.Subscriber == subscriber);
				if (subscription == null)
				{
					throw new ArgumentException("Unsubscribe called with no active subscription", "subscriber");
				}
				List<AsyncEvent<TEventArgs>.Subscription> list = new List<AsyncEvent<TEventArgs>.Subscription>(this.m_subscriptions);
				list.Remove(subscription);
				this.m_subscriptions = list;
			}
			subscription.Stop();
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00022AD8 File Offset: 0x00020CD8
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00022AE0 File Offset: 0x00020CE0
		private void SubscribeImpl(AsyncEvent<TEventArgs>.Subscription subscription)
		{
			using (new WriterLock(this.m_locker))
			{
				ExtendedDiagnostics.EnsureArgumentNotInCollection<AsyncEvent<TEventArgs>.Subscription>(this.m_subscriptions.Find((AsyncEvent<TEventArgs>.Subscription s) => s.Subscriber == subscription.Subscriber), "subscription", "m_subscriptions");
				this.m_subscriptions = new List<AsyncEvent<TEventArgs>.Subscription>(this.m_subscriptions) { subscription };
			}
		}

		// Token: 0x040003EE RID: 1006
		private ReaderWriterLockSlim m_locker;

		// Token: 0x040003EF RID: 1007
		private string m_name;

		// Token: 0x040003F0 RID: 1008
		private AsyncEventSubscriptionOptions m_options;

		// Token: 0x040003F1 RID: 1009
		private List<AsyncEvent<TEventArgs>.Subscription> m_subscriptions;

		// Token: 0x02000650 RID: 1616
		private class Subscription
		{
			// Token: 0x06002D10 RID: 11536 RVA: 0x000A01CE File Offset: 0x0009E3CE
			public Subscription(IIdentifiable subscriber, WorkTicket subscriberTicket, [NotNull] EventHandler<TEventArgs> callback, AsyncEventSubscriptionOptions options)
				: this(subscriber, subscriberTicket, options)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<EventHandler<TEventArgs>>(callback, "callback");
				this.m_callback = callback;
			}

			// Token: 0x06002D11 RID: 11537 RVA: 0x000A01EC File Offset: 0x0009E3EC
			public Subscription(IIdentifiable subscriber, WorkTicket subscriberTicket, [NotNull] EventHandlerWithContext<TEventArgs> callback, object context, AsyncEventSubscriptionOptions options)
				: this(subscriber, subscriberTicket, options)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<EventHandlerWithContext<TEventArgs>>(callback, "callback");
				this.m_callbackWithContext = callback;
				this.m_context = context;
			}

			// Token: 0x06002D12 RID: 11538 RVA: 0x000A0214 File Offset: 0x0009E414
			private Subscription([NotNull] IIdentifiable subscriber, [NotNull] WorkTicket subscriberTicket, AsyncEventSubscriptionOptions options)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
				ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(subscriberTicket, "subscriberTicket");
				ExtendedDiagnostics.EnsureArgument("(invalid work ticket)", "subscriberTicket", subscriberTicket.IsValid());
				this.m_subscriber = subscriber;
				this.m_subscriberTicket = subscriberTicket;
				this.m_options = options;
				this.m_cleanupLock = new StopLock(new Action(this.OnStopCompleted));
			}

			// Token: 0x06002D13 RID: 11539 RVA: 0x000A0280 File Offset: 0x0009E480
			public void InvokeCallback(object sender, TEventArgs args)
			{
				if (this.m_cleanupLock.TryEnter())
				{
					try
					{
						if (this.m_callback != null)
						{
							this.m_callback(sender, args);
						}
						else
						{
							this.m_callbackWithContext(sender, args, this.m_context);
						}
					}
					finally
					{
						this.m_cleanupLock.Leave();
					}
				}
			}

			// Token: 0x06002D14 RID: 11540 RVA: 0x000A02E4 File Offset: 0x0009E4E4
			public void Stop()
			{
				this.m_cleanupLock.Stop();
			}

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000A02F1 File Offset: 0x0009E4F1
			public IIdentifiable Subscriber
			{
				get
				{
					return this.m_subscriber;
				}
			}

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000A02F9 File Offset: 0x0009E4F9
			public bool IsAsync
			{
				get
				{
					return (this.m_options & AsyncEventSubscriptionOptions.AsyncCallback) > AsyncEventSubscriptionOptions.None;
				}
			}

			// Token: 0x06002D17 RID: 11543 RVA: 0x000A0306 File Offset: 0x0009E506
			private void OnStopCompleted()
			{
				this.m_subscriberTicket.Dispose();
				this.m_subscriberTicket = null;
			}

			// Token: 0x040011CA RID: 4554
			private IIdentifiable m_subscriber;

			// Token: 0x040011CB RID: 4555
			private WorkTicket m_subscriberTicket;

			// Token: 0x040011CC RID: 4556
			private EventHandler<TEventArgs> m_callback;

			// Token: 0x040011CD RID: 4557
			private EventHandlerWithContext<TEventArgs> m_callbackWithContext;

			// Token: 0x040011CE RID: 4558
			private object m_context;

			// Token: 0x040011CF RID: 4559
			private AsyncEventSubscriptionOptions m_options;

			// Token: 0x040011D0 RID: 4560
			private StopLock m_cleanupLock;
		}
	}
}
