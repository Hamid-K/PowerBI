using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000067 RID: 103
	internal class ConcurrentRequestQueue : AsyncRequestQueueBase
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x0001684D File Offset: 0x00014A4D
		public ConcurrentRequestQueue(int requestLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			base.RequestLimit = requestLimit;
			base.OnOverflow = overflowAction;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001686E File Offset: 0x00014A6E
		public override bool IsEmpty
		{
			get
			{
				return this._logEventInfoQueue.IsEmpty && Interlocked.Read(ref this._count) == 0L;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001688E File Offset: 0x00014A8E
		public int Count
		{
			get
			{
				return (int)this._count;
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00016898 File Offset: 0x00014A98
		public override bool Enqueue(AsyncLogEventInfo logEventInfo)
		{
			long num = Interlocked.Increment(ref this._count);
			bool flag = num == 1L;
			if (num > (long)base.RequestLimit)
			{
				InternalLogger.Debug("Async queue is full");
				switch (base.OnOverflow)
				{
				case AsyncTargetWrapperOverflowAction.Grow:
					InternalLogger.Debug("The overflow action is Grow, adding element anyway");
					base.OnLogEventQueueGrows(num);
					base.RequestLimit *= 2;
					break;
				case AsyncTargetWrapperOverflowAction.Discard:
				{
					AsyncLogEventInfo asyncLogEventInfo;
					while (!this._logEventInfoQueue.TryDequeue(out asyncLogEventInfo))
					{
						num = Interlocked.Read(ref this._count);
						flag = true;
						if (num <= (long)base.RequestLimit)
						{
							goto IL_00C1;
						}
					}
					InternalLogger.Debug("Discarding one element from queue");
					flag = Interlocked.Decrement(ref this._count) == 1L || flag;
					base.OnLogEventDropped(asyncLogEventInfo.LogEvent);
					break;
				}
				case AsyncTargetWrapperOverflowAction.Block:
					this.WaitForBelowRequestLimit();
					flag = true;
					break;
				}
			}
			IL_00C1:
			this._logEventInfoQueue.Enqueue(logEventInfo);
			return flag;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00016974 File Offset: 0x00014B74
		private void WaitForBelowRequestLimit()
		{
			bool flag = false;
			try
			{
				for (long num = this.TrySpinWaitForLowerCount(); num > (long)base.RequestLimit; num = Interlocked.Increment(ref this._count))
				{
					Interlocked.Decrement(ref this._count);
					InternalLogger.Debug("Blocking because the overflow action is Block...");
					if (!flag)
					{
						Monitor.Enter(this._logEventInfoQueue);
					}
					else
					{
						Monitor.Wait(this._logEventInfoQueue);
					}
					flag = true;
					InternalLogger.Trace("Entered critical section.");
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this._logEventInfoQueue);
				}
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00016A04 File Offset: 0x00014C04
		private long TrySpinWaitForLowerCount()
		{
			long num = 0L;
			bool flag = true;
			SpinWait spinWait = default(SpinWait);
			for (int i = 0; i <= 20; i++)
			{
				if (spinWait.NextSpinWillYield)
				{
					if (flag)
					{
						InternalLogger.Debug("Yielding because the overflow action is Block...");
					}
					flag = false;
				}
				spinWait.SpinOnce();
				num = Interlocked.Read(ref this._count);
				if (num <= (long)base.RequestLimit)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00016A64 File Offset: 0x00014C64
		public override AsyncLogEventInfo[] DequeueBatch(int count)
		{
			if (this._logEventInfoQueue.IsEmpty)
			{
				return ArrayHelper.Empty<AsyncLogEventInfo>();
			}
			if (this._count < (long)count)
			{
				count = Math.Min(count, this.Count);
			}
			List<AsyncLogEventInfo> list = new List<AsyncLogEventInfo>(count);
			this.DequeueBatch(count, list);
			if (list.Count == 0)
			{
				return ArrayHelper.Empty<AsyncLogEventInfo>();
			}
			return list.ToArray();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00016AC0 File Offset: 0x00014CC0
		public override void DequeueBatch(int count, IList<AsyncLogEventInfo> result)
		{
			bool flag = base.OnOverflow == AsyncTargetWrapperOverflowAction.Block;
			int num = 0;
			AsyncLogEventInfo asyncLogEventInfo;
			while (num < count && this._logEventInfoQueue.TryDequeue(out asyncLogEventInfo))
			{
				if (!flag)
				{
					Interlocked.Decrement(ref this._count);
				}
				result.Add(asyncLogEventInfo);
				num++;
			}
			if (flag)
			{
				bool flag2 = false;
				if (result.Count == count)
				{
					Monitor.Enter(this._logEventInfoQueue);
					flag2 = true;
				}
				try
				{
					Interlocked.Add(ref this._count, (long)(-(long)result.Count));
					if (flag2)
					{
						Monitor.PulseAll(this._logEventInfoQueue);
					}
				}
				finally
				{
					if (flag2)
					{
						Monitor.Exit(this._logEventInfoQueue);
					}
				}
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00016B68 File Offset: 0x00014D68
		public override void Clear()
		{
			while (!this._logEventInfoQueue.IsEmpty)
			{
				AsyncLogEventInfo asyncLogEventInfo;
				this._logEventInfoQueue.TryDequeue(out asyncLogEventInfo);
			}
			Interlocked.Exchange(ref this._count, 0L);
		}

		// Token: 0x040001ED RID: 493
		private readonly ConcurrentQueue<AsyncLogEventInfo> _logEventInfoQueue = new ConcurrentQueue<AsyncLogEventInfo>();

		// Token: 0x040001EE RID: 494
		private long _count;
	}
}
