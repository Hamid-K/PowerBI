using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200019B RID: 411
	public class BatchedAsyncEvent<T> : IIdentifiable, IShuttable
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x000240C4 File Offset: 0x000222C4
		public BatchedAsyncEvent([NotNull] string identity, int periodTime, int maxNumEventsInBatch, BatchedAsyncEventOptions options)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(identity, "identity");
			ExtendedDiagnostics.EnsureArgument(periodTime, "periodTime", periodTime > 0 || periodTime == -1);
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxNumEventsInBatch, "maxNumEventsInBatch");
			this.m_lock = new object();
			this.Name = identity;
			this.m_timerFactory = new TimerFactory(this.Name, TimerCreationFlags.Crash);
			this.m_timer = this.m_timerFactory.SchedulePeriodicTimer(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { this.Name, "Timer" }), -1, delegate(object state)
			{
				this.OnCurrentEventBatchCompleted();
			}, null);
			this.UpdateThreshold(maxNumEventsInBatch);
			this.m_options = options;
			this.m_asyncEvent = new SerializedAsyncEvent<EventBatchEventArgs<T>>(this.Name);
			if ((options & BatchedAsyncEventOptions.SaveHistory) == BatchedAsyncEventOptions.SaveHistory)
			{
				this.m_historicEventBatch = new List<T>();
				this.m_workTicketManager = new WorkTicketManager(string.Format(CultureInfo.CurrentCulture, "{0}.WorkTicketManager", new object[] { this.Name }));
				WorkTicket workTicket = this.m_workTicketManager.CreateWorkTicket(this);
				using (DisposeController disposeController = new DisposeController(workTicket))
				{
					this.m_asyncEvent.Subscribe(this, delegate(object sender, EventBatchEventArgs<T> args)
					{
						this.m_historicEventBatch.AddRange(args.Data);
					}, delegate
					{
						this.m_asyncEvent.Unsubscribe(this);
					}, workTicket);
					disposeController.PreventDispose();
				}
			}
			this.m_currentEventBatch = new List<T>();
			if (periodTime != -1)
			{
				this.UpdateTimerPeriod(periodTime);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00024238 File Offset: 0x00022438
		public void Subscribe([NotNull] IIdentifiable subscriber, [NotNull] EventHandler<EventBatchEventArgs<T>> consumeEvents, Action onStop, WorkTicket subscriberTicket)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
			ExtendedDiagnostics.EnsureArgumentNotNull<EventHandler<EventBatchEventArgs<T>>>(consumeEvents, "consumeEvents");
			bool firstCall = true;
			EventHandler<EventBatchEventArgs<T>> eventHandler = delegate(object sender, EventBatchEventArgs<T> e)
			{
				EventBatchEventArgs<T> eventBatchEventArgs = e;
				if (firstCall)
				{
					firstCall = false;
					if (this.m_historicEventBatch != null)
					{
						eventBatchEventArgs = new EventBatchEventArgs<T>(this.m_historicEventBatch.AsReadOnly());
					}
				}
				consumeEvents(sender, eventBatchEventArgs);
			};
			this.m_asyncEvent.Subscribe(subscriber, eventHandler, onStop, subscriberTicket);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00024298 File Offset: 0x00022498
		public void Subscribe([NotNull] IIdentifiable subscriber, [NotNull] EventHandlerWithContext<EventBatchEventArgs<T>> consumeEvents, Action<object> onStop, WorkTicket subscriberTicket, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
			ExtendedDiagnostics.EnsureArgumentNotNull<EventHandlerWithContext<EventBatchEventArgs<T>>>(consumeEvents, "consumeEvents");
			bool firstCall = true;
			EventHandlerWithContext<EventBatchEventArgs<T>> eventHandlerWithContext = delegate(object sender, EventBatchEventArgs<T> e, object ctx)
			{
				EventBatchEventArgs<T> eventBatchEventArgs = e;
				if (firstCall)
				{
					firstCall = false;
					if (this.m_historicEventBatch != null)
					{
						eventBatchEventArgs = new EventBatchEventArgs<T>(this.m_historicEventBatch.AsReadOnly());
					}
				}
				consumeEvents(sender, eventBatchEventArgs, ctx);
			};
			this.m_asyncEvent.Subscribe(subscriber, eventHandlerWithContext, onStop, subscriberTicket, context);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000242F8 File Offset: 0x000224F8
		public void Unsubscribe([NotNull] IIdentifiable subscriber)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
			this.m_asyncEvent.Unsubscribe(subscriber);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00024314 File Offset: 0x00022514
		public void FireEvent(T e)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_currentEventBatch.Add(e);
				if (this.m_currentEventBatch.Count >= this.m_maxNumEventsInBatch)
				{
					this.OnCurrentEventBatchCompleted();
				}
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00024374 File Offset: 0x00022574
		public void UpdateTimerPeriod(int periodTime)
		{
			ExtendedDiagnostics.EnsureArgument(periodTime, "periodTime", periodTime > 0 || periodTime == -1);
			PeriodicTimer timer = this.m_timer;
			lock (timer)
			{
				this.m_timer.UpdatePeriod(periodTime);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000243D0 File Offset: 0x000225D0
		public void UpdateThreshold(int maxNumEventsInBatch)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxNumEventsInBatch, "maxNumEventsInBatch");
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_maxNumEventsInBatch = maxNumEventsInBatch;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002441C File Offset: 0x0002261C
		public void ClearEvents()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_historicEventBatch.Clear();
				this.m_currentEventBatch.Clear();
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002446C File Offset: 0x0002266C
		public void Stop()
		{
			PeriodicTimer timer = this.m_timer;
			lock (timer)
			{
				this.m_timerFactory.Stop();
				this.OnCurrentEventBatchCompleted();
				this.m_asyncEvent.Stop();
				if (this.m_workTicketManager != null)
				{
					this.m_workTicketManager.Stop();
				}
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000244D8 File Offset: 0x000226D8
		public void WaitForStopToComplete()
		{
			this.m_timerFactory.WaitForStopToComplete();
			if (this.m_workTicketManager != null)
			{
				this.m_workTicketManager.WaitForStopToComplete();
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000244F8 File Offset: 0x000226F8
		public void Shutdown()
		{
			this.m_timerFactory.Shutdown();
			if (this.m_workTicketManager != null)
			{
				this.m_workTicketManager.Shutdown();
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00024518 File Offset: 0x00022718
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00024520 File Offset: 0x00022720
		public string Name { get; private set; }

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002452C File Offset: 0x0002272C
		private void OnCurrentEventBatchCompleted()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				bool flag2 = (this.m_options & BatchedAsyncEventOptions.FireEmptyBatch) == BatchedAsyncEventOptions.FireEmptyBatch;
				if (this.m_currentEventBatch.Any<T>() || flag2)
				{
					ReadOnlyCollection<T> readOnlyCollection = this.m_currentEventBatch.AsReadOnly();
					this.m_currentEventBatch = new List<T>();
					EventBatchEventArgs<T> eventBatchEventArgs = new EventBatchEventArgs<T>(readOnlyCollection);
					this.m_asyncEvent.FireEvent(this, eventBatchEventArgs);
				}
			}
		}

		// Token: 0x0400041E RID: 1054
		private List<T> m_historicEventBatch;

		// Token: 0x0400041F RID: 1055
		private List<T> m_currentEventBatch;

		// Token: 0x04000420 RID: 1056
		private SerializedAsyncEvent<EventBatchEventArgs<T>> m_asyncEvent;

		// Token: 0x04000421 RID: 1057
		private TimerFactory m_timerFactory;

		// Token: 0x04000422 RID: 1058
		private PeriodicTimer m_timer;

		// Token: 0x04000423 RID: 1059
		private WorkTicketManager m_workTicketManager;

		// Token: 0x04000424 RID: 1060
		private int m_maxNumEventsInBatch;

		// Token: 0x04000425 RID: 1061
		private BatchedAsyncEventOptions m_options;

		// Token: 0x04000426 RID: 1062
		private object m_lock;
	}
}
