using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000288 RID: 648
	public class SerializedAsyncEvent<TEventArgs> : IIdentifiable where TEventArgs : EventArgs
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x0003CC30 File Offset: 0x0003AE30
		public SerializedAsyncEvent([NotNull] string identity)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>("identity", identity);
			this.Name = identity;
			this.m_stop = false;
			this.m_asyncEvent = new AsyncEvent<TEventArgs>(identity, AsyncEventSubscriptionOptions.None);
			this.m_callGate = new CallGate();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003CC6C File Offset: 0x0003AE6C
		public void FireEvent(object sender, TEventArgs args)
		{
			this.m_callGate.CallAsync(delegate(object obj)
			{
				this.FireEventImpl(sender, args, false);
			}, null);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		public void Stop()
		{
			this.m_callGate.CallAsync(delegate(object obj)
			{
				this.FireEventImpl(this, default(TEventArgs), true);
			}, null);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003CCC8 File Offset: 0x0003AEC8
		public void Subscribe(IIdentifiable subscriber, [NotNull] EventHandler<TEventArgs> consumeEvents, Action onStop, WorkTicket subscriberTicket)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EventHandler<TEventArgs>>(consumeEvents, "consumeEvents");
			EventHandler<TEventArgs> eventHandler = delegate(object sender, TEventArgs e)
			{
				if (this.m_stop)
				{
					if (onStop != null)
					{
						onStop();
						return;
					}
				}
				else
				{
					consumeEvents(sender, e);
				}
			};
			this.m_asyncEvent.Subscribe(subscriber, subscriberTicket, eventHandler, AsyncEventSubscriptionOptions.None);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		public void Subscribe(IIdentifiable subscriber, [NotNull] EventHandlerWithContext<TEventArgs> consumeEvents, Action<object> onStop, WorkTicket subscriberTicket, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EventHandlerWithContext<TEventArgs>>(consumeEvents, "consumeEvents");
			EventHandler<TEventArgs> eventHandler = delegate(object sender, TEventArgs e)
			{
				if (this.m_stop)
				{
					if (onStop != null)
					{
						onStop(context);
						return;
					}
				}
				else
				{
					consumeEvents(sender, e, context);
				}
			};
			this.m_asyncEvent.Subscribe(subscriber, subscriberTicket, eventHandler, AsyncEventSubscriptionOptions.None);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003CD77 File Offset: 0x0003AF77
		public void Unsubscribe(IIdentifiable subscriber)
		{
			this.m_asyncEvent.Unsubscribe(subscriber);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003CD85 File Offset: 0x0003AF85
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x0003CD8D File Offset: 0x0003AF8D
		public string Name { get; private set; }

		// Token: 0x06001175 RID: 4469 RVA: 0x0003CD96 File Offset: 0x0003AF96
		private void FireEventImpl(object sender, TEventArgs args, bool isStopped)
		{
			if (!this.m_stop)
			{
				if (isStopped)
				{
					this.m_stop = true;
				}
				this.m_asyncEvent.FireEvent(sender, args);
				return;
			}
			if (isStopped)
			{
				throw new SerializedAsyncEventAlreadyStoppedException();
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "FireEvent was called after stop therefore no event will be fired");
		}

		// Token: 0x0400065C RID: 1628
		private AsyncEvent<TEventArgs> m_asyncEvent;

		// Token: 0x0400065D RID: 1629
		private CallGate m_callGate;

		// Token: 0x0400065E RID: 1630
		private bool m_stop;
	}
}
