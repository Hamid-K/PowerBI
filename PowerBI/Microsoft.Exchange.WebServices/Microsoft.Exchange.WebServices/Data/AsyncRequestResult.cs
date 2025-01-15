using System;
using System.Threading;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000EA RID: 234
	internal class AsyncRequestResult : IAsyncResult
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x00028388 File Offset: 0x00027388
		public AsyncRequestResult(ServiceRequestBase serviceRequest, IEwsHttpWebRequest webRequest, IAsyncResult webAsyncResult, object asyncState)
		{
			EwsUtilities.ValidateParam(serviceRequest, "serviceRequest");
			EwsUtilities.ValidateParam(webRequest, "webRequest");
			EwsUtilities.ValidateParam(webAsyncResult, "webAsyncResult");
			this.ServiceRequest = serviceRequest;
			this.WebAsyncResult = webAsyncResult;
			this.WebRequest = webRequest;
			this.AsyncState = asyncState;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x000283D9 File Offset: 0x000273D9
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x000283E1 File Offset: 0x000273E1
		public ServiceRequestBase ServiceRequest { get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x000283EA File Offset: 0x000273EA
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x000283F2 File Offset: 0x000273F2
		public IEwsHttpWebRequest WebRequest { get; private set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000283FB File Offset: 0x000273FB
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00028403 File Offset: 0x00027403
		public IAsyncResult WebAsyncResult { get; private set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002840C File Offset: 0x0002740C
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00028414 File Offset: 0x00027414
		public object AsyncState { get; private set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002841D File Offset: 0x0002741D
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.WebAsyncResult.AsyncWaitHandle;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0002842A File Offset: 0x0002742A
		public bool CompletedSynchronously
		{
			get
			{
				return this.WebAsyncResult.CompletedSynchronously;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00028437 File Offset: 0x00027437
		public bool IsCompleted
		{
			get
			{
				return this.WebAsyncResult.IsCompleted;
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00028444 File Offset: 0x00027444
		public static T ExtractServiceRequest<T>(ExchangeService exchangeService, IAsyncResult asyncResult) where T : SimpleServiceRequestBase
		{
			EwsUtilities.ValidateParam(asyncResult, "asyncResult");
			AsyncRequestResult asyncRequestResult = asyncResult as AsyncRequestResult;
			if (asyncRequestResult == null)
			{
				throw new ArgumentException(Strings.InvalidAsyncResult, "asyncResult");
			}
			if (asyncRequestResult.ServiceRequest == null)
			{
				throw new ArgumentException(Strings.InvalidAsyncResult, "asyncResult");
			}
			if (!object.ReferenceEquals(asyncRequestResult.ServiceRequest.Service, exchangeService))
			{
				throw new ArgumentException(Strings.InvalidAsyncResult, "asyncResult");
			}
			T t = asyncRequestResult.ServiceRequest as T;
			if (t == null)
			{
				throw new ArgumentException(Strings.InvalidAsyncResult, "asyncResult");
			}
			return t;
		}
	}
}
