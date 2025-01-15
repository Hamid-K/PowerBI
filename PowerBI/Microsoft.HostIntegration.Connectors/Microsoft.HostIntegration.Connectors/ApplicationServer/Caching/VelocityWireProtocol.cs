using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000185 RID: 389
	internal class VelocityWireProtocol : IClientSocketProtocol, IServerSocketProtocol
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x0002A318 File Offset: 0x00028518
		private static IEnumerable<ArraySegment<byte>> GetReadPacketBuffer(IList<IVelocityPacket> packets, IBufferManager bufferManager, byte expectedMagicByte, CreateEmptyVelocityPacket createPacket)
		{
			bool hasMorePackets = false;
			VelocityPacket currentPacket;
			for (;;)
			{
				currentPacket = createPacket();
				packets.Add(currentPacket);
				byte[] buffer = bufferManager.TakeBuffer(32);
				try
				{
					yield return new ArraySegment<byte>(buffer, 0, 32);
					currentPacket.ReadHeader(buffer);
				}
				finally
				{
					bufferManager.ReleaseBuffer(buffer);
				}
				if (currentPacket.MagicByte != expectedMagicByte)
				{
					break;
				}
				if (currentPacket.PayloadLength > 2147483647U || (ulong)currentPacket.PayloadLength > (ulong)((long)(bufferManager.MaxMessageSize - 32)))
				{
					goto IL_0126;
				}
				int payloadLength = (int)currentPacket.PayloadLength;
				if (payloadLength != 0)
				{
					buffer = bufferManager.TakeBuffer(payloadLength);
					try
					{
						yield return new ArraySegment<byte>(buffer, 0, payloadLength);
						currentPacket.ReadPayload(buffer, 0);
					}
					finally
					{
						bufferManager.ReleaseBuffer(buffer);
						DiagOperation.OpState = new DiagOperationState();
						VelocityDiagnostics.Publish(DiagEventName.PacketRead, true, DiagOperation.OpState, TraceLevel.Info, "VelocityWireProtocol.Parsing", "Packet state at end of GetReadPacketBuffer {0}", new object[] { currentPacket });
					}
				}
				hasMorePackets = currentPacket.HasHeaderFlagSet(VelocityPacketHeaderFlags.IsLinkedToNext);
				if (!hasMorePackets)
				{
					goto Block_6;
				}
			}
			throw new VelocityPacketFormatFatalException("Unsupported packet version {0:X2}. Expected {1:X2}", new object[] { currentPacket.MagicByte, expectedMagicByte });
			IL_0126:
			long num = (long)((ulong)(currentPacket.PayloadLength + 32U));
			throw new VelocityPacketTooBigException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ReceivedMessageTooLarge", num, bufferManager.MaxMessageSize));
			Block_6:
			yield break;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002A34C File Offset: 0x0002854C
		public static IVelocityRequestPacket GetRequestPacket(RequestBody requestBody)
		{
			if (requestBody.Req == ReqType.PARTITION_CLEAR_CACHE)
			{
				IClientSocketProtocol clientSocketProtocol = new VelocityWireProtocol();
				IVelocityRequestPacket velocityRequestPacket = clientSocketProtocol.CreateRequestPacket(VelocityPacketType.ClearPartition);
				velocityRequestPacket.CacheName = requestBody.CacheName;
				velocityRequestPacket.MessageID = requestBody.ClientReqId;
				PartitionId partitionId = (PartitionId)requestBody.ValObject;
				velocityRequestPacket.PartitionKey = partitionId;
				return velocityRequestPacket;
			}
			IVelocityRequestPacket velocityRequestPacket2 = requestBody.Packet;
			if (velocityRequestPacket2 == null)
			{
				velocityRequestPacket2 = requestBody.GetRequestPacket();
			}
			else
			{
				if (requestBody.ClientRequestTracking)
				{
					velocityRequestPacket2.PropertyBag.AddRequestedProperty(VelocityPacketProperty.MessageGatewayTracker);
				}
				if (requestBody.PrimaryRequestTracking)
				{
					velocityRequestPacket2.PropertyBag.AddRequestedProperty(VelocityPacketProperty.MessagePrimaryTracker);
				}
			}
			return velocityRequestPacket2;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002A3E4 File Offset: 0x000285E4
		public static IVelocityResponsePacket FromResponseBody(IVelocityPacket request, ResponseBody response)
		{
			VelocityWireProtocol velocityWireProtocol = new VelocityWireProtocol();
			IVelocityResponsePacket velocityResponsePacket = velocityWireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
			velocityResponsePacket.IsMemcacheProtocol = request.IsMemcacheProtocol;
			velocityResponsePacket.ResponseCode = response.ResponseCode;
			if (response.Item != null)
			{
				DataCacheItem item = response.Item;
				velocityResponsePacket.Version = item.Version;
				velocityResponsePacket.PropertyBag.SetTags(item.Tags);
				velocityResponsePacket.Value = item.SerializedValue;
				velocityResponsePacket.ExpiryTTL = VelocityWireProtocol.GetTtl(item.Timeout);
			}
			else
			{
				if (response.LockObject != null && response.LockObject.IsValid)
				{
					velocityResponsePacket.LockHandle = response.LockObject;
				}
				if (response.Version != InternalCacheItemVersion.Null)
				{
					velocityResponsePacket.Version = new DataCacheItemVersion(response.Version);
				}
				velocityResponsePacket.ExpiryTTL = VelocityWireProtocol.GetTtl(response.Timeout);
				velocityResponsePacket.PropertyBag.SetTags(response.Tags);
				IList<KeyValuePair<VelocityPacketProperty, byte[]>> list = response.ValObject as IList<KeyValuePair<VelocityPacketProperty, byte[]>>;
				if (list != null)
				{
					foreach (KeyValuePair<VelocityPacketProperty, byte[]> keyValuePair in list)
					{
						velocityResponsePacket.PropertyBag.SetElement(keyValuePair.Key, keyValuePair.Value);
					}
					velocityResponsePacket.Value = null;
				}
				else if (velocityResponsePacket.MessageType == VelocityPacketType.NotificationLsn || velocityResponsePacket.MessageType == VelocityPacketType.Notification)
				{
					if (response.ValObject != null)
					{
						velocityResponsePacket.PropertyBag.SetNotificationReply(VelocityPacketProperty.NotificationReply, response.ValObject as NotificationReply);
					}
				}
				else
				{
					velocityResponsePacket.Value = response.Value;
				}
			}
			if (response.IsTrackingIdPresent)
			{
				velocityResponsePacket.PropertyBag.SetMessageTrackingId(response.ResponseTrackingId);
			}
			if (response.RequestTracker != null)
			{
				velocityResponsePacket.PropertyBag.SetRequestTracker(VelocityPacketProperty.MessageGatewayTracker, response.RequestTracker);
			}
			if (response.RequestTrackerOnPrimary != null)
			{
				velocityResponsePacket.PropertyBag.SetRequestTracker(VelocityPacketProperty.MessagePrimaryTracker, response.RequestTrackerOnPrimary);
			}
			return velocityResponsePacket;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002A5DC File Offset: 0x000287DC
		internal static IList<IVelocityResponsePacket> GetBatchResponse(IVelocityPacket request, ResponseBody response)
		{
			VelocityWireProtocol velocityWireProtocol = new VelocityWireProtocol();
			IList<IVelocityResponsePacket> list = new List<IVelocityResponsePacket>();
			bool flag = false;
			if (request.CacheItemMask != 0)
			{
				flag = true;
			}
			List<KeyValuePair<string, object>> list2 = response.ValObject as List<KeyValuePair<string, object>>;
			if (list2 != null && list2.Count != 0)
			{
				foreach (KeyValuePair<string, object> keyValuePair in list2)
				{
					IVelocityResponsePacket velocityResponsePacket = velocityWireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
					velocityResponsePacket.Key = keyValuePair.Key;
					if (!flag)
					{
						velocityResponsePacket.Value = (byte[][])keyValuePair.Value;
					}
					IVelocityResponsePacket velocityResponsePacket2 = velocityResponsePacket;
					velocityResponsePacket2.HeaderFlags |= VelocityPacketHeaderFlags.IsLinkedToNext;
					list.Add(velocityResponsePacket);
				}
				list[list.Count - 1].HeaderFlags = list[list.Count - 1].HeaderFlags & ~VelocityPacketHeaderFlags.IsLinkedToNext;
			}
			else
			{
				IVelocityResponsePacket velocityResponsePacket3 = velocityWireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
				velocityResponsePacket3.HeaderFlags &= ~VelocityPacketHeaderFlags.IsLinkedToNext;
				list.Add(velocityResponsePacket3);
			}
			if (response.MoreMsgs)
			{
				list[0].PropertyBag.SetProperty(VelocityPacketProperty.EnumState, response.EnumState, new Converter<byte[][], byte[]>(VelocityWireProtocol.ConvertStateToByteArray));
			}
			return list;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002A738 File Offset: 0x00028938
		public static RequestBody GetRequestBody(IVelocityRequestPacket packet)
		{
			if (packet == null)
			{
				return null;
			}
			RequestBody requestBody = new RequestBody(VelocityWireProtocol.GetRequestType(packet.MessageType), false);
			requestBody.Action = "http://schemas.microsoft.com/velocity/msgs/request";
			requestBody.ClientReqId = packet.MessageID;
			requestBody.ServiceReqId = packet.MessageID;
			requestBody.CacheName = packet.CacheName;
			requestBody.GetAndLockFlag = packet.MessageType == VelocityPacketType.GetAndLockForce;
			requestBody.GetByTagsOp = VelocityWireProtocol.GetByTagsOperationType(packet.MessageType);
			requestBody.EnumState = packet.PropertyBag.GetProperty(VelocityPacketProperty.EnumState, null, new Converter<byte[], byte[][]>(VelocityWireProtocol.GetStateFromByteArray));
			requestBody.RegionName = packet.Region;
			requestBody.Key = ((packet.Key != null) ? new Key(packet.Key) : null);
			requestBody.Value = packet.Value;
			requestBody.SerializationCategory = (packet.IsMemcacheProtocol ? SerializationCategory.Memcache : SerializationCategory.Native);
			requestBody.protocolType = (packet.IsMemcacheProtocol ? ProtocolType.Memcache : ProtocolType.Regular);
			if (requestBody.Req == ReqType.NOTIFICATION_REQ)
			{
				requestBody.ValObject = packet.PropertyBag.GetNotificationRequest(VelocityPacketProperty.NotificationRequest, requestBody.CacheName);
			}
			else if (requestBody.Req == ReqType.NOTIFICATION_LSN_REQ)
			{
				requestBody.ValObject = packet.PropertyBag.GetNotificationRequest(VelocityPacketProperty.NotificationLsnRequest, requestBody.CacheName);
			}
			else if (requestBody.Req == ReqType.INCREMENT || requestBody.Req == ReqType.DECREMENT)
			{
				byte[] array = null;
				if (packet.PropertyBag.TryGetElement(VelocityPacketProperty.InitialValue, out array))
				{
					if (!packet.IsMemcacheProtocol)
					{
						if (array != null && array.Length == 8)
						{
							requestBody.InitialValue = PrimitiveDataCacheObjectSerializationProvider.WireProtocolType.SerializeUserObject(VelocityProperties.ReadLong(array), SerializationCategory.Native);
						}
						else
						{
							requestBody.InitialValue = null;
						}
					}
					else if (array != null)
					{
						requestBody.InitialValue = Utility.GetChunkedArray(array, 0);
					}
				}
			}
			if (packet.CacheItemMask != 0)
			{
				requestBody.IsKeyOnly = true;
			}
			IEnumerable<DataCacheTag> tags = packet.PropertyBag.GetTags();
			if (tags != null && tags.Count<DataCacheTag>() != 0)
			{
				requestBody.Tags = tags.ToArray<DataCacheTag>();
			}
			if (packet.LockHandle != null && packet.LockHandle.IsValid)
			{
				requestBody.LockObject = packet.LockHandle;
			}
			if (packet.Version != null)
			{
				requestBody.Version = packet.Version.InternalVersion;
			}
			requestBody.TTL = (packet.IsMemcacheProtocol ? MemcachePacket.GetTimeSpan(packet.ExpiryTTL) : VelocityWireProtocol.GetTimeSpan(packet.ExpiryTTL));
			requestBody.PartitionId = packet.PartitionKey;
			requestBody.RequestTrackingId = packet.PropertyBag.GetMessageTrackingId();
			requestBody.IsTrackingIdPresent = requestBody.RequestTrackingId != Guid.Empty;
			if (string.IsNullOrEmpty(requestBody.UniqueTrackingId) && !string.IsNullOrEmpty(DiagOperation.ChannelName))
			{
				requestBody.UniqueTrackingId = string.Format(CultureInfo.CurrentCulture, "{0};{1}", new object[]
				{
					requestBody.ClientReqId,
					DiagOperation.ChannelName
				});
				DiagOperationState orAddOperationState = VelocityDiagnostics.GetOrAddOperationState(requestBody.UniqueTrackingId);
				if (orAddOperationState != null)
				{
					orAddOperationState.UniqueIdentifier = requestBody.UniqueTrackingId;
					orAddOperationState.AddEvent(new DiagEvent(DiagEventName.RequestCreated));
					orAddOperationState.Merge(DiagOperation.OpState);
					requestBody.RequestStates = orAddOperationState;
				}
			}
			IEnumerable<VelocityPacketProperty> requestedProperties = packet.PropertyBag.GetRequestedProperties();
			requestBody.ClientRequestTracking = requestedProperties.Contains(VelocityPacketProperty.MessageGatewayTracker);
			requestBody.PrimaryRequestTracking = requestedProperties.Contains(VelocityPacketProperty.MessagePrimaryTracker);
			return requestBody;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002AA62 File Offset: 0x00028C62
		private static byte[] ConvertStateToByteArray(byte[][] array)
		{
			return Utility.ConvertToByteArray(array);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002AA6A File Offset: 0x00028C6A
		private static byte[][] GetStateFromByteArray(byte[] array)
		{
			return Utility.GetChunkedArray(array, 0);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002AA74 File Offset: 0x00028C74
		public static uint? GetTtl(TimeSpan timespan)
		{
			if (timespan == TimeSpan.Zero)
			{
				return null;
			}
			if (timespan == TimeSpan.MaxValue)
			{
				return new uint?(0U);
			}
			double num = Math.Round(timespan.TotalSeconds);
			if (num >= 0.0 && num <= 4294967295.0)
			{
				return new uint?((uint)num);
			}
			return new uint?(0U);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00024B98 File Offset: 0x00022D98
		public static TimeSpan GetTimeSpan(uint? seconds)
		{
			if (seconds == null)
			{
				return TimeSpan.Zero;
			}
			if (seconds.Value == 0U)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromSeconds(seconds.Value);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		private static GetByTagsOperation GetByTagsOperationType(VelocityPacketType type)
		{
			switch (type)
			{
			case VelocityPacketType.GetBatchByNone:
				return GetByTagsOperation.ByNone;
			case VelocityPacketType.GetBatchByIntersection:
				return GetByTagsOperation.ByIntersection;
			case VelocityPacketType.GetBatchByUnion:
				return GetByTagsOperation.ByUnion;
			default:
				return GetByTagsOperation.ByNone;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002AB10 File Offset: 0x00028D10
		internal static ReqType GetRequestType(VelocityPacketType velocityPacketType)
		{
			switch (velocityPacketType)
			{
			case VelocityPacketType.GetProperties:
				return ReqType.SOCKET_GET_PROPERTIES;
			case VelocityPacketType.Get:
				return ReqType.GET;
			case VelocityPacketType.GetIfNewer:
				return ReqType.GET_IF_NEWER;
			case VelocityPacketType.GetCacheItem:
				return ReqType.GET_CACHE_ITEM;
			case VelocityPacketType.Put:
				return ReqType.PUT;
			case VelocityPacketType.Add:
				return ReqType.ADD;
			case VelocityPacketType.Replace:
				return ReqType.REPLACE;
			case VelocityPacketType.Increment:
				return ReqType.INCREMENT;
			case VelocityPacketType.Append:
				return ReqType.APPEND;
			case VelocityPacketType.Prepend:
				return ReqType.PREPEND;
			case VelocityPacketType.ContainsKey:
				return ReqType.CONTAINSKEY;
			case VelocityPacketType.Remove:
				return ReqType.REMOVE;
			case VelocityPacketType.ResetTimeout:
				return ReqType.RESET_TIMEOUT;
			case VelocityPacketType.GetAndLock:
			case VelocityPacketType.GetAndLockForce:
				return ReqType.GET_AND_LOCK;
			case VelocityPacketType.PutAndUnlock:
				return ReqType.PUT_AND_UNLOCK;
			case VelocityPacketType.Unlock:
				return ReqType.UNLOCK;
			case VelocityPacketType.LockedRemove:
				return ReqType.LOCKED_REMOVE;
			case VelocityPacketType.CreateRegion:
				return ReqType.CREATE_REGION;
			case VelocityPacketType.RemoveRegion:
				return ReqType.REMOVE_REGION;
			case VelocityPacketType.ClearRegion:
				return ReqType.CLEAR_REGION;
			case VelocityPacketType.GetBatchByNone:
			case VelocityPacketType.GetBatchByIntersection:
			case VelocityPacketType.GetBatchByUnion:
				return ReqType.GET_NEXT_BATCH;
			case VelocityPacketType.Clear:
				return ReqType.CLEAR_CACHE;
			case VelocityPacketType.ClearPartition:
				return ReqType.PARTITION_CLEAR_CACHE;
			case VelocityPacketType.Notification:
				return ReqType.NOTIFICATION_REQ;
			case VelocityPacketType.NotificationLsn:
				return ReqType.NOTIFICATION_LSN_REQ;
			default:
				switch (velocityPacketType)
				{
				case VelocityPacketType.Memcache_CacheBulkGet:
					return ReqType.CACHE_BULK_GET;
				case VelocityPacketType.Memcache_Decrement:
					return ReqType.DECREMENT;
				case VelocityPacketType.Memcache_Noop:
					return ReqType.PING_SERVICE;
				case VelocityPacketType.Memcache_Stat:
					return ReqType.MEMCACHE_GET_STATS;
				default:
					return ReqType.SOCKET_INVALID_REQUEST_TYPE;
				}
				break;
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002AC10 File Offset: 0x00028E10
		internal static void InitializeVwPacketFromRequestBody(RequestBody request, IVelocityPacket packet)
		{
			packet.CacheName = request.CacheName;
			packet.IsMemcacheProtocol = request.SerializationCategory == SerializationCategory.Memcache;
			ReqType req = request.Req;
			switch (req)
			{
			case ReqType.ADD:
				packet.MessageType = VelocityPacketType.Add;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.Value = request.Value;
				packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
				if (request.Tags != null && request.Tags.Length != 0)
				{
					packet.PropertyBag.SetTags(request.Tags);
					return;
				}
				return;
			case ReqType.PUT:
				packet.MessageType = VelocityPacketType.Put;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.Value = request.Value;
				packet.Version = new DataCacheItemVersion(request.Version);
				packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
				if (request.Tags != null && request.Tags.Length != 0)
				{
					packet.PropertyBag.SetTags(request.Tags);
					return;
				}
				return;
			case ReqType.GET:
				packet.MessageType = VelocityPacketType.Get;
				packet.Key = request.Key.StringValue;
				packet.Region = request.RegionName;
				return;
			case ReqType.GET_CACHE_ITEM:
				packet.MessageType = VelocityPacketType.GetCacheItem;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				return;
			case ReqType.GET_ALL:
			case ReqType.BULK_GET:
			case ReqType.GET_NODE_STATS:
			case ReqType.GET_DETAILED_CACHE_NODE_STATS:
			case ReqType.GET_DETAILED_NAMED_CACHE_STATS:
			case ReqType.GET_NAMED_CACHE_STATS:
			case ReqType.GET_NAMED_CACHES:
			case ReqType.GET_REGION_STATS:
			case ReqType.GET_REGIONS:
			case ReqType.CACHE_CONFIG_CHANGED:
			case ReqType.ADVANCED_CONFIG_CHANGED:
			case ReqType.HOSTS_CHANGED:
			case ReqType.RUN_GC:
			case ReqType.GET_LPM:
				break;
			case ReqType.GET_AND_LOCK:
				packet.MessageType = (request.GetAndLockFlag ? VelocityPacketType.GetAndLockForce : VelocityPacketType.GetAndLock);
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
				return;
			case ReqType.GET_NEXT_BATCH:
				packet.MessageType = VelocityWireProtocol.GetNextBatchMessageType(request.GetByTagsOp);
				if (request.EnumState != null)
				{
					packet.PropertyBag.SetElement(VelocityPacketProperty.EnumState, Utility.ConvertToByteArray(request.EnumState));
					return;
				}
				if (request.Tags != null && request.Tags.Length != 0)
				{
					packet.PropertyBag.SetTags(request.Tags);
					return;
				}
				return;
			case ReqType.GET_IF_NEWER:
				packet.Key = request.Key.StringValue;
				packet.Region = request.RegionName;
				packet.MessageType = VelocityPacketType.GetIfNewer;
				packet.Version = new DataCacheItemVersion(request.Version);
				return;
			case ReqType.RESET_TIMEOUT:
				packet.MessageType = VelocityPacketType.ResetTimeout;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
				return;
			case ReqType.PUT_AND_UNLOCK:
				packet.MessageType = VelocityPacketType.PutAndUnlock;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
				packet.LockHandle = request.LockObject;
				packet.Value = request.Value;
				if (request.Tags != null && request.Tags.Length != 0)
				{
					packet.PropertyBag.SetTags(request.Tags);
					return;
				}
				return;
			case ReqType.UNLOCK:
				packet.MessageType = VelocityPacketType.Unlock;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.LockHandle = request.LockObject;
				packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
				return;
			case ReqType.REMOVE:
				packet.MessageType = VelocityPacketType.Remove;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.Version = new DataCacheItemVersion(request.Version);
				return;
			case ReqType.CREATE_REGION:
				packet.MessageType = VelocityPacketType.CreateRegion;
				packet.Region = request.RegionName;
				return;
			case ReqType.REMOVE_REGION:
				packet.MessageType = VelocityPacketType.RemoveRegion;
				packet.Region = request.RegionName;
				return;
			case ReqType.CLEAR_REGION:
				packet.MessageType = VelocityPacketType.ClearRegion;
				packet.Region = request.RegionName;
				return;
			case ReqType.LOCKED_REMOVE:
				packet.MessageType = VelocityPacketType.LockedRemove;
				packet.Region = request.RegionName;
				packet.Key = request.Key.StringValue;
				packet.LockHandle = request.LockObject;
				return;
			case ReqType.NOTIFICATION_REQ:
				packet.MessageType = VelocityPacketType.Notification;
				packet.PropertyBag.SetNotificationRequest(VelocityPacketProperty.NotificationRequest, request.ValObject as NotificationRequest);
				return;
			case ReqType.NOTIFICATION_LSN_REQ:
				packet.MessageType = VelocityPacketType.NotificationLsn;
				packet.PropertyBag.SetNotificationRequest(VelocityPacketProperty.NotificationLsnRequest, request.ValObject as NotificationRequest);
				return;
			case ReqType.PING_SERVICE:
				packet.MessageType = VelocityPacketType.Memcache_Noop;
				throw new InvalidOperationException("No Ping message forwarding.");
			default:
				switch (req)
				{
				case ReqType.CLEAR_CACHE:
					throw new InvalidOperationException("Clear cache can't be forwarded");
				case ReqType.PARTITION_CLEAR_CACHE:
					packet.MessageType = VelocityPacketType.ClearPartition;
					packet.PartitionKey = request.PartitionId;
					return;
				case ReqType.CACHE_BULK_GET:
					throw new InvalidOperationException("No Cache Bulk request expected");
				case (ReqType)48:
					break;
				case ReqType.INCREMENT:
					packet.MessageType = VelocityPacketType.Increment;
					packet.Region = request.RegionName;
					packet.Key = request.Key.StringValue;
					packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
					packet.Value = request.Value;
					packet.Version = new DataCacheItemVersion(request.Version);
					packet.PropertyBag.SetProperty(VelocityPacketProperty.InitialValue, request.InitialValue, new Converter<byte[][], byte[]>(Utility.ConvertToByteArray));
					return;
				case ReqType.DECREMENT:
					packet.MessageType = VelocityPacketType.Memcache_Decrement;
					packet.Region = request.RegionName;
					packet.Key = request.Key.StringValue;
					packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
					packet.Value = request.Value;
					packet.Version = new DataCacheItemVersion(request.Version);
					packet.PropertyBag.SetProperty(VelocityPacketProperty.InitialValue, request.InitialValue, new Converter<byte[][], byte[]>(Utility.ConvertToByteArray));
					return;
				case ReqType.APPEND:
					packet.MessageType = VelocityPacketType.Append;
					packet.Region = request.RegionName;
					packet.Key = request.Key.StringValue;
					packet.Value = request.Value;
					packet.Version = new DataCacheItemVersion(request.Version);
					packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
					return;
				case ReqType.PREPEND:
					packet.MessageType = VelocityPacketType.Prepend;
					packet.Region = request.RegionName;
					packet.Key = request.Key.StringValue;
					packet.Value = request.Value;
					packet.Version = new DataCacheItemVersion(request.Version);
					packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
					return;
				case ReqType.CONTAINSKEY:
					packet.MessageType = VelocityPacketType.ContainsKey;
					packet.Region = request.RegionName;
					packet.Key = request.Key.StringValue;
					return;
				case ReqType.REPLACE:
					packet.MessageType = VelocityPacketType.Replace;
					packet.Region = request.RegionName;
					packet.Key = request.Key.StringValue;
					packet.Value = request.Value;
					packet.Version = new DataCacheItemVersion(request.Version);
					packet.ExpiryTTL = VelocityWireProtocol.GetTtl(request.TTL);
					if (request.Tags != null && request.Tags.Length != 0)
					{
						packet.PropertyBag.SetTags(request.Tags);
						return;
					}
					return;
				default:
					if (req == ReqType.SOCKET_GET_PROPERTIES)
					{
						throw new InvalidOperationException("Socket Get Properties can't be converted");
					}
					break;
				}
				break;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, " Invalid request type {0}", new object[] { request.Req }));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002B388 File Offset: 0x00029588
		internal static VelocityPacketType GetNextBatchMessageType(GetByTagsOperation opType)
		{
			switch (opType)
			{
			case GetByTagsOperation.ByNone:
				return VelocityPacketType.GetBatchByNone;
			case GetByTagsOperation.ByIntersection:
				return VelocityPacketType.GetBatchByIntersection;
			case GetByTagsOperation.ByUnion:
				return VelocityPacketType.GetBatchByUnion;
			default:
				throw new ArgumentOutOfRangeException("opType");
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002B3C0 File Offset: 0x000295C0
		public IVelocityRequestPacket CreateRequestPacket(VelocityPacketType type)
		{
			VelocityPacket velocityPacket = new VelocityPacket
			{
				MessageType = type,
				MagicByte = 193,
				MessageID = RequestIdGenerator.GetNewRequestId()
			};
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				Guid guid = Guid.NewGuid();
				velocityPacket.PropertyBag.SetMessageTrackingId(guid);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("VelocityWireProtocol.Parsing", "Packet {0} created with request tracking ID: {1}", new object[] { velocityPacket.MessageID, guid });
				}
			}
			return velocityPacket;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002B443 File Offset: 0x00029643
		public IVelocityResponsePacket CreateEmptyResponsePacket()
		{
			return new VelocityPacket();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002B44A File Offset: 0x0002964A
		public IEnumerable<ArraySegment<byte>> GetReadResponseBuffer(IList<IVelocityPacket> packets, IBufferManager bufferManager)
		{
			return VelocityWireProtocol.GetReadPacketBuffer(packets, bufferManager, 209, () => this.CreateEmptyResponsePacket() as VelocityPacket);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002B464 File Offset: 0x00029664
		private static IList<ArraySegment<byte>> GetWritePacketBuffer(VelocityPacket packet, IBufferManager bufferManager, bool ignoreSizeLimit, out IList<AllocationType> allocationType)
		{
			packet.PrepareHeaderForWrite();
			if (!ignoreSizeLimit && (ulong)(packet.PayloadLength + 32U) > (ulong)((long)bufferManager.MaxMessageSize))
			{
				throw new VelocityPacketTooBigException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "SendMessageTooLarge", packet.PayloadLength + 32U, bufferManager.MaxMessageSize));
			}
			return packet.GetPayloadSegments(bufferManager, out allocationType);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002B4C4 File Offset: 0x000296C4
		public IList<ArraySegment<byte>> GetWriteRequestBuffer(IVelocityRequestPacket packet, IBufferManager bufferManager)
		{
			IList<AllocationType> list;
			return VelocityWireProtocol.GetWritePacketBuffer(packet as VelocityPacket, bufferManager, false, out list);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002B4E0 File Offset: 0x000296E0
		public static IList<ArraySegment<byte>> GetWriteRequestBuffer(IVelocityRequestPacket packet, IBufferManager bufferManager, out IList<AllocationType> allocationType)
		{
			return VelocityWireProtocol.GetWritePacketBuffer(packet as VelocityPacket, bufferManager, false, out allocationType);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000189CC File Offset: 0x00016BCC
		public IVelocityRequestPacket GetInitializationPacket(string cacheName)
		{
			return null;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002B4F0 File Offset: 0x000296F0
		public IVelocityResponsePacket CreateResponsePacket(VelocityPacketType type, int packetId)
		{
			return new VelocityPacket
			{
				MessageType = type,
				MagicByte = 209,
				MessageID = packetId
			};
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002B51D File Offset: 0x0002971D
		public IReplyContext CreateReplyContext(ITcpChannel tcpChannel, IList<IVelocityPacket> packets, VelocityPacketException exception)
		{
			return new SocketReplyContext(tcpChannel, packets, exception);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002B443 File Offset: 0x00029643
		public IVelocityRequestPacket CreateEmptyRequestPacket()
		{
			return new VelocityPacket();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002B527 File Offset: 0x00029727
		public IEnumerable<ArraySegment<byte>> GetReadRequestBuffer(IList<IVelocityPacket> packets, IBufferManager bufferManager)
		{
			return VelocityWireProtocol.GetReadPacketBuffer(packets, bufferManager, 193, () => this.CreateEmptyRequestPacket() as VelocityPacket);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002B544 File Offset: 0x00029744
		public IList<ArraySegment<byte>> GetWriteResponseBuffer(IVelocityResponsePacket packet, IBufferManager bufferManager)
		{
			IList<AllocationType> list;
			return VelocityWireProtocol.GetWritePacketBuffer(packet as VelocityPacket, bufferManager, true, out list);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002B560 File Offset: 0x00029760
		internal static IList<ArraySegment<byte>> GetWriteResponseBuffer(IVelocityResponsePacket packet, IBufferManager bufferManager, out IList<AllocationType> allocationType)
		{
			return VelocityWireProtocol.GetWritePacketBuffer(packet as VelocityPacket, bufferManager, true, out allocationType);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002B570 File Offset: 0x00029770
		public IList<ArraySegment<byte>> GetWriteResponseBuffer(ICollection<IVelocityResponsePacket> packets, IBufferManager bufferManager)
		{
			IList<AllocationType> list;
			return VelocityWireProtocol.GetWriteResponseBuffer(packets, bufferManager, out list);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002B588 File Offset: 0x00029788
		internal static IList<ArraySegment<byte>> GetWriteResponseBuffer(ICollection<IVelocityResponsePacket> packets, IBufferManager bufferManager, out IList<AllocationType> allocationType)
		{
			if (packets.Count == 1)
			{
				return VelocityWireProtocol.GetWritePacketBuffer(packets.Single<IVelocityResponsePacket>() as VelocityPacket, bufferManager, true, out allocationType);
			}
			IList<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			allocationType = new List<AllocationType>();
			foreach (IVelocityResponsePacket velocityResponsePacket in packets)
			{
				VelocityPacket velocityPacket = (VelocityPacket)velocityResponsePacket;
				IList<AllocationType> list2;
				IList<ArraySegment<byte>> writePacketBuffer = VelocityWireProtocol.GetWritePacketBuffer(velocityPacket, bufferManager, true, out list2);
				for (int i = 0; i < writePacketBuffer.Count; i++)
				{
					list.Add(writePacketBuffer[i]);
					allocationType.Add(list2[i]);
				}
			}
			return list;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002B63C File Offset: 0x0002983C
		internal static void TraceBytesOnVerbose(object prepend, byte[] bytes, int offset, int length, object append)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<object, string, object>("VelocityWireProtocol.Parsing", "{0}\r\n{1}\r\n{2}", prepend, Utility.PrintBytes(bytes, offset, length), append);
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002B660 File Offset: 0x00029860
		internal static void ReleaseMemory(IBufferManager bufferManager, IList<ArraySegment<byte>> message)
		{
			foreach (ArraySegment<byte> arraySegment in message)
			{
				bufferManager.ReleaseBuffer(arraySegment.Array);
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002B6B0 File Offset: 0x000298B0
		internal static void ReleaseMemory(IBufferManager bufferManager, IList<ArraySegment<byte>> message, IList<AllocationType> segmentAllocationType)
		{
			if (segmentAllocationType != null)
			{
				for (int i = 0; i < message.Count; i++)
				{
					if (segmentAllocationType[i] == AllocationType.BufferManager)
					{
						bufferManager.ReleaseBuffer(message[i].Array);
					}
				}
				return;
			}
			VelocityWireProtocol.ReleaseMemory(bufferManager, message);
		}

		// Token: 0x040008F1 RID: 2289
		public const byte ProtocolVersion = 1;

		// Token: 0x040008F2 RID: 2290
		public const byte RequestMagicByte = 192;

		// Token: 0x040008F3 RID: 2291
		public const byte ResponseMagicByte = 208;

		// Token: 0x040008F4 RID: 2292
		private const string _logSource = "VelocityWireProtocol.Parsing";

		// Token: 0x040008F5 RID: 2293
		public static int MaxTagLength = 65535;

		// Token: 0x040008F6 RID: 2294
		public static int MaxKeyLength = 65535;

		// Token: 0x040008F7 RID: 2295
		public static int MaxRegionLength = 65535;
	}
}
