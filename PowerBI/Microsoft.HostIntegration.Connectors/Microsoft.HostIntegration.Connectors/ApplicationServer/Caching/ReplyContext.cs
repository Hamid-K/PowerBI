using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B6 RID: 694
	internal class ReplyContext : IReplyContext, IDisposable
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x0004B66E File Offset: 0x0004986E
		internal ReplyContext(Message message, IChannelContainer container, WcfTransportChannel transportLayer)
		{
			this._message = message;
			this._container = container;
			this._transportLayer = transportLayer;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0004B68C File Offset: 0x0004988C
		private static void EndSend(IAsyncResult result)
		{
			ReplyContext replyContext = result.AsyncState as ReplyContext;
			IChannelContainer container = replyContext._container;
			try
			{
				container.Channel.EndSend(result);
				replyContext._asyncSendEndCallback(replyContext._callerContext);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(ReplyContext._logSource, "Endsend called on channel {0}", new object[] { container.Channel.GetHashCode() });
				}
			}
			catch (Exception ex)
			{
				if (!ReplyContext.TryHandleFailure(container.Channel, ex))
				{
					throw;
				}
			}
			finally
			{
				if (replyContext._message != null)
				{
					replyContext._message.Close();
					replyContext._message = null;
				}
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0004B74C File Offset: 0x0004994C
		internal static bool TryHandleFailure(IDuplexSessionChannel channel, Exception e)
		{
			CacheConnectionProperty property = channel.GetProperty<CacheConnectionProperty>();
			if (property == null)
			{
				Utility.LogException(e, ReplyContext._logSource, TraceLevel.Info, channel.GetHashCode());
			}
			else
			{
				Utility.LogException(e, ReplyContext._logSource, TraceLevel.Info, property.CacheName, property.TerminationReason);
			}
			if (!(e is TimeoutException))
			{
				ReplyContext.CleanupChannel(channel);
			}
			return WcfTransportChannel.IsCommunicationException(e) || WcfTransportChannel.IsNotSupportedException(e);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0004B7B0 File Offset: 0x000499B0
		private static void CleanupChannel(IDuplexSessionChannel channel)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ReplyContext._logSource, "Aborting the channel {0}", new object[] { channel.GetHashCode() });
			}
			channel.Abort();
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0004B7F0 File Offset: 0x000499F0
		public RemoteEndpoint RemoteEndpoint
		{
			get
			{
				return this._container.GetProperty<RemoteEndpoint>();
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0004B7FD File Offset: 0x000499FD
		public void Reply(ResponseBody message)
		{
			this.Reply(message, this._transportLayer.SendTimeout);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0004B814 File Offset: 0x00049A14
		private void Reply(ResponseBody responseBody, TimeSpan timeout)
		{
			if (responseBody.Ack == AckNack.Nack && (responseBody.ResponseCode == ErrStatus.INVALID_REQUEST_BODY || responseBody.ResponseCode == ErrStatus.REQUEST_TYPE_NOT_SUPPORTED))
			{
				return;
			}
			using (Message message = this.GetMessage(responseBody))
			{
				try
				{
					this._container.Channel.Send(message, timeout);
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(ReplyContext._logSource, "Message sent synchronously {0}", new object[] { this._container.Channel.GetHashCode() });
					}
				}
				catch (Exception ex)
				{
					if (!ReplyContext.TryHandleFailure(this._container.Channel, ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0004B8D4 File Offset: 0x00049AD4
		public void AsyncReply(ResponseBody responseBody, WaitCallback callback, object callerContext)
		{
			this.AsyncReply(responseBody, this._transportLayer.SendTimeout, callback, callerContext);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0004B8EC File Offset: 0x00049AEC
		private void AsyncReply(ResponseBody responseBody, TimeSpan timeout, WaitCallback callback, object callerContext)
		{
			try
			{
				Message message = this.GetMessage(responseBody);
				this._asyncSendEndCallback = callback;
				this._callerContext = callerContext;
				this._container.Channel.BeginSend(message, timeout, ReplyContext._endSend, this);
			}
			catch (Exception ex)
			{
				if (!ReplyContext.TryHandleFailure(this._container.Channel, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0004B954 File Offset: 0x00049B54
		private Message GetMessage(ResponseBody response)
		{
			if (Utility.RemoteEndpointUsesLegacyWcfValueFlags(this.RemoteVersionInfo))
			{
				response.Value = ValueFlagsUtility.NormalizeUserData(response.Value, ValueFlagsVersion.WireProtocolType, ValueFlagsVersion.LegacyWcfType);
				List<LocalCacheItem> list = response.ValObject as List<LocalCacheItem>;
				if (list != null)
				{
					response.BulkValue = new List<byte[][]>(list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].Value != null)
						{
							response.BulkValue.Add(ValueFlagsUtility.NormalizeUserData(list[i].Value, ValueFlagsVersion.WireProtocolType, ValueFlagsVersion.LegacyWcfType));
						}
						else
						{
							response.BulkValue.Add(null);
						}
					}
				}
				if (response.Item != null)
				{
					response.Item.NormalizeValue(ValueFlagsVersion.LegacyWcfType);
				}
			}
			Message message = response.CreateClientMessage(response.RequestType, this.RemoteVersionInfo);
			if (response.RequestTracker != null)
			{
				message.Headers.Add(MessageHeader.CreateHeader("Tracker", "urn:AppFabricCaching", response.RequestTracker));
			}
			else if (response.RequestTrackerOnPrimary != null)
			{
				message.Headers.Add(MessageHeader.CreateHeader("PrimaryTracker", "urn:AppFabricCaching", response.RequestTrackerOnPrimary));
			}
			return message;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0004BA69 File Offset: 0x00049C69
		public object State
		{
			get
			{
				return this._container.Channel;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0004BA76 File Offset: 0x00049C76
		public IChannelContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0004BA7E File Offset: 0x00049C7E
		public ClientVersionInfo RemoteVersionInfo
		{
			get
			{
				return this.Container.RemoteVersionInfo;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0004BA8B File Offset: 0x00049C8B
		public void AbortRequestChannel()
		{
			ReplyContext.CleanupChannel(this._container.Channel);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0004BAA0 File Offset: 0x00049CA0
		public RequestBody GetRequest(out IDictionary<string, string> headers)
		{
			RequestBody request = RequestBody.GetRequest(this._message, out headers);
			if (Utility.RemoteEndpointUsesLegacyWcfValueFlags(this.RemoteVersionInfo))
			{
				if (request.ValObject == null)
				{
					request.Value = ValueFlagsUtility.NormalizeUserData(request.Value, ValueFlagsVersion.LegacyWcfType, ValueFlagsVersion.WireProtocolType);
				}
				request.InitialValue = ValueFlagsUtility.NormalizeUserData(request.InitialValue, ValueFlagsVersion.LegacyWcfType, ValueFlagsVersion.WireProtocolType);
			}
			return request;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0004BAF6 File Offset: 0x00049CF6
		public ResponseBody GetResponse()
		{
			return ResponseBody.PeekResponse(this._message);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0004BB03 File Offset: 0x00049D03
		public T GetRequestTracker<T>(string tracker)
		{
			return Utility.GetMessageHeader<T>(this._message, tracker, "urn:AppFabricCaching");
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x0004BB16 File Offset: 0x00049D16
		public CacheConnectionProperty ConnectionProperty
		{
			get
			{
				return this._container.GetProperty<CacheConnectionProperty>();
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0004BB23 File Offset: 0x00049D23
		public void Dispose()
		{
			if (this._message != null)
			{
				this._message.Close();
				this._message = null;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000189CC File Offset: 0x00016BCC
		public string MessageAuthorizationToken
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x000189CC File Offset: 0x00016BCC
		public VelocityPacketException PacketException
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000DC0 RID: 3520
		private static AsyncCallback _endSend = new AsyncCallback(ReplyContext.EndSend);

		// Token: 0x04000DC1 RID: 3521
		private static string _logSource = "DistributedCache.ReplyContext";

		// Token: 0x04000DC2 RID: 3522
		private WaitCallback _asyncSendEndCallback;

		// Token: 0x04000DC3 RID: 3523
		private object _callerContext;

		// Token: 0x04000DC4 RID: 3524
		private Message _message;

		// Token: 0x04000DC5 RID: 3525
		private IChannelContainer _container;

		// Token: 0x04000DC6 RID: 3526
		private WcfTransportChannel _transportLayer;
	}
}
