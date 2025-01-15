using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A4 RID: 932
	internal class SendReceiveSynchronizer
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x00065B4B File Offset: 0x00063D4B
		internal bool IsRequestTrackingEnabled
		{
			get
			{
				return this._startTime != long.MaxValue;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x00065B61 File Offset: 0x00063D61
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x00065B69 File Offset: 0x00063D69
		internal bool IsRequestTimedOut
		{
			get
			{
				return this._isRequestTimedOut;
			}
			set
			{
				this._isRequestTimedOut = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x00065B72 File Offset: 0x00063D72
		// (set) Token: 0x06002116 RID: 8470 RVA: 0x00065B7A File Offset: 0x00063D7A
		internal OperationResult OperationResult { get; private set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x00065B83 File Offset: 0x00063D83
		public LightWeightEventMonitorBased Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x00065B8B File Offset: 0x00063D8B
		public ResponseBody Resp
		{
			get
			{
				return this._resp;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x00065B93 File Offset: 0x00063D93
		internal IRequestTracker Tracker
		{
			get
			{
				return this._tracker;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00065B9B File Offset: 0x00063D9B
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x00065BA3 File Offset: 0x00063DA3
		internal long RequestSentTime
		{
			get
			{
				return this.requestSentTime;
			}
			set
			{
				this.requestSentTime = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00065BAC File Offset: 0x00063DAC
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00065BB4 File Offset: 0x00063DB4
		internal long ResponseReceivedTime
		{
			get
			{
				return this.responseReceivedTime;
			}
			set
			{
				this.responseReceivedTime = value;
			}
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x00065BBD File Offset: 0x00063DBD
		public SendReceiveSynchronizer(RequestBody req, bool enableRequestTracker, ServiceCallback callback)
		{
			this._req = req;
			this._startTime = (enableRequestTracker ? Stopwatch.GetTimestamp() : long.MaxValue);
			this._callback = callback;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00065BF7 File Offset: 0x00063DF7
		public SendReceiveSynchronizer(RequestBody req, bool enableRequestTracker)
		{
			this._req = req;
			this._handle = new LightWeightEventMonitorBased();
			this._startTime = (enableRequestTracker ? Stopwatch.GetTimestamp() : long.MaxValue);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00065C35 File Offset: 0x00063E35
		public SendReceiveSynchronizer()
			: this(null, false)
		{
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00065C40 File Offset: 0x00063E40
		internal virtual void AssociateTracker(RequestTrackerOnPrimary reqTrackerOnPrimary)
		{
			if (this.IsRequestTrackingEnabled)
			{
				RequestTimeTracker requestTimeTracker = new RequestTimeTracker();
				this.StopRequestTimer(requestTimeTracker);
				requestTimeTracker.PrimaryTracker = reqTrackerOnPrimary;
				this._tracker = requestTimeTracker;
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00065C70 File Offset: 0x00063E70
		internal virtual void ProcessResponse(ResponseBody resp, object context)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.Synchronizer", "{0} - ProcessResponse: Invoking the callback", new object[] { resp });
			}
			if (this._responseList != null)
			{
				resp.ResponseList = this._responseList;
			}
			this._callback(resp, this._req, context);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00065CC8 File Offset: 0x00063EC8
		internal virtual void ProcessResponse(ResponseBody resp, IRequestTracker tracker)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.Synchronizer", "{0} - ProcessResponse: Unblocking the waiting thread.", new object[] { resp });
			}
			if (this._responseList != null)
			{
				resp.ResponseList = this._responseList;
			}
			this._resp = resp;
			if (this.IsRequestTrackingEnabled)
			{
				RequestTimeTracker requestTimeTracker = tracker as RequestTimeTracker;
				if (requestTimeTracker != null)
				{
					this.StopRequestTimer(requestTimeTracker);
					this._tracker = requestTimeTracker;
				}
			}
			this._handle.Set();
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<ResponseBody>("DistributedCache.Synchronizer", "{0} - ProcessResponse: End Processing Response", resp);
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00065D56 File Offset: 0x00063F56
		internal void SetOperationResult(OperationResult result)
		{
			this.OperationResult = result;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00065D60 File Offset: 0x00063F60
		internal bool TryComplete(ResponseBody resp)
		{
			bool flag2;
			lock (this._lockObject)
			{
				if (this._responseList == null)
				{
					this._responseList = new List<ResponseBody>();
				}
				this._responseList.Add(resp);
				if (!resp.Continue)
				{
					this._countResponses = resp.MultiPartResponseCount;
				}
				if (this._responseList.Count == this._countResponses)
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00065DE8 File Offset: 0x00063FE8
		private void StopRequestTimer(RequestTimeTracker timeTracker)
		{
			long num = Utility.StopwatchTicksToSystemTicks(Stopwatch.GetTimestamp() - this._startTime);
			timeTracker.ClientEndToEnd = TimeSpan.FromTicks(num);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00065E14 File Offset: 0x00064014
		public override string ToString()
		{
			object obj = null;
			if (this.OperationResult != null)
			{
				obj = this.OperationResult.ResultContext;
			}
			return string.Format(CultureInfo.InvariantCulture, "Context:{0}", new object[] { (obj != null) ? obj.ToString() : "null" });
		}

		// Token: 0x04001519 RID: 5401
		private const long _invalidStartTime = 9223372036854775807L;

		// Token: 0x0400151A RID: 5402
		private const string LogSource = "DistributedCache.Synchronizer";

		// Token: 0x0400151B RID: 5403
		private object _lockObject = new object();

		// Token: 0x0400151C RID: 5404
		private RequestBody _req;

		// Token: 0x0400151D RID: 5405
		private ServiceCallback _callback;

		// Token: 0x0400151E RID: 5406
		private LightWeightEventMonitorBased _handle;

		// Token: 0x0400151F RID: 5407
		private long _startTime;

		// Token: 0x04001520 RID: 5408
		private ResponseBody _resp;

		// Token: 0x04001521 RID: 5409
		private IRequestTracker _tracker;

		// Token: 0x04001522 RID: 5410
		private int _countResponses;

		// Token: 0x04001523 RID: 5411
		private List<ResponseBody> _responseList;

		// Token: 0x04001524 RID: 5412
		internal bool _isRequestTimedOut;

		// Token: 0x04001525 RID: 5413
		private long requestSentTime;

		// Token: 0x04001526 RID: 5414
		private long responseReceivedTime;
	}
}
