using System;
using System.Net;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x02000464 RID: 1124
	public interface IHttpResponseMessage
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002317 RID: 8983
		// (set) Token: 0x06002318 RID: 8984
		long ResponseContentLength { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002319 RID: 8985
		// (set) Token: 0x0600231A RID: 8986
		string ResponseContentType { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600231B RID: 8987
		// (set) Token: 0x0600231C RID: 8988
		HttpStatusCode ResponseStatusCode { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600231D RID: 8989
		// (set) Token: 0x0600231E RID: 8990
		string ResponseLocation { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600231F RID: 8991
		// (set) Token: 0x06002320 RID: 8992
		string ResponseStatusDescription { get; set; }

		// Token: 0x06002321 RID: 8993
		void SetResponseHeader(string headerKey, string headerValue);

		// Token: 0x06002322 RID: 8994
		void AddResponseHeader(string headerKey, string headerValue);
	}
}
