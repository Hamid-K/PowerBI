using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200019F RID: 415
	internal class MemcacheReplyContext : IReplyContext, IDisposable
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0002E015 File Offset: 0x0002C215
		public MemcacheReplyContext(MemcacheWireProtocol protocol, ITcpChannel tcpChannel, IList<IVelocityPacket> packets, VelocityPacketException exception)
		{
			this.memcacheWireProtocol = protocol;
			this.tcpChannel = tcpChannel;
			this.packets = packets;
			this.exception = exception;
			this.responsesLock = new object();
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0002E045 File Offset: 0x0002C245
		internal MemcacheWireProtocol WireProtocol
		{
			get
			{
				return this.memcacheWireProtocol;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0002E04D File Offset: 0x0002C24D
		internal IVelocityPacket SocketPacket
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SocketPacket");
				}
				if (this.packets.Count == 0)
				{
					return null;
				}
				return this.packets[0];
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00003CAB File Offset: 0x00001EAB
		public RemoteEndpoint RemoteEndpoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002E07D File Offset: 0x0002C27D
		public void Reply(ResponseBody responseBody)
		{
			if (responseBody.Continue)
			{
				this.ReplyBulkIfCompleted(responseBody);
				return;
			}
			this.expectedResponseCount = responseBody.MultiPartResponseCount;
			if (this.expectedResponseCount == 0)
			{
				this.ProcessReply(responseBody);
				return;
			}
			this.ReplyBulkIfCompleted(responseBody);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		public void AsyncReply(ResponseBody responseBody, WaitCallback callback, object callerContext)
		{
			IBufferManager bufferManager = SocketTransportChannel.BufferManager;
			TcpPacketSendTypes tcpPacketSendTypes;
			int num;
			IList<ArraySegment<byte>> list = this.PrepareSocketResponse(responseBody, bufferManager, out tcpPacketSendTypes, out num);
			OperationResult operationResult = this.tcpChannel.Send(list, tcpPacketSendTypes, new int?(num));
			this.Log(operationResult, responseBody);
			callback(callerContext);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002E0FC File Offset: 0x0002C2FC
		private void Log(OperationResult result, ResponseBody resp)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemcacheReplyContext", "Send status {0}:{1}, response {2} Channel {3}", new object[]
				{
					result.Status,
					(result.Fault == null) ? "" : result.Fault.ToString(),
					resp,
					this.tcpChannel
				});
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002E160 File Offset: 0x0002C360
		public void AbortRequestChannel()
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemcacheReplyContext", "Abort on channel is requested {0}", new object[] { this.tcpChannel });
			}
			this.tcpChannel.Abort();
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		public object State
		{
			get
			{
				return this.tcpChannel.ToString();
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000189CC File Offset: 0x00016BCC
		public ClientVersionInfo RemoteVersionInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
		public RequestBody GetRequest(out IDictionary<string, string> headers)
		{
			headers = new Dictionary<string, string>();
			RequestBody requestBody = MemcacheReplyContext.GetRequestBody(this.SocketPacket as IVelocityRequestPacket);
			if (this.exception != null)
			{
				requestBody.Req = ReqType.SOCKET_INVALID_REQUEST_BODY;
			}
			return requestBody;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002E1E9 File Offset: 0x0002C3E9
		public ResponseBody GetResponse()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SocketMessage");
			}
			return ResponseBody.GetSocketResponse(this.SocketPacket);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00003CAB File Offset: 0x00001EAB
		public T GetRequestTracker<T>(string tracker)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00003CAB File Offset: 0x00001EAB
		public CacheConnectionProperty ConnectionProperty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x000189CC File Offset: 0x00016BCC
		public string MessageAuthorizationToken
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0002E209 File Offset: 0x0002C409
		public VelocityPacketException PacketException
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002E211 File Offset: 0x0002C411
		public void Dispose()
		{
			this.disposed = true;
			this.packets = null;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002E224 File Offset: 0x0002C424
		private IList<ArraySegment<byte>> PrepareSocketResponse(ResponseBody response, IBufferManager bufferManager, out TcpPacketSendTypes sendType, out int sequence)
		{
			sequence = this.SocketPacket.MessageID;
			IList<ArraySegment<byte>> list2;
			if (response.RequestType == ReqType.MEMCACHE_GET_STATS)
			{
				List<IVelocityResponsePacket> list = new List<IVelocityResponsePacket>();
				list.AddRange(MemcacheReplyContext.GetStatResponse(this.memcacheWireProtocol, this.SocketPacket, response));
				MemcacheReplyContext.AddEmptyPacket(this.memcacheWireProtocol, list, this.SocketPacket);
				list2 = this.PrepareSocketResponse(list, bufferManager);
				sendType = this.SocketPacket.SendType;
			}
			else
			{
				IVelocityResponsePacket velocityResponsePacket = MemcacheReplyContext.FromResponseBody(this.memcacheWireProtocol, this.SocketPacket, response);
				velocityResponsePacket.Opaque = this.SocketPacket.Opaque;
				list2 = this.memcacheWireProtocol.GetWriteResponseBuffer(velocityResponsePacket, bufferManager);
				sendType = velocityResponsePacket.SendType;
			}
			return list2;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		private IList<ArraySegment<byte>> PrepareSocketResponse(IEnumerable<ResponseBody> responseBodies, IBufferManager bufferManager, out TcpPacketSendTypes sendType, out int sequence)
		{
			IVelocityPacket socketPacket = this.SocketPacket;
			sendType = this.SocketPacket.SendType;
			sequence = this.SocketPacket.MessageID;
			List<IVelocityResponsePacket> list = new List<IVelocityResponsePacket>();
			foreach (ResponseBody responseBody in responseBodies)
			{
				list.AddRange(MemcacheReplyContext.GetBatchResponse(this.memcacheWireProtocol, socketPacket, responseBody));
			}
			MemcacheReplyContext.AddEmptyPacket(this.memcacheWireProtocol, list, socketPacket);
			return this.PrepareSocketResponse(list, bufferManager);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0002E364 File Offset: 0x0002C564
		private IList<ArraySegment<byte>> PrepareSocketResponse(ICollection<IVelocityResponsePacket> responsePackets, IBufferManager bufferManager)
		{
			foreach (IVelocityResponsePacket velocityResponsePacket in responsePackets)
			{
				velocityResponsePacket.Opaque = this.SocketPacket.Opaque;
			}
			return this.memcacheWireProtocol.GetWriteResponseBuffer(responsePackets, bufferManager);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		private void ReplyBulkIfCompleted(ResponseBody responseBody)
		{
			lock (this.responsesLock)
			{
				if (responseBody.ResponseList != null)
				{
					this.responses = responseBody.ResponseList;
				}
				else
				{
					if (this.responses == null)
					{
						this.responses = new List<ResponseBody>();
					}
					this.responses.Add(responseBody);
				}
				if (this.expectedResponseCount == this.responses.Count)
				{
					IBufferManager bufferManager = SocketTransportChannel.BufferManager;
					TcpPacketSendTypes tcpPacketSendTypes;
					int num;
					IList<ArraySegment<byte>> list = this.PrepareSocketResponse(this.responses, bufferManager, out tcpPacketSendTypes, out num);
					OperationResult operationResult = this.tcpChannel.Send(list, tcpPacketSendTypes, new int?(num));
					this.Log(operationResult, responseBody);
				}
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0002E480 File Offset: 0x0002C680
		private void ProcessReply(ResponseBody responseBody)
		{
			IBufferManager bufferManager = SocketTransportChannel.BufferManager;
			TcpPacketSendTypes tcpPacketSendTypes;
			int num;
			IList<ArraySegment<byte>> list = this.PrepareSocketResponse(responseBody, bufferManager, out tcpPacketSendTypes, out num);
			OperationResult operationResult = this.tcpChannel.Send(list, tcpPacketSendTypes, new int?(num));
			this.Log(operationResult, responseBody);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0002E4C0 File Offset: 0x0002C6C0
		internal static void ReleaseBuffers(IList<ArraySegment<byte>> buffers)
		{
			IBufferManager bufferManager = SocketTransportChannel.BufferManager;
			foreach (ArraySegment<byte> arraySegment in buffers)
			{
				bufferManager.ReleaseBuffer(arraySegment.Array);
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002E514 File Offset: 0x0002C714
		private static IVelocityResponsePacket FromResponseBody(MemcacheWireProtocol wireProtocol, IVelocityPacket request, ResponseBody response)
		{
			IVelocityResponsePacket velocityResponsePacket = wireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
			if (response.Ack == AckNack.Nack)
			{
				if (response.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
				{
					velocityResponsePacket.ResponseCode = ErrStatus.INTERNAL_ERROR;
					EventLogWriter.WriteInfo("DistributedCache.MemcacheReplyContext", "Response returned Nack with {0} error, converted to {1}", new object[] { response.ResponseCode, velocityResponsePacket.ResponseCode });
				}
				else
				{
					velocityResponsePacket.ResponseCode = response.ResponseCode;
				}
			}
			else
			{
				velocityResponsePacket.ResponseCode = response.ResponseCode;
				velocityResponsePacket.Version = new DataCacheItemVersion(response.Version);
				velocityResponsePacket.Value = response.Value;
			}
			return velocityResponsePacket;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		private static RequestBody GetRequestBody(IVelocityRequestPacket packet)
		{
			if (packet == null)
			{
				return null;
			}
			ReqType requestType = VelocityWireProtocol.GetRequestType(packet.MessageType);
			RequestBody requestBody = new RequestBody(requestType)
			{
				Action = "http://schemas.microsoft.com/velocity/msgs/request",
				ClientReqId = packet.MessageID,
				ServiceReqId = packet.MessageID,
				CacheName = packet.CacheName,
				protocolType = ProtocolType.Memcache
			};
			if (requestType == ReqType.CACHE_BULK_GET)
			{
				requestBody.Keys = new Key[packet.Keys.Count];
				for (int i = 0; i < packet.Keys.Count; i++)
				{
					requestBody.Keys[i] = new Key(packet.Keys[i]);
				}
			}
			else
			{
				if (requestBody.Req == ReqType.INCREMENT || requestBody.Req == ReqType.DECREMENT)
				{
					byte[] array;
					packet.PropertyBag.TryGetElement(VelocityPacketProperty.InitialValue, out array);
					requestBody.InitialValue = Utility.GetChunkedArray(array, 0);
				}
				requestBody.Key = ((packet.Key != null) ? new Key(packet.Key) : null);
				requestBody.Value = packet.Value;
				requestBody.Version = ((packet.Version == null) ? InternalCacheItemVersion.Null : packet.Version.InternalVersion);
				requestBody.TTL = MemcachePacket.GetPassThroughTime(packet.ExpiryTTL);
				requestBody.SerializationCategory = SerializationCategory.Memcache;
			}
			return requestBody;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002E708 File Offset: 0x0002C908
		private static IEnumerable<IVelocityResponsePacket> GetBatchResponse(MemcacheWireProtocol wireProtocol, IVelocityPacket request, ResponseBody response)
		{
			IList<IVelocityResponsePacket> list = new List<IVelocityResponsePacket>();
			if (response.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
			{
				IVelocityResponsePacket velocityResponsePacket = wireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
				velocityResponsePacket.Key = response.Key;
				velocityResponsePacket.Value = response.Value;
				velocityResponsePacket.Version = new DataCacheItemVersion(response.Version);
				if (velocityResponsePacket.Value == null)
				{
					velocityResponsePacket.ResponseCode = ErrStatus.KEY_DOES_NOT_EXIST;
				}
				list.Add(velocityResponsePacket);
			}
			return list;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002E778 File Offset: 0x0002C978
		private static IEnumerable<IVelocityResponsePacket> GetStatResponse(MemcacheWireProtocol wireProtocol, IVelocityPacket request, ResponseBody response)
		{
			IList<IVelocityResponsePacket> list = new List<IVelocityResponsePacket>();
			if (response.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
			{
				Dictionary<string, byte[][]> dictionary = (Dictionary<string, byte[][]>)response.ValObject;
				foreach (KeyValuePair<string, byte[][]> keyValuePair in dictionary)
				{
					IVelocityResponsePacket velocityResponsePacket = wireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
					velocityResponsePacket.Key = keyValuePair.Key;
					velocityResponsePacket.Value = keyValuePair.Value;
					list.Add(velocityResponsePacket);
				}
			}
			return list;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002E810 File Offset: 0x0002CA10
		private static void AddEmptyPacket(MemcacheWireProtocol wireProtocol, List<IVelocityResponsePacket> responsePackets, IVelocityPacket request)
		{
			IVelocityResponsePacket velocityResponsePacket = wireProtocol.CreateResponsePacket(request.MessageType, request.MessageID);
			velocityResponsePacket.IsEmptyPacket = true;
			responsePackets.Add(velocityResponsePacket);
		}

		// Token: 0x0400095B RID: 2395
		private const string LogSource = "DistributedCache.MemcacheReplyContext";

		// Token: 0x0400095C RID: 2396
		private readonly ITcpChannel tcpChannel;

		// Token: 0x0400095D RID: 2397
		private IList<IVelocityPacket> packets;

		// Token: 0x0400095E RID: 2398
		private bool disposed;

		// Token: 0x0400095F RID: 2399
		private readonly MemcacheWireProtocol memcacheWireProtocol;

		// Token: 0x04000960 RID: 2400
		private int expectedResponseCount;

		// Token: 0x04000961 RID: 2401
		private readonly VelocityPacketException exception;

		// Token: 0x04000962 RID: 2402
		private readonly object responsesLock;

		// Token: 0x04000963 RID: 2403
		private List<ResponseBody> responses;
	}
}
