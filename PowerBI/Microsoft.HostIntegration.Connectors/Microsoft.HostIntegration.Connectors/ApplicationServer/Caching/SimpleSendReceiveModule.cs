using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A6 RID: 934
	internal class SimpleSendReceiveModule : IDisposable
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0006609F File Offset: 0x0006429F
		internal IClientChannel Channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x000660A7 File Offset: 0x000642A7
		internal bool UseLegacyProtocol
		{
			get
			{
				return this._isLegacyProtocol;
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002130 RID: 8496 RVA: 0x000660B0 File Offset: 0x000642B0
		// (remove) Token: 0x06002131 RID: 8497 RVA: 0x000660E8 File Offset: 0x000642E8
		internal event Action<RequestBody, bool> RequestSentEvent;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002132 RID: 8498 RVA: 0x00066120 File Offset: 0x00064320
		// (remove) Token: 0x06002133 RID: 8499 RVA: 0x00066158 File Offset: 0x00064358
		internal event Action<SendReceiveSynchronizer, ResponseBody> ResponseReceivedFromServerEvent;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002134 RID: 8500 RVA: 0x00066190 File Offset: 0x00064390
		// (remove) Token: 0x06002135 RID: 8501 RVA: 0x000661C8 File Offset: 0x000643C8
		internal event Action ErrorResponseGenerated;

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x000661FD File Offset: 0x000643FD
		// (set) Token: 0x06002137 RID: 8503 RVA: 0x00066205 File Offset: 0x00064405
		internal IBaseHashTable RequestTable
		{
			get
			{
				return this._reqTable;
			}
			set
			{
				this._reqTable = value;
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00066210 File Offset: 0x00064410
		private static IClientChannel CreateChannel(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider, bool useLegacyProtocol)
		{
			if (useLegacyProtocol)
			{
				return new WcfClientChannel(dataCacheSecurity, transportProps, id, verifyObject, verifyCallback, chnlOpenTimeout, sendTimeout, maxChannelCount, endpointIdentityProvider);
			}
			return new SocketClientChannel(transportProps, maxChannelCount, chnlOpenTimeout, sendTimeout, id, dataCacheSecurity);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00066244 File Offset: 0x00064444
		internal SimpleSendReceiveModule(IClientChannel channel, bool isLegacyProtocol)
		{
			this._isLegacyProtocol = isLegacyProtocol;
			this.RequestTable = new BaseHashTable(new ObjectDirectoryNodeFactory());
			this._channel = channel;
			this.RegisterChannelCallbacks();
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				this.ResponseReceivedFromServerEvent += ClientPerfCounterUpdate.OnResponseReceivedFromServer;
				this.RequestSentEvent += ClientPerfCounterUpdate.OnRequestSent;
				this.ErrorResponseGenerated += ClientPerfCounterUpdate.OnErrorResponseGenerated;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<bool>("SimpleSendReceiveModule", "Use sockets = {0}", !this._isLegacyProtocol);
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000662EC File Offset: 0x000644EC
		internal SimpleSendReceiveModule(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider, bool useLegacyProtocol)
			: this(SimpleSendReceiveModule.CreateChannel(dataCacheSecurity, transportProps, id, verifyObject, verifyCallback, chnlOpenTimeout, sendTimeout, maxChannelCount, endpointIdentityProvider, useLegacyProtocol), useLegacyProtocol)
		{
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00066318 File Offset: 0x00064518
		private void RegisterChannelCallbacks()
		{
			if (this._isLegacyProtocol)
			{
				this._channel.RegisterDeadCallback(new OnRemoteGoingDown(this.DeadServerCallback));
				this._channel.RegisterReceiveCallback("http://schemas.microsoft.com/velocity/msgs/NotificationResponse", new OnMessageReceived(this.RespCallBack));
				this._channel.RegisterReceiveCallback("http://schemas.microsoft.com/velocity/msgs/response", new OnMessageReceived(this.RespCallBack));
				return;
			}
			this._channel.RegisterReceiveCallback(MessageType.SocketResponse, new OnMessageReceived(this.RespCallBack));
			this._channel.RegisterDeadCallback(new OnRemoteGoingDown(this.DeadServerCallback));
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000663AC File Offset: 0x000645AC
		internal void EnableVasRoutingChannel(EndpointID endpoint, int nodeCount, DataCacheDeploymentMode routingType)
		{
			WcfClientChannel wcfClientChannel = this._channel as WcfClientChannel;
			if (wcfClientChannel != null)
			{
				wcfClientChannel.ConvertToVasRoutingChannel(endpoint, nodeCount, routingType);
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000663D4 File Offset: 0x000645D4
		internal OperationResult SendMessage(EndpointID endpt, RequestBody reqMsg, ServiceCallback callback)
		{
			SendReceiveSynchronizer sendReceiveSynchronizer = new SendReceiveSynchronizer(reqMsg, reqMsg.ClientRequestTracking, callback);
			return this.SendMessage(endpt, reqMsg, sendReceiveSynchronizer);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000663F8 File Offset: 0x000645F8
		internal OperationResult SendMessage(EndpointID endpt, RequestBody reqMsg, SendReceiveSynchronizer sync)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Guid, int>("SimpleSendReceiveModule", "{0}:SendMessage: Begin: msgId = {1}", reqMsg.RequestTrackingId, reqMsg.ClientReqId);
			}
			if (ConfigManager.IsTestingMode)
			{
				SimpleSendReceiveModule.LastRequestTrackingId = reqMsg.RequestTrackingId;
			}
			sync.RequestSentTime = Stopwatch.GetTimestamp();
			int id = reqMsg.ID;
			this.StoreAsyncState(id, sync);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("SimpleSendReceiveModule", "{0}:SendMessage: Sending Msg to Server: msgId = {1}", new object[] { reqMsg.RequestTrackingId, reqMsg.ClientReqId });
			}
			OperationResult operationResult = this.Send(endpt, reqMsg, 0);
			if (this.RequestSentEvent != null)
			{
				this.RequestSentEvent(reqMsg, false);
			}
			if (!operationResult.IsSuccess)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("SimpleSendReceiveModule", "{0}:Request - {1}, result - {2}", new object[] { reqMsg.RequestTrackingId, id, operationResult });
				}
				if (this.ErrorResponseGenerated != null)
				{
					this.ErrorResponseGenerated();
				}
				this.RequestTable.Delete(id);
			}
			sync.SetOperationResult(operationResult);
			return operationResult;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00066520 File Offset: 0x00064720
		internal ResponseBody SendMsgAndWait(EndpointID endpt, RequestBody reqMsg, TimeSpan requestTimeout, out IRequestTracker tracker)
		{
			tracker = null;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Guid, int>("SimpleSendReceiveModule", "{0}:SendMsgAndWait: Begin: msgId = {1}", reqMsg.RequestTrackingId, reqMsg.ClientReqId);
			}
			if (ConfigManager.IsTestingMode)
			{
				SimpleSendReceiveModule.LastRequestTrackingId = reqMsg.RequestTrackingId;
			}
			SendReceiveSynchronizer sendReceiveSynchronizer = new SendReceiveSynchronizer(reqMsg, reqMsg.ClientRequestTracking);
			int id = reqMsg.ID;
			this.StoreAsyncState(id, sendReceiveSynchronizer);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("SimpleSendReceiveModule", "{0}:SendMsgAndWait: Sending Msg to Server: msgId = {1}", new object[] { reqMsg.RequestTrackingId, reqMsg.ClientReqId });
			}
			sendReceiveSynchronizer.RequestSentTime = Stopwatch.GetTimestamp();
			OperationResult operationResult = this.Send(endpt, reqMsg, 1);
			if (this.RequestSentEvent != null)
			{
				this.RequestSentEvent(reqMsg, false);
			}
			ResponseBody responseBody;
			if (!operationResult.IsSuccess)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("SimpleSendReceiveModule", "{0}:Request - {1}, result - {2} for end point {3}", new object[] { reqMsg.RequestTrackingId, id, operationResult, endpt });
				}
				responseBody = new ResponseBody(AckNack.Nack, reqMsg.ClientReqId);
				responseBody.UniqueTrackingId = reqMsg.UniqueTrackingId;
				if (operationResult.HasVerificationFailed)
				{
					responseBody.ResponseCode = ErrStatus.CLIENT_SERVER_VERSION_MISMATCH;
				}
				else if (operationResult.HasChannelAuthenticationFailed)
				{
					responseBody.Exception = operationResult.Fault;
					responseBody.ResponseCode = ErrStatus.CHANNEL_AUTH_FAILED;
				}
				else if (operationResult.HasCertificateRevocationCheckFailed)
				{
					responseBody.Exception = operationResult.Fault;
					responseBody.ResponseCode = ErrStatus.CHANNEL_AUTH_CRL_OFFLINE;
				}
				else if (operationResult.HasAuthorizationFailed)
				{
					responseBody.Exception = operationResult.Fault;
					DataCacheException ex = operationResult.Fault as DataCacheException;
					if (ex != null && ex.ErrorCode == 29)
					{
						responseBody.ResponseCode = ErrStatus.AUTH_HEADER_INVALID;
					}
					else
					{
						responseBody.ResponseCode = ErrStatus.AUTH_HEADER_EXPIRED;
					}
				}
				else
				{
					responseBody.Exception = operationResult.Fault;
					responseBody.ResponseCode = ErrStatus.SERVER_DEAD;
				}
				SimpleSendReceiveModule.LogResponseMessage(responseBody);
				this.RequestTable.Delete(id);
				if (this.ErrorResponseGenerated != null)
				{
					this.ErrorResponseGenerated();
				}
				return responseBody;
			}
			sendReceiveSynchronizer.SetOperationResult(operationResult);
			if (this.EnterWait(sendReceiveSynchronizer, id, requestTimeout))
			{
				tracker = sendReceiveSynchronizer.Tracker;
				if (sendReceiveSynchronizer.Resp.Ack == AckNack.Nack && sendReceiveSynchronizer.Resp.ResponseCode == ErrStatus.CONNECTION_TERMINATED && reqMsg.IsReadRequest())
				{
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo("SimpleSendReceiveModule.CloseReported", "{0}:'{1}:{2}' - The server is dead.", new object[] { reqMsg.RequestTrackingId, reqMsg.ClientReqId, reqMsg.ServiceReqId });
					}
					sendReceiveSynchronizer.Resp.ResponseCode = ErrStatus.SERVER_DEAD;
					SimpleSendReceiveModule.LogResponseMessage(sendReceiveSynchronizer.Resp);
					return sendReceiveSynchronizer.Resp;
				}
				responseBody = sendReceiveSynchronizer.Resp;
			}
			else
			{
				responseBody = new ResponseBody(AckNack.Nack, reqMsg.ClientReqId);
				responseBody.UniqueTrackingId = reqMsg.UniqueTrackingId;
				responseBody.ResponseCode = ErrStatus.TIMEOUT;
				sendReceiveSynchronizer.IsRequestTimedOut = true;
				if (this.ErrorResponseGenerated != null)
				{
					this.ErrorResponseGenerated();
				}
			}
			SimpleSendReceiveModule.LogResponseMessage(responseBody);
			return responseBody;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00066828 File Offset: 0x00064A28
		internal void StoreAsyncState(int msgId, SendReceiveSynchronizer sync)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int>("SimpleSendReceiveModule", "PrepareWait: Preparing For Wait, msgId = {0}", msgId);
			}
			if (msgId != -1)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>("SimpleSendReceiveModule", "PrepareWait: Populating Hashtable, msgId = {0}", msgId);
				}
				this.RequestTable.Upsert(msgId, sync);
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0006687C File Offset: 0x00064A7C
		internal bool EnterWait(SendReceiveSynchronizer sync, int msgId, TimeSpan timeout)
		{
			bool flag = true;
			LightWeightEventMonitorBased handle = sync.Handle;
			if (handle != null)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>("SimpleSendReceiveModule", "EnterWait: Entering Wait, msgId = {0}", msgId);
				}
				flag = handle.WaitOne(timeout);
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int>("SimpleSendReceiveModule", "EnterWait: Came Out of Wait, msgId = {0}", msgId);
			}
			if (!flag && msgId != -1)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>("SimpleSendReceiveModule", "EnterWait: Wait on the handle timed out, msgId = {0}", msgId);
				}
				this.RequestTable.Delete(msgId);
			}
			return flag;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00066900 File Offset: 0x00064B00
		private OperationResult Send(EndpointID endpoint, RequestBody request, int retries)
		{
			int num = 0;
			OperationResult operationResult;
			do
			{
				IClientChannel channel = this._channel;
				bool isSocketHack = request.IsSocketHack;
				operationResult = ((channel != null) ? channel.Send(endpoint, request) : OperationResult.InstanceClosed);
			}
			while (operationResult.IsRetryable && num++ < retries);
			return operationResult;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00066941 File Offset: 0x00064B41
		private static void LogResponseMessage(ResponseBody resp)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<ResponseBody>("SimpleSendReceiveModule", "SendMsgAndWait: Exiting with the response: {0}", resp);
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0006695B File Offset: 0x00064B5B
		internal bool MarkRequestCompleted(RequestBody request)
		{
			return this.MarkRequestCompleted(request.ID);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00066969 File Offset: 0x00064B69
		internal bool MarkRequestCompleted(int reqId)
		{
			return this._reqTable.Delete(reqId) != null;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00066984 File Offset: 0x00064B84
		internal void RespCallBack(IReplyContext replyContext)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("SimpleSendReceiveModule", "RespCallBack: Begin Processing Response");
			}
			try
			{
				ResponseBody responseBody = replyContext.GetResponse();
				if (responseBody == null)
				{
					return;
				}
				if (responseBody.Ack == AckNack.Nack && responseBody.ResponseCode == ErrStatus.AUTH_HEADER_EXPIRED)
				{
					string authenticationToken = replyContext.ConnectionProperty.AuthenticationToken;
					this.Channel.MarkAuthorizationTokenInvalid(authenticationToken);
				}
				int id = responseBody.ID;
				if (id == 0)
				{
					EventLogWriter.WriteWarning("SimpleSendReceiveModule", "{0}:RespCallBack: Spurious msg with Id zero = {1}", new object[] { responseBody.ResponseTrackingId, id });
					return;
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int, bool, int>("SimpleSendReceiveModule", "RespCallBack: Resp Message {0} Continue{1}, SeqNo {2}", id, responseBody.Continue, responseBody.MultiPartResponseCount);
				}
				SendReceiveSynchronizer sendReceiveSynchronizer;
				if (!responseBody.Continue && responseBody.MultiPartResponseCount == 0)
				{
					sendReceiveSynchronizer = this.RequestTable.Delete(id) as SendReceiveSynchronizer;
				}
				else
				{
					sendReceiveSynchronizer = this.RequestTable.Get(id) as SendReceiveSynchronizer;
					if (sendReceiveSynchronizer != null)
					{
						if (!sendReceiveSynchronizer.TryComplete(responseBody))
						{
							return;
						}
						responseBody = new ResponseBody(AckNack.Ack, id);
						sendReceiveSynchronizer = this.RequestTable.Delete(id) as SendReceiveSynchronizer;
					}
				}
				if (this.ResponseReceivedFromServerEvent != null)
				{
					this.ResponseReceivedFromServerEvent(sendReceiveSynchronizer, responseBody);
				}
				if (sendReceiveSynchronizer == null)
				{
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning("SimpleSendReceiveModule", "{0}:RespCallBack: Spurious msgId = {1}", new object[] { responseBody.ResponseTrackingId, id });
					}
					return;
				}
				if (sendReceiveSynchronizer.Handle == null)
				{
					sendReceiveSynchronizer.ProcessResponse(responseBody, replyContext);
					return;
				}
				RequestTimeTracker requestTimeTracker = null;
				if (sendReceiveSynchronizer.IsRequestTrackingEnabled)
				{
					requestTimeTracker = replyContext.GetRequestTracker<RequestTimeTracker>("Tracker");
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<Guid, int, RequestTimeTracker>("SimpleSendReceiveModule", "{0}:RespCallBack: Processing Response, msgId = {1}. {2}", responseBody.ResponseTrackingId, id, requestTimeTracker);
				}
				sendReceiveSynchronizer.ProcessResponse(responseBody, requestTimeTracker);
			}
			finally
			{
				if (replyContext != null)
				{
					replyContext.Dispose();
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("SimpleSendReceiveModule", "RespCallBack: End Processing Response");
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00066BA4 File Offset: 0x00064DA4
		internal void DeadServerCallback(EndpointID server, object context, Exception e)
		{
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("SimpleSendReceiveModule", "DeadServerCallback Called, Server URI: {0}, Underlying exception - {1}", new object[] { server, e });
			}
			lock (this._lockObject)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("SimpleSendReceiveModule", "DeadServerCallback: Matches My Server, Cleaning Pending Requests", new object[0]);
				}
				bool flag2 = true;
				IEnumerator enumerator = this.RequestTable.Keys.GetEnumerator();
				if (enumerator != null)
				{
					while (flag2)
					{
						try
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								SendReceiveSynchronizer sendReceiveSynchronizer = (SendReceiveSynchronizer)this.RequestTable.Get(obj);
								if (sendReceiveSynchronizer != null)
								{
									if (Provider.IsEnabled(TraceLevel.Verbose))
									{
										EventLogWriter.WriteVerbose<SendReceiveSynchronizer, object>("SimpleSendReceiveModule", "Checking sync {0}== {1}", sendReceiveSynchronizer, context);
									}
									if (sendReceiveSynchronizer.OperationResult != null && sendReceiveSynchronizer.OperationResult.IsMatchingContext(context))
									{
										sendReceiveSynchronizer = this.RequestTable.Delete(enumerator.Current) as SendReceiveSynchronizer;
										if (sendReceiveSynchronizer != null)
										{
											ResponseBody responseBody = new ResponseBody(AckNack.Nack);
											responseBody.ResponseCode = ErrStatus.CONNECTION_TERMINATED;
											responseBody.Exception = e;
											if (Provider.IsEnabled(TraceLevel.Verbose))
											{
												EventLogWriter.WriteVerbose<object, SendReceiveSynchronizer>("SimpleSendReceiveModule", "Cleaning request {0}= {1}", enumerator.Current, sendReceiveSynchronizer);
											}
											if (sendReceiveSynchronizer.Handle == null)
											{
												sendReceiveSynchronizer.ProcessResponse(responseBody, null);
											}
											else
											{
												sendReceiveSynchronizer.ProcessResponse(responseBody, null);
											}
										}
									}
								}
							}
							flag2 = false;
						}
						catch (InvalidOperationException)
						{
							enumerator = this.RequestTable.Keys.GetEnumerator();
						}
					}
				}
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00066D50 File Offset: 0x00064F50
		public void Dispose()
		{
			lock (this)
			{
				if (this._channel != null)
				{
					this._channel.Dispose();
					this._channel = null;
				}
			}
		}

		// Token: 0x0400152A RID: 5418
		private const int _maxRetries = 1;

		// Token: 0x0400152B RID: 5419
		private const string _logSource = "SimpleSendReceiveModule";

		// Token: 0x0400152C RID: 5420
		private readonly bool _isLegacyProtocol = true;

		// Token: 0x0400152D RID: 5421
		private IClientChannel _channel;

		// Token: 0x0400152E RID: 5422
		private IBaseHashTable _reqTable;

		// Token: 0x0400152F RID: 5423
		private object _lockObject = new object();

		// Token: 0x04001533 RID: 5427
		[ThreadStatic]
		internal static Guid LastRequestTrackingId;
	}
}
