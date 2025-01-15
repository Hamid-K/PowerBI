using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200035C RID: 860
	[BlockServiceProvider(typeof(IEventsListener))]
	[BlockServiceProvider(typeof(IEventsPublisher))]
	[BlockServiceProvider(typeof(IEventTestability))]
	public class InProcessEventsDatabase : Block, IEventsListener, IEventsPublisher, IEventTestability
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x0005E2B4 File Offset: 0x0005C4B4
		public InProcessEventsDatabase()
			: base(typeof(InProcessEventsDatabase).Name)
		{
			this.m_lock = new object();
			this.m_periodTimeTweak = Anchor.Tweaks.RegisterTweak<int>("Microsoft.Cloud.Platform.EventsKit.Mocks.InProcessEventsDatabase.PeriodTime", "The period time that the InProcessEventsDatabase will set the BatchedAsyncEvent to raise events.", delegate(Tweak t)
			{
				object lock2 = this.m_lock;
				lock (lock2)
				{
					if (this.m_batchedAsyncEvent != null)
					{
						this.m_batchedAsyncEvent.UpdateTimerPeriod(((Tweak<int>)t).Value);
					}
				}
			}, 500);
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_batchedAsyncEvent = new BatchedAsyncEvent<WireEventBase>(base.Name + ".BatchedAsyncEvent", this.m_periodTimeTweak.Value, int.MaxValue, BatchedAsyncEventOptions.SaveHistory | BatchedAsyncEventOptions.FireEmptyBatch);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005E368 File Offset: 0x0005C568
		public void PublishEvent([NotNull] WireEventBase firedEvent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<WireEventBase>(firedEvent, "firedEvent");
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Publishing event {0}", new object[] { firedEvent.ToMonitoringString() });
			this.m_batchedAsyncEvent.FireEvent(firedEvent);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0005E3A0 File Offset: 0x0005C5A0
		public IAsyncResult BeginWaitForEvent<T>([NotNull] Expression<Func<T, bool>> predicate, int occurrences, int timeout, AsyncCallback callback, object state) where T : WireEventBase
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(occurrences, "occurrences");
			ExtendedDiagnostics.EnsureArgumentNotNull<Expression<Func<T, bool>>>(predicate, "predicate");
			ExtendedDiagnostics.EnsureArgument("timeout", timeout == -1 || timeout >= 0, "timeout can be either Timeout.Infinite or not negative.");
			WorkTicket workTicket = base.WorkTicketManager.CreateWorkTicket(this);
			InProcessEventsDatabase.PendingWaitForEvent<T> requestHandler;
			using (DisposeController disposeController = new DisposeController(workTicket))
			{
				requestHandler = new InProcessEventsDatabase.PendingWaitForEvent<T>(predicate, timeout, occurrences, this.m_batchedAsyncEvent, callback, state);
				disposeController.PreventDispose();
			}
			EventHandler<EventBatchEventArgs<WireEventBase>> eventHandler = delegate(object sender, EventBatchEventArgs<WireEventBase> args)
			{
				IEnumerable<T> enumerable = args.Data.OfType<T>();
				requestHandler.ProcessEvents(enumerable);
			};
			this.m_batchedAsyncEvent.Subscribe(requestHandler, eventHandler, new Action(requestHandler.Stop), workTicket);
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Start waiting for {0} occurrences of event type {1}", new object[]
			{
				occurrences,
				typeof(T).Name
			});
			return requestHandler;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0005E49C File Offset: 0x0005C69C
		public EventsQueryResult<T> EndWaitForEvent<T>([NotNull] IAsyncResult asyncResult) where T : WireEventBase
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IAsyncResult>(asyncResult, "asyncResult");
			EventsQueryResult<T> eventsQueryResult = ((InProcessEventsDatabase.PendingWaitForEvent<T>)asyncResult).End();
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "End wait for event {0} finish successfully after {1} occurrences", new object[]
			{
				typeof(T).Name,
				eventsQueryResult.Events.Count<T>()
			});
			return eventsQueryResult;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0005E4FC File Offset: 0x0005C6FC
		public EventsQueryResult<T> WaitForEvent<T>(Expression<Func<T, bool>> predicate, int occurrences, int timeout) where T : WireEventBase
		{
			return this.EndWaitForEvent<T>(this.BeginWaitForEvent<T>(predicate, occurrences, timeout, null, null));
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0005E50F File Offset: 0x0005C70F
		public void ClearEvents()
		{
			this.m_batchedAsyncEvent.ClearEvents();
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005E51C File Offset: 0x0005C71C
		internal static void SetNotificationPeriodTime(int periodTimeMSec)
		{
			Anchor.Tweaks.SetProgrammaticAppSwitch("Microsoft.Cloud.Platform.EventsKit.Mocks.InProcessEventsDatabase.PeriodTime", periodTimeMSec.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x040008C5 RID: 2245
		[AutoShuttable]
		protected BatchedAsyncEvent<WireEventBase> m_batchedAsyncEvent;

		// Token: 0x040008C6 RID: 2246
		private Tweak<int> m_periodTimeTweak;

		// Token: 0x040008C7 RID: 2247
		private object m_lock;

		// Token: 0x040008C8 RID: 2248
		private const int c_defaultPeriodTimeMSec = 500;

		// Token: 0x040008C9 RID: 2249
		private const string c_periodTimeTweakName = "Microsoft.Cloud.Platform.EventsKit.Mocks.InProcessEventsDatabase.PeriodTime";

		// Token: 0x020007A7 RID: 1959
		private class PendingWaitForEvent<T> : AsyncResult<EventsQueryResult<T>>, IIdentifiable where T : WireEventBase
		{
			// Token: 0x06003124 RID: 12580 RVA: 0x000A7590 File Offset: 0x000A5790
			public PendingWaitForEvent(Expression<Func<T, bool>> predicate, int timeout, int occurrences, BatchedAsyncEvent<WireEventBase> batchedAsyncEvent, AsyncCallback callback, object context)
				: base(callback, context)
			{
				this.Name = string.Format(CultureInfo.InvariantCulture, "EventsQueryRequestHandler<{0}>.{1}", new object[]
				{
					typeof(T).Name,
					Guid.NewGuid()
				});
				this.m_matchedEvents = new List<T>();
				this.m_predicate = predicate.Compile();
				this.m_timeout = ((timeout == -1) ? DateTime.MaxValue : (ExtendedDateTime.UtcNow + TimeSpan.FromMilliseconds((double)timeout)));
				this.m_expectedOccurrences = occurrences;
				this.m_batchedAsyncEvent = batchedAsyncEvent;
			}

			// Token: 0x06003125 RID: 12581 RVA: 0x000A762C File Offset: 0x000A582C
			public void Stop()
			{
				this.m_batchedAsyncEvent.Unsubscribe(this);
				EventsQueryStoppedException ex = new EventsQueryStoppedException(this.m_matchedEvents.Cast<WireEventBase>(), new int?(this.m_matchedEvents.Count), new int?(this.m_expectedOccurrences));
				AsyncInvoker.InvokeMethodAsynchronously(delegate
				{
					this.SignalCompletion(false, ex);
				}, WaitOrNot.DontWait, "Stop");
			}

			// Token: 0x06003126 RID: 12582 RVA: 0x000A7698 File Offset: 0x000A5898
			public void ProcessEvents(IEnumerable<T> events)
			{
				this.GetRemainingOccurrences();
				IEnumerable<T> enumerable = events.Where(this.m_predicate);
				this.m_matchedEvents.AddRange(enumerable);
				if (this.GetRemainingOccurrences() == 0)
				{
					this.m_batchedAsyncEvent.Unsubscribe(this);
					EventsQueryResult<T> result = new EventsQueryResult<T>(this.m_matchedEvents);
					AsyncInvoker.InvokeMethodAsynchronously(delegate
					{
						this.SignalCompletion(false, result);
					}, WaitOrNot.DontWait, "ProcessEvents");
					return;
				}
				if (ExtendedDateTime.UtcNow > this.m_timeout)
				{
					this.m_batchedAsyncEvent.Unsubscribe(this);
					EventsQueryTimeoutException ex = new EventsQueryTimeoutException(this.m_matchedEvents.Cast<WireEventBase>(), new int?(this.m_matchedEvents.Count), new int?(this.m_expectedOccurrences));
					AsyncInvoker.InvokeMethodAsynchronously(delegate
					{
						this.SignalCompletion(false, ex);
					}, WaitOrNot.DontWait, "ProcessEvents");
				}
			}

			// Token: 0x06003127 RID: 12583 RVA: 0x000A7780 File Offset: 0x000A5980
			private int GetRemainingOccurrences()
			{
				int num = this.m_expectedOccurrences - this.m_matchedEvents.Count;
				if (num <= 0)
				{
					return 0;
				}
				return num;
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x06003128 RID: 12584 RVA: 0x000A77A7 File Offset: 0x000A59A7
			// (set) Token: 0x06003129 RID: 12585 RVA: 0x000A77AF File Offset: 0x000A59AF
			public string Name { get; private set; }

			// Token: 0x04001690 RID: 5776
			private DateTime m_timeout;

			// Token: 0x04001691 RID: 5777
			protected BatchedAsyncEvent<WireEventBase> m_batchedAsyncEvent;

			// Token: 0x04001692 RID: 5778
			private List<T> m_matchedEvents;

			// Token: 0x04001693 RID: 5779
			private Func<T, bool> m_predicate;

			// Token: 0x04001694 RID: 5780
			private int m_expectedOccurrences;
		}
	}
}
