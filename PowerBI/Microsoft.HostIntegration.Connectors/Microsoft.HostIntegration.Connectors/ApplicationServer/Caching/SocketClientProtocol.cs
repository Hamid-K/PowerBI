using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000031 RID: 49
	internal sealed class SocketClientProtocol : IClientProtocol
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00007072 File Offset: 0x00005272
		public SocketClientProtocol(string cacheName, IClientSocketProtocol protocolProvider, SimpleSendReceiveModule module, DataCacheFactoryConfiguration factoryConfig)
		{
			this._cacheName = cacheName;
			this._protocol = protocolProvider;
			this._sendReceiveModule = module;
			this._factoryConfiguration = factoryConfig;
			this._serializationProvider = new DataCacheObjectSerializationProvider(factoryConfig.SerializationProperties);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000070AC File Offset: 0x000052AC
		public CacheServerProperties Initialize(IEnumerable<string> servers)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.GetProperties);
			velocityRequestPacket.CacheName = this._cacheName;
			bool flag = false;
			ICollection<VelocityPacketProperty> collection = new LinkedList<VelocityPacketProperty>();
			if (this._factoryConfiguration.AutoDiscoverProperty.IdentifierTypeSpecified == IdentifierType.EndPoint && this._factoryConfiguration.AutoDiscoverProperty.IsEnabled)
			{
				flag = true;
			}
			foreach (VelocityPacketProperty velocityPacketProperty in SocketClientProtocol.GetAllProperties(flag))
			{
				velocityRequestPacket.PropertyBag.AddRequestedProperty(velocityPacketProperty);
				collection.Add(velocityPacketProperty);
			}
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, (RequestBody req) => DataCacheFactory.EstablishConnection(servers, req, new Func<EndpointID, RequestBody, ResponseBody>(this.SendMessage), this._factoryConfiguration.CacheReadyRetryPolicy), out endpointID);
			ErrStatus responseCode = velocityResponsePacket.ResponseCode;
			if (responseCode != ErrStatus.UNINITIALIZED_ERROR)
			{
				DataCache.ThrowException(responseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			}
			this.CacheConfiguration = new NamedCacheConfiguration();
			foreach (VelocityPacketProperty velocityPacketProperty2 in collection)
			{
				byte[] property = velocityResponsePacket.PropertyBag.GetProperty(velocityPacketProperty2, null, (byte[] x) => x);
				if (property != null)
				{
					NamedCacheProperty namedCacheProperty;
					if (VelocityProperties.TryGetNamedCacheProperty(velocityPacketProperty2, out namedCacheProperty))
					{
						try
						{
							VelocityProperties.DeserializeNamedCachePropertyToConfiguration(this.CacheConfiguration, namedCacheProperty, property);
							continue;
						}
						catch (VelocityPacketFormatException ex)
						{
							DataCache.ThrowException(ErrStatus.INTERNAL_ERROR, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), ex, null, endpointID);
							continue;
						}
					}
					if (velocityPacketProperty2 == VelocityPacketProperty.ServerVersion && Provider.IsEnabled(TraceLevel.Info))
					{
						Version version = VelocityProperties.DeserializeVersion(property);
						EventLogWriter.WriteInfo("SocketClientProtocol", "Server version = {0}", new object[] { version });
					}
				}
			}
			if (flag)
			{
				return new CacheServerProperties
				{
					CacheConfiguration = this.CacheConfiguration,
					InitialLookupTable = this.TranslateCacheLookupTableTransfer(velocityResponsePacket.PropertyBag.GetLookupTable(this._cacheName))
				};
			}
			return new CacheServerProperties
			{
				CacheConfiguration = this.CacheConfiguration,
				InitialLookupTable = velocityResponsePacket.PropertyBag.GetLookupTable(this._cacheName)
			};
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000730C File Offset: 0x0000550C
		public void AggregateProperties(IEnumerable<VelocityPacketProperty> properties, Action<VelocityPacketProperty, byte[]> callback)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.GetProperties);
			velocityRequestPacket.CacheName = this._cacheName;
			foreach (VelocityPacketProperty velocityPacketProperty in properties)
			{
				velocityRequestPacket.PropertyBag.AddRequestedProperty(velocityPacketProperty);
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			ICollection<KeyValuePair<IVelocityRequestPacket, SendReceiveSynchronizer>> collection = new LinkedList<KeyValuePair<IVelocityRequestPacket, SendReceiveSynchronizer>>();
			IEnumerable<EndpointID> hostEndpoints = this._routingStrategy.GetHostEndpoints(this._cacheName);
			foreach (EndpointID endpointID in hostEndpoints)
			{
				velocityRequestPacket.MessageID = RequestIdGenerator.GetNewRequestId();
				RequestBody requestBodyFromPacket = this.GetRequestBodyFromPacket(velocityRequestPacket);
				SendReceiveSynchronizer sendReceiveSynchronizer = new SendReceiveSynchronizer(requestBodyFromPacket, requestBodyFromPacket.ClientRequestTracking);
				if (this._sendReceiveModule.SendMessage(endpointID, requestBodyFromPacket, sendReceiveSynchronizer).IsSuccess)
				{
					collection.Add(new KeyValuePair<IVelocityRequestPacket, SendReceiveSynchronizer>(velocityRequestPacket, sendReceiveSynchronizer));
				}
				else if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("SocketClientProtocol", "AggregateProperties: Could not receive properties from {0}", new object[] { endpointID });
				}
			}
			foreach (KeyValuePair<IVelocityRequestPacket, SendReceiveSynchronizer> keyValuePair in collection)
			{
				TimeSpan elapsed = stopwatch.Elapsed;
				if (elapsed < this._factoryConfiguration.RequestTimeout)
				{
					keyValuePair.Value.Handle.WaitOne(this._factoryConfiguration.RequestTimeout - elapsed);
				}
				ResponseBody resp = keyValuePair.Value.Resp;
				if (resp != null && resp.Ack == AckNack.Ack)
				{
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<ResponseBody, TimeSpan>("SocketClientProtocol", "AggregateProperties: Received stats response: {0}, timer: {1}", resp, elapsed);
					}
					IVelocityResponsePacket packetFromResponseBody = this.GetPacketFromResponseBody(keyValuePair.Key, resp);
					using (IEnumerator<VelocityPacketProperty> enumerator4 = properties.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							VelocityPacketProperty velocityPacketProperty2 = enumerator4.Current;
							byte[] array;
							if (packetFromResponseBody.PropertyBag.TryGetElement(velocityPacketProperty2, out array))
							{
								callback(velocityPacketProperty2, array);
							}
						}
						continue;
					}
				}
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("SocketClientProtocol", "AggregateProperties: Unexpected response received: {0}, timer: {1}", new object[] { resp, elapsed });
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000075BC File Offset: 0x000057BC
		private ResponseBody SendMessage(EndpointID endPt, RequestBody request)
		{
			IRequestTracker requestTracker;
			ResponseBody responseBody = this._sendReceiveModule.SendMsgAndWait(endPt, request, this._factoryConfiguration.RequestTimeout, out requestTracker);
			if (responseBody.Value != null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<Guid, int>("SocketClientProtocol", "{0}:SendMsgAndWait: DeSerializing Object: msgId = {1}", request.RequestTrackingId, request.ClientReqId);
				}
				responseBody.ValObject = SerializationUtility.Deserialize(responseBody.Value, true);
			}
			return responseBody;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007624 File Offset: 0x00005824
		private static IEnumerable<VelocityPacketProperty> GetAllProperties(bool isExternalClient)
		{
			yield return VelocityPacketProperty.DeploymentMode;
			yield return VelocityPacketProperty.PartitionCount;
			yield return VelocityPacketProperty.RegionCount;
			yield return VelocityPacketProperty.DefaultTTL;
			yield return VelocityPacketProperty.EvictionType;
			yield return VelocityPacketProperty.ExpirationType;
			yield return VelocityPacketProperty.NotificationProperties;
			yield return VelocityPacketProperty.ServerVersion;
			if (isExternalClient)
			{
				yield return VelocityPacketProperty.ExternalLookupTableWithIdentifiers;
			}
			else
			{
				yield return VelocityPacketProperty.LookupTable;
			}
			yield break;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007644 File Offset: 0x00005844
		private byte[][] SerializeValue(object obj)
		{
			if (this._factoryConfiguration.Protocol != ProtocolType.Memcache)
			{
				return this._serializationProvider.SerializeUserObject(obj, this._factoryConfiguration.IsCompressionEnabled, ValueFlagsVersion.WireProtocolType);
			}
			return obj as byte[][];
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007680 File Offset: 0x00005880
		private object DeserializeValue(byte[][] mem)
		{
			if (this._factoryConfiguration.Protocol != ProtocolType.Memcache)
			{
				return this._serializationProvider.DeserializeUserObject(mem, ValueFlagsVersion.WireProtocolType);
			}
			return mem;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000076A0 File Offset: 0x000058A0
		private IVelocityResponsePacket SendReceive(IVelocityRequestPacket request, IMonitoringListener listener, out EndpointID destination)
		{
			IRequestTracker tracker = null;
			request.CacheName = this._cacheName;
			request.IsMemcacheProtocol = this._factoryConfiguration.Protocol == ProtocolType.Memcache;
			bool isRequestTrackingEnabled = listener != null && listener.IsRequestTrackingSupported();
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(request, delegate(RequestBody req)
			{
				req.ClientRequestTracking = isRequestTrackingEnabled;
				return this.SendMsgAndWait(req, out tracker);
			}, out destination);
			if (isRequestTrackingEnabled)
			{
				listener.AddTrackerInfo(tracker);
			}
			return velocityResponsePacket;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007720 File Offset: 0x00005920
		private IVelocityResponsePacket ExecuteApi(IVelocityRequestPacket request, IMonitoringListener listener)
		{
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(request, listener, out endpointID);
			if (velocityResponsePacket.ResponseCode != ErrStatus.UNINITIALIZED_ERROR)
			{
				DataCache.ThrowException(velocityResponsePacket.ResponseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			}
			return velocityResponsePacket;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007760 File Offset: 0x00005960
		private ResponseBody SendReceiveMultiResponseRequest(IVelocityRequestPacket request, IMonitoringListener listener)
		{
			RequestBody requestBodyFromPacket = this.GetRequestBodyFromPacket(request);
			IRequestTracker requestTracker;
			return this.SendMsgAndWait(requestBodyFromPacket, out requestTracker);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007780 File Offset: 0x00005980
		private ResponseBody SendReceiveMultipleRequests(IVelocityRequestPacket[] requests, IMonitoringListener listener)
		{
			List<RequestBody> list = new List<RequestBody>(requests.Length);
			foreach (IVelocityRequestPacket velocityRequestPacket in requests)
			{
				list.Add(this.GetRequestBodyFromPacket(velocityRequestPacket));
			}
			MultiRequest multiRequest = new MultiRequest(list, new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT), false, RequestIdGenerator.GetNewRequestId());
			IRequestTracker requestTracker;
			return this._routingStrategy.SendMultiMessageAndWaitForAll(multiRequest, out requestTracker);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000077F0 File Offset: 0x000059F0
		private void SendReceiveBulkRequest(ReqType requestType, IMonitoringListener listener)
		{
			RequestBody requestBody = new RequestBody(requestType)
			{
				Action = "http://schemas.microsoft.com/velocity/msgs/request",
				CacheName = this._cacheName,
				SystemRegionCount = this.CacheConfiguration.SystemRegionCount
			};
			IRequestTracker requestTracker;
			this.SendMsgAndWait(requestBody, out requestTracker);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000783C File Offset: 0x00005A3C
		private IVelocityResponsePacket SendReceive(IVelocityRequestPacket request, Func<RequestBody, ResponseBody> @delegate, out EndpointID destination)
		{
			RequestBody requestBodyFromPacket = this.GetRequestBodyFromPacket(request);
			ResponseBody responseBody = @delegate(requestBodyFromPacket);
			destination = requestBodyFromPacket.Destination;
			return this.GetPacketFromResponseBody(request, responseBody);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000786C File Offset: 0x00005A6C
		private RequestBody GetRequestBodyFromPacket(IVelocityRequestPacket request)
		{
			RequestBody requestBody = new RequestBody(request)
			{
				Action = "http://schemas.microsoft.com/velocity/msgs/request",
				Key = ((request.Key != null) ? new Key(request.Key) : null),
				RegionName = request.Region,
				CacheName = this._cacheName
			};
			requestBody.RequestTrackingId = request.PropertyBag.GetMessageTrackingId();
			requestBody.IsTrackingIdPresent = requestBody.RequestTrackingId != Guid.Empty;
			IEnumerable<VelocityPacketProperty> requestedProperties = request.PropertyBag.GetRequestedProperties();
			requestBody.ClientRequestTracking = requestedProperties.Contains(VelocityPacketProperty.MessageGatewayTracker);
			requestBody.PrimaryRequestTracking = requestedProperties.Contains(VelocityPacketProperty.MessagePrimaryTracker);
			if (this.CacheConfiguration != null)
			{
				if (request.Key != null && requestBody.RegionName == null)
				{
					requestBody.RegionName = RegionNameProvider.GetSystemRegionName(request.Key, this.CacheConfiguration.SystemRegionCount);
				}
				if (requestBody.RegionName != null)
				{
					requestBody.SystemRegionCount = this.CacheConfiguration.SystemRegionCount;
				}
			}
			return requestBody;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007964 File Offset: 0x00005B64
		private IVelocityResponsePacket GetPacketFromResponseBody(IVelocityRequestPacket request, ResponseBody responseBody)
		{
			IVelocityResponsePacket velocityResponsePacket = responseBody.Packet;
			if (velocityResponsePacket == null)
			{
				velocityResponsePacket = this._protocol.CreateEmptyResponsePacket();
				velocityResponsePacket.MessageID = request.MessageID;
				velocityResponsePacket.ResponseCode = ((responseBody.Ack == AckNack.Nack) ? responseBody.ResponseCode : ErrStatus.INTERNAL_ERROR);
				velocityResponsePacket.Exception = responseBody.Exception;
			}
			return velocityResponsePacket;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000079B8 File Offset: 0x00005BB8
		internal ResponseBody SendMsgAndWait(RequestBody reqMsg, out IRequestTracker tracker)
		{
			ResponseBody responseBody = this._routingStrategy.SendMessageAndWait(reqMsg, out tracker);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Guid, int, AckNack, ErrStatus>("SocketClientProtocol", "{0}:SendReceive: Request {1} response's status is {2} : ErrorCode={3}", reqMsg.RequestTrackingId, reqMsg.ClientReqId, responseBody.Ack, responseBody.ResponseCode);
			}
			return responseBody;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007A03 File Offset: 0x00005C03
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00007A0B File Offset: 0x00005C0B
		public NamedCacheConfiguration CacheConfiguration { get; set; }

		// Token: 0x0600017A RID: 378 RVA: 0x00007A14 File Offset: 0x00005C14
		public void SetRoutingStrategy(IRoutingStrategy strategy)
		{
			this._routingStrategy = strategy;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007A20 File Offset: 0x00005C20
		public CacheLookupTableTransfer GetLookupTable(EndpointID endpoint, CacheLookupTableRequest request, TimeSpan timeout)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("SocketClientProtocol", "Sending lookup table refresh message to {0} over sockets.", endpoint.UriString);
			}
			string text = request.CacheName;
			bool flag = false;
			if (text == null)
			{
				if (request.CacheNames.Count != 1)
				{
					throw new NotImplementedException("Multiple caches are not supported currently on the same channel");
				}
				text = request.CacheNames.Single<string>();
			}
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.GetProperties);
			velocityRequestPacket.CacheName = text;
			if (this._factoryConfiguration.AutoDiscoverProperty.IdentifierTypeSpecified == IdentifierType.EndPoint && this._factoryConfiguration.AutoDiscoverProperty.IsEnabled)
			{
				velocityRequestPacket.PropertyBag.AddRequestedProperty(VelocityPacketProperty.ExternalLookupTableWithIdentifiers);
				flag = true;
			}
			else
			{
				velocityRequestPacket.PropertyBag.AddRequestedProperty(VelocityPacketProperty.LookupTable);
			}
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, delegate(RequestBody req)
			{
				req.Destination = endpoint;
				IRequestTracker requestTracker;
				ResponseBody responseBody = this._sendReceiveModule.SendMsgAndWait(endpoint, req, timeout, out requestTracker);
				if (responseBody.Value != null && responseBody.ValObject == null)
				{
					responseBody.ValObject = SerializationUtility.Deserialize(responseBody.Value, true);
				}
				return responseBody;
			}, out endpointID);
			ErrStatus responseCode = velocityResponsePacket.ResponseCode;
			if (responseCode != ErrStatus.UNINITIALIZED_ERROR)
			{
				int num = -1;
				int num2 = Utility.ConvertToDataCacheErrorCode(responseCode, out num);
				throw new OperationContextAbortedException("Failed getting Lookup Table from " + endpoint.UriString, DataCache.NewException(num2, num));
			}
			CacheLookupTableTransfer lookupTable = velocityResponsePacket.PropertyBag.GetLookupTable(text);
			if (flag)
			{
				return this.TranslateCacheLookupTableTransfer(lookupTable);
			}
			return lookupTable;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007B70 File Offset: 0x00005D70
		private CacheLookupTableTransfer TranslateCacheLookupTableTransfer(CacheLookupTableTransfer rawTableTransfer)
		{
			foreach (CacheLookupTableEntry cacheLookupTableEntry in rawTableTransfer.Entries)
			{
				int num = int.Parse(cacheLookupTableEntry.Config.Primary, CultureInfo.InvariantCulture);
				string serviceUri = Utility.GetServiceUri(this._factoryConfiguration.AutoDiscoverProperty.Identifier, this._factoryConfiguration.AutoDiscoverProperty.StartPort + num, TransportProtocol.NetTcp);
				cacheLookupTableEntry.Config.Primary = serviceUri;
			}
			return rawTableTransfer;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007C08 File Offset: 0x00005E08
		public object Get(string key, out DataCacheItemVersion version, out TimeSpan timeout, out ErrStatus err, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.Get);
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			err = velocityResponsePacket.ResponseCode;
			if (velocityResponsePacket.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
			{
				version = velocityResponsePacket.Version;
				timeout = VelocityWireProtocol.GetTimeSpan(velocityResponsePacket.ExpiryTTL);
				return this.DeserializeValue(velocityResponsePacket.Value);
			}
			if (velocityResponsePacket.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && velocityResponsePacket.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(velocityResponsePacket.ResponseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			}
			version = null;
			timeout = TimeSpan.Zero;
			return null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public DataCacheItemVersion Replace(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			return this.Upsert(VelocityPacketType.Replace, key, value, oldVersion, timeout, tags, region, listener);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public DataCacheItemVersion Add(string key, object value, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			return this.Upsert(VelocityPacketType.Add, key, value, null, timeout, tags, region, listener);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			return this.Upsert(VelocityPacketType.Put, key, value, oldVersion, timeout, tags, region, listener);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007D14 File Offset: 0x00005F14
		private DataCacheItemVersion Upsert(VelocityPacketType type, string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(type);
			velocityRequestPacket.Version = oldVersion;
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(timeout);
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Value = this.SerializeValue(value);
			if (tags != null && tags.Length != 0)
			{
				velocityRequestPacket.PropertyBag.SetTags(tags);
			}
			return this.ExecuteApi(velocityRequestPacket, listener).Version;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007D84 File Offset: 0x00005F84
		public bool ContainsKey(string key, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.ContainsKey);
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			IVelocityResponsePacket velocityResponsePacket = this.ExecuteApi(velocityRequestPacket, listener);
			return (bool)this.DeserializeValue(velocityResponsePacket.Value);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public long IncrementDecrement(string key, long value, long initialValue, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.Increment);
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Value = this.SerializeValue(value);
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(timeout);
			velocityRequestPacket.PropertyBag.SetElement(VelocityPacketProperty.InitialValue, VelocityProperties.WriteLong(initialValue));
			IVelocityResponsePacket velocityResponsePacket = this.ExecuteApi(velocityRequestPacket, listener);
			return (long)this.DeserializeValue(velocityResponsePacket.Value);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007E40 File Offset: 0x00006040
		public void Concatenate(string key, string value, bool isAppend, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket;
			if (isAppend)
			{
				velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.Append);
			}
			else
			{
				velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.Prepend);
			}
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Value = this.SerializeValue(value);
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(timeout);
			this.ExecuteApi(velocityRequestPacket, listener);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007EA4 File Offset: 0x000060A4
		public object GetIfNewer(string key, ref DataCacheItemVersion version, out TimeSpan timeout, out ErrStatus err, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.GetIfNewer);
			velocityRequestPacket.Version = version;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Key = key;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			err = velocityResponsePacket.ResponseCode;
			if (velocityResponsePacket.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
			{
				version = velocityResponsePacket.Version;
				timeout = VelocityWireProtocol.GetTimeSpan(velocityResponsePacket.ExpiryTTL);
				return this.DeserializeValue(velocityResponsePacket.Value);
			}
			if (velocityResponsePacket.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && velocityResponsePacket.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(velocityResponsePacket.ResponseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			}
			timeout = TimeSpan.Zero;
			return null;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007F58 File Offset: 0x00006158
		public bool Remove(string key, DataCacheItemVersion version, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.Remove);
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Version = version;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			if (velocityResponsePacket.ResponseCode != ErrStatus.UNINITIALIZED_ERROR && velocityResponsePacket.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && velocityResponsePacket.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST && velocityResponsePacket.ResponseCode != ErrStatus.VERSION_MISMATCH)
			{
				DataCache.ThrowException(velocityResponsePacket.ResponseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			}
			return velocityResponsePacket.ResponseCode == ErrStatus.UNINITIALIZED_ERROR;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007FE0 File Offset: 0x000061E0
		public void ResetObjectTimeout(string key, TimeSpan newTimeout, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.ResetTimeout);
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(newTimeout);
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Key = key;
			this.ExecuteApi(velocityRequestPacket, listener);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008020 File Offset: 0x00006220
		public object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, string region, bool lockKey, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(lockKey ? VelocityPacketType.GetAndLockForce : VelocityPacketType.GetAndLock);
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(timeout);
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Key = key;
			IVelocityPacket velocityPacket = this.ExecuteApi(velocityRequestPacket, listener);
			lockHandle = velocityPacket.LockHandle;
			if (velocityPacket.Value != null)
			{
				return this.DeserializeValue(velocityPacket.Value);
			}
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008088 File Offset: 0x00006288
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.PutAndUnlock);
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(timeout);
			velocityRequestPacket.LockHandle = lockHandle;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Value = this.SerializeValue(value);
			if (tags != null && tags.Length != 0)
			{
				velocityRequestPacket.PropertyBag.SetTags(tags);
			}
			return this.ExecuteApi(velocityRequestPacket, listener).Version;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000080F8 File Offset: 0x000062F8
		public bool LockedRemove(string key, DataCacheLockHandle lockHandle, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.LockedRemove);
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.LockHandle = lockHandle;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			ErrStatus responseCode = velocityResponsePacket.ResponseCode;
			if (responseCode <= ErrStatus.REGION_DOES_NOT_EXIST)
			{
				if (responseCode == ErrStatus.UNINITIALIZED_ERROR)
				{
					return true;
				}
				if (responseCode != ErrStatus.REGION_DOES_NOT_EXIST)
				{
					goto IL_0060;
				}
			}
			else if (responseCode != ErrStatus.KEY_DOES_NOT_EXIST)
			{
				switch (responseCode)
				{
				case ErrStatus.OBJECT_NOT_LOCKED:
				case ErrStatus.INVALID_LOCK:
					break;
				default:
					goto IL_0060;
				}
			}
			return false;
			IL_0060:
			DataCache.ThrowException(velocityResponsePacket.ResponseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			return false;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008184 File Offset: 0x00006384
		public void Unlock(string key, DataCacheLockHandle lockHandle, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.Unlock);
			velocityRequestPacket.ExpiryTTL = VelocityWireProtocol.GetTtl(timeout);
			velocityRequestPacket.LockHandle = lockHandle;
			velocityRequestPacket.Region = region;
			velocityRequestPacket.Key = key;
			this.ExecuteApi(velocityRequestPacket, listener);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000081CC File Offset: 0x000063CC
		public bool CreateRegion(string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.CreateRegion);
			velocityRequestPacket.Region = region;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			ErrStatus responseCode = velocityResponsePacket.ResponseCode;
			ErrStatus errStatus = responseCode;
			if (errStatus == ErrStatus.UNINITIALIZED_ERROR)
			{
				return true;
			}
			if (errStatus != ErrStatus.REGION_ALREADY_EXISTS)
			{
				DataCache.ThrowException(responseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
				return false;
			}
			return false;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008230 File Offset: 0x00006430
		public bool RemoveRegion(string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.RemoveRegion);
			velocityRequestPacket.Region = region;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			ErrStatus responseCode = velocityResponsePacket.ResponseCode;
			ErrStatus errStatus = responseCode;
			if (errStatus == ErrStatus.UNINITIALIZED_ERROR)
			{
				return true;
			}
			if (errStatus != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(responseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
				return false;
			}
			return false;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008294 File Offset: 0x00006494
		public void ClearRegion(string region, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.ClearRegion);
			velocityRequestPacket.Region = region;
			this.ExecuteApi(velocityRequestPacket, listener);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000082BF File Offset: 0x000064BF
		public void Clear(IMonitoringListener listener)
		{
			this.SendReceiveBulkRequest(ReqType.CLEAR_CACHE, listener);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000082CC File Offset: 0x000064CC
		public DataCacheItem GetCacheItem(string key, string region, out ErrStatus err, IMonitoringListener listener)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityPacketType.GetCacheItem);
			velocityRequestPacket.Key = key;
			velocityRequestPacket.Region = region;
			EndpointID endpointID;
			IVelocityResponsePacket velocityResponsePacket = this.SendReceive(velocityRequestPacket, listener, out endpointID);
			err = velocityResponsePacket.ResponseCode;
			if (velocityResponsePacket.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
			{
				if (velocityResponsePacket.Version == null)
				{
					DataCache.ThrowException(ErrStatus.INTERNAL_ERROR, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), null, null, endpointID);
				}
				if (region == null)
				{
					region = RegionNameProvider.GetSystemRegionName(key, this.CacheConfiguration.SystemRegionCount);
				}
				IEnumerable<DataCacheTag> tags = velocityResponsePacket.PropertyBag.GetTags();
				return new DataCacheItem(new Key(key), velocityResponsePacket.Value, ValueFlagsVersion.WireProtocolType, velocityResponsePacket.Version.InternalVersion, (tags != null) ? tags.ToArray<DataCacheTag>() : null, region, this._cacheName, VelocityWireProtocol.GetTimeSpan(velocityResponsePacket.ExpiryTTL), TimeSpan.Zero);
			}
			if (velocityResponsePacket.ResponseCode != ErrStatus.KEY_DOES_NOT_EXIST && velocityResponsePacket.ResponseCode != ErrStatus.REGION_DOES_NOT_EXIST)
			{
				DataCache.ThrowException(velocityResponsePacket.ResponseCode, velocityResponsePacket.PropertyBag.GetMessageTrackingId(), velocityResponsePacket.Exception, null, endpointID);
			}
			return null;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000083CC File Offset: 0x000065CC
		public IList<KeyValuePair<string, object>> GetNextBatch(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object enumeratorState, out bool more)
		{
			IVelocityRequestPacket velocityRequestPacket = this.CreateNextBatchRequest(region, tags, op, enumeratorState);
			ResponseBody responseBody = this.SendReceiveMultiResponseRequest(velocityRequestPacket, listener);
			more = false;
			if (responseBody.Ack == AckNack.Ack)
			{
				List<KeyValuePair<string, object>> list = responseBody.ValObject as List<KeyValuePair<string, object>>;
				enumeratorState = responseBody.EnumState;
				more = responseBody.MoreMsgs;
				return list;
			}
			if (responseBody.ResponseCode == ErrStatus.REGION_DOES_NOT_EXIST)
			{
				return null;
			}
			DataCache.ThrowException(responseBody.ResponseCode, responseBody.ResponseTrackingId, responseBody.Exception, null, null);
			return null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008444 File Offset: 0x00006644
		public IList<string> GetNextBatchOfKeys(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object enumeratorState, out bool more)
		{
			IVelocityRequestPacket velocityRequestPacket = this.CreateNextKeyBatchRequest(region, tags, op, enumeratorState);
			ResponseBody responseBody = this.SendReceiveMultiResponseRequest(velocityRequestPacket, listener);
			more = false;
			if (responseBody.Ack == AckNack.Ack)
			{
				List<KeyValuePair<string, object>> list = responseBody.ValObject as List<KeyValuePair<string, object>>;
				List<string> list2 = null;
				if (list != null)
				{
					list2 = new List<string>(list.Count);
					foreach (KeyValuePair<string, object> keyValuePair in list)
					{
						list2.Add(keyValuePair.Key);
					}
					if (responseBody.EnumState != null)
					{
						enumeratorState = responseBody.EnumState;
						more = true;
					}
				}
				return list2;
			}
			if (responseBody.ResponseCode == ErrStatus.REGION_DOES_NOT_EXIST)
			{
				return null;
			}
			DataCache.ThrowException(responseBody.ResponseCode, responseBody.ResponseTrackingId, responseBody.Exception, null, null);
			return null;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008518 File Offset: 0x00006718
		private IVelocityRequestPacket CreateNextKeyBatchRequest(string region, DataCacheTag[] tags, GetByTagsOperation op, object enumeratorState)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityWireProtocol.GetNextBatchMessageType(op));
			velocityRequestPacket.Region = region;
			if (tags != null && tags.Length != 0)
			{
				velocityRequestPacket.PropertyBag.SetTags(tags);
			}
			if (enumeratorState != null)
			{
				byte[][] array = enumeratorState as byte[][];
				ReleaseAssert.IsTrue(array != null);
				velocityRequestPacket.PropertyBag.SetElement(VelocityPacketProperty.EnumState, array[0]);
			}
			velocityRequestPacket.CacheItemMask = 1;
			return velocityRequestPacket;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008584 File Offset: 0x00006784
		private IVelocityRequestPacket CreateNextBatchRequest(string region, DataCacheTag[] tags, GetByTagsOperation op, object enumeratorState)
		{
			IVelocityRequestPacket velocityRequestPacket = this._protocol.CreateRequestPacket(VelocityWireProtocol.GetNextBatchMessageType(op));
			velocityRequestPacket.Region = region;
			velocityRequestPacket.CacheName = this._cacheName;
			if (enumeratorState == null)
			{
				if (tags != null && tags.Length != 0)
				{
					velocityRequestPacket.PropertyBag.SetTags(tags);
				}
			}
			else
			{
				byte[][] array = enumeratorState as byte[][];
				ReleaseAssert.IsTrue(array != null);
				velocityRequestPacket.PropertyBag.SetElement(VelocityPacketProperty.EnumState, array[0]);
			}
			return velocityRequestPacket;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000085F8 File Offset: 0x000067F8
		public IList<LocalCacheItem> BulkGet(Key[] keys, string region, IMonitoringListener listener)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("CacheApi", "Region BulkGet: {0} Keys requested ", new object[] { keys.Length });
			}
			return this.GetMultipleItems(keys, region, listener);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008638 File Offset: 0x00006838
		public IList<LocalCacheItem> BulkGet(Key[] keys, IMonitoringListener listener)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("CacheApi", "Cache BulkGet: {0} Keys requested ", new object[] { keys.Length });
			}
			return this.GetMultipleItems(keys, null, listener);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008678 File Offset: 0x00006878
		public IList<LocalCacheItem> GetMultipleItems(Key[] keys, string region, IMonitoringListener listener)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("CacheApi", "CacheBulkGet: {0} Keys requested, region {1} ", new object[]
				{
					keys.Length,
					region ?? "DefaultRegion"
				});
			}
			IVelocityRequestPacket[] array = new IVelocityRequestPacket[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this._protocol.CreateRequestPacket(VelocityPacketType.Get);
				array[i].CacheName = this._cacheName;
				array[i].Key = keys[i].StringValue;
				array[i].Region = region;
				array[i].IsMemcacheProtocol = this._factoryConfiguration.Protocol == ProtocolType.Memcache;
			}
			ResponseBody responseBody = this.SendReceiveMultipleRequests(array, listener);
			if (responseBody.Ack == AckNack.Nack)
			{
				DataCache.ThrowException(responseBody, null);
			}
			List<LocalCacheItem> list = new List<LocalCacheItem>(responseBody.ResponseList.Count);
			foreach (ResponseBody responseBody2 in responseBody.ResponseList)
			{
				if (responseBody2.Ack == AckNack.Ack)
				{
					IVelocityResponsePacket packet = responseBody2.Packet;
					LocalCacheItem localCacheItem = new LocalCacheItem(responseBody2.Key, packet.Value, packet.Version.InternalVersion, VelocityWireProtocol.GetTimeSpan(packet.ExpiryTTL));
					if (packet.Value != null)
					{
						localCacheItem.DeserializedValue = this.DeserializeValue(packet.Value);
					}
					list.Add(localCacheItem);
				}
				else if (Utility.IsValidBulkGetItemErrorCode(responseBody2.ResponseCode))
				{
					list.Add(new LocalCacheItem(responseBody2.Key));
				}
				else
				{
					DataCache.ThrowException(responseBody2.ResponseCode, responseBody2.ResponseTrackingId, responseBody.Exception, null, null);
				}
			}
			return list;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008838 File Offset: 0x00006A38
		public OperationResult SendNotificationRequestAsync(ReqType reqType, PollerRequestContext context, Action<PollerRequestContext> notifyCallback, Action<PollerRequestContext> errorCallback, out int notificationRequestId)
		{
			IVelocityRequestPacket requestPacket = null;
			VelocityPacketType velocityPacketType;
			VelocityPacketProperty velocityPacketProperty;
			switch (reqType)
			{
			case ReqType.NOTIFICATION_REQ:
				velocityPacketType = VelocityPacketType.Notification;
				velocityPacketProperty = VelocityPacketProperty.NotificationRequest;
				break;
			case ReqType.NOTIFICATION_LSN_REQ:
				velocityPacketType = VelocityPacketType.NotificationLsn;
				velocityPacketProperty = VelocityPacketProperty.NotificationLsnRequest;
				break;
			default:
				throw new ArgumentOutOfRangeException("reqType", reqType, "Not a notification request type");
			}
			requestPacket = this._protocol.CreateRequestPacket(velocityPacketType);
			requestPacket.CacheName = this._cacheName;
			requestPacket.PropertyBag.SetNotificationRequest(velocityPacketProperty, context.Request);
			RequestBody requestBodyFromPacket = this.GetRequestBodyFromPacket(requestPacket);
			requestBodyFromPacket.Session = context;
			requestBodyFromPacket.Caller = delegate(ResponseBody resp, RequestBody req, object unused)
			{
				IVelocityResponsePacket packetFromResponseBody = this.GetPacketFromResponseBody(requestPacket, resp);
				if (packetFromResponseBody.ResponseCode != ErrStatus.UNINITIALIZED_ERROR)
				{
					context.ResponseCode = packetFromResponseBody.ResponseCode;
					errorCallback(context);
					return;
				}
				NotificationReply notificationReply = packetFromResponseBody.PropertyBag.GetNotificationReply(VelocityPacketProperty.NotificationReply, this._cacheName);
				context.Reply = notificationReply;
				notifyCallback(context);
			};
			notificationRequestId = requestBodyFromPacket.ID;
			return this._sendReceiveModule.SendMessage(context.Endpoint, requestBodyFromPacket, requestBodyFromPacket.Caller);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000893A File Offset: 0x00006B3A
		public bool DismissNotificationRequest(int notificationRequestId)
		{
			return this._sendReceiveModule.MarkRequestCompleted(notificationRequestId);
		}

		// Token: 0x040000B9 RID: 185
		private const string _logSource = "SocketClientProtocol";

		// Token: 0x040000BA RID: 186
		private readonly string _cacheName;

		// Token: 0x040000BB RID: 187
		private readonly IClientSocketProtocol _protocol;

		// Token: 0x040000BC RID: 188
		private readonly DataCacheObjectSerializationProvider _serializationProvider;

		// Token: 0x040000BD RID: 189
		private readonly SimpleSendReceiveModule _sendReceiveModule;

		// Token: 0x040000BE RID: 190
		private readonly DataCacheFactoryConfiguration _factoryConfiguration;

		// Token: 0x040000BF RID: 191
		private IRoutingStrategy _routingStrategy;
	}
}
