using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200001F RID: 31
	internal sealed class NotificationManager : IDisposable
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x0000535C File Offset: 0x0000355C
		internal NotificationManager(DataCacheNotificationProperties properties, int nwTimeout, IDRMUtility nUtility, NamedCacheConfiguration cacheConfig, Hashtable cacheProtocolTable)
		{
			this._eventQueue = new ProducerConsumerEventQueue();
			this._nwPoller = new NetworkPoller(this._eventQueue, nUtility, this, nwTimeout, properties.MaxQueueLength, cacheConfig, cacheProtocolTable);
			this._delegateStore = new DelegateStore();
			int num = (int)properties.PollInterval.TotalMilliseconds;
			this._pollingInterval = ((num < 0) ? int.MaxValue : num);
			this._dispatcher = new Dispatcher(this._eventQueue, new Filter(), this._delegateStore);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000053EC File Offset: 0x000035EC
		internal bool Initialize()
		{
			this._nwPoller.Open();
			this.StartTimerBasedPoll();
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.Dispatch));
			return true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005412 File Offset: 0x00003612
		internal void Close()
		{
			this._timer.Dispose();
			this._nwPoller.Close();
			this._dispatcher.StopDispatch();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005435 File Offset: 0x00003635
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000543D File Offset: 0x0000363D
		private void OnNotificationChangeEvent(string cacheName, string regionName, string key, int filter, RegistrationEventType rEventType)
		{
			this.NotificationChangeEvent(cacheName, regionName, key, filter, rEventType);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005454 File Offset: 0x00003654
		public DataCacheNotificationDescriptor AddNotificationCallback(string cacheName, int filter, DataCacheNotificationCallback clientCallback)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int>(this._myComponentName, "AddNotificationCallback: Add Cache Level Notification {0}, Mask: {1} ", cacheName, filter);
			}
			this.OnNotificationChangeEvent(cacheName, null, null, filter, RegistrationEventType.AddCallbackEvent);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor = this._delegateStore.AddDelegate(cacheName, filter, clientCallback);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DataCacheNotificationDescriptor, int>(this._myComponentName, "AddNotificationCallback: Add Cache Level Notification. ND={0}, mask={1}", dataCacheNotificationDescriptor, filter);
			}
			return dataCacheNotificationDescriptor;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000054B0 File Offset: 0x000036B0
		public DataCacheNotificationDescriptor AddNotificationCallback(string cacheName, string regionName, int filter, DataCacheNotificationCallback clientCallback)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, string, int>(this._myComponentName, "AddNotificationCallback: Add Region Level Notification CacheName={0}, RegionName={1}, Mask={2}", cacheName, regionName, filter);
			}
			this.OnNotificationChangeEvent(cacheName, regionName, null, filter, RegistrationEventType.AddCallbackEvent);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor = this._delegateStore.AddDelegate(cacheName, regionName, filter, clientCallback);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DataCacheNotificationDescriptor, int>(this._myComponentName, "AddNotificationCallback: Add Region Level Notification. ND={0}, mask={1}", dataCacheNotificationDescriptor, filter);
			}
			return dataCacheNotificationDescriptor;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005510 File Offset: 0x00003710
		public DataCacheNotificationDescriptor AddNotificationCallback(string cacheName, string regionName, string key, int filter, DataCacheNotificationCallback clientCallback)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, string, string, int>(this._myComponentName, "AddNotificationCallback: Add Item Level Notification CacheName={0}, RegionName={1}, Key={2}, Mask={3}", cacheName, regionName, key, filter);
			}
			this.OnNotificationChangeEvent(cacheName, regionName, key, filter, RegistrationEventType.AddCallbackEvent);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor = this._delegateStore.AddDelegate(cacheName, regionName, key, filter, clientCallback);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DataCacheNotificationDescriptor, int>(this._myComponentName, "AddNotificationCallback: Add Item Level Notification. ND={0}, mask={1}", dataCacheNotificationDescriptor, filter);
			}
			return dataCacheNotificationDescriptor;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005578 File Offset: 0x00003778
		public DataCacheNotificationDescriptor AddBulkNotificationCallback(string cacheName, int filter, DataCacheBulkNotificationCallback clientCallback)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int>(this._myComponentName, "AddBulkNotificationCallback: Add Bulk Notification CacheName={0}, Mask={1}", cacheName, filter);
			}
			this.OnNotificationChangeEvent(cacheName, null, null, filter, RegistrationEventType.AddCallbackEvent);
			return this._delegateStore.AddBulkDelegate(cacheName, filter, clientCallback);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000055BC File Offset: 0x000037BC
		public void RemoveCallback(DataCacheNotificationDescriptor nd)
		{
			PerDelegateInfo perDelegateInfo = this._delegateStore.RemoveDelegate(nd);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<DataCacheNotificationDescriptor>(this._myComponentName, "RemoveCallback: {0}", nd);
			}
			if (perDelegateInfo != null)
			{
				ItemNotificationDescriptor itemNotificationDescriptor = nd as ItemNotificationDescriptor;
				if (itemNotificationDescriptor != null)
				{
					this.OnNotificationChangeEvent(itemNotificationDescriptor.CacheName, itemNotificationDescriptor.RegionName, itemNotificationDescriptor.Key, perDelegateInfo.FilterMask, RegistrationEventType.RemoveCallbackEvent);
					return;
				}
				RegionNotificationDescriptor regionNotificationDescriptor = nd as RegionNotificationDescriptor;
				if (regionNotificationDescriptor != null)
				{
					this.OnNotificationChangeEvent(regionNotificationDescriptor.CacheName, regionNotificationDescriptor.RegionName, null, perDelegateInfo.FilterMask, RegistrationEventType.RemoveCallbackEvent);
					return;
				}
				if (perDelegateInfo.FilterMask != 64)
				{
					this.OnNotificationChangeEvent(nd.CacheName, null, null, perDelegateInfo.FilterMask, RegistrationEventType.RemoveCallbackEvent);
					return;
				}
			}
			else
			{
				EventLogWriter.WriteVerbose<DataCacheNotificationDescriptor>(this._myComponentName, "RemoveCallback: ND={0} not available.", nd);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005672 File Offset: 0x00003872
		public DataCacheNotificationDescriptor AddFailureNotificationCallback(string cacheName, DataCacheFailureNotificationCallback failureDelegate)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>(this._myComponentName, "AddFailureNotificationCallback: Add for CacheName {0}", cacheName);
			}
			return this._delegateStore.AddDelegate(cacheName, 64, failureDelegate);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000569C File Offset: 0x0000389C
		internal DataCacheItemVersion GetNextPollVersionForKey(string cacheName, string key)
		{
			return this._nwPoller.GetNextPollVersionForKey(cacheName, key);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000056AB File Offset: 0x000038AB
		internal DataCacheItemVersion GetNextPollVersion(string cacheName, string region)
		{
			return this._nwPoller.GetNextPollVersion(cacheName, region);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000056BA File Offset: 0x000038BA
		internal NetworkPoller NwPoller
		{
			get
			{
				return this._nwPoller;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000056C2 File Offset: 0x000038C2
		internal long CountPendingEvents
		{
			get
			{
				return this._eventQueue.CountPendingEvent;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000056CF File Offset: 0x000038CF
		internal void StopTimerBasedPoll()
		{
			this._timer.Dispose();
			this._timer = null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000056E3 File Offset: 0x000038E3
		internal void StartTimerBasedPoll()
		{
			this._timer = new global::System.Threading.Timer(new TimerCallback(this.Poll), null, this._pollingInterval, this._pollingInterval);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005709 File Offset: 0x00003909
		private void Poll(object state)
		{
			this._nwPoller.PullNotificationEvent();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005716 File Offset: 0x00003916
		private void Dispatch(object state)
		{
			this._dispatcher.StartDispatch();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000E5 RID: 229 RVA: 0x00005724 File Offset: 0x00003924
		// (remove) Token: 0x060000E6 RID: 230 RVA: 0x0000575C File Offset: 0x0000395C
		public event NotificationChangedHandler NotificationChangeEvent;

		// Token: 0x04000081 RID: 129
		private NetworkPoller _nwPoller;

		// Token: 0x04000082 RID: 130
		private Dispatcher _dispatcher;

		// Token: 0x04000083 RID: 131
		private DelegateStore _delegateStore;

		// Token: 0x04000084 RID: 132
		private int _pollingInterval;

		// Token: 0x04000085 RID: 133
		private string _myComponentName = "DistributedCache.NotificationManager";

		// Token: 0x04000086 RID: 134
		private global::System.Threading.Timer _timer;

		// Token: 0x04000087 RID: 135
		private ProducerConsumerEventQueue _eventQueue;
	}
}
