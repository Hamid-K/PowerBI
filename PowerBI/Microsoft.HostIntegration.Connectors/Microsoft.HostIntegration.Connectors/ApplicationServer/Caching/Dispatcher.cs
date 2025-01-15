using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000019 RID: 25
	internal class Dispatcher
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00004EAD File Offset: 0x000030AD
		public Dispatcher(Filter filter, DelegateStore delegateStore)
		{
			this._filter = filter;
			this._delegateStore = delegateStore;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004ED7 File Offset: 0x000030D7
		public Dispatcher(IProducerConsumerEventQueue eventQueue, Filter filter, DelegateStore delegateStore)
		{
			this._filter = filter;
			this._eventQueue = eventQueue;
			this._delegateStore = delegateStore;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004F08 File Offset: 0x00003108
		public void StopDispatch()
		{
			this._continue = false;
			CacheNotificationGroup cacheNotificationGroup = new CacheNotificationGroup(null, null, CacheEventType.StopDispatchEvent);
			this._eventQueue.Enqueue(cacheNotificationGroup);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004F37 File Offset: 0x00003137
		public void StartDispatch()
		{
			this._continue = true;
			this.Dispatch();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004F48 File Offset: 0x00003148
		public void Dispatch()
		{
			while (this._continue)
			{
				CacheNotificationGroup cacheNotificationGroup = this._eventQueue.Dequeue();
				if (cacheNotificationGroup.ControlOperation == CacheEventType.StopDispatchEvent)
				{
					return;
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					foreach (BaseOperationNotification baseOperationNotification in cacheNotificationGroup.Notifications)
					{
						this.LogEvent(baseOperationNotification);
					}
				}
				if (cacheNotificationGroup.ControlOperation == CacheEventType.NotificationMissEvent)
				{
					this.DispatchFailureNotification(cacheNotificationGroup.CacheName);
				}
				if (cacheNotificationGroup.Notifications.Count > 0)
				{
					if (this._delegateStore.IsNonBulkNotificationsPresent(cacheNotificationGroup.CacheName))
					{
						this.DispatchNonBulkNotification(cacheNotificationGroup.Notifications);
					}
					this.DispatchBulkNotifications(cacheNotificationGroup.CacheName, cacheNotificationGroup.Notifications);
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005024 File Offset: 0x00003224
		private void LogEvent(BaseOperationNotification aEvent)
		{
			EventLogWriter.WriteVerbose<BaseOperationNotification>(this._myComponentName, "Received notification {0}", aEvent);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005038 File Offset: 0x00003238
		public void DispatchNonBulkNotification(List<DataCacheOperationDescriptor> list)
		{
			foreach (DataCacheOperationDescriptor dataCacheOperationDescriptor in list)
			{
				if (this._filter.filter(dataCacheOperationDescriptor))
				{
					Queue<PerDelegateInfo> invocationList = this._delegateStore.GetInvocationList(dataCacheOperationDescriptor);
					if (invocationList != null)
					{
						while (invocationList.Count != 0)
						{
							PerDelegateInfo perDelegateInfo = invocationList.Dequeue();
							DataCacheNotificationDescriptor nd = perDelegateInfo.Nd;
							string text = dataCacheOperationDescriptor.Key;
							DataCacheOperations dataCacheOperations = dataCacheOperationDescriptor.OperationType;
							if (dataCacheOperationDescriptor.InternalOperationType == CacheEventType.ClearRegionEvent || dataCacheOperationDescriptor.InternalOperationType == CacheEventType.RemoveRegionEvent)
							{
								ItemNotificationDescriptor itemNotificationDescriptor = nd as ItemNotificationDescriptor;
								if (itemNotificationDescriptor != null)
								{
									text = itemNotificationDescriptor.Key;
									dataCacheOperations = DataCacheOperations.RemoveItem;
								}
							}
							DataCacheNotificationCallback dataCacheNotificationCallback = perDelegateInfo.CacheDelegate as DataCacheNotificationCallback;
							if (dataCacheNotificationCallback != null)
							{
								dataCacheNotificationCallback(dataCacheOperationDescriptor.CacheName, dataCacheOperationDescriptor.RegionName, text, new DataCacheItemVersion(dataCacheOperationDescriptor.InternalVersion), dataCacheOperations, nd);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005138 File Offset: 0x00003338
		public void DispatchFailureNotification(string cacheName)
		{
			DataCacheOperationDescriptor dataCacheOperationDescriptor = new DataCacheOperationDescriptor(cacheName, null, null, CacheEventType.NotificationMissEvent, InternalCacheItemVersion.Null);
			Queue<PerDelegateInfo> invocationList = this._delegateStore.GetInvocationList(dataCacheOperationDescriptor);
			if (invocationList != null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>(this._myComponentName, "Number of Failure Delegate found :{0}", invocationList.Count);
				}
				while (invocationList.Count != 0)
				{
					PerDelegateInfo perDelegateInfo = invocationList.Dequeue();
					DataCacheFailureNotificationCallback dataCacheFailureNotificationCallback = perDelegateInfo.CacheDelegate as DataCacheFailureNotificationCallback;
					if (dataCacheFailureNotificationCallback != null)
					{
						dataCacheFailureNotificationCallback(cacheName, perDelegateInfo.Nd);
					}
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000051B0 File Offset: 0x000033B0
		public void DispatchBulkNotifications(string cacheName, List<DataCacheOperationDescriptor> list)
		{
			Queue<PerDelegateInfo> bulkInvocationList = this._delegateStore.GetBulkInvocationList(cacheName);
			if (bulkInvocationList != null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>(this._myComponentName, "Number of Bulk Delegate found :{0}", bulkInvocationList.Count);
				}
				while (bulkInvocationList.Count != 0)
				{
					PerDelegateInfo perDelegateInfo = bulkInvocationList.Dequeue();
					DataCacheBulkNotificationCallback dataCacheBulkNotificationCallback = perDelegateInfo.CacheDelegate as DataCacheBulkNotificationCallback;
					if (dataCacheBulkNotificationCallback != null)
					{
						dataCacheBulkNotificationCallback(cacheName, new ReadOnlyCollection<DataCacheOperationDescriptor>(list), perDelegateInfo.Nd);
					}
				}
			}
		}

		// Token: 0x04000077 RID: 119
		private Filter _filter;

		// Token: 0x04000078 RID: 120
		private IProducerConsumerEventQueue _eventQueue;

		// Token: 0x04000079 RID: 121
		private DelegateStore _delegateStore;

		// Token: 0x0400007A RID: 122
		private volatile bool _continue = true;

		// Token: 0x0400007B RID: 123
		private string _myComponentName = "DistributedCache.Dispatcher";
	}
}
