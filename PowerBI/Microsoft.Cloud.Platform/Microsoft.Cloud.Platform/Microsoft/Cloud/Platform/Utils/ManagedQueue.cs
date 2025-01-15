using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200024A RID: 586
	public class ManagedQueue<T> : IManagedQueue<T>, IShuttable
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00033BC0 File Offset: 0x00031DC0
		public ManagedQueue(int maxCapacity)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxCapacity, "maxCapacity");
			this.m_queue = new Queue<T>();
			this.m_active = false;
			this.m_startCalled = false;
			this.m_maxCapacity = maxCapacity;
			this.m_locker = new object();
			this.m_queueFullErr = string.Format(CultureInfo.CurrentCulture, "Unable to enqueue item. Queue is full (capacity = {0})", new object[] { this.m_maxCapacity });
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00033C34 File Offset: 0x00031E34
		public virtual T Dequeue(int millisecondsTimeout)
		{
			ExtendedDiagnostics.EnsureArgument(millisecondsTimeout, "millisecondsTimeout", millisecondsTimeout == -1 || millisecondsTimeout >= 0);
			Stopwatch stopwatch = null;
			int num = millisecondsTimeout;
			if (num > 0 && num != -1)
			{
				stopwatch = Stopwatch.StartNew();
			}
			object locker = this.m_locker;
			T t2;
			lock (locker)
			{
				while (this.m_queue.Count <= 0)
				{
					this.EnsureActive();
					if (num != -1)
					{
						if (num == 0)
						{
							throw new TimeoutException("Timeout expired waiting for items to dequeue");
						}
						num = millisecondsTimeout - (int)stopwatch.ElapsedMilliseconds;
						num = ((num < 0) ? 0 : num);
						if (num > 0)
						{
							Monitor.Wait(this.m_locker, num);
						}
					}
					else
					{
						Monitor.Wait(this.m_locker);
					}
				}
				T t = this.m_queue.Dequeue();
				if (this.m_queue.Count == 0 && !this.m_active)
				{
					Monitor.PulseAll(this.m_locker);
				}
				t2 = t;
			}
			return t2;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00033D28 File Offset: 0x00031F28
		public virtual T Dequeue()
		{
			return this.Dequeue(-1);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00033D34 File Offset: 0x00031F34
		public virtual bool TryDequeue(out T item)
		{
			item = default(T);
			object locker = this.m_locker;
			bool flag2;
			lock (locker)
			{
				if (this.m_queue.Count > 0 || !this.m_active)
				{
					item = this.Dequeue(0);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00033DA0 File Offset: 0x00031FA0
		public virtual void Enqueue(T item)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.EnsureActive();
				if (this.m_queue.Count == this.m_maxCapacity)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "{0}", new object[] { this.m_queueFullErr });
					throw new QueueFullException(this.m_queue.Count, this.m_queueFullErr);
				}
				this.m_queue.Enqueue(item);
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Enqueued item {0}", new object[] { item });
				if (this.m_queue.Count == 1)
				{
					Monitor.Pulse(this.m_locker);
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00033E70 File Offset: 0x00032070
		public int Count
		{
			get
			{
				return this.m_queue.Count;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00033E7D File Offset: 0x0003207D
		public int MaxCapacity
		{
			get
			{
				return this.m_maxCapacity;
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00033E88 File Offset: 0x00032088
		public void Start()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Managed queue Starting");
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_startCalled = true;
				this.m_active = true;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00033EE0 File Offset: 0x000320E0
		public void Stop()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				if (this.m_active)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Managed queue requested to Stop");
					this.m_active = false;
					Monitor.PulseAll(this.m_locker);
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Managed queue requested to Stop multiple times");
				}
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00033F58 File Offset: 0x00032158
		public void WaitForStopToComplete()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Managed queue waiting for Stop to complete");
				while (!this.m_active)
				{
					if (this.m_queue.Count == 0)
					{
						TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Managed queue stopped. Queue is empty.");
						return;
					}
					Monitor.Wait(this.m_locker);
				}
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "WaitForStopToComplete must be called after Stop");
				throw new InvalidOperationException("WaitForStopToComplete must be called after Stop");
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Shutdown()
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00033FF0 File Offset: 0x000321F0
		private void EnsureActive()
		{
			if (!this.m_active)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Can't enqueue or dequeue - queue stopped");
				throw new ShutdownSequenceStartedException("Can't enqueue or dequeue - queue stopped");
			}
		}

		// Token: 0x040005C1 RID: 1473
		private Queue<T> m_queue;

		// Token: 0x040005C2 RID: 1474
		private bool m_active;

		// Token: 0x040005C3 RID: 1475
		private object m_locker;

		// Token: 0x040005C4 RID: 1476
		private int m_maxCapacity;

		// Token: 0x040005C5 RID: 1477
		private string m_queueFullErr;

		// Token: 0x040005C6 RID: 1478
		private bool m_startCalled;
	}
}
