using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000012 RID: 18
	internal sealed class RoutingClientStrategy : IRoutingStrategy, IDisposable
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000401F File Offset: 0x0000221F
		public RoutingClientStrategy(string cacheName, DataCacheFactoryConfiguration config, ClientDRM drm)
		{
			this._factoryConfig = config;
			this._clientDRM = drm;
			this.InitializeLookupTable(cacheName, config.CacheReadyRetryPolicy);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.RoutingClient", "Initialize: Completed");
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000405C File Offset: 0x0000225C
		public IEnumerable<EndpointID> GetHostEndpoints(string cacheName)
		{
			IList<PartitionId> partitionIdList = this._clientDRM.GetPartitionIdList(cacheName);
			return new HashSet<EndpointID>(partitionIdList.Select((PartitionId partition) => this._clientDRM.GetPrimaryEndPoint(partition)));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004090 File Offset: 0x00002290
		public ResponseBody SendMessageAndWait(RequestBody reqMsg, out IRequestTracker tracker)
		{
			tracker = null;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Guid, int>("DistributedCache.RoutingClient", "{0}:SendMsgAndWait: Begin: msgId = {1}", reqMsg.RequestTrackingId, reqMsg.ClientReqId);
			}
			reqMsg.PrimaryRequestTracking = reqMsg.ClientRequestTracking;
			reqMsg.Fwd = ForwardingType.Routed;
			SendReceiveSynchronizer sendReceiveSynchronizer = new SendReceiveSynchronizer(reqMsg, reqMsg.ClientRequestTracking);
			reqMsg.Table = this._lookupTable;
			ResponseBody responseBody = this._clientDRM.ProcessRequest(reqMsg, new ServiceCallback(RoutingClientStrategy.RoutingClientCallback), sendReceiveSynchronizer);
			if (responseBody.Ack == AckNack.Pending)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<Guid, int>("DistributedCache.RoutingClient", "{0}:SendMsgAndWait: Request is Pending, msgId = {1}", reqMsg.RequestTrackingId, reqMsg.ClientReqId);
				}
				if (!sendReceiveSynchronizer.Handle.WaitOne(this._factoryConfig.RequestTimeout))
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError("DistributedCache.RoutingClient", "{0}:SendMsgAndWait: Request TimedOut, msgId = {1}", new object[] { reqMsg.RequestTrackingId, reqMsg.ClientReqId });
					}
					responseBody = this._clientDRM.GetPartialResponse(reqMsg);
					sendReceiveSynchronizer.IsRequestTimedOut = true;
				}
				else
				{
					responseBody = sendReceiveSynchronizer.Resp;
				}
			}
			tracker = sendReceiveSynchronizer.Tracker;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Guid, ResponseBody>("DistributedCache.RoutingClient", "{0}:SendMsgAndWait: Exiting with the response: {1}", reqMsg.RequestTrackingId, responseBody);
			}
			return responseBody;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000041D4 File Offset: 0x000023D4
		public ResponseBody SendMultiMessageAndWaitForAll(MultiRequest multiReqMsg, out IRequestTracker tracker)
		{
			tracker = null;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int>("DistributedCache.RoutingClient", "SendMultiMessageAndWaitForAll: Begin: msgId = {0}", multiReqMsg.MultiReqId);
			}
			SendReceiveSynchronizer sendReceiveSynchronizer = new SendReceiveSynchronizer();
			ResponseBody responseBody = this._clientDRM.ProcessMultiRequest(multiReqMsg, new ServiceCallback(RoutingClientStrategy.RoutingClientCallback), sendReceiveSynchronizer);
			if (responseBody.Ack == AckNack.Pending)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<int>("DistributedCache.RoutingClient", "SendMultiMessageAndWaitForAll: Request is Pending, msgId = {0}", multiReqMsg.MultiReqId);
				}
				if (!sendReceiveSynchronizer.Handle.WaitOne(this._factoryConfig.RequestTimeout))
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError("DistributedCache.RoutingClient", "SendMultiMessageAndWaitForAll: Request TimedOut, msgId = {0}", new object[] { multiReqMsg.MultiReqId });
					}
					responseBody = this._clientDRM.GetTimeoutResponse(multiReqMsg);
					sendReceiveSynchronizer.IsRequestTimedOut = true;
				}
				else
				{
					responseBody = sendReceiveSynchronizer.Resp;
				}
			}
			tracker = sendReceiveSynchronizer.Tracker;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int, ResponseBody>("DistributedCache.RoutingClient", "SendMultiMessageAndWaitForAll: End : msgId = {0} - Exiting with the response: {1}", multiReqMsg.MultiReqId, responseBody);
			}
			return responseBody;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000042D4 File Offset: 0x000024D4
		private void InitializeLookupTable(string cacheName, DataCacheReadyRetryPolicy policy)
		{
			int num = 0;
			for (;;)
			{
				this._lookupTable = this._clientDRM.GetLookupTable(cacheName);
				if (this._lookupTable == null)
				{
					num++;
					if (num > policy.RetryCount)
					{
						break;
					}
					int num2 = Math.Min(num, policy.MaximumRetryIntervalInSeconds);
					Thread.Sleep(num2 * 1000);
					this._clientDRM.EndRefreshLookupTable(this._clientDRM.BeginRefreshLookupTable(null, null), true);
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteInfo("DistributedCache.RoutingClient", "InitializeLookupTable: Retrying {0} times", new object[] { num });
					}
				}
				if (this._lookupTable != null)
				{
					return;
				}
			}
			throw DataCache.NewException(9);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000437C File Offset: 0x0000257C
		private static void RoutingClientCallback(ResponseBody resp, RequestBody request, object session)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.RoutingClient", "RoutingClientCallback: Begin Processing Response", new object[0]);
			}
			SendReceiveSynchronizer sendReceiveSynchronizer = (SendReceiveSynchronizer)session;
			sendReceiveSynchronizer.ProcessResponse(resp, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000043B5 File Offset: 0x000025B5
		public void Dispose()
		{
			this._clientDRM.Close(this._factoryConfig.UseConnectionPool);
		}

		// Token: 0x0400005E RID: 94
		private const string _logSource = "DistributedCache.RoutingClient";

		// Token: 0x0400005F RID: 95
		private readonly DataCacheFactoryConfiguration _factoryConfig;

		// Token: 0x04000060 RID: 96
		private readonly ClientDRM _clientDRM;

		// Token: 0x04000061 RID: 97
		private ReplicaSetMap _lookupTable;
	}
}
