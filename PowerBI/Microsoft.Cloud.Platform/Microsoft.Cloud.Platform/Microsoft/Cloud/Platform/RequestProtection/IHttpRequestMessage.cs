using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x02000462 RID: 1122
	public interface IHttpRequestMessage
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600230C RID: 8972
		IPEndPoint ClientEndpoint { get; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600230D RID: 8973
		string RequestContentType { get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600230E RID: 8974
		IDictionary<string, IEnumerable<string>> RequestHeaders { get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600230F RID: 8975
		Uri RequestUri { get; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002310 RID: 8976
		long RequestLength { get; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002311 RID: 8977
		HttpMethod Method { get; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002312 RID: 8978
		IDictionary<string, IEnumerable<string>> QueryString { get; }

		// Token: 0x06002313 RID: 8979
		IDictionary<string, IEnumerable<string>> GetFormData();

		// Token: 0x06002314 RID: 8980
		IAsyncResult BeginGetClientCertificate(AsyncCallback asyncCallback, object state);

		// Token: 0x06002315 RID: 8981
		X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult);

		// Token: 0x06002316 RID: 8982
		IEnumerable<string> GetRequestHeader(string headerKey);
	}
}
