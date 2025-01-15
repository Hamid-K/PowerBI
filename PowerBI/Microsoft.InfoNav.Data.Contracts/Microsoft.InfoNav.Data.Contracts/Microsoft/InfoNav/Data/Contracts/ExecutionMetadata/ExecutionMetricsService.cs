using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F5 RID: 245
	public sealed class ExecutionMetricsService : IExecutionMetricsService
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0000D6BE File Offset: 0x0000B8BE
		public ExecutionMetricsService(int? maxEventCount)
		{
			this._maxEventCount = maxEventCount;
			this._events = new ConcurrentQueue<ExecutionEvent>();
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public bool ExceededMaxEventCount
		{
			get
			{
				int eventCount = this._eventCount;
				int? maxEventCount = this._maxEventCount;
				return (eventCount > maxEventCount.GetValueOrDefault()) & (maxEventCount != null);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000D703 File Offset: 0x0000B903
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000D714 File Offset: 0x0000B914
		private string ActiveEventId
		{
			get
			{
				return (string)CallContext.LogicalGetData(ExecutionMetricsService.ActiveIdCallContextSlotName);
			}
			set
			{
				CallContext.LogicalSetData(ExecutionMetricsService.ActiveIdCallContextSlotName, value);
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000D721 File Offset: 0x0000B921
		public ExecutionMetrics ToExecutionMetrics()
		{
			return new ExecutionMetrics
			{
				Version = ExecutionMetricsVersions.Version1_0_0,
				Events = this._events.ToList<ExecutionEvent>()
			};
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000D744 File Offset: 0x0000B944
		public ITimedEventTracker BeginEvent(string eventName, string componentName)
		{
			ExecutionEvent executionEvent = this.CreateEvent(eventName, componentName);
			this.ActiveEventId = executionEvent.Id;
			this.AddEvent(executionEvent, false);
			return new ExecutionMetricsService.TimedEventTracker(this, executionEvent);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000D775 File Offset: 0x0000B975
		private ExecutionEvent CreateEvent(string eventName, string componentName)
		{
			return new ExecutionEvent
			{
				Name = eventName,
				Component = componentName,
				Id = ExecutionMetricsServiceUtils.NewId(),
				ParentId = this.ActiveEventId,
				Start = ExecutionMetricsServiceUtils.Timestamp()
			};
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		private void EndEvent(ExecutionEvent execEvent)
		{
			execEvent.End = new DateTime?(ExecutionMetricsServiceUtils.Timestamp());
			this.ActiveEventId = execEvent.ParentId;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		public IInstantEventTracker FireInstantEvent(string eventName, string componentName, bool bypassMaxEventCount = false)
		{
			ExecutionEvent executionEvent = this.CreateEvent(eventName, componentName);
			this.AddEvent(executionEvent, bypassMaxEventCount);
			return new ExecutionMetricsService.EventTracker(executionEvent);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public void AttachExternalEvent(ExecutionEvent execEvent)
		{
			this.AddEvent(execEvent, false);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		private void AddEvent(ExecutionEvent execEvent, bool bypassMaxEventCount)
		{
			int eventCount = this._eventCount;
			int? num = this._maxEventCount;
			if (((eventCount > num.GetValueOrDefault()) & (num != null)) && !bypassMaxEventCount)
			{
				return;
			}
			int num2 = Interlocked.Increment(ref this._eventCount);
			num = this._maxEventCount;
			if (((num2 > num.GetValueOrDefault()) & (num != null)) && !bypassMaxEventCount)
			{
				return;
			}
			this._events.Enqueue(execEvent);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000D864 File Offset: 0x0000BA64
		[Conditional("DEBUG")]
		private void AssertUniqueId(ExecutionEvent externalEvent)
		{
			ExecutionEvent executionEvent = this._events.FirstOrDefault((ExecutionEvent e) => e.Id == externalEvent.Id);
			if (executionEvent != null)
			{
				throw new ArgumentException(StringUtil.FormatInvariant("External event {0}/{1} has the same Id {2} as event {3}/{4}", new object[] { externalEvent.Component, externalEvent.Name, externalEvent.Id, executionEvent.Component, executionEvent.Name }));
			}
		}

		// Token: 0x040002BE RID: 702
		private static readonly string ActiveIdCallContextSlotName = "ExecMetricsId";

		// Token: 0x040002BF RID: 703
		private readonly int? _maxEventCount;

		// Token: 0x040002C0 RID: 704
		private readonly ConcurrentQueue<ExecutionEvent> _events;

		// Token: 0x040002C1 RID: 705
		private int _eventCount;

		// Token: 0x0200030D RID: 781
		private class EventTracker : IInstantEventTracker, IEventTracker
		{
			// Token: 0x0600196D RID: 6509 RVA: 0x0002DC2C File Offset: 0x0002BE2C
			public EventTracker(ExecutionEvent execEvent)
			{
				this._event = execEvent;
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x0600196E RID: 6510 RVA: 0x0002DC3B File Offset: 0x0002BE3B
			public string Id
			{
				get
				{
					return this._event.Id;
				}
			}

			// Token: 0x0600196F RID: 6511 RVA: 0x0002DC48 File Offset: 0x0002BE48
			public void SetMetric(string name, object value)
			{
				if (this._event.Metrics == null)
				{
					this._event.Metrics = new Dictionary<string, object>(StringComparer.Ordinal);
				}
				this._event.Metrics[name] = value;
			}

			// Token: 0x0400096A RID: 2410
			protected ExecutionEvent _event;
		}

		// Token: 0x0200030E RID: 782
		private sealed class TimedEventTracker : ExecutionMetricsService.EventTracker, ITimedEventTracker, IEventTracker, IDisposable
		{
			// Token: 0x06001970 RID: 6512 RVA: 0x0002DC7E File Offset: 0x0002BE7E
			public TimedEventTracker(ExecutionMetricsService service, ExecutionEvent execEvent)
				: base(execEvent)
			{
				this._service = service;
			}

			// Token: 0x06001971 RID: 6513 RVA: 0x0002DC8E File Offset: 0x0002BE8E
			public void Dispose()
			{
				Contract.Check(!this._wasDisposed, "Cannot dispose event multiple times.");
				this._wasDisposed = true;
				this._service.EndEvent(this._event);
			}

			// Token: 0x0400096B RID: 2411
			private ExecutionMetricsService _service;

			// Token: 0x0400096C RID: 2412
			private bool _wasDisposed;
		}
	}
}
