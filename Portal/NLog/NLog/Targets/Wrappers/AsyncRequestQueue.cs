using System;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200005F RID: 95
	internal class AsyncRequestQueue : AsyncRequestQueueBase
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x00015498 File Offset: 0x00013698
		public AsyncRequestQueue(int requestLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			base.RequestLimit = requestLimit;
			base.OnOverflow = overflowAction;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000154C0 File Offset: 0x000136C0
		public int RequestCount
		{
			get
			{
				Queue<AsyncLogEventInfo> logEventInfoQueue = this._logEventInfoQueue;
				int count;
				lock (logEventInfoQueue)
				{
					count = this._logEventInfoQueue.Count;
				}
				return count;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00015508 File Offset: 0x00013708
		public override bool IsEmpty
		{
			get
			{
				return this.RequestCount == 0;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00015514 File Offset: 0x00013714
		public override bool Enqueue(AsyncLogEventInfo logEventInfo)
		{
			Queue<AsyncLogEventInfo> logEventInfoQueue = this._logEventInfoQueue;
			bool flag2;
			lock (logEventInfoQueue)
			{
				if (this._logEventInfoQueue.Count >= base.RequestLimit)
				{
					InternalLogger.Debug("Async queue is full");
					switch (base.OnOverflow)
					{
					case AsyncTargetWrapperOverflowAction.Grow:
						InternalLogger.Debug("The overflow action is Grow, adding element anyway");
						base.OnLogEventQueueGrows((long)(this.RequestCount + 1));
						base.RequestLimit *= 2;
						break;
					case AsyncTargetWrapperOverflowAction.Discard:
						InternalLogger.Debug("Discarding one element from queue");
						base.OnLogEventDropped(this._logEventInfoQueue.Dequeue().LogEvent);
						break;
					case AsyncTargetWrapperOverflowAction.Block:
						while (this._logEventInfoQueue.Count >= base.RequestLimit)
						{
							InternalLogger.Debug("Blocking because the overflow action is Block...");
							Monitor.Wait(this._logEventInfoQueue);
							InternalLogger.Trace("Entered critical section.");
						}
						InternalLogger.Trace("Limit ok.");
						break;
					}
				}
				this._logEventInfoQueue.Enqueue(logEventInfo);
				flag2 = this._logEventInfoQueue.Count == 1;
			}
			return flag2;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00015638 File Offset: 0x00013838
		public override AsyncLogEventInfo[] DequeueBatch(int count)
		{
			Queue<AsyncLogEventInfo> logEventInfoQueue = this._logEventInfoQueue;
			AsyncLogEventInfo[] array;
			lock (logEventInfoQueue)
			{
				if (this._logEventInfoQueue.Count < count)
				{
					count = this._logEventInfoQueue.Count;
				}
				if (count == 0)
				{
					return ArrayHelper.Empty<AsyncLogEventInfo>();
				}
				array = new AsyncLogEventInfo[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this._logEventInfoQueue.Dequeue();
				}
				if (base.OnOverflow == AsyncTargetWrapperOverflowAction.Block)
				{
					Monitor.PulseAll(this._logEventInfoQueue);
				}
			}
			return array;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000156DC File Offset: 0x000138DC
		public override void DequeueBatch(int count, IList<AsyncLogEventInfo> result)
		{
			Queue<AsyncLogEventInfo> logEventInfoQueue = this._logEventInfoQueue;
			lock (logEventInfoQueue)
			{
				if (this._logEventInfoQueue.Count < count)
				{
					count = this._logEventInfoQueue.Count;
				}
				for (int i = 0; i < count; i++)
				{
					result.Add(this._logEventInfoQueue.Dequeue());
				}
				if (base.OnOverflow == AsyncTargetWrapperOverflowAction.Block)
				{
					Monitor.PulseAll(this._logEventInfoQueue);
				}
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00015764 File Offset: 0x00013964
		public override void Clear()
		{
			Queue<AsyncLogEventInfo> logEventInfoQueue = this._logEventInfoQueue;
			lock (logEventInfoQueue)
			{
				this._logEventInfoQueue.Clear();
			}
		}

		// Token: 0x040001CA RID: 458
		private readonly Queue<AsyncLogEventInfo> _logEventInfoQueue = new Queue<AsyncLogEventInfo>(1000);
	}
}
