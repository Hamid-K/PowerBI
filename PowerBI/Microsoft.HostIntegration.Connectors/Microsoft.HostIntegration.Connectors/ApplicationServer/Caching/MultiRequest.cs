using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000320 RID: 800
	internal class MultiRequest
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00057FC9 File Offset: 0x000561C9
		internal IEnumerable<RequestBody> Requests
		{
			get
			{
				return this._requests;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00057FD1 File Offset: 0x000561D1
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x00057FD9 File Offset: 0x000561D9
		internal int MultiReqId
		{
			get
			{
				return this._multiReqId;
			}
			set
			{
				this._multiReqId = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00057FE2 File Offset: 0x000561E2
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x00057FEA File Offset: 0x000561EA
		internal ServiceCallback CompletionCallback { get; set; }

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001CFF RID: 7423 RVA: 0x00057FF4 File Offset: 0x000561F4
		// (remove) Token: 0x06001D00 RID: 7424 RVA: 0x0005802C File Offset: 0x0005622C
		public event PartialResponseHandler ProgressChanged;

		// Token: 0x06001D01 RID: 7425 RVA: 0x00058061 File Offset: 0x00056261
		public MultiRequest(IEnumerable<RequestBody> requests, TimeSpan timespan, bool keepState, int reqId)
			: this(requests, timespan, keepState)
		{
			this._multiReqId = reqId;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00058074 File Offset: 0x00056274
		public MultiRequest(IEnumerable<RequestBody> requests, TimeSpan timespan, bool keepState)
		{
			int num = requests.Count<RequestBody>();
			this._requests = requests;
			this._timeSpan = timespan;
			this._keepState = keepState;
			this._pendingRequestCount = num;
			this.requestResponse = new Dictionary<RequestBody, ResponseBody>(this._pendingRequestCount);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000580C4 File Offset: 0x000562C4
		public IAsyncResult BeginExecution(AsyncCallback callback, object state)
		{
			this._asyncresult = new AsyncResult<Dictionary<RequestBody, ResponseBody>>(callback, state);
			this._timer = new Timer(new TimerCallback(this.OnTimerExpired), this, this._timeSpan, TimeSpan.FromMilliseconds(-1.0));
			foreach (RequestBody requestBody in this._requests)
			{
				requestBody.Caller = new ServiceCallback(this.OnResponseReceived);
				requestBody.Session = this;
				requestBody.Send();
			}
			return this._asyncresult;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00058168 File Offset: 0x00056368
		public Dictionary<RequestBody, ResponseBody> EndExecution(IAsyncResult result)
		{
			AsyncResult<Dictionary<RequestBody, ResponseBody>> asyncResult = (AsyncResult<Dictionary<RequestBody, ResponseBody>>)result;
			return asyncResult.EndInvoke();
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00058184 File Offset: 0x00056384
		private void OnResponseReceived(ResponseBody response, RequestBody request, object context)
		{
			if (this._keepState)
			{
				lock (this.requestResponse)
				{
					this.requestResponse[request] = response;
				}
			}
			if (this.ProgressChanged != null)
			{
				this.ProgressChanged(request, response, this._asyncresult.AsyncState);
			}
			if (Interlocked.Decrement(ref this._pendingRequestCount) == 0)
			{
				this._timer.Dispose();
				try
				{
					this._asyncresult.SetAsCompleted(this.requestResponse, false);
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00058230 File Offset: 0x00056430
		private void OnTimerExpired(object state)
		{
			try
			{
				if (this._pendingRequestCount != 0)
				{
					this._asyncresult.SetAsCompleted(new TimeoutException(), false);
				}
			}
			catch (InvalidOperationException)
			{
			}
			finally
			{
				this._timer.Dispose();
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00058288 File Offset: 0x00056488
		internal ResponseBody GetErrorResponse(ErrStatus error)
		{
			return new ResponseBody(AckNack.Nack)
			{
				ResponseCode = error,
				ClientReqId = this.MultiReqId
			};
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000582B0 File Offset: 0x000564B0
		internal ResponseBody GetPendingResponse()
		{
			return new ResponseBody(AckNack.Pending)
			{
				ClientReqId = this.MultiReqId
			};
		}

		// Token: 0x0400101A RID: 4122
		private int _multiReqId = -1;

		// Token: 0x0400101B RID: 4123
		private IEnumerable<RequestBody> _requests;

		// Token: 0x0400101C RID: 4124
		private AsyncResult<Dictionary<RequestBody, ResponseBody>> _asyncresult;

		// Token: 0x0400101D RID: 4125
		private TimeSpan _timeSpan;

		// Token: 0x0400101E RID: 4126
		private int _pendingRequestCount;

		// Token: 0x0400101F RID: 4127
		private Dictionary<RequestBody, ResponseBody> requestResponse;

		// Token: 0x04001020 RID: 4128
		private readonly bool _keepState;

		// Token: 0x04001021 RID: 4129
		private Timer _timer;
	}
}
