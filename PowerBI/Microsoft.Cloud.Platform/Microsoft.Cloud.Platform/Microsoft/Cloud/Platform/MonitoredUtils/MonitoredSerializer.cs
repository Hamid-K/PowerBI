using System;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000147 RID: 327
	public class MonitoredSerializer<T> : Serializer<T> where T : IEquatable<T>
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x0001DF50 File Offset: 0x0001C150
		public MonitoredSerializer(string serializerId, IEventsKitFactory eventsKitFactory)
		{
			this.m_eventsKit = eventsKitFactory.CreateEventsKit<IMonitoredSerializerEventsKit>(serializerId, PerformanceCounterPrefixSetting.ElementName);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001DF68 File Offset: 0x0001C168
		public override IAsyncResult BeginAcquireLock(T key, AsyncCallback callback, object state)
		{
			Pair<IAsyncResult, Serializer<T>.SerializerCountersData> pair = base.BeginAcquireLockImplementation(key, callback, state, -1);
			Serializer<T>.SerializerCountersData second = pair.Second;
			this.m_eventsKit.NotifyItemsQueuesCountChanged(second.QueuesCount, second.ItemsCount, second.PeakQueueSize);
			return pair.First;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		public override bool TryAcquireLock(T key, out IDisposable lockHandle)
		{
			Pair<IDisposable, Serializer<T>.SerializerCountersData> pair;
			bool flag = base.TryAcquireLockImplementation(key, out pair);
			lockHandle = pair.First;
			Serializer<T>.SerializerCountersData second = pair.Second;
			if (flag)
			{
				this.m_eventsKit.NotifyItemsQueuesCountChanged(second.QueuesCount, second.ItemsCount, second.PeakQueueSize);
			}
			return flag;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001DFF4 File Offset: 0x0001C1F4
		protected override Serializer<T>.SerializerCountersData ReleaseLock(T key)
		{
			Serializer<T>.SerializerCountersData serializerCountersData = base.ReleaseLock(key);
			this.m_eventsKit.NotifyItemsQueuesCountChanged(serializerCountersData.QueuesCount, serializerCountersData.ItemsCount, serializerCountersData.PeakQueueSize);
			return serializerCountersData;
		}

		// Token: 0x0400033D RID: 829
		private readonly IMonitoredSerializerEventsKit m_eventsKit;
	}
}
