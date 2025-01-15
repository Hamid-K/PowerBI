using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000038 RID: 56
	internal sealed class WcfClientProtocol : IClientProtocol
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00008C86 File Offset: 0x00006E86
		public WcfClientProtocol(string cacheName, SimpleSendReceiveModule module, DataCacheFactoryConfiguration factoryConfig)
		{
			this._cacheName = cacheName;
			this._sendReceiveModule = module;
			this._factoryConfiguration = factoryConfig;
			this._serializationProvider = new DataCacheObjectSerializationProvider(factoryConfig.SerializationProperties);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public CacheServerProperties Initialize(IEnumerable<string> servers)
		{
			ResponseBody responseBody = DataCacheFactory.EstablishConnection(servers, new RequestBody(ReqType.GET_NAMED_CACHE_CONFIGURATION)
			{
				CacheName = this._cacheName,
				Action = "http://schemas.microsoft.com/velocity/msgs/request",
				Fwd = ForwardingType.Routed,
				ValObject = new NamedCachePropertyList
				{
					propertiesRequired = (NamedCacheProperty[])Enum.GetValues(typeof(NamedCacheProperty))
				}
			}, new Func<EndpointID, RequestBody, ResponseBody>(this.SendMessage), this._factoryConfiguration.CacheReadyRetryPolicy);
			NamedCacheConfiguration namedCacheConfiguration = WcfClientProtocol.ExtractCachePropsFromList(responseBody.ValObject as List<KeyValuePair<NamedCacheProperty, object>>);
			this.CacheConfiguration = namedCacheConfiguration;
			return new CacheServerProperties
			{
				CacheConfiguration = namedCacheConfiguration,
				InitialLookupTable = null
			};
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003D71 File Offset: 0x00001F71
		public void AggregateProperties(IEnumerable<VelocityPacketProperty> properties, Action<VelocityPacketProperty, byte[]> callback)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008D60 File Offset: 0x00006F60
		private ResponseBody SendMessage(EndpointID endPt, RequestBody request)
		{
			IRequestTracker requestTracker;
			ResponseBody responseBody = this._sendReceiveModule.SendMsgAndWait(endPt, request, this._factoryConfiguration.RequestTimeout, out requestTracker);
			if (responseBody.Value != null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<Guid, int>("WcfClientProtocol", "{0}:SendMsgAndWait: DeSerializing Object: msgId = {1}", request.RequestTrackingId, request.ClientReqId);
				}
				responseBody.ValObject = SerializationUtility.Deserialize(responseBody.Value, true);
			}
			return responseBody;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008DC8 File Offset: 0x00006FC8
		private static NamedCacheConfiguration ExtractCachePropsFromList(IEnumerable<KeyValuePair<NamedCacheProperty, object>> propsList)
		{
			if (propsList == null)
			{
				return null;
			}
			NamedCacheConfiguration namedCacheConfiguration = new NamedCacheConfiguration();
			foreach (KeyValuePair<NamedCacheProperty, object> keyValuePair in propsList)
			{
				switch (keyValuePair.Key)
				{
				case NamedCacheProperty.CacheName:
					namedCacheConfiguration.Name = (string)keyValuePair.Value;
					break;
				case NamedCacheProperty.PartitionCount:
					namedCacheConfiguration.PartitionCount = (int)keyValuePair.Value;
					break;
				case NamedCacheProperty.RegionCount:
					namedCacheConfiguration.SystemRegionCount = (int)keyValuePair.Value;
					break;
				case NamedCacheProperty.Secondaries:
					namedCacheConfiguration.Secondaries = (int)keyValuePair.Value;
					break;
				case NamedCacheProperty.DefaultTTL:
					namedCacheConfiguration.DefaultTTL = (long)keyValuePair.Value;
					break;
				case NamedCacheProperty.EvictionType:
					namedCacheConfiguration.EvictionType = (EvictionType)keyValuePair.Value;
					break;
				case NamedCacheProperty.IsExpirable:
					namedCacheConfiguration.IsExpirable = (bool)keyValuePair.Value;
					break;
				case NamedCacheProperty.NotificationProps:
					namedCacheConfiguration.Notification = (ServerNotificationProperties)keyValuePair.Value;
					break;
				case NamedCacheProperty.AdditionalRoutingProps:
					namedCacheConfiguration.DeploymentMode = new DeploymentModeElement((DataCacheDeploymentMode)keyValuePair.Value);
					break;
				case NamedCacheProperty.ExpirationType:
					namedCacheConfiguration.ExpirationType = (ExpirationType)keyValuePair.Value;
					break;
				}
			}
			return namedCacheConfiguration;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008F3C File Offset: 0x0000713C
		internal void FillRegionNameIfRequired(RequestBody req)
		{
			if (req.IsCacheItemLevelRequest() && req.RegionName == null)
			{
				string systemRegionName = RegionNameProvider.GetSystemRegionName(req.Key.ToString(), this.CacheConfiguration.SystemRegionCount);
				req.SetRegionName(systemRegionName, this.CacheConfiguration.SystemRegionCount);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008F88 File Offset: 0x00007188
		internal ResponseBody SendReceive(RequestBody requestBody, IMonitoringListener listener)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody>("WcfClientProtocol", "SendReceive: Begin: {0}", requestBody);
			}
			this.FillRegionNameIfRequired(requestBody);
			requestBody.ClientRequestTracking = listener != null && listener.IsRequestTrackingSupported();
			IRequestTracker requestTracker;
			ResponseBody responseBody = this.SendMsgAndWait(requestBody, out requestTracker);
			if (listener != null && listener.IsRequestTrackingSupported())
			{
				listener.AddTrackerInfo(requestTracker);
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Guid, int, AckNack, ErrStatus>("WcfClientProtocol", "{0}:SendReceive: Request's {1} response's status is {2} : ErrorCode={3}", requestBody.RequestTrackingId, requestBody.ClientReqId, responseBody.Ack, responseBody.ResponseCode);
			}
			return responseBody;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009014 File Offset: 0x00007214
		internal ResponseBody SendMsgAndWait(RequestBody reqMsg, out IRequestTracker tracker)
		{
			if (reqMsg.UserObject != null)
			{
				reqMsg.Value = this._serializationProvider.SerializeUserObject(reqMsg.UserObject, this._factoryConfiguration.IsCompressionEnabled, ValueFlagsVersion.WireProtocolType);
			}
			if (reqMsg.InitialUserObject != null)
			{
				reqMsg.InitialValue = this._serializationProvider.SerializeUserObject(reqMsg.InitialUserObject, this._factoryConfiguration.IsCompressionEnabled, ValueFlagsVersion.WireProtocolType);
			}
			ResponseBody responseBody = this._routingStrategy.SendMessageAndWait(reqMsg, out tracker);
			if (responseBody.Value != null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<Guid, int>("WcfClientProtocol", "{0}:SendMsgAndWait: DeSerializing Object: msgId = {1}", reqMsg.RequestTrackingId, reqMsg.ClientReqId);
				}
				responseBody.ValObject = this._serializationProvider.DeserializeUserObject(responseBody.Value, ValueFlagsVersion.EitherType);
			}
			return responseBody;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000090CC File Offset: 0x000072CC
		private ResponseBody ExecuteAPI(RequestBody reqMsg, IMonitoringListener listener)
		{
			ResponseBody responseBody = this.SendReceive(reqMsg, listener);
			if (responseBody.Ack == AckNack.Nack)
			{
				DataCache.ThrowException(responseBody, reqMsg.Destination);
			}
			return responseBody;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000090F8 File Offset: 0x000072F8
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009100 File Offset: 0x00007300
		public NamedCacheConfiguration CacheConfiguration { get; set; }

		// Token: 0x060001BA RID: 442 RVA: 0x00009109 File Offset: 0x00007309
		public void SetRoutingStrategy(IRoutingStrategy routingStrategy)
		{
			this._routingStrategy = routingStrategy;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009114 File Offset: 0x00007314
		public CacheLookupTableTransfer GetLookupTable(EndpointID endpoint, CacheLookupTableRequest request, TimeSpan timeout)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("WcfClientProtocol", "Sending lookup table refresh message to {0}.", endpoint.UriString);
			}
			ICreateMessage createMessage = new VasCacheLookupTableRequestMessage(request, "http://schemas.microsoft.com/velocity/msgs/CacheLookupTableRequestActionInternal");
			Message message = null;
			CacheLookupTableTransfer cacheLookupTableTransfer;
			try
			{
				IClientChannel channel = this._sendReceiveModule.Channel;
				OperationResult operationResult;
				if (channel != null)
				{
					operationResult = this._sendReceiveModule.Channel.SendReceive(endpoint, createMessage, timeout, out message);
				}
				else
				{
					operationResult = new OperationResult(OperationStatus.InstanceClosed);
				}
				if (!operationResult.IsSuccess || message == null)
				{
					Exception ex = (operationResult.HasVerificationFailed ? DataCache.NewException(19) : operationResult.Fault);
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo("WcfClientProtocol", "Lookup table refresh from {0} Failed - {1}", new object[] { this._cacheName, operationResult });
					}
					throw new OperationContextAbortedException("Failed getting Lookup Table from " + endpoint.UriString, ex);
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string>("WcfClientProtocol", "Lookup table refresh received from {0}.", endpoint.UriString);
				}
				try
				{
					cacheLookupTableTransfer = message.GetBody<CacheLookupTableTransfer>();
				}
				catch (SerializationException ex2)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError("WcfClientProtocol", "Invalid message body for lookup table transfer. - {0}", new object[] { ex2 });
					}
					cacheLookupTableTransfer = null;
				}
			}
			finally
			{
				if (message != null)
				{
					message.Close();
				}
			}
			return cacheLookupTableTransfer;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IList<string> GetNextBatchOfKeys(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object state, out bool more)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009264 File Offset: 0x00007464
		public bool ContainsKey(string key, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.CONTAINSKEY);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.Version = InternalCacheItemVersion.Null;
			ResponseBody responseBody = this.ExecuteAPI(requestBody, listener);
			return (bool)responseBody.ValObject;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000092C4 File Offset: 0x000074C4
		public long IncrementDecrement(string key, long value, long initialValue, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.INCREMENT);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.Version = InternalCacheItemVersion.Null;
			requestBody.UserObject = value;
			requestBody.InitialUserObject = initialValue;
			requestBody.TTL = timeout;
			ResponseBody responseBody = this.ExecuteAPI(requestBody, listener);
			return (long)responseBody.ValObject;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009344 File Offset: 0x00007544
		public void Concatenate(string key, string value, bool isAppend, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			RequestBody requestBody;
			if (isAppend)
			{
				requestBody = new RequestBody(ReqType.APPEND);
			}
			else
			{
				requestBody = new RequestBody(ReqType.PREPEND);
			}
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.TTL = timeout;
			requestBody.Key = new Key(key);
			requestBody.Version = InternalCacheItemVersion.Null;
			requestBody.UserObject = value;
			this.ExecuteAPI(requestBody, listener);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000093B8 File Offset: 0x000075B8
		public object Get(string key, out DataCacheItemVersion version, out TimeSpan timeout, out ErrStatus err, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.GET);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.Version = InternalCacheItemVersion.Null;
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				err = ErrStatus.UNINITIALIZED_ERROR;
				version = new DataCacheItemVersion(responseBody.Version);
				timeout = responseBody.Timeout;
				return responseBody.ValObject;
			}
			if (responseBody.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			err = responseBody.ResponseCode;
			version = null;
			timeout = TimeSpan.Zero;
			return null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00003CAB File Offset: 0x00001EAB
		public DataCacheItemVersion Replace(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009470 File Offset: 0x00007670
		public DataCacheItemVersion Add(string key, object value, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.ADD);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.UserObject = value;
			requestBody.TTL = timeout;
			requestBody.Tags = tags;
			ResponseBody responseBody = this.ExecuteAPI(requestBody, listener);
			return new DataCacheItemVersion(responseBody.Version);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000094DC File Offset: 0x000076DC
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.PUT);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.UserObject = value;
			requestBody.Version = ((oldVersion == null) ? InternalCacheItemVersion.Null : oldVersion.InternalVersion);
			requestBody.TTL = timeout;
			requestBody.Tags = tags;
			ResponseBody responseBody = this.ExecuteAPI(requestBody, listener);
			return new DataCacheItemVersion(responseBody.Version);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009564 File Offset: 0x00007764
		public object GetIfNewer(string key, ref DataCacheItemVersion version, out TimeSpan timeout, out ErrStatus err, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.GET_IF_NEWER);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.Version = version.InternalVersion;
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				err = ErrStatus.UNINITIALIZED_ERROR;
				version = new DataCacheItemVersion(responseBody.Version);
				timeout = responseBody.Timeout;
				return responseBody.ValObject;
			}
			if (responseBody.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			err = responseBody.ResponseCode;
			timeout = TimeSpan.Zero;
			return null;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000961C File Offset: 0x0000781C
		public bool Remove(string key, DataCacheItemVersion version, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.REMOVE);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.Version = ((version != null) ? version.InternalVersion : InternalCacheItemVersion.Null);
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				return true;
			}
			if (responseBody.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.VERSION_MISMATCH)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			return false;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000096B8 File Offset: 0x000078B8
		public DataCacheItem GetCacheItem(string key, string region, out ErrStatus err, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.GET_CACHE_ITEM);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			err = responseBody.ResponseCode;
			if (responseBody.Ack == AckNack.Ack)
			{
				return responseBody.Item;
			}
			if (responseBody.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			return null;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009738 File Offset: 0x00007938
		public void Clear(IMonitoringListener listener)
		{
			this.ExecuteAPI(new RequestBody(ReqType.CLEAR_CACHE)
			{
				CacheName = this._cacheName,
				SystemRegionCount = this.CacheConfiguration.SystemRegionCount
			}, listener);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009774 File Offset: 0x00007974
		public object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, string region, bool lockKey, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.GET_AND_LOCK);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.TTL = timeout;
			requestBody.GetAndLockFlag = lockKey;
			ResponseBody responseBody = this.ExecuteAPI(requestBody, listener);
			lockHandle = responseBody.LockObject;
			return responseBody.ValObject;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000097DC File Offset: 0x000079DC
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.PUT_AND_UNLOCK);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.UserObject = value;
			requestBody.TTL = timeout;
			requestBody.LockObject = lockHandle;
			requestBody.Tags = tags;
			ResponseBody responseBody = this.ExecuteAPI(requestBody, listener);
			return new DataCacheItemVersion(responseBody.Version);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009850 File Offset: 0x00007A50
		public bool LockedRemove(string key, DataCacheLockHandle lockHandle, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.LOCKED_REMOVE);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.LockObject = lockHandle;
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				return true;
			}
			if (responseBody.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST && responseBody.ResponseCode != ErrStatus.INVALID_LOCK && responseBody.ResponseCode != ErrStatus.OBJECT_NOT_LOCKED)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000098E0 File Offset: 0x00007AE0
		public void Unlock(string key, DataCacheLockHandle lockHandle, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.UNLOCK);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.LockObject = lockHandle;
			requestBody.TTL = timeout;
			this.ExecuteAPI(requestBody, listener);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009938 File Offset: 0x00007B38
		public void ResetObjectTimeout(string key, TimeSpan newTimeout, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.RESET_TIMEOUT);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Key = new Key(key);
			requestBody.TTL = newTimeout;
			this.ExecuteAPI(requestBody, listener);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009988 File Offset: 0x00007B88
		public bool CreateRegion(string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.CREATE_REGION);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				return true;
			}
			if (responseBody.ResponseCode != ErrStatus.REGION_ALREADY_EXISTS)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			return false;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000099E4 File Offset: 0x00007BE4
		public bool RemoveRegion(string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.REMOVE_REGION);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				return true;
			}
			if (responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseBody, requestBody.Destination);
			}
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009A40 File Offset: 0x00007C40
		public void ClearRegion(string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.CLEAR_REGION);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			this.ExecuteAPI(requestBody, listener);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009A7C File Offset: 0x00007C7C
		public IList<KeyValuePair<string, object>> GetNextBatch(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object state, out bool more)
		{
			RequestBody requestBody = new RequestBody(ReqType.GET_NEXT_BATCH);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.GetByTagsOp = op;
			if (state == null)
			{
				requestBody.Tags = tags;
			}
			else
			{
				requestBody.EnumState = (byte[][])state;
			}
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				state = responseBody.EnumState;
				more = responseBody.MoreMsgs;
				return (List<KeyValuePair<string, object>>)responseBody.ValObject;
			}
			if (responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseBody, null);
			}
			more = false;
			return null;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009B18 File Offset: 0x00007D18
		public IList<LocalCacheItem> BulkGet(Key[] keys, string region, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(ReqType.BULK_GET);
			requestBody.SetRegionName(region, this.CacheConfiguration.SystemRegionCount);
			requestBody.CacheName = this._cacheName;
			requestBody.Keys = keys;
			requestBody.Version = InternalCacheItemVersion.Null;
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				IList<LocalCacheItem> list = (List<LocalCacheItem>)responseBody.ValObject;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Value != null)
					{
						list[i].DeserializedValue = this._serializationProvider.DeserializeUserObject(list[i].Value, ValueFlagsVersion.EitherType);
					}
				}
				return list;
			}
			if (responseBody.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseBody, null);
			}
			IList<LocalCacheItem> list2 = new List<LocalCacheItem>(keys.Length);
			for (int j = 0; j < keys.Length; j++)
			{
				list2.Add(new LocalCacheItem(keys[j].StringValue));
			}
			return list2;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009C04 File Offset: 0x00007E04
		public IList<LocalCacheItem> BulkGet(Key[] keys, IMonitoringListener listener)
		{
			List<LocalCacheItem> list = new List<LocalCacheItem>(keys.Length);
			RequestBody requestBody = new RequestBody(ReqType.CACHE_BULK_GET);
			requestBody.CacheName = this._cacheName;
			requestBody.Keys = keys;
			requestBody.Version = InternalCacheItemVersion.Null;
			requestBody.SystemRegionCount = this.CacheConfiguration.SystemRegionCount;
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("CacheApi", "CacheBulkGet: {0} Keys requested , ReqId {1}", new object[] { keys.Length, requestBody.ClientReqId });
			}
			ResponseBody responseBody = this.SendReceive(requestBody, listener);
			if (responseBody.Ack == AckNack.Ack)
			{
				using (List<ResponseBody>.Enumerator enumerator = responseBody.ResponseList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ResponseBody responseBody2 = enumerator.Current;
						if (responseBody2.Ack == AckNack.Nack)
						{
							if (responseBody2.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST)
							{
								DataCache.ThrowException(responseBody2, null);
							}
							else
							{
								list.Add(new LocalCacheItem(responseBody2.Key));
							}
						}
						else
						{
							list.Add(new LocalCacheItem(responseBody2.Key, responseBody2.Value, responseBody2.Version, responseBody2.Timeout)
							{
								DeserializedValue = this._serializationProvider.DeserializeUserObject(responseBody2.Value, ValueFlagsVersion.EitherType)
							});
						}
					}
					return list;
				}
			}
			DataCache.ThrowException(responseBody, null);
			return list;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009D58 File Offset: 0x00007F58
		public OperationResult SendNotificationRequestAsync(ReqType reqType, PollerRequestContext context, Action<PollerRequestContext> notifyCallback, Action<PollerRequestContext> errorCallback, out int notificationRequestId)
		{
			RequestBody requestBody = new RequestBody(reqType);
			requestBody.Action = "http://schemas.microsoft.com/velocity/msgs/DOMRequest";
			requestBody.Session = context;
			requestBody.ValObject = context.Request;
			requestBody.Caller = delegate(ResponseBody resp, RequestBody req, object unused)
			{
				if (resp.Ack == AckNack.Nack)
				{
					context.ResponseCode = resp.ResponseCode;
					errorCallback(context);
					return;
				}
				NotificationReply notificationReply = new NotificationReply();
				SerializationUtility.DeserializeBinaryFormat(resp.Value, notificationReply);
				context.Reply = notificationReply;
				notifyCallback(context);
			};
			notificationRequestId = requestBody.ID;
			return this._sendReceiveModule.SendMessage(context.Endpoint, requestBody, requestBody.Caller);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public bool DismissNotificationRequest(int notificationRequestId)
		{
			return this._sendReceiveModule.MarkRequestCompleted(notificationRequestId);
		}

		// Token: 0x040000D6 RID: 214
		private const string _logSource = "WcfClientProtocol";

		// Token: 0x040000D7 RID: 215
		private readonly string _cacheName;

		// Token: 0x040000D8 RID: 216
		private readonly DataCacheObjectSerializationProvider _serializationProvider;

		// Token: 0x040000D9 RID: 217
		private readonly SimpleSendReceiveModule _sendReceiveModule;

		// Token: 0x040000DA RID: 218
		private readonly DataCacheFactoryConfiguration _factoryConfiguration;

		// Token: 0x040000DB RID: 219
		private IRoutingStrategy _routingStrategy;
	}
}
