using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000254 RID: 596
	internal abstract class DRM : IDisposable
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x0003EAC0 File Offset: 0x0003CCC0
		internal DRM(string actionString, string id, SimpleSendReceiveModule sendReceiveModule)
		{
			this._logSource = "DistributedCache.DRM." + id;
			this._actionString = actionString;
			this._retryRequestCallback = new AsyncCallback(this.RetryRequest);
			this._sendReceiveModule = sendReceiveModule;
			this._cachedEndpointID = new Hashtable();
			int num = ConfigManager.TIMEOUT / 4;
			int num2 = 3000;
			if (num < num2)
			{
				num = num2;
			}
			this._hardComplaintTimeout = new TimeSpan(0, 0, 0, 0, num);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "DRM instance created.");
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0003EB4B File Offset: 0x0003CD4B
		public DOMDelegate LocalDomCallback
		{
			get
			{
				return this._localDOMCallback;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0003EB53 File Offset: 0x0003CD53
		public LocalDRMCallback DrmCallback
		{
			get
			{
				return this._asyncResponseCallback;
			}
		}

		// Token: 0x06001404 RID: 5124
		internal abstract bool IsLocalRequest(EndpointID address);

		// Token: 0x06001405 RID: 5125 RVA: 0x0003EB5B File Offset: 0x0003CD5B
		internal IAsyncResult BeginRefreshLookupTable(AsyncCallback callback, object state)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Begin refresh lookup table.", new object[0]);
			}
			return this._casClient.BeginRefreshAll(this._hardComplaintTimeout, callback, state);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0003EB90 File Offset: 0x0003CD90
		internal ServiceReplicaSet EndRefreshLookupTable(IAsyncResult ar, bool refreshAll)
		{
			ServiceReplicaSet serviceReplicaSet = null;
			if (refreshAll)
			{
				this._casClient.EndRefreshAll(ar);
			}
			else
			{
				serviceReplicaSet = this._casClient.EndRefresh(ar);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "End refresh lookup table. Result - [{0}].", new object[] { serviceReplicaSet });
			}
			return serviceReplicaSet;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0003EBE4 File Offset: 0x0003CDE4
		internal ReplicaSetMap GetLookupTable(string cacheName)
		{
			ReplicaSetMap replicaSetMap = null;
			try
			{
				replicaSetMap = this._casClient[cacheName];
			}
			catch (LookupNamespaceNotFoundException ex)
			{
				this._casClient.TriggerRefreshAll();
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this._logSource, "Lookup table not found for [{0}] - {1}.", new object[] { cacheName, ex });
				}
			}
			return replicaSetMap;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0003EC4C File Offset: 0x0003CE4C
		internal ServiceReplicaSet GetPartitionConfig(ReplicaSetMap table, string cacheName, int regionId, ref ErrStatus error)
		{
			ServiceReplicaSet serviceReplicaSet;
			try
			{
				serviceReplicaSet = table.Lookup(regionId);
			}
			catch (LookupKeyNotFoundException)
			{
				this._casClient.TriggerRefresh(cacheName, regionId, null);
				error = ErrStatus.REGIONID_NOT_FOUND;
				return null;
			}
			catch (LookupNamespaceNotFoundException)
			{
				this._casClient.TriggerRefresh(cacheName, regionId, null);
				error = ErrStatus.NAMED_CACHE_DOES_NOT_EXIST;
				return null;
			}
			if (!serviceReplicaSet.IsUsable)
			{
				this._casClient.TriggerRefresh(cacheName, regionId, serviceReplicaSet);
				error = ErrStatus.NOT_PRIMARY;
				return null;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int, ServiceReplicaSet>(this._logSource, "Config for [{0},{1}] is [{2}].", cacheName, regionId, serviceReplicaSet);
			}
			return serviceReplicaSet;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0003ECF0 File Offset: 0x0003CEF0
		internal EndpointID GetEndpointID(string uriString)
		{
			EndpointID endpointID = (EndpointID)this._cachedEndpointID[uriString];
			if (endpointID == null)
			{
				endpointID = new EndpointID(uriString);
				if (Monitor.TryEnter(this._cachedEndpointID.SyncRoot))
				{
					this._cachedEndpointID[uriString] = endpointID;
				}
			}
			return endpointID;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		internal ServiceReplicaSet GetPartitionConfig(PartitionId partitionId, ref ErrStatus error)
		{
			ReplicaSetMap lookupTable = this.GetLookupTable(partitionId.ServiceNamespace);
			if (lookupTable == null)
			{
				return null;
			}
			return this.GetPartitionConfig(lookupTable, partitionId.ServiceNamespace, partitionId.LowKey, ref error);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003ED70 File Offset: 0x0003CF70
		internal EndpointID GetPrimaryEndPoint(string cacheName, string regionName, int systemRegionCount)
		{
			PartitionId partitionId = null;
			int regionId = Utility.GetRegionId(regionName, systemRegionCount);
			bool flag = this.TryGetPartitionId(cacheName, regionId, out partitionId);
			if (flag)
			{
				return this.GetPrimaryEndPoint(partitionId);
			}
			return null;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0003EDA0 File Offset: 0x0003CFA0
		internal string GetPrimaryEndPointString(string cacheName, string regionName, int systemRegionCount)
		{
			PartitionId partitionId = null;
			int regionId = Utility.GetRegionId(regionName, systemRegionCount);
			bool flag = this.TryGetPartitionId(cacheName, regionId, out partitionId);
			if (flag)
			{
				return this.GetPrimaryEndPointString(partitionId);
			}
			return null;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0003EDD0 File Offset: 0x0003CFD0
		internal EndpointID GetPrimaryEndPoint(PartitionId partitionId)
		{
			string primaryEndPointString = this.GetPrimaryEndPointString(partitionId);
			if (primaryEndPointString == null)
			{
				return null;
			}
			return this.GetEndpointID(primaryEndPointString);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
		internal string GetPrimaryEndPointString(PartitionId partitionId)
		{
			ErrStatus errStatus = ErrStatus.UNINITIALIZED_ERROR;
			ServiceReplicaSet partitionConfig = this.GetPartitionConfig(partitionId, ref errStatus);
			if (partitionConfig == null)
			{
				return null;
			}
			return this.PrimaryEndPointString(partitionConfig);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003EE1C File Offset: 0x0003D01C
		internal ReadOnlyCollection<PartitionId> GetPartitionIdList(string cacheName)
		{
			ReadOnlyCollection<PartitionId> readOnlyCollection;
			try
			{
				readOnlyCollection = this._casClient[cacheName].GetPartitionIds();
			}
			catch (LookupException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this._logSource, "Cache lookup list not found for [{0}] - {1}", new object[] { cacheName, ex });
				}
				readOnlyCollection = null;
			}
			return readOnlyCollection;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0003EE7C File Offset: 0x0003D07C
		internal bool TryGetPartitionId(string cacheName, int regionkey, out PartitionId partitionId)
		{
			partitionId = null;
			bool flag;
			try
			{
				partitionId = this._casClient[cacheName].GetPartitionId(regionkey);
				flag = true;
			}
			catch (LookupException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this._logSource, "Cache lookup list not found for [{0}] - {1}", new object[] { cacheName, ex });
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0003EEE4 File Offset: 0x0003D0E4
		internal PartitionId GetPartitionId(string cacheName, int regionkey)
		{
			PartitionId partitionId = null;
			bool flag = this.TryGetPartitionId(cacheName, regionkey, out partitionId);
			if (flag)
			{
				return partitionId;
			}
			return null;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0003EF04 File Offset: 0x0003D104
		internal virtual string PrimaryEndPointString(ServiceReplicaSet config)
		{
			return config.Primary;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0003EF0C File Offset: 0x0003D10C
		internal void RaiseComplaint(PartitionId partitionId, EndpointID endpoint)
		{
			ErrStatus errStatus = ErrStatus.UNINITIALIZED_ERROR;
			ServiceReplicaSet partitionConfig = this.GetPartitionConfig(partitionId, ref errStatus);
			if (partitionConfig == null)
			{
				this._casClient.TriggerRefreshAll();
				return;
			}
			if (partitionConfig.Primary == endpoint.UriString)
			{
				this._casClient.TriggerRefresh(partitionId.ServiceNamespace, partitionId.LowKey, partitionConfig);
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected virtual bool ShouldAssignPrimaryTrackerData(RequestBody requestBody)
		{
			return requestBody.PrimaryRequestTracking;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void AssignPrimaryTrackerData(RequestBody body, object context)
		{
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void BeforeSend(RequestBody body)
		{
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void AfterSend(RequestBody body)
		{
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void ProcessPartialResponse(ResponseBody resp, object session)
		{
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0003EF68 File Offset: 0x0003D168
		internal OperationResult SendRequest(EndpointID address, RequestBody request)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody, EndpointID>(this._logSource, "{0} - Sending request to {1}.", request, address);
			}
			this.BeforeSend(request);
			request.Action = this._actionString;
			SimpleSendReceiveModule sendReceiveModule = this._sendReceiveModule;
			if (sendReceiveModule == null)
			{
				return OperationResult.InstanceClosed;
			}
			OperationResult operationResult = sendReceiveModule.SendMessage(address, request, new ServiceCallback(this.ResponseCallback));
			this.AfterSend(request);
			request.InTransit = operationResult.IsSuccess;
			if (operationResult.IsConnectionFailedOrOpening)
			{
				if (request.RegionName != null)
				{
					this._casClient.TriggerRefresh(request.CacheName, request.RegionID, request.Config);
				}
				else
				{
					this._casClient.TriggerRefreshAll();
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody, OperationResult>(this._logSource, "{0} - Send result - {1}.", request, operationResult);
			}
			return operationResult;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0003F030 File Offset: 0x0003D230
		internal ResponseBody GetPartialResponse(RequestBody request)
		{
			SimpleSendReceiveModule sendReceiveModule = this._sendReceiveModule;
			if (sendReceiveModule != null)
			{
				sendReceiveModule.MarkRequestCompleted(request);
			}
			return request.GetPartialResponse();
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0003F058 File Offset: 0x0003D258
		internal ResponseBody GetTimeoutResponse(MultiRequest request)
		{
			SimpleSendReceiveModule sendReceiveModule = this._sendReceiveModule;
			if (sendReceiveModule != null)
			{
				sendReceiveModule.MarkRequestCompleted(request.MultiReqId);
			}
			return request.GetErrorResponse(ErrStatus.TIMEOUT);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0003F083 File Offset: 0x0003D283
		internal virtual ResponseBody ProcessRequest(RequestBody request, ServiceCallback callback, object session)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody>(this._logSource, "{0} - Starting to process.", request);
			}
			request.Router = this;
			request.Session = session;
			request.Caller = callback;
			this.ProcessRequest(request);
			return request.GetPendingResponse();
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0003F0C0 File Offset: 0x0003D2C0
		internal ResponseBody ProcessMultiRequest(MultiRequest multiRequest, ServiceCallback callback, object session)
		{
			int num = 0;
			foreach (RequestBody requestBody in multiRequest.Requests)
			{
				requestBody.Router = this;
				requestBody.Table = this.GetLookupTable(requestBody.CacheName);
				requestBody.FindDestinations();
				num++;
			}
			multiRequest.CompletionCallback = callback;
			multiRequest.ProgressChanged += this.BulkGetProgressChanged;
			multiRequest.BeginExecution(new AsyncCallback(this.BulkGetCallback), new MultiRequestContext(multiRequest, session, num));
			return multiRequest.GetPendingResponse();
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0003F168 File Offset: 0x0003D368
		private void ProcessRequest(RequestBody request)
		{
			if (request.IsSimpleRequest())
			{
				DRM.ProcessSimpleRequest(request);
				return;
			}
			this.ProcessBulkRequest(request);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0003F180 File Offset: 0x0003D380
		private static void ProcessSimpleRequest(RequestBody request)
		{
			request.FindDestinations();
			request.Send();
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0003F190 File Offset: 0x0003D390
		private void ProcessBulkRequest(RequestBody request)
		{
			if (request.Req == ReqType.CLEAR_CACHE)
			{
				ReadOnlyCollection<PartitionId> partitionIdList = this.GetPartitionIdList(request.CacheName);
				List<RequestBody> clearCacheRequests = this.GetClearCacheRequests(request, partitionIdList);
				MultiRequest multiRequest = new MultiRequest(clearCacheRequests, new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT), true);
				multiRequest.BeginExecution(new AsyncCallback(this.ClearCacheCallback), new MultiRequestContext(request, multiRequest));
				return;
			}
			if (request.Req == ReqType.CACHE_BULK_GET)
			{
				List<RequestBody> bulkGetRequests = this.GetBulkGetRequests(request);
				MultiRequest multiRequest2 = new MultiRequest(bulkGetRequests, new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT), false);
				multiRequest2.MultiReqId = request.ClientReqId;
				multiRequest2.CompletionCallback = request.Caller;
				multiRequest2.ProgressChanged += this.BulkGetProgressChanged;
				multiRequest2.BeginExecution(new AsyncCallback(this.BulkGetCallback), new MultiRequestContext(multiRequest2, request.Session, bulkGetRequests.Count));
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0003F26C File Offset: 0x0003D46C
		private List<RequestBody> GetClearCacheRequests(RequestBody request, ReadOnlyCollection<PartitionId> partitionList)
		{
			List<RequestBody> list = new List<RequestBody>(partitionList.Count);
			foreach (PartitionId partitionId in partitionList)
			{
				RequestBody partitionClearCacheRequest = this.GetPartitionClearCacheRequest(request, partitionId);
				list.Add(partitionClearCacheRequest);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "{0} - Child Request Created for req Id {1} ", new object[] { partitionClearCacheRequest, request.ID });
				}
			}
			return list;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0003F300 File Offset: 0x0003D500
		private List<RequestBody> GetBulkGetRequests(RequestBody request)
		{
			List<RequestBody> list = new List<RequestBody>(request.Keys.Length);
			foreach (Key key in request.Keys)
			{
				RequestBody routedRequest = this.GetRoutedRequest(request, ReqType.GET);
				routedRequest.Key = key;
				routedRequest.RegionName = RegionNameProvider.GetSystemRegionName(key.StringValue, routedRequest.SystemRegionCount);
				routedRequest.Table = request.Table;
				routedRequest.FindDestinations();
				list.Add(routedRequest);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "{0} - Child Request Created for req Id {1} ", new object[] { routedRequest, request.ID });
				}
			}
			return list;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
		private RequestBody GetRoutedRequest(RequestBody request, ReqType reqType)
		{
			return new RequestBody(reqType)
			{
				CacheName = request.CacheName,
				Fwd = ForwardingType.Routed,
				Router = this,
				SystemRegionCount = request.SystemRegionCount
			};
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0003F3F4 File Offset: 0x0003D5F4
		private RequestBody GetPartitionClearCacheRequest(RequestBody request, PartitionId partition)
		{
			RequestBody routedRequest = this.GetRoutedRequest(request, ReqType.PARTITION_CLEAR_CACHE);
			routedRequest.Destination = this.GetPrimaryEndPoint(partition);
			routedRequest.ValObject = partition;
			routedRequest.PartitionId = partition;
			return routedRequest;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0003F428 File Offset: 0x0003D628
		private void ClearCacheCallback(IAsyncResult result)
		{
			MultiRequestContext multiRequestContext = (MultiRequestContext)result.AsyncState;
			ResponseBody responseBody = new ResponseBody(AckNack.Ack, multiRequestContext.Request.ClientReqId);
			try
			{
				Dictionary<RequestBody, ResponseBody> dictionary = multiRequestContext.MRequest.EndExecution(result);
				foreach (ResponseBody responseBody2 in dictionary.Values)
				{
					if (responseBody2.Ack == AckNack.Nack)
					{
						responseBody.ResponseCode = responseBody2.ResponseCode;
						responseBody.Ack = AckNack.Nack;
						break;
					}
				}
			}
			catch (TimeoutException)
			{
				responseBody.ResponseCode = ErrStatus.TIMEOUT;
				responseBody.Ack = AckNack.Nack;
			}
			this.ProcessResponse(multiRequestContext.Request, responseBody, null);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003F4EC File Offset: 0x0003D6EC
		private void BulkGetProgressChanged(RequestBody request, ResponseBody response, object state)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "ProgressChangedCallback: Begin Processing Response resp {0}", new object[] { response });
			}
			MultiRequestContext multiRequestContext = (MultiRequestContext)state;
			int num = Interlocked.Decrement(ref multiRequestContext.PendingRequestCount);
			response.Continue = num != 0;
			if (num == 0)
			{
				response.MultiPartResponseCount = multiRequestContext.TotalRequestCount;
			}
			response.ClientReqId = multiRequestContext.MRequest.MultiReqId;
			response.ServiceReqId = -1;
			Utility.AddRequestItemInfo(request, response);
			this.ProcessPartialResponse(response, multiRequestContext.Session);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0003F578 File Offset: 0x0003D778
		private void BulkGetCallback(IAsyncResult result)
		{
			MultiRequestContext multiRequestContext = (MultiRequestContext)result.AsyncState;
			ResponseBody responseBody = new ResponseBody(AckNack.Ack, multiRequestContext.MRequest.MultiReqId);
			try
			{
				multiRequestContext.MRequest.EndExecution(result);
			}
			catch (TimeoutException)
			{
				responseBody.ResponseCode = ErrStatus.TIMEOUT;
				responseBody.Ack = AckNack.Nack;
			}
			responseBody.SendNotRequired = multiRequestContext.PendingRequestCount == 0;
			multiRequestContext.MRequest.CompletionCallback(responseBody, null, multiRequestContext.Session);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0003F5FC File Offset: 0x0003D7FC
		internal void ProcessResponse(RequestBody request, ResponseBody response)
		{
			request.InTransit = false;
			switch (request.ProcessResponse(response))
			{
			case ProcessResponseResult.RequestPartiallyCompleted:
				return;
			case ProcessResponseResult.RequestFullyCompleted:
				request.Caller(response, request, request.Session);
				return;
			case ProcessResponseResult.RetryRequired:
				this.RequestTableUpdateAndRetry(request.GetChildRequest());
				return;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0003F655 File Offset: 0x0003D855
		internal void ProcessResponse(RequestBody request, ResponseBody response, Func<object> contextDelegate)
		{
			if (this.ShouldAssignPrimaryTrackerData(request))
			{
				this.AssignPrimaryTrackerData(request, (contextDelegate == null) ? null : contextDelegate());
			}
			this.ProcessResponse(request, response);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0003F67C File Offset: 0x0003D87C
		private void RequestTableUpdateAndRetry(RequestBody request)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<RequestBody>(this._logSource, "{0} - Requesting table update and retrying.", request);
			}
			if (request.RegionName != null)
			{
				this._casClient.BeginRefresh(request.CacheName, request.RegionID, request.Config, this._hardComplaintTimeout, this._retryRequestCallback, request);
				return;
			}
			this.BeginRefreshLookupTable(this._retryRequestCallback, request);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
		private void RetryRequest(IAsyncResult ar)
		{
			RequestBody requestBody = ar.AsyncState as RequestBody;
			bool flag = requestBody.RegionName == null;
			ServiceReplicaSet serviceReplicaSet = this.EndRefreshLookupTable(ar, flag);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "{0} - Processing again. Config obtained after complaint - {1}", new object[] { requestBody, serviceReplicaSet });
			}
			if (requestBody.IsExplicitRoutedRequest())
			{
				requestBody.Send();
				return;
			}
			this.ProcessRequest(requestBody);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0003F750 File Offset: 0x0003D950
		internal void ResponseCallback(ResponseBody response, RequestBody request, object context)
		{
			if (response == null)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this._logSource, "Invalid Response got from remote DOM. response is null", new object[0]);
				}
				return;
			}
			if (context == null)
			{
				this.ProcessResponse(request, response);
				return;
			}
			IReplyContext replyContext = context as IReplyContext;
			this.ProcessResponse(request, response, new Func<object>(new DRM.RequestTrackerExtractor
			{
				Context = replyContext
			}.GetRequestTracker));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000036A9 File Offset: 0x000018A9
		internal virtual void Close(bool pool)
		{
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0003F7B3 File Offset: 0x0003D9B3
		public virtual void Dispose()
		{
			this.Close(false);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000C08 RID: 3080
		private string _actionString;

		// Token: 0x04000C09 RID: 3081
		private AsyncCallback _retryRequestCallback;

		// Token: 0x04000C0A RID: 3082
		private string _logSource;

		// Token: 0x04000C0B RID: 3083
		protected ServiceResolverBase _casClient;

		// Token: 0x04000C0C RID: 3084
		private TimeSpan _hardComplaintTimeout;

		// Token: 0x04000C0D RID: 3085
		protected SimpleSendReceiveModule _sendReceiveModule;

		// Token: 0x04000C0E RID: 3086
		private Hashtable _cachedEndpointID;

		// Token: 0x04000C0F RID: 3087
		protected DOMDelegate _localDOMCallback;

		// Token: 0x04000C10 RID: 3088
		protected LocalDRMCallback _asyncResponseCallback;

		// Token: 0x02000255 RID: 597
		private sealed class RequestTrackerExtractor
		{
			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003F7C2 File Offset: 0x0003D9C2
			// (set) Token: 0x06001430 RID: 5168 RVA: 0x0003F7CA File Offset: 0x0003D9CA
			public IReplyContext Context { get; set; }

			// Token: 0x06001431 RID: 5169 RVA: 0x0003F7D3 File Offset: 0x0003D9D3
			public object GetRequestTracker()
			{
				return this.Context.GetRequestTracker<RequestTrackerOnPrimary>("PrimaryTracker");
			}
		}
	}
}
