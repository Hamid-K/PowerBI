using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000029 RID: 41
	internal class ProducerConsumerEventQueue : IProducerConsumerEventQueue
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00006BED File Offset: 0x00004DED
		public ProducerConsumerEventQueue()
		{
			this._eventQueue = new Queue<CacheNotificationGroup>();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006C0C File Offset: 0x00004E0C
		public void Enqueue(CacheNotificationGroup notificationGroup)
		{
			lock (this._mutexObject)
			{
				this._eventQueue.Enqueue(notificationGroup);
				if (notificationGroup.Notifications != null)
				{
					this._count += (long)notificationGroup.Notifications.Count;
				}
				Monitor.Pulse(this._mutexObject);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006C80 File Offset: 0x00004E80
		public CacheNotificationGroup Dequeue()
		{
			CacheNotificationGroup cacheNotificationGroup;
			lock (this._mutexObject)
			{
				while (this._eventQueue.Count == 0)
				{
					Monitor.Wait(this._mutexObject);
				}
				cacheNotificationGroup = this._eventQueue.Dequeue();
				if (cacheNotificationGroup.Notifications != null)
				{
					this._count -= (long)cacheNotificationGroup.Notifications.Count;
				}
			}
			return cacheNotificationGroup;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006D04 File Offset: 0x00004F04
		public long CountPendingEvent
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006D0C File Offset: 0x00004F0C
		public bool IsEmpty
		{
			get
			{
				return this._count == 0L;
			}
		}

		// Token: 0x040000A9 RID: 169
		private object _mutexObject = new object();

		// Token: 0x040000AA RID: 170
		private Queue<CacheNotificationGroup> _eventQueue;

		// Token: 0x040000AB RID: 171
		private long _count;
	}
}
