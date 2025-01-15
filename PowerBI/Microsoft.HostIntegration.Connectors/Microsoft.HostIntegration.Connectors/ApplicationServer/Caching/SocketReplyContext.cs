using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002CB RID: 715
	internal class SocketReplyContext : IReplyContext, IDisposable
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x00050353 File Offset: 0x0004E553
		public SocketReplyContext(ITcpChannel tcpChannel, IList<IVelocityPacket> packetList, VelocityPacketException exception)
		{
			this._tcpChannel = tcpChannel;
			this._packets = packetList;
			this._parseException = exception;
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00050370 File Offset: 0x0004E570
		internal IList<IVelocityPacket> Packets
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("SocketPacket");
				}
				return this._packets;
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0005038C File Offset: 0x0004E58C
		private IList<ArraySegment<byte>> PrepareSocketResponse(ResponseBody response, IBufferManager bufferManager, out IList<AllocationType> segmentAllocationType)
		{
			IVelocityPacket velocityPacket = this.Packets[0];
			IList<ArraySegment<byte>> list;
			try
			{
				if (!Utility.IsGetBatchRequest(velocityPacket.MessageType) || response.Ack == AckNack.Nack)
				{
					IVelocityResponsePacket velocityResponsePacket = response.Packet;
					if (velocityResponsePacket == null)
					{
						velocityResponsePacket = VelocityWireProtocol.FromResponseBody(velocityPacket, response);
					}
					list = VelocityWireProtocol.GetWriteResponseBuffer(velocityResponsePacket, bufferManager, out segmentAllocationType);
				}
				else
				{
					IList<IVelocityResponsePacket> batchResponse = VelocityWireProtocol.GetBatchResponse(velocityPacket, response);
					list = VelocityWireProtocol.GetWriteResponseBuffer(batchResponse, bufferManager, out segmentAllocationType);
				}
			}
			catch (DataCacheException ex)
			{
				if (ex.ErrorCode != 38)
				{
					throw;
				}
				ResponseBody responseBody = new ResponseBody(AckNack.Nack);
				responseBody.Exception = ex;
				Utility.AddInfo(response, responseBody);
				IVelocityResponsePacket velocityResponsePacket2 = VelocityWireProtocol.FromResponseBody(velocityPacket, responseBody);
				list = VelocityWireProtocol.GetWriteResponseBuffer(velocityResponsePacket2, bufferManager, out segmentAllocationType);
			}
			return list;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00003CAB File Offset: 0x00001EAB
		public RemoteEndpoint RemoteEndpoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0005043C File Offset: 0x0004E63C
		public void Reply(ResponseBody responseBody)
		{
			IBufferManager bufferManager = SocketTransportChannel.BufferManager;
			IList<AllocationType> list2;
			IList<ArraySegment<byte>> list = this.PrepareSocketResponse(responseBody, bufferManager, out list2);
			if (list == null)
			{
				return;
			}
			try
			{
				OperationResult operationResult = this._tcpChannel.Send(list);
				this.Log(operationResult, responseBody);
			}
			finally
			{
				VelocityWireProtocol.ReleaseMemory(bufferManager, list, list2);
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00050490 File Offset: 0x0004E690
		public void AsyncReply(ResponseBody responseBody, WaitCallback callback, object callerContext)
		{
			IBufferManager bufferManager = SocketTransportChannel.BufferManager;
			IList<AllocationType> list2;
			IList<ArraySegment<byte>> list = this.PrepareSocketResponse(responseBody, bufferManager, out list2);
			if (list == null)
			{
				return;
			}
			try
			{
				OperationResult operationResult = this._tcpChannel.Send(list);
				this.Log(operationResult, responseBody);
				callback(callerContext);
			}
			finally
			{
				VelocityWireProtocol.ReleaseMemory(bufferManager, list, list2);
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000504EC File Offset: 0x0004E6EC
		private void Log(OperationResult result, ResponseBody resp)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(SocketReplyContext._logSource, "Send status {0}:{1}, response {2} Channel {3}", new object[]
				{
					result.Status,
					(result.Fault == null) ? "" : result.Fault.ToString(),
					resp,
					this._tcpChannel
				});
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00050550 File Offset: 0x0004E750
		public void AbortRequestChannel()
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(SocketReplyContext._logSource, " Abort on channel is requested {0}", new object[] { this._tcpChannel });
			}
			this._tcpChannel.Abort();
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00050590 File Offset: 0x0004E790
		public object State
		{
			get
			{
				return this._tcpChannel.ToString();
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x000189CC File Offset: 0x00016BCC
		public ClientVersionInfo RemoteVersionInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000505A0 File Offset: 0x0004E7A0
		public RequestBody GetRequest(out IDictionary<string, string> headers)
		{
			headers = new Dictionary<string, string>();
			if (this.Packets == null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose(SocketReplyContext._logSource, "Invalid Request, no Packets present");
				}
				return null;
			}
			if (this.Packets.Count != 1)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>(SocketReplyContext._logSource, "Invalid no. of Packets in request - {0}", this.Packets.Count);
				}
				return null;
			}
			if (this._parseException is VelocityPacketTooBigException)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(SocketReplyContext._logSource, "Large packed received - {0}", new object[] { this._parseException.Message });
				}
				return null;
			}
			RequestBody requestBody = VelocityWireProtocol.GetRequestBody(this.Packets[0] as IVelocityRequestPacket);
			if (this._parseException != null)
			{
				requestBody.Req = ReqType.SOCKET_INVALID_REQUEST_BODY;
			}
			return requestBody;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0005066E File Offset: 0x0004E86E
		public ResponseBody GetResponse()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("SocketMessage");
			}
			return ResponseBody.GetResponseBody(this._packets, this._parseException);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00050694 File Offset: 0x0004E894
		public T GetRequestTracker<T>(string tracker)
		{
			if (tracker != null)
			{
				if (tracker == "Tracker")
				{
					return this._packets[0].PropertyBag.GetRequestTracker(VelocityPacketProperty.MessageGatewayTracker);
				}
				if (tracker == "PrimaryTracker")
				{
					return this._packets[0].PropertyBag.GetRequestTracker(VelocityPacketProperty.MessagePrimaryTracker);
				}
			}
			throw new NotImplementedException();
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00050700 File Offset: 0x0004E900
		public CacheConnectionProperty ConnectionProperty
		{
			get
			{
				IAuthorizedChannel authorizedChannel = this._tcpChannel as IAuthorizedChannel;
				if (authorizedChannel != null)
				{
					return authorizedChannel.ConnectionProperty;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x00050728 File Offset: 0x0004E928
		public VelocityPacketException PacketException
		{
			get
			{
				return this._parseException;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00050730 File Offset: 0x0004E930
		public string MessageAuthorizationToken
		{
			get
			{
				if (this._packets == null || this._packets.Count == 0)
				{
					return null;
				}
				return this._packets[0].PropertyBag.GetAuthorizationToken();
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0005075F File Offset: 0x0004E95F
		public bool UsesSecureChannel
		{
			get
			{
				return this._tcpChannel.IsSecureChannel;
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0005076C File Offset: 0x0004E96C
		public void Dispose()
		{
			this._disposed = true;
			this._packets = null;
		}

		// Token: 0x04000E19 RID: 3609
		private static string _logSource = "DistributedCache.SocketReplyContext";

		// Token: 0x04000E1A RID: 3610
		private ITcpChannel _tcpChannel;

		// Token: 0x04000E1B RID: 3611
		private IList<IVelocityPacket> _packets;

		// Token: 0x04000E1C RID: 3612
		private VelocityPacketException _parseException;

		// Token: 0x04000E1D RID: 3613
		private bool _disposed;
	}
}
