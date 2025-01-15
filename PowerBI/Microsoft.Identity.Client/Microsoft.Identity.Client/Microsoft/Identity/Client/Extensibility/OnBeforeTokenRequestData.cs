using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x0200029B RID: 667
	public sealed class OnBeforeTokenRequestData
	{
		// Token: 0x0600193F RID: 6463 RVA: 0x00052DA7 File Offset: 0x00050FA7
		public OnBeforeTokenRequestData(IDictionary<string, string> bodyParameters, IDictionary<string, string> headers, Uri requestUri, CancellationToken cancellationToken)
		{
			this.BodyParameters = bodyParameters;
			this.Headers = headers;
			this.RequestUri = requestUri;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00052DCC File Offset: 0x00050FCC
		public IDictionary<string, string> BodyParameters { get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00052DD4 File Offset: 0x00050FD4
		public IDictionary<string, string> Headers { get; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00052DDC File Offset: 0x00050FDC
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x00052DE4 File Offset: 0x00050FE4
		public Uri RequestUri { get; set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00052DED File Offset: 0x00050FED
		public CancellationToken CancellationToken { get; }
	}
}
