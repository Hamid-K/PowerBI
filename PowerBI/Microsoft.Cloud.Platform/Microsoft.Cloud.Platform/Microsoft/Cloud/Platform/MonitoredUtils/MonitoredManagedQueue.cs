using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000143 RID: 323
	public class MonitoredManagedQueue<T> : IManagedQueue<T>, IShuttable
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001CD1C File Offset: 0x0001AF1C
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0001CD24 File Offset: 0x0001AF24
		public string QueueRoleId { get; private set; }

		// Token: 0x06000873 RID: 2163 RVA: 0x0001CD30 File Offset: 0x0001AF30
		public MonitoredManagedQueue(int maxCapacity, [NotNull] string queueRoleId, [NotNull] IEventsKitFactory eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(queueRoleId, "queueRoleId");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			this.QueueRoleId = queueRoleId;
			this.m_monitoringEvents = eventsKitFactory.CreateEventsKit<IMonitoredManagedQueueEventsKit>(this.QueueRoleId, PerformanceCounterPrefixSetting.ElementName);
			this.m_managedQueue = new ManagedQueue<T>(maxCapacity);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001CD7F File Offset: 0x0001AF7F
		public T Dequeue(int millisecondsTimeout)
		{
			T t = this.m_managedQueue.Dequeue(millisecondsTimeout);
			this.m_monitoringEvents.NotifyCapacityChanged(this.m_managedQueue.MaxCapacity, this.m_managedQueue.Count);
			return t;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001CDAE File Offset: 0x0001AFAE
		public T Dequeue()
		{
			return this.Dequeue(-1);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001CDB7 File Offset: 0x0001AFB7
		public bool TryDequeue(out T item)
		{
			bool flag = this.m_managedQueue.TryDequeue(out item);
			if (flag)
			{
				this.m_monitoringEvents.NotifyCapacityChanged(this.m_managedQueue.MaxCapacity, this.m_managedQueue.Count);
			}
			return flag;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public void Enqueue(T item)
		{
			try
			{
				this.m_managedQueue.Enqueue(item);
			}
			catch (QueueFullException ex)
			{
				this.m_monitoringEvents.NotifyQueueFull(this.QueueRoleId, ex.Capacity, ex);
				throw;
			}
			this.m_monitoringEvents.NotifyCapacityChanged(this.m_managedQueue.MaxCapacity, this.m_managedQueue.Count);
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001CE54 File Offset: 0x0001B054
		public int Count
		{
			get
			{
				return this.m_managedQueue.Count;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001CE61 File Offset: 0x0001B061
		public int MaxCapacity
		{
			get
			{
				return this.m_managedQueue.MaxCapacity;
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001CE6E File Offset: 0x0001B06E
		public void Start()
		{
			this.m_managedQueue.Start();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001CE7B File Offset: 0x0001B07B
		public void Stop()
		{
			this.m_managedQueue.Stop();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001CE88 File Offset: 0x0001B088
		public void WaitForStopToComplete()
		{
			this.m_managedQueue.WaitForStopToComplete();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001CE95 File Offset: 0x0001B095
		public void Shutdown()
		{
			this.m_managedQueue.Shutdown();
		}

		// Token: 0x04000324 RID: 804
		private ManagedQueue<T> m_managedQueue;

		// Token: 0x04000325 RID: 805
		private IMonitoredManagedQueueEventsKit m_monitoringEvents;
	}
}
