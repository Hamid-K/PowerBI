using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000372 RID: 882
	[DataContract(Name = "RequestBody", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class RequestBody : MessageBody, ICreateMessage
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0005E82E File Offset: 0x0005CA2E
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x0005E836 File Offset: 0x0005CA36
		public DiagOperationState RequestStates
		{
			get
			{
				return this._requestStateBuffer;
			}
			set
			{
				if (value != null)
				{
					this._requestStateBuffer = value;
					this._requestStateBuffer.Type = (int)this.Req;
					this._requestStateBuffer.AddEvent(new DiagEvent(DiagEventName.Started));
				}
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0005E864 File Offset: 0x0005CA64
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x0005E86C File Offset: 0x0005CA6C
		public string Action { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0005E875 File Offset: 0x0005CA75
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0005E87D File Offset: 0x0005CA7D
		internal long TraceId
		{
			get
			{
				return this._traceId;
			}
			set
			{
				this._traceId = value;
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0005E886 File Offset: 0x0005CA86
		public RequestBody()
		{
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0005E8B1 File Offset: 0x0005CAB1
		internal RequestBody(IVelocityRequestPacket packet)
			: this(ReqType.SOCKET_HACK, packet.MessageID, false)
		{
			this.Packet = packet;
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x0005E8CC File Offset: 0x0005CACC
		internal bool IsSocketHack
		{
			get
			{
				return this.Req == ReqType.SOCKET_HACK && this.Packet != null;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0005E8E9 File Offset: 0x0005CAE9
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x0005E8F1 File Offset: 0x0005CAF1
		internal IVelocityRequestPacket Packet { get; private set; }

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0005E8FA File Offset: 0x0005CAFA
		public RequestBody(ReqType reqType)
			: this(reqType, RequestIdGenerator.GetNewRequestId())
		{
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0005E908 File Offset: 0x0005CB08
		public RequestBody(ReqType reqType, int requestId)
			: this(reqType, requestId, Provider.IsEnabled(TraceLevel.Error))
		{
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0005E918 File Offset: 0x0005CB18
		public RequestBody(ReqType reqType, bool isRequestTrackingEnabled)
			: this(reqType, RequestIdGenerator.GetNewRequestId(), isRequestTrackingEnabled)
		{
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0005E928 File Offset: 0x0005CB28
		internal RequestBody(ReqType reqType, int requestId, bool isRequestTrackingEnabled)
		{
			this.ClientReqId = requestId;
			this.Req = reqType;
			if (isRequestTrackingEnabled)
			{
				this.RequestTrackingId = Guid.NewGuid();
				this.IsTrackingIdPresent = true;
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0005E981 File Offset: 0x0005CB81
		internal void AssignRequestId()
		{
			this.ClientReqId = RequestIdGenerator.GetNewRequestId();
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0005E990 File Offset: 0x0005CB90
		public Message CreateWcfMessage(ClientVersionInfo versionInfo)
		{
			if (this.ValObject != null && this.Value == null)
			{
				if (Utility.RemoteEndpointUsesNetDataConractSerializer(versionInfo))
				{
					this.Value = SerializationUtility.NetDataContractSerialize(this.ValObject);
				}
				else
				{
					this.Value = SerializationUtility.DataContractSerialize(this.ValObject);
				}
			}
			Message message = Utility.CreateMessage(this.Action, this);
			if (this.IsTrackingIdPresent)
			{
				message.Headers.Add(MessageHeader.CreateHeader("TrackingId", "urn:AppFabricCaching", this.RequestTrackingId));
			}
			return message;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0005EA14 File Offset: 0x0005CC14
		public IVelocityRequestPacket GetRequestPacket()
		{
			VelocityPacket velocityPacket = new VelocityPacket();
			velocityPacket.MessageID = base.ID;
			velocityPacket.MagicByte = 193;
			VelocityWireProtocol.InitializeVwPacketFromRequestBody(this, velocityPacket);
			return velocityPacket;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0005EA48 File Offset: 0x0005CC48
		internal static RequestBody GetRequest(Message message)
		{
			IDictionary<string, string> dictionary;
			return RequestBody.GetRequest(message, false, out dictionary);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0005EA5E File Offset: 0x0005CC5E
		internal static RequestBody GetRequest(Message message, out IDictionary<string, string> headers)
		{
			return RequestBody.GetRequest(message, true, out headers);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0005EA68 File Offset: 0x0005CC68
		private static RequestBody GetRequest(Message message, bool extractHeaders, out IDictionary<string, string> headers)
		{
			RequestBody requestBody = null;
			headers = null;
			try
			{
				requestBody = message.GetBody<RequestBody>();
				if (requestBody.ServiceReqId == -1)
				{
					requestBody.TraceId = Utility.ConvertToLong(Interlocked.Increment(ref RequestBody.serviceTraceCounter), requestBody.ClientReqId);
				}
				requestBody.StartTime = DateTime.UtcNow;
				if (extractHeaders)
				{
					headers = requestBody.ExtractHeaders(message);
					requestBody.ProcessHeaders(headers);
				}
				if (requestBody.IsNotificationRequest())
				{
					requestBody.ValObject = SerializationUtility.Deserialize(requestBody.Value, true);
				}
				if (requestBody.HasPartitionId())
				{
					requestBody.PartitionId = SerializationUtility.Deserialize(requestBody.Value, true) as PartitionId;
				}
			}
			catch (SerializationException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError("DistributedCache.RequestBody", "Invalid message body. - {0}", new object[] { ex });
				}
			}
			finally
			{
				message.Close();
			}
			return requestBody;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0005EB4C File Offset: 0x0005CD4C
		private bool HasPartitionId()
		{
			return this.Req == ReqType.PARTITION_CLEAR_CACHE;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0005EB58 File Offset: 0x0005CD58
		private void ProcessHeaders(IDictionary<string, string> headers)
		{
			string text;
			if (this._flags.TrackingIDPresenceFlag && headers.TryGetValue("TrackingId", out text))
			{
				this.FillRequestTrackingId(text);
			}
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0005EB88 File Offset: 0x0005CD88
		private void FillRequestTrackingId(string requestTrackingIdString)
		{
			this.RequestTrackingId = new Guid(requestTrackingIdString);
			this.UniqueTrackingId = this.RequestTrackingId.ToString();
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0005EBB0 File Offset: 0x0005CDB0
		private Dictionary<string, string> ExtractHeaders(Message message)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text = Utility.GetMessageHeader<string>(message, "Authorization", "urn:AppFabricCaching");
			if (!string.IsNullOrEmpty(text))
			{
				dictionary.Add("Authorization", text);
			}
			Uri uri = message.Headers.To;
			if (ConfigManager.CurrentClusterDeploymentMode == DataCacheDeploymentMode.DIPClient)
			{
				text = Utility.GetMessageHeader<string>(message, "VipForDip", "urn:AppFabricCaching");
				if (!string.IsNullOrEmpty(text))
				{
					uri = new Uri(text);
				}
			}
			if (uri != null)
			{
				dictionary["To"] = uri.ToString();
			}
			if (this._flags.TrackingIDPresenceFlag)
			{
				dictionary.Add("TrackingId", Utility.GetMessageHeader<Guid>(message, "TrackingId", "urn:AppFabricCaching").ToString());
			}
			return dictionary;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0005EC70 File Offset: 0x0005CE70
		internal bool IsReadRequest()
		{
			if (this.Req == ReqType.SOCKET_HACK)
			{
				return this.Packet.MessageType == VelocityPacketType.Get || this.Packet.MessageType == VelocityPacketType.GetAndLock || this.Packet.MessageType == VelocityPacketType.GetCacheItem || this.Packet.MessageType == VelocityPacketType.GetIfNewer;
			}
			return this.Req == ReqType.GET || this.Req == ReqType.CONTAINSKEY || this.Req == ReqType.GET_CACHE_ITEM || this.Req == ReqType.GET_IF_NEWER || this.Req == ReqType.GET_NEXT_BATCH || this.Req == ReqType.BULK_GET || this.Req == ReqType.PING_SERVICE || this.IsAdminRequest() || this.IsNotificationRequest() || this.IsNamedCachePropertiesRequest();
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0005ED20 File Offset: 0x0005CF20
		internal bool IsMemorySafeWriteRequest()
		{
			return this.Req == ReqType.REMOVE || this.Req == ReqType.REMOVE_REGION || this.Req == ReqType.CLEAR_REGION || this.Req == ReqType.GET_AND_LOCK || this.Req == ReqType.RESET_TIMEOUT || this.Req == ReqType.UNLOCK || this.Req == ReqType.LOCKED_REMOVE || this.Req == ReqType.RUN_GC || this.Req == ReqType.DELETE_NAMED_CACHE || this.Req == ReqType.PARTITION_CLEAR_CACHE;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0005ED94 File Offset: 0x0005CF94
		internal bool IsWriteRequest()
		{
			if (this.Req == ReqType.SOCKET_HACK)
			{
				return this.Packet.MessageType == VelocityPacketType.Put || this.Packet.MessageType == VelocityPacketType.PutAndUnlock || this.Packet.MessageType == VelocityPacketType.Append || this.Packet.MessageType == VelocityPacketType.Prepend || this.Packet.MessageType == VelocityPacketType.Add || this.Packet.MessageType == VelocityPacketType.Replace || this.Packet.MessageType == VelocityPacketType.Increment;
			}
			return !this.IsReadRequest();
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0005EE1F File Offset: 0x0005D01F
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x0005EE2C File Offset: 0x0005D02C
		internal bool IsNewVersionRequired
		{
			get
			{
				return this._flags.IsNewVersionRequired;
			}
			set
			{
				this._flags.IsNewVersionRequired = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0005EE3A File Offset: 0x0005D03A
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x0005EE47 File Offset: 0x0005D047
		internal bool PrimaryRequestTracking
		{
			get
			{
				return this._flags.PrimaryRequestTrackingFlag;
			}
			set
			{
				this._flags.PrimaryRequestTrackingFlag = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0005EE55 File Offset: 0x0005D055
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x0005EE62 File Offset: 0x0005D062
		internal bool ClientRequestTracking
		{
			get
			{
				return this._flags.ClientRequestTrackingFlag;
			}
			set
			{
				this._flags.ClientRequestTrackingFlag = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0005EE70 File Offset: 0x0005D070
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x0005EE7D File Offset: 0x0005D07D
		internal bool IsTrackingIdPresent
		{
			get
			{
				return this._flags.TrackingIDPresenceFlag;
			}
			set
			{
				this._flags.TrackingIDPresenceFlag = value;
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0005EE8B File Offset: 0x0005D08B
		internal bool IsRegionCreationRequest()
		{
			return this.Req == ReqType.CREATE_REGION;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0005EE98 File Offset: 0x0005D098
		internal void FillPrimaryStateInfo()
		{
			this.RequestStates.UniqueIdentifier = this.UniqueTrackingId;
			this.RequestStates.BaseRawData = string.Format(CultureInfo.InvariantCulture, ";ReqType:{0}", new object[] { this.Req });
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0005EEE6 File Offset: 0x0005D0E6
		internal void FillPrimaryStateInfoExtended(ResponseBody resp)
		{
			this.FillPrimaryStateInfo();
			this.RequestStates.BaseRawData = this.ToString();
			this.RequestStates.ErrorCode = (int)resp.ResponseCode;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0005EF10 File Offset: 0x0005D110
		internal bool IsBulkGetRequest()
		{
			return this.Req == ReqType.BULK_GET || this.Req == ReqType.CACHE_BULK_GET;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0005EF28 File Offset: 0x0005D128
		internal bool IsSimpleRequest()
		{
			return this.Req != ReqType.CLEAR_CACHE && this.Req != ReqType.CACHE_BULK_GET;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0005EF44 File Offset: 0x0005D144
		internal bool IsAdminRequest()
		{
			switch (this.Req)
			{
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
			case ReqType.SHUTDOWN:
			case ReqType.SHUTDOWN_STATUS:
			case ReqType.CANCEL_SHUTDOWN:
				return true;
			}
			return false;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0005EFC6 File Offset: 0x0005D1C6
		internal bool IsCommitRequired()
		{
			return !this.IsRegionLevelRequest() && !this.IsNotificationRequest() && this.Req != ReqType.COMPACT_PARTITION && this.Req != ReqType.EVICT_BATCH && this.Req != ReqType.WRITE_BEHIND_CHECKPOINT && this.Req != ReqType.PARTITION_CLEAR_CACHE;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0005F005 File Offset: 0x0005D205
		internal bool IsRegionLevelRequest()
		{
			return this.Req == ReqType.CREATE_REGION || this.Req == ReqType.CLEAR_REGION || this.Req == ReqType.REMOVE_REGION || this.Req == ReqType.BULK_GET || this.Req == ReqType.GET_NEXT_BATCH;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0005F03A File Offset: 0x0005D23A
		internal bool IsCacheItemLevelRequest()
		{
			return !this.IsRegionLevelRequest() && !this.IsAdminRequest() && !this.IsNamedCachePropertiesRequest() && this.Req != ReqType.PING_SERVICE && this.Req != ReqType.CLEAR_CACHE && this.Req != ReqType.CACHE_BULK_GET;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0005F077 File Offset: 0x0005D277
		internal bool IsNotificationRequest()
		{
			return RequestBody.IsNotificationRequest(this.Req);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0005F084 File Offset: 0x0005D284
		internal static bool IsNotificationRequest(ReqType req)
		{
			return req == ReqType.NOTIFICATION_REQ || req == ReqType.NOTIFICATION_LSN_REQ;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0005F092 File Offset: 0x0005D292
		internal bool IsNamedCachePropertiesRequest()
		{
			return this.Req == ReqType.GET_NAMED_CACHE_CONFIGURATION || this.Req == ReqType.GET_NAMED_CACHE_CONFIGURATION_VERSION_MATCH || this.Req == ReqType.SOCKET_GET_PROPERTIES;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0005F0B7 File Offset: 0x0005D2B7
		internal bool IsExplicitRoutedRequest()
		{
			return this.IsNotificationRequest() || this.IsNamedCachePropertiesRequest();
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0005F0CC File Offset: 0x0005D2CC
		internal bool IsExpirableItemRequestType()
		{
			ReqType req = this.Req;
			switch (req)
			{
			case ReqType.ADD:
			case ReqType.PUT:
			case ReqType.GET_AND_LOCK:
			case ReqType.RESET_TIMEOUT:
			case ReqType.PUT_AND_UNLOCK:
			case ReqType.UNLOCK:
				break;
			case ReqType.GET:
			case ReqType.GET_CACHE_ITEM:
			case ReqType.GET_ALL:
			case ReqType.GET_NEXT_BATCH:
			case ReqType.GET_IF_NEWER:
				return false;
			default:
				if (req != ReqType.REPLACE)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0005F11C File Offset: 0x0005D31C
		internal ResponseBody GetErrorResponse(ErrStatus error)
		{
			ResponseBody responseBody = new ResponseBody(AckNack.Nack);
			responseBody.ResponseCode = error;
			Utility.AddInfo(this, responseBody);
			return responseBody;
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0005F140 File Offset: 0x0005D340
		internal ResponseBody GetErrorResponse(ErrStatus error, Exception e)
		{
			ResponseBody errorResponse = this.GetErrorResponse(error);
			errorResponse.Exception = e;
			return errorResponse;
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0005F160 File Offset: 0x0005D360
		internal ResponseBody GetPendingResponse()
		{
			ResponseBody responseBody = new ResponseBody(AckNack.Pending);
			Utility.AddInfo(this, responseBody);
			return responseBody;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0005F17C File Offset: 0x0005D37C
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x0005F184 File Offset: 0x0005D384
		[DataMember]
		public PartitionId PartitionId { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0005F18D File Offset: 0x0005D38D
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0005F195 File Offset: 0x0005D395
		[DataMember]
		internal string RegionName
		{
			get
			{
				return this._regionName;
			}
			set
			{
				this._regionName = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0005F19E File Offset: 0x0005D39E
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0005F1A6 File Offset: 0x0005D3A6
		public int SystemRegionCount
		{
			get
			{
				return this._systemRegionCount;
			}
			set
			{
				this._systemRegionCount = value;
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0005F1AF File Offset: 0x0005D3AF
		internal void SetRegionName(string regionName, int systemRegionCount)
		{
			this._regionName = regionName;
			this._systemRegionCount = systemRegionCount;
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0005F1BF File Offset: 0x0005D3BF
		internal void SetRegionName(string regionName)
		{
			this.SetRegionName(regionName, int.MaxValue);
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0005F21A File Offset: 0x0005D41A
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0005F1D0 File Offset: 0x0005D3D0
		internal DataCacheLockHandle LockObject
		{
			get
			{
				return new DataCacheLockHandle(this._DMLockHandle, this.Version);
			}
			set
			{
				if (value != null)
				{
					this._DMLockHandle = value.Handle;
					if (!this._DMLockHandle.IsValid || value.Version.CompareTo(InternalCacheItemVersion.Null) != 0)
					{
						this.Version = value.Version;
					}
				}
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0005F22D File Offset: 0x0005D42D
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x0005F23A File Offset: 0x0005D43A
		public bool NewElement
		{
			get
			{
				return this._flags.NewElementFlag;
			}
			set
			{
				this._flags.NewElementFlag = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0005F248 File Offset: 0x0005D448
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x0005F255 File Offset: 0x0005D455
		internal bool GetAndLockFlag
		{
			get
			{
				return this._flags.GetAndLockFlag;
			}
			set
			{
				this._flags.GetAndLockFlag = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0005F263 File Offset: 0x0005D463
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x0005F270 File Offset: 0x0005D470
		internal bool ReadThroughAddFlag
		{
			get
			{
				return this._flags.ReadThroughAddFlag;
			}
			set
			{
				this._flags.ReadThroughAddFlag = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0005F27E File Offset: 0x0005D47E
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x0005F28B File Offset: 0x0005D48B
		internal bool ReadThroughAttemptedFlag
		{
			get
			{
				return this._flags.ReadThroughAttemptedFlag;
			}
			set
			{
				this._flags.ReadThroughAttemptedFlag = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0005F299 File Offset: 0x0005D499
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x0005F2A1 File Offset: 0x0005D4A1
		internal RequestBodyFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0005F2AA File Offset: 0x0005D4AA
		public override int GetHashCode()
		{
			return base.ID;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
		public override string ToString()
		{
			if (this.Req == ReqType.SOCKET_HACK && this.Packet != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "VW;Client: '{0}' {1};{2};{3};{4};{5}", new object[]
				{
					this.Packet.MessageID,
					this.Packet.MessageType,
					this.Packet.CacheName,
					this.Packet.Region,
					this.Packet.Key,
					(this.Packet.Version != null) ? this.Packet.Version.InternalVersion : InternalCacheItemVersion.Null
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "'{0}:{1}' {2};{3};{4};{5};{6};{7};{8};{9}", new object[]
			{
				this.ClientReqId,
				this.ServiceReqId,
				this.Req,
				this.Fwd,
				this.CacheName,
				this._regionName,
				(this._regionName != null) ? this.RegionID.ToString(CultureInfo.InvariantCulture) : string.Empty,
				this.Key,
				this.Version,
				this.RequestTrackingId
			});
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0005F423 File Offset: 0x0005D623
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x0005F42B File Offset: 0x0005D62B
		internal DRM Router
		{
			get
			{
				return this._drm;
			}
			set
			{
				this._drm = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0005F43D File Offset: 0x0005D63D
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x0005F434 File Offset: 0x0005D634
		internal ReplicaSetMap Table
		{
			get
			{
				return this._table;
			}
			set
			{
				this._table = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0005F445 File Offset: 0x0005D645
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x0005F44D File Offset: 0x0005D64D
		internal LocalDRMCallback DRMCallback { get; set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0005F456 File Offset: 0x0005D656
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x0005F45E File Offset: 0x0005D65E
		internal ServiceCallback Caller
		{
			get
			{
				return this._callback;
			}
			set
			{
				this._callback = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0005F467 File Offset: 0x0005D667
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x0005F46F File Offset: 0x0005D66F
		internal bool InTransit
		{
			get
			{
				return this._inTransit;
			}
			set
			{
				this._inTransit = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0005F488 File Offset: 0x0005D688
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0005F478 File Offset: 0x0005D678
		internal EndpointID Destination
		{
			get
			{
				return this._address;
			}
			set
			{
				this._inTransit = false;
				this._address = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0005F490 File Offset: 0x0005D690
		internal ServiceReplicaSet Config
		{
			get
			{
				return this._config;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0005F498 File Offset: 0x0005D698
		internal int RegionID
		{
			get
			{
				return Utility.GetRegionId(this._regionName, this._systemRegionCount);
			}
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0005F4AC File Offset: 0x0005D6AC
		internal bool CanFindDestination()
		{
			bool flag;
			switch (this.Fwd)
			{
			case ForwardingType.Routable:
				flag = !string.IsNullOrEmpty(this._regionName) || this.PartitionId != null;
				break;
			case ForwardingType.Routed:
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			return flag;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		internal bool IsSameDestination(EndpointID endpoint)
		{
			EndpointID address = this._address;
			return address != null && endpoint.Equals(address);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0005F518 File Offset: 0x0005D718
		private EndpointID GetAddress(ServiceReplicaSet config)
		{
			string text = this._drm.PrimaryEndPointString(config);
			if (text != null)
			{
				return this._drm.GetEndpointID(text);
			}
			return null;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0005F544 File Offset: 0x0005D744
		internal void FindDestinations()
		{
			if (!string.IsNullOrEmpty(this.RegionName))
			{
				this._config = this._drm.GetPartitionConfig(this._table, this.CacheName, this.RegionID, ref this._error);
				this.Destination = ((this._config != null) ? this.GetAddress(this._config) : null);
			}
			else
			{
				this.Destination = this._drm.GetPrimaryEndPoint(this.PartitionId);
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody, EndpointID>(RequestBody.LogSource, "{0} - Destination - {1}.", this, this._address);
			}
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0005F5DC File Offset: 0x0005D7DC
		internal void Send()
		{
			if (this._address == null)
			{
				this._drm.ProcessResponse(this, this.GetErrorResponse(this._error));
				return;
			}
			if (this._drm.IsLocalRequest(this._address))
			{
				this.IsFromLocalDRM = true;
				this.DRMCallback = this._drm.DrmCallback;
				ResponseBody responseBody = this._drm.LocalDomCallback(this, this.Session);
				if (responseBody.Ack != AckNack.Pending)
				{
					this._drm.ProcessResponse(this, responseBody, () => this.Session);
					return;
				}
			}
			else
			{
				OperationResult operationResult = this._drm.SendRequest(this._address, this);
				if (!operationResult.IsSuccess)
				{
					ErrStatus errStatus = RequestBody.GetErrStatus(operationResult);
					this._drm.ProcessResponse(this, this.GetErrorResponse(errStatus, operationResult.Fault));
				}
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0005F6B2 File Offset: 0x0005D8B2
		private static ErrStatus GetErrStatus(OperationResult result)
		{
			if (result.Status == OperationStatus.MessageLargerThanConfiguredSize)
			{
				return ErrStatus.MESSAGE_LARGER_THAN_CONFIGURED;
			}
			if (result.HasChannelAuthenticationFailed)
			{
				return ErrStatus.CHANNEL_AUTH_FAILED;
			}
			if (result.HasCertificateRevocationCheckFailed)
			{
				return ErrStatus.CHANNEL_AUTH_CRL_OFFLINE;
			}
			if (result.HasVerificationFailed)
			{
				return ErrStatus.CLIENT_SERVER_VERSION_MISMATCH;
			}
			return ErrStatus.SERVER_DEAD;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0005F6E3 File Offset: 0x0005D8E3
		internal ProcessResponseResult ProcessResponse(ResponseBody response)
		{
			if (this.IsRetryRequired(response))
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<RequestBody, ResponseBody>(RequestBody.LogSource, "{0} - Retry required on response - {1}.", this, response);
				}
				return ProcessResponseResult.RetryRequired;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody, ResponseBody>(RequestBody.LogSource, "{0} - Request completed on response - {1}.", this, response);
			}
			return ProcessResponseResult.RequestFullyCompleted;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0005F724 File Offset: 0x0005D924
		private bool IsRetryRequired(ResponseBody response)
		{
			return (response.IsNotPrimaryResponse() || (response.ResponseCode == ErrStatus.SERVER_DEAD && this.Fwd == ForwardingType.Routable)) && this._counter++ < RequestBody.NumRetries;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00008948 File Offset: 0x00006B48
		internal RequestBody GetChildRequest()
		{
			return this;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0005F765 File Offset: 0x0005D965
		internal ResponseBody GetPartialResponse()
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody>(RequestBody.LogSource, "{0} - Returning partial response.", this);
			}
			return this.GetErrorResponse(ErrStatus.TIMEOUT);
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0005F786 File Offset: 0x0005D986
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0005F78E File Offset: 0x0005D98E
		internal bool IsFromLocalDRM { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0005F798 File Offset: 0x0005D998
		internal int Size
		{
			get
			{
				int num = 0;
				if (this.Key != null)
				{
					num += this.Key.Length;
				}
				if (this.Keys != null)
				{
					foreach (Key key in this.Keys)
					{
						num += key.Length;
					}
				}
				num += Utility.Get2DByteArraySize(this.Value);
				num += (string.IsNullOrEmpty(this.CacheName) ? 0 : this.CacheName.Length);
				num += (string.IsNullOrEmpty(this.RegionName) ? 0 : this.RegionName.Length);
				if (this.Tags != null)
				{
					foreach (DataCacheTag dataCacheTag in this.Tags)
					{
						num += dataCacheTag.Length;
					}
				}
				return num;
			}
		}

		// Token: 0x040011CB RID: 4555
		private const ushort TypeVersion = 1;

		// Token: 0x040011CC RID: 4556
		private static int serviceTraceCounter;

		// Token: 0x040011CD RID: 4557
		private DiagOperationState _requestStateBuffer;

		// Token: 0x040011CE RID: 4558
		public DateTime StartTime;

		// Token: 0x040011CF RID: 4559
		public bool IsKeyOnly;

		// Token: 0x040011D0 RID: 4560
		private long _traceId;

		// Token: 0x040011D1 RID: 4561
		private static string LogSource = "DistributedCache.RequestBody";

		// Token: 0x040011D2 RID: 4562
		[DataMember]
		public ForwardingType Fwd;

		// Token: 0x040011D3 RID: 4563
		[DataMember]
		public ReqType Req;

		// Token: 0x040011D4 RID: 4564
		[DataMember]
		public string CacheName;

		// Token: 0x040011D5 RID: 4565
		public int PartitionKey;

		// Token: 0x040011D6 RID: 4566
		private string _regionName;

		// Token: 0x040011D7 RID: 4567
		[DataMember]
		private int _systemRegionCount;

		// Token: 0x040011D8 RID: 4568
		[DataMember]
		public Key Key;

		// Token: 0x040011D9 RID: 4569
		[DataMember]
		public Key[] Keys;

		// Token: 0x040011DA RID: 4570
		[DataMember]
		public byte[][] Value;

		// Token: 0x040011DB RID: 4571
		[DataMember]
		public byte[][] InitialValue;

		// Token: 0x040011DC RID: 4572
		public object ValObject;

		// Token: 0x040011DD RID: 4573
		[DataMember]
		public SerializationCategory SerializationCategory;

		// Token: 0x040011DE RID: 4574
		public object InitialUserObject;

		// Token: 0x040011DF RID: 4575
		public object UserObject;

		// Token: 0x040011E0 RID: 4576
		[DataMember]
		public InternalCacheItemVersion Version = default(InternalCacheItemVersion);

		// Token: 0x040011E1 RID: 4577
		[DataMember]
		public TimeSpan TTL = TimeSpan.Zero;

		// Token: 0x040011E2 RID: 4578
		[DataMember]
		public DataCacheTag[] Tags;

		// Token: 0x040011E3 RID: 4579
		[DataMember]
		public GetByTagsOperation GetByTagsOp;

		// Token: 0x040011E4 RID: 4580
		[DataMember]
		private DMLockHandle _DMLockHandle = default(DMLockHandle);

		// Token: 0x040011E5 RID: 4581
		[DataMember]
		public byte[][] EnumState;

		// Token: 0x040011E6 RID: 4582
		public object Partition;

		// Token: 0x040011E7 RID: 4583
		public object Region;

		// Token: 0x040011E8 RID: 4584
		public string[] RegionNames;

		// Token: 0x040011E9 RID: 4585
		public OMRegion[] Regions;

		// Token: 0x040011EA RID: 4586
		public object RegionRanges;

		// Token: 0x040011EB RID: 4587
		public object Session;

		// Token: 0x040011EC RID: 4588
		public Guid RequestTrackingId;

		// Token: 0x040011ED RID: 4589
		[DataMember]
		private RequestBodyFlags _flags;

		// Token: 0x040011EE RID: 4590
		[DataMember]
		public InternalCacheItemVersion EventVersion;

		// Token: 0x040011EF RID: 4591
		[DataMember]
		public int NodeId;

		// Token: 0x040011F0 RID: 4592
		internal ProtocolType protocolType;

		// Token: 0x040011F1 RID: 4593
		private DRM _drm;

		// Token: 0x040011F2 RID: 4594
		private ReplicaSetMap _table;

		// Token: 0x040011F3 RID: 4595
		private ServiceCallback _callback;

		// Token: 0x040011F4 RID: 4596
		private static int NumRetries = 1;

		// Token: 0x040011F5 RID: 4597
		private ServiceReplicaSet _config;

		// Token: 0x040011F6 RID: 4598
		private EndpointID _address;

		// Token: 0x040011F7 RID: 4599
		private ErrStatus _error;

		// Token: 0x040011F8 RID: 4600
		private int _counter;

		// Token: 0x040011F9 RID: 4601
		private bool _inTransit;
	}
}
