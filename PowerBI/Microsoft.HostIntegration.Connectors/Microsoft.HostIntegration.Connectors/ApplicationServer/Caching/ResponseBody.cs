using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000378 RID: 888
	[DataContract(Name = "ResponseBody", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class ResponseBody : MessageBody
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0005F981 File Offset: 0x0005DB81
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x0005F9A2 File Offset: 0x0005DBA2
		internal DiagOperationState ResponseStatesBuffer
		{
			get
			{
				if (this._respStatesBuffer == null)
				{
					this._respStatesBuffer = VelocityDiagnostics.GetOperationState(this.UniqueTrackingId);
				}
				return this._respStatesBuffer;
			}
			set
			{
				this._respStatesBuffer = value;
			}
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0005F9AB File Offset: 0x0005DBAB
		public ResponseBody()
		{
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0005F9BF File Offset: 0x0005DBBF
		public ResponseBody(AckNack ack)
		{
			this.Ack = ack;
			this.LockObject = ResponseBody.InvalidLockHandle;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0005F9E5 File Offset: 0x0005DBE5
		public ResponseBody(string trackingId)
		{
			this.UniqueTrackingId = trackingId;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0005FA00 File Offset: 0x0005DC00
		public ResponseBody(AckNack ack, string trackingId)
			: this(ack)
		{
			this.UniqueTrackingId = trackingId;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0005FA10 File Offset: 0x0005DC10
		public ResponseBody(AckNack ack, int clientReqId)
		{
			this.ClientReqId = clientReqId;
			this.Ack = ack;
			this.LockObject = ResponseBody.InvalidLockHandle;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0005FA3D File Offset: 0x0005DC3D
		public ResponseBody(AckNack ack, int clientReqId, string trackingId)
		{
			this.ClientReqId = clientReqId;
			this.Ack = ack;
			this.LockObject = ResponseBody.InvalidLockHandle;
			this.UniqueTrackingId = trackingId;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0005FA71 File Offset: 0x0005DC71
		public ResponseBody(ErrStatus status, int clientReqId)
			: this(Utility.GetResponseSuccessStatus(status), clientReqId)
		{
			this.ResponseCode = status;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0005FA87 File Offset: 0x0005DC87
		public Message CreateMessage()
		{
			return this.CreateMessage("http://schemas.microsoft.com/velocity/msgs/response");
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0005FA94 File Offset: 0x0005DC94
		public Message CreateClientMessage(ReqType reqType, ClientVersionInfo clientVersion, string action)
		{
			if (RequestBody.IsNotificationRequest(reqType))
			{
				this.Value = SerializationUtility.SerializeBinaryFormat((IBinarySerializable)this.ValObject);
				if (!Utility.IsOlderThanCodeVersion3Client(clientVersion))
				{
					return this.CreateMessage("http://schemas.microsoft.com/velocity/msgs/NotificationResponse");
				}
			}
			return this.CreateClientMessage(clientVersion, action);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0005FAD0 File Offset: 0x0005DCD0
		public Message CreateClientMessage(ReqType request, ClientVersionInfo clientVersion)
		{
			return this.CreateClientMessage(request, clientVersion, "http://schemas.microsoft.com/velocity/msgs/response");
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0005FADF File Offset: 0x0005DCDF
		public Message CreateClientMessage(ClientVersionInfo clientVersion, string action)
		{
			if (this.Value == null && this.ValObject != null)
			{
				this.Value = this.SerializeValObject(clientVersion);
			}
			return this.CreateMessage(action);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0005FB08 File Offset: 0x0005DD08
		private byte[][] SerializeValObject(ClientVersionInfo clientVersion)
		{
			if (Utility.RemoteEndpointUsesNetDataConractSerializer(clientVersion))
			{
				SerializationContext serializationContext = new SerializationContext(null, clientVersion);
				return SerializationUtility.NetDataContractSerialize(this.ValObject, serializationContext);
			}
			return SerializationUtility.DataContractSerialize(this.ValObject);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0005FB40 File Offset: 0x0005DD40
		public Message CreateMessage(string action)
		{
			Message message = Utility.CreateMessage(action, this);
			if (this.IsTrackingIdPresent)
			{
				message.Headers.Add(MessageHeader.CreateHeader("TrackingId", "urn:AppFabricCaching", this.ResponseTrackingId));
			}
			return message;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0005FB84 File Offset: 0x0005DD84
		internal static ResponseBody PeekResponse(Message message)
		{
			ResponseBody responseBody;
			try
			{
				ResponseBody body = message.GetBody<ResponseBody>();
				if (body.IsTrackingIdPresent)
				{
					body.ResponseTrackingId = Utility.GetMessageHeader<Guid>(message, "TrackingId", "urn:AppFabricCaching");
				}
				responseBody = body;
			}
			catch (SerializationException ex)
			{
				ResponseBody.LogInvalidResponseMessage(ex);
				responseBody = null;
			}
			return responseBody;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
		internal static ResponseBody GetResponseBody(IList<IVelocityPacket> packets, Exception exception)
		{
			ResponseBody responseBody2;
			try
			{
				ResponseBody responseBody = new ResponseBody(packets[0].ResponseCode, packets[0].MessageID);
				if (exception is VelocityPacketTooBigException)
				{
					responseBody.Ack = AckNack.Nack;
					responseBody.ResponseCode = ErrStatus.MESSAGE_LARGER_THAN_CONFIGURED;
					responseBody.Exception = exception;
					responseBody2 = responseBody;
				}
				else
				{
					if (Utility.IsGetBatchRequest(packets[0].MessageType) && responseBody.Ack == AckNack.Ack && packets[0].Key != null)
					{
						List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(packets.Count);
						foreach (IVelocityPacket velocityPacket in packets)
						{
							list.Add(new KeyValuePair<string, object>(velocityPacket.Key, velocityPacket.Value));
						}
						byte[][] array = new byte[1][];
						if (packets[0].PropertyBag.TryGetElement(VelocityPacketProperty.EnumState, out array[0]))
						{
							responseBody.EnumState = array;
							responseBody.MoreMsgs = true;
						}
						responseBody.ValObject = list;
					}
					else
					{
						responseBody.ResponseTrackingId = packets[0].PropertyBag.GetMessageTrackingId();
						responseBody.IsTrackingIdPresent = responseBody.ResponseTrackingId != Guid.Empty;
						responseBody.Packet = (IVelocityResponsePacket)packets[0];
						responseBody.Value = packets[0].Value;
						if (packets[0].Version != null)
						{
							responseBody.Version = packets[0].Version.InternalVersion;
						}
					}
					responseBody2 = responseBody;
				}
			}
			catch (SerializationException ex)
			{
				ResponseBody.LogInvalidResponseMessage(ex);
				responseBody2 = null;
			}
			return responseBody2;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0005FDA8 File Offset: 0x0005DFA8
		internal static ResponseBody GetSocketResponse(IVelocityPacket message)
		{
			ResponseBody responseBody2;
			try
			{
				ResponseBody responseBody = new ResponseBody(message.ResponseCode, message.MessageID);
				responseBody.Packet = (IVelocityResponsePacket)message;
				responseBody.ResponseTrackingId = message.PropertyBag.GetMessageTrackingId();
				responseBody.IsTrackingIdPresent = responseBody.ResponseTrackingId != Guid.Empty;
				responseBody2 = responseBody;
			}
			catch (SerializationException ex)
			{
				ResponseBody.LogInvalidResponseMessage(ex);
				responseBody2 = null;
			}
			return responseBody2;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0005FE1C File Offset: 0x0005E01C
		private static void LogInvalidResponseMessage(SerializationException ex)
		{
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError("DistributedCache.ResponseBody", "Invalid message body. - {0}", new object[] { ex });
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0005FE4C File Offset: 0x0005E04C
		internal static ResponseBody GetResponse(Message message)
		{
			ResponseBody responseBody;
			try
			{
				responseBody = ResponseBody.PeekResponse(message);
			}
			finally
			{
				if (message != null)
				{
					((IDisposable)message).Dispose();
				}
			}
			return responseBody;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0005FE80 File Offset: 0x0005E080
		public bool IsNotPrimaryResponse()
		{
			return this.ResponseCode == ErrStatus.NOT_PRIMARY && this.Ack == AckNack.Nack;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0005FE97 File Offset: 0x0005E097
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0005FE9F File Offset: 0x0005E09F
		public IVelocityResponsePacket Packet { get; set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x0005FEB6 File Offset: 0x0005E0B6
		// (set) Token: 0x06001F61 RID: 8033 RVA: 0x0005FEA8 File Offset: 0x0005E0A8
		public DataCacheLockHandle LockObject
		{
			get
			{
				return new DataCacheLockHandle(this._handle, this.Version);
			}
			set
			{
				this._handle = value.Handle;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0005FEC9 File Offset: 0x0005E0C9
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x0005FED1 File Offset: 0x0005E0D1
		[DataMember(Order = 1)]
		internal string Key { get; set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0005FEDA File Offset: 0x0005E0DA
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x0005FEE7 File Offset: 0x0005E0E7
		internal bool IsTrackingIdPresent
		{
			get
			{
				return this._flags.TrackingIdPresenceFlag;
			}
			set
			{
				this._flags.TrackingIdPresenceFlag = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0005FEF5 File Offset: 0x0005E0F5
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x0005FF02 File Offset: 0x0005E102
		internal bool IsProcessedAtGateway
		{
			get
			{
				return this._flags.ProcessedAtGatewayFlag;
			}
			set
			{
				this._flags.ProcessedAtGatewayFlag = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0005FF10 File Offset: 0x0005E110
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x0005FF1D File Offset: 0x0005E11D
		internal bool IsClientRoutingTableStale
		{
			get
			{
				return this._flags.IsClientRoutingTableStaleFlag;
			}
			set
			{
				this._flags.IsClientRoutingTableStaleFlag = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0005FF2B File Offset: 0x0005E12B
		// (set) Token: 0x06001F6C RID: 8044 RVA: 0x0005FF33 File Offset: 0x0005E133
		[DataMember]
		public int MultiPartResponseCount { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0005FF3C File Offset: 0x0005E13C
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x0005FF44 File Offset: 0x0005E144
		public RequestTimeTracker RequestTracker { get; set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0005FF4D File Offset: 0x0005E14D
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x0005FF55 File Offset: 0x0005E155
		public RequestTrackerOnPrimary RequestTrackerOnPrimary { get; set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0005FF5E File Offset: 0x0005E15E
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x0005FF66 File Offset: 0x0005E166
		public ReqType RequestType { get; set; }

		// Token: 0x06001F73 RID: 8051 RVA: 0x0005FF70 File Offset: 0x0005E170
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "'{0}:{1}';{2};{3};{4};{5}", new object[] { this.ClientReqId, this.ServiceReqId, this.Ack, this.ResponseCode, this.ResponseTrackingId, this.Exception });
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0005FFE4 File Offset: 0x0005E1E4
		public int Size
		{
			get
			{
				int num = Utility.Get2DByteArraySize(this.Value);
				if (this.BulkValue != null)
				{
					foreach (byte[][] array in this.BulkValue)
					{
						num = Utility.Get2DByteArraySize(array);
					}
				}
				if (this.Item != null)
				{
					num += this.Item.Size;
				}
				if (this.Tags != null)
				{
					foreach (DataCacheTag dataCacheTag in this.Tags)
					{
						num += dataCacheTag.Length;
					}
				}
				num += this.Version.Size;
				return num;
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000600A4 File Offset: 0x0005E2A4
		public new void ReadStream(ISerializationReader reader)
		{
			base.ReadStream(reader);
			this.Ack = (AckNack)Enum.ToObject(typeof(AckNack), reader.ReadInt32());
			this.ResponseCode = (ErrStatus)Enum.ToObject(typeof(ErrStatus), reader.ReadInt32());
			this.Version.ReadStream(reader);
			this._handle.ReadStream(reader);
			this._flags.ReadStream(reader);
			this.MoreMsgs = reader.ReadBoolean();
			this.Timeout = TimeSpan.FromTicks(reader.ReadInt64());
			this.Value = reader.ReadChunkedByteArray();
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00060148 File Offset: 0x0005E348
		public new void WriteStream(ISerializationWriter writer)
		{
			base.WriteStream(writer);
			writer.Write((int)this.Ack);
			writer.Write((int)this.ResponseCode);
			this.Version.WriteStream(writer);
			this._handle.WriteStream(writer);
			this._flags.WriteStream(writer);
			writer.Write(this.MoreMsgs);
			writer.Write(this.Timeout.Ticks);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000601B5 File Offset: 0x0005E3B5
		public new int GetSerializedSize()
		{
			return base.GetSerializedSize() + 4 + 4 + this.Version.GetSerializedSize() + this._handle.GetSerializedSize() + this._flags.GetSerializedSize() + 1 + 8 + 4;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000601EB File Offset: 0x0005E3EB
		public byte[][] WritePayloadDetails(ISerializationWriter writer, out int payloadLength)
		{
			payloadLength = this.GetValueLength();
			writer.Write(payloadLength);
			return this.Value;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00060204 File Offset: 0x0005E404
		internal int GetValueLength()
		{
			int num = 0;
			if (this.Value != null)
			{
				for (int i = 0; i < this.Value.Length; i++)
				{
					num += this.Value[i].Length;
				}
			}
			return num;
		}

		// Token: 0x0400129D RID: 4765
		private const ushort TypeVersion = 1;

		// Token: 0x0400129E RID: 4766
		internal static DataCacheLockHandle InvalidLockHandle = new DataCacheLockHandle(default(DMLockHandle), InternalCacheItemVersion.Null);

		// Token: 0x0400129F RID: 4767
		private DiagOperationState _respStatesBuffer;

		// Token: 0x040012A0 RID: 4768
		[DataMember]
		public AckNack Ack;

		// Token: 0x040012A1 RID: 4769
		[DataMember]
		public ErrStatus ResponseCode;

		// Token: 0x040012A2 RID: 4770
		[DataMember]
		public byte[][] Value;

		// Token: 0x040012A3 RID: 4771
		public object ValObject;

		// Token: 0x040012A4 RID: 4772
		internal Exception Exception;

		// Token: 0x040012A5 RID: 4773
		[DataMember]
		public DataCacheTag[] Tags;

		// Token: 0x040012A6 RID: 4774
		[DataMember]
		public InternalCacheItemVersion Version = default(InternalCacheItemVersion);

		// Token: 0x040012A7 RID: 4775
		[DataMember]
		public DataCacheItem Item;

		// Token: 0x040012A8 RID: 4776
		[DataMember]
		public List<byte[][]> BulkValue;

		// Token: 0x040012A9 RID: 4777
		[DataMember]
		private DMLockHandle _handle;

		// Token: 0x040012AA RID: 4778
		[DataMember]
		private ResponseBodyFlags _flags;

		// Token: 0x040012AB RID: 4779
		[DataMember]
		public byte[][] EnumState;

		// Token: 0x040012AC RID: 4780
		[DataMember]
		public bool MoreMsgs;

		// Token: 0x040012AD RID: 4781
		[DataMember]
		public bool Continue;

		// Token: 0x040012AE RID: 4782
		[DataMember]
		public TimeSpan Timeout;

		// Token: 0x040012AF RID: 4783
		[DataMember]
		public Uri RedirectUri;

		// Token: 0x040012B0 RID: 4784
		[DataMember]
		public Key[] Keys;

		// Token: 0x040012B1 RID: 4785
		public Guid ResponseTrackingId;

		// Token: 0x040012B2 RID: 4786
		public List<ResponseBody> ResponseList;

		// Token: 0x040012B3 RID: 4787
		public bool SendNotRequired;

		// Token: 0x040012B4 RID: 4788
		internal bool SendPayloadSeparately;
	}
}
