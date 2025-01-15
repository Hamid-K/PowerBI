using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000EB RID: 235
	internal class WebAsyncCallStateAnchor
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x000284EF File Offset: 0x000274EF
		public WebAsyncCallStateAnchor(ServiceRequestBase serviceRequest, IEwsHttpWebRequest webRequest, AsyncCallback asyncCallback, object asyncState)
		{
			EwsUtilities.ValidateParam(serviceRequest, "serviceRequest");
			EwsUtilities.ValidateParam(webRequest, "webRequest");
			this.ServiceRequest = serviceRequest;
			this.WebRequest = webRequest;
			this.AsyncCallback = asyncCallback;
			this.AsyncState = asyncState;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002852A File Offset: 0x0002752A
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00028532 File Offset: 0x00027532
		public ServiceRequestBase ServiceRequest { get; private set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002853B File Offset: 0x0002753B
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00028543 File Offset: 0x00027543
		public IEwsHttpWebRequest WebRequest { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002854C File Offset: 0x0002754C
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00028554 File Offset: 0x00027554
		public object AsyncState { get; private set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002855D File Offset: 0x0002755D
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00028565 File Offset: 0x00027565
		public AsyncCallback AsyncCallback { get; private set; }
	}
}
