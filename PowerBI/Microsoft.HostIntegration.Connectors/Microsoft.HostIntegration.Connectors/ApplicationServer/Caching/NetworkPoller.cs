using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000024 RID: 36
	internal class NetworkPoller
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000057B8 File Offset: 0x000039B8
		internal NetworkPoller(IProducerConsumerEventQueue eventQueue, IDRMUtility nUtility, NotificationManager nm, int nwTimeout, long pendingEventCount, NamedCacheConfiguration cacheConfig, Hashtable cacheProtocolTable)
		{
			this._drmUtility = nUtility;
			this._pollerData = new PollerStructure(this._drmUtility);
			this._eventQueue = eventQueue;
			this._nwTimeout = new TimeSpan(0, 0, 0, 0, nwTimeout);
			this._maxPendingEvents = pendingEventCount;
			this._cacheConfig = cacheConfig;
			this._notificationReceived = new Action<PollerRequestContext>(this.OnNotificationReceived);
			this._notificationLsnReceived = new Action<PollerRequestContext>(this.OnNotificationLsnReceived);
			this._notificationFailed = new Action<PollerRequestContext>(this.OnNotificationFailed);
			nm.NotificationChangeEvent += this.NotificationChangedHandler;
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				this.NotificationsReceivedEvent += ClientPerfCounterUpdate.OnNotificationsReceived;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.Poller", "Network Poller constructed", new object[0]);
			}
			this._cacheProtocolTable = cacheProtocolTable;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000589C File Offset: 0x00003A9C
		public void PullNotificationEvent()
		{
			if (this._pollOn == 0)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this._pollCount, 1, 0) != 0)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.Poller", "PullNotificationEvent: Poll is already on, so aborting current run.", new object[0]);
				}
				return;
			}
			long countPendingEvent = this._eventQueue.CountPendingEvent;
			if (countPendingEvent >= this._maxPendingEvents)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.Poller", "PullNotificationEvent: Number of Message exceeded the max count, So This iteration of poll is aborted. Pending {0}, Max {1}.", new object[] { countPendingEvent, this._maxPendingEvents });
				}
				Interlocked.Decrement(ref this._pollCount);
				return;
			}
			long num = this._maxPendingEvents - countPendingEvent;
			if (num > 2147483647L)
			{
				num = 2147483647L;
			}
			Dictionary<EndpointID, Dictionary<string, List<PartitionNotificationRequest>>> dictionary = null;
			int num2 = this._pollerData.PreparePerNodePartitionMsg((int)num, out dictionary);
			if (num2 == 0)
			{
				Interlocked.Decrement(ref this._pollCount);
				return;
			}
			using (CountDownLatch countDownLatch = new CountDownLatch(num2))
			{
				IList<int> list = new List<int>(num2);
				foreach (KeyValuePair<EndpointID, Dictionary<string, List<PartitionNotificationRequest>>> keyValuePair in dictionary)
				{
					if (keyValuePair.Value.Count != 0)
					{
						foreach (KeyValuePair<string, List<PartitionNotificationRequest>> keyValuePair2 in keyValuePair.Value)
						{
							PollerRequestContext pollerRequestContext = new PollerRequestContext(keyValuePair.Key, new NotificationRequest(keyValuePair2.Value.ToArray()), countDownLatch);
							IClientProtocol cacheProtocolHandle = this.GetCacheProtocolHandle(keyValuePair2.Key);
							int num3;
							cacheProtocolHandle.SendNotificationRequestAsync(ReqType.NOTIFICATION_REQ, pollerRequestContext, this._notificationReceived, this._notificationFailed, out num3);
							list.Add(num3);
						}
					}
				}
				if (!countDownLatch.Wait(this._nwTimeout) && Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose("DistributedCache.Poller", "PullNotificationEvent: Timeout,Notification Message not received from all nodes");
				}
				Interlocked.Decrement(ref this._pollCount);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005ADC File Offset: 0x00003CDC
		private IClientProtocol GetCacheProtocolHandle(string cacheName)
		{
			return (IClientProtocol)this._cacheProtocolTable[cacheName];
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005AEF File Offset: 0x00003CEF
		public void Close()
		{
			Interlocked.Exchange(ref this._pollOn, 0);
			while (Interlocked.CompareExchange(ref this._pollCount, 0, 0) != 0)
			{
				Thread.Sleep(10);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005B16 File Offset: 0x00003D16
		public void Open()
		{
			Interlocked.Exchange(ref this._pollOn, 1);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005B25 File Offset: 0x00003D25
		public void NotificationChangedHandler(string cacheName, string regionName, string key, int filter, RegistrationEventType eventType)
		{
			if (eventType == RegistrationEventType.AddCallbackEvent)
			{
				this.AddNotification(cacheName, regionName);
				return;
			}
			this.RemoveNotification(cacheName, regionName);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005B3D File Offset: 0x00003D3D
		private static PartitionId GetFirstPartitionId(NotificationRequest notificationRequest)
		{
			return notificationRequest.PartitionReqList[0].PartitionId;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005B4C File Offset: 0x00003D4C
		private NotificationLsn GetLsn(PartitionId pid)
		{
			List<PartitionNotificationLsn> lsn = this.GetLsn(new PartitionId[] { pid });
			return lsn[0].LastLsn;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005B78 File Offset: 0x00003D78
		private List<PartitionNotificationLsn> GetLsn(PartitionId[] pList)
		{
			Dictionary<EndpointID, List<PartitionNotificationLsnRequest>> dictionary = this.PreparePerNodePartitionLsnMsg(pList);
			int count = dictionary.Count;
			List<PartitionNotificationLsn> list4;
			using (CountDownLatch countDownLatch = new CountDownLatch(count))
			{
				IList<PollerRequestContext> list = new List<PollerRequestContext>(count);
				IList<int> list2 = new List<int>(count);
				foreach (KeyValuePair<EndpointID, List<PartitionNotificationLsnRequest>> keyValuePair in dictionary)
				{
					if (keyValuePair.Value.Count != 0)
					{
						PollerRequestContext pollerRequestContext = new PollerRequestContext(keyValuePair.Key, new NotificationRequest(keyValuePair.Value.ToArray()), countDownLatch);
						IClientProtocol cacheProtocolHandle = this.GetCacheProtocolHandle(pList[0].ServiceNamespace);
						int num;
						cacheProtocolHandle.SendNotificationRequestAsync(ReqType.NOTIFICATION_LSN_REQ, pollerRequestContext, this._notificationLsnReceived, this._notificationFailed, out num);
						list.Add(pollerRequestContext);
						list2.Add(num);
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<EndpointID>("DistributedCache.Poller", "GetLsn: Message prepared for endPoint :{0}", keyValuePair.Key);
						}
					}
				}
				if (!countDownLatch.Wait(this._nwTimeout))
				{
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose("DistributedCache.Poller", "GetLsn: Timeout,Notification Lsn Message not received from all nodes");
					}
					DataCache.ThrowException(ErrStatus.TIMEOUT, Guid.Empty, null, null, null);
				}
				List<PartitionNotificationLsn> list3 = new List<PartitionNotificationLsn>();
				foreach (PollerRequestContext pollerRequestContext2 in list)
				{
					if (!pollerRequestContext2.IsReplyAvailable)
					{
						throw new DataCacheException("DistributedCache.Poller", 18, Utility.GetErrorMessage(CultureInfo.CurrentUICulture, 18));
					}
					for (int i = 0; i < pollerRequestContext2.Reply.PartitionNotificationReplyList.Count; i++)
					{
						PartitionNotificationReply partitionNotificationReply = pollerRequestContext2.Reply.PartitionNotificationReplyList[i];
						switch (partitionNotificationReply.RespStatus)
						{
						case PartitionRespStatus.Success:
						{
							PartitionNotificationLsn partitionNotificationLsn = new PartitionNotificationLsn(partitionNotificationReply.PartitionId, partitionNotificationReply.LastLsnResp);
							list3.Add(partitionNotificationLsn);
							break;
						}
						case PartitionRespStatus.NotPrimary:
							this._drmUtility.RaiseComplaint(partitionNotificationReply.PartitionId, pollerRequestContext2.Endpoint);
							throw new DataCacheException("DistributedCache.Poller", 17, 1, Utility.GetErrorMessage(CultureInfo.CurrentUICulture, 17));
						case PartitionRespStatus.CacheDoesNotExist:
							throw new DataCacheException("DistributedCache.Poller", 9, Utility.GetErrorMessage(CultureInfo.CurrentUICulture, 9));
						}
					}
				}
				list4 = list3;
			}
			return list4;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005E10 File Offset: 0x00004010
		public Dictionary<EndpointID, List<PartitionNotificationLsnRequest>> PreparePerNodePartitionLsnMsg(PartitionId[] pList)
		{
			Dictionary<EndpointID, List<PartitionNotificationLsnRequest>> dictionary = new Dictionary<EndpointID, List<PartitionNotificationLsnRequest>>();
			foreach (PartitionId partitionId in pList)
			{
				EndpointID primaryEndPoint = this._drmUtility.GetPrimaryEndPoint(partitionId);
				if (primaryEndPoint == null)
				{
					throw new DataCacheException("DistributedCache.Poller", 17, Utility.GetErrorMessage(CultureInfo.CurrentUICulture, 17));
				}
				PartitionNotificationLsnRequest partitionNotificationLsnRequest = new PartitionNotificationLsnRequest(partitionId);
				List<PartitionNotificationLsnRequest> list;
				if (dictionary.TryGetValue(primaryEndPoint, out list))
				{
					list.Add(partitionNotificationLsnRequest);
				}
				else
				{
					list = new List<PartitionNotificationLsnRequest>();
					list.Add(partitionNotificationLsnRequest);
					dictionary.Add(primaryEndPoint, list);
				}
			}
			return dictionary;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005EA0 File Offset: 0x000040A0
		private void OnNotificationLsnReceived(PollerRequestContext context)
		{
			try
			{
				context.CountDownLatch.Signal();
			}
			catch (ObjectDisposedException)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.Poller", "OnNotificationLsnReceived: wait handle already disposed.", new object[0]);
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005EEC File Offset: 0x000040EC
		private void OnNotificationFailed(PollerRequestContext context)
		{
			PartitionId firstPartitionId = NetworkPoller.GetFirstPartitionId(context.Request);
			this._drmUtility.RaiseComplaint(firstPartitionId, context.Endpoint);
			try
			{
				context.CountDownLatch.Signal();
			}
			catch (ObjectDisposedException)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.Poller", "OnNotificationFailed: wait handle already disposed.", new object[0]);
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005F54 File Offset: 0x00004154
		private void OnNotificationReceived(PollerRequestContext context)
		{
			List<PartitionNotificationReply> partitionNotificationReplyList = context.Reply.PartitionNotificationReplyList;
			foreach (PartitionNotificationReply partitionNotificationReply in partitionNotificationReplyList)
			{
				switch (partitionNotificationReply.RespStatus)
				{
				case PartitionRespStatus.Success:
					this.ProcessRcvdPartitionEventList(partitionNotificationReply);
					break;
				case PartitionRespStatus.NotPrimary:
					this._drmUtility.RaiseComplaint(partitionNotificationReply.PartitionId, context.Endpoint);
					break;
				}
			}
			try
			{
				context.CountDownLatch.Signal();
			}
			catch (ObjectDisposedException)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.Poller", "OnNotificationReceived: wait handle already disposed.", new object[0]);
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000601C File Offset: 0x0000421C
		private void ProcessRcvdPartitionEventList(PartitionNotificationReply pNodeReply)
		{
			string serviceNamespace = pNodeReply.PartitionId.ServiceNamespace;
			if (this.NotificationsReceivedEvent != null && pNodeReply.RcvdEventList != null)
			{
				this.NotificationsReceivedEvent(pNodeReply.RcvdEventList.Count);
			}
			switch (pNodeReply.NotificationRespStatus)
			{
			case NotificationRespStatus.Success:
				break;
			case NotificationRespStatus.NotificationMissed:
			case NotificationRespStatus.DataLoss:
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.Poller", "NotificationReceived: Notification {0}", new object[] { pNodeReply.RespStatus });
				}
				lock (this._updateQueueLock)
				{
					if (this._pollerData.UpdateLsnOnNotificationReceive(pNodeReply.PartitionId, pNodeReply.LastLsnResp))
					{
						List<DataCacheOperationDescriptor> list = NetworkPoller.GetSimpleNotificationList(pNodeReply.RcvdEventList);
						CacheNotificationGroup cacheNotificationGroup = new CacheNotificationGroup(serviceNamespace, list, CacheEventType.NotificationMissEvent);
						this._eventQueue.Enqueue(cacheNotificationGroup);
					}
					return;
				}
				break;
			case NotificationRespStatus.NotificationsNotSupported:
				return;
			default:
				return;
			}
			lock (this._updateQueueLock)
			{
				if (this._pollerData.UpdateLsnOnNotificationReceive(pNodeReply.PartitionId, pNodeReply.LastLsnResp))
				{
					List<DataCacheOperationDescriptor> list = NetworkPoller.GetSimpleNotificationList(pNodeReply.RcvdEventList);
					CacheNotificationGroup cacheNotificationGroup = new CacheNotificationGroup(serviceNamespace, list);
					this._eventQueue.Enqueue(cacheNotificationGroup);
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000617C File Offset: 0x0000437C
		private static List<DataCacheOperationDescriptor> GetSimpleNotificationList(List<BaseOperationNotification> list)
		{
			List<DataCacheOperationDescriptor> list2 = new List<DataCacheOperationDescriptor>();
			if (list == null)
			{
				return list2;
			}
			foreach (BaseOperationNotification baseOperationNotification in list)
			{
				DataCacheOperationDescriptor dataCacheOperationDescriptor = baseOperationNotification as DataCacheOperationDescriptor;
				if (dataCacheOperationDescriptor != null)
				{
					list2.Add(dataCacheOperationDescriptor);
				}
				else
				{
					BulkOpNotificationEvent bulkOpNotificationEvent = (BulkOpNotificationEvent)baseOperationNotification;
					if (bulkOpNotificationEvent.EventList != null)
					{
						list2.AddRange(bulkOpNotificationEvent.EventList);
					}
				}
			}
			return list2;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006200 File Offset: 0x00004400
		public DataCacheItemVersion GetNextPollVersionForKey(string cacheName, string key)
		{
			string systemRegionName = RegionNameProvider.GetSystemRegionName(key, this._cacheConfig.SystemRegionCount);
			return this.GetNextPollVersion(cacheName, systemRegionName);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006228 File Offset: 0x00004428
		public DataCacheItemVersion GetNextPollVersion(string cacheName, string regionName)
		{
			PartitionId partitionId = this._drmUtility.GetPartitionId(cacheName, regionName, this._cacheConfig.SystemRegionCount);
			return this._pollerData.GetPollVersion(partitionId);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000625C File Offset: 0x0000445C
		private void AddNotification(string cacheName, string regionName)
		{
			if (regionName != null)
			{
				PartitionId partitionId = this._drmUtility.GetPartitionId(cacheName, regionName, this._cacheConfig.SystemRegionCount);
				if (!this._pollerData.PutRegistrationPartitionNode(partitionId, regionName))
				{
					NotificationLsn lsn = this.GetLsn(partitionId);
					this._pollerData.PutRegistrationPartitionNode(partitionId, regionName, lsn);
					return;
				}
			}
			else
			{
				PartitionId[] partitionIdArray = this.GetPartitionIdArray(cacheName);
				if (!this._pollerData.PutRegistrationPartitionNodeList(partitionIdArray))
				{
					List<PartitionNotificationLsn> lsn2 = this.GetLsn(partitionIdArray);
					this._pollerData.PutRegistrationPartitionNodeList(lsn2);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000062E0 File Offset: 0x000044E0
		private void RemoveNotification(string cacheName, string regionName)
		{
			if (regionName != null)
			{
				PartitionId partitionId = this._drmUtility.GetPartitionId(cacheName, regionName, this._cacheConfig.SystemRegionCount);
				this._pollerData.RemoveRegistrationPartitionNode(partitionId, regionName);
				return;
			}
			PartitionId[] partitionIdArray = this.GetPartitionIdArray(cacheName);
			this._pollerData.RemoveRegistrationPartitionNodeList(partitionIdArray);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000632C File Offset: 0x0000452C
		private PartitionId[] GetPartitionIdArray(string cacheName)
		{
			ReadOnlyCollection<PartitionId> partitionIdList = this._drmUtility.GetPartitionIdList(cacheName);
			if (partitionIdList != null && partitionIdList.Count > 0)
			{
				PartitionId[] array = new PartitionId[partitionIdList.Count];
				partitionIdList.CopyTo(array, 0);
				return array;
			}
			return null;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600010A RID: 266 RVA: 0x0000636C File Offset: 0x0000456C
		// (remove) Token: 0x0600010B RID: 267 RVA: 0x000063A4 File Offset: 0x000045A4
		internal event Action<int> NotificationsReceivedEvent;

		// Token: 0x0400008A RID: 138
		private const string _myComponentName = "DistributedCache.Poller";

		// Token: 0x0400008B RID: 139
		internal const int InitialPollListSize = 32;

		// Token: 0x0400008C RID: 140
		private Hashtable _cacheProtocolTable;

		// Token: 0x0400008D RID: 141
		private TimeSpan _nwTimeout;

		// Token: 0x0400008E RID: 142
		private IDRMUtility _drmUtility;

		// Token: 0x0400008F RID: 143
		private long _maxPendingEvents;

		// Token: 0x04000090 RID: 144
		private IProducerConsumerEventQueue _eventQueue;

		// Token: 0x04000091 RID: 145
		private PollerStructure _pollerData;

		// Token: 0x04000092 RID: 146
		private object _updateQueueLock = new object();

		// Token: 0x04000093 RID: 147
		private NamedCacheConfiguration _cacheConfig;

		// Token: 0x04000094 RID: 148
		private int _pollCount;

		// Token: 0x04000095 RID: 149
		private int _pollOn;

		// Token: 0x04000096 RID: 150
		private readonly Action<PollerRequestContext> _notificationReceived;

		// Token: 0x04000097 RID: 151
		private readonly Action<PollerRequestContext> _notificationLsnReceived;

		// Token: 0x04000098 RID: 152
		private readonly Action<PollerRequestContext> _notificationFailed;
	}
}
