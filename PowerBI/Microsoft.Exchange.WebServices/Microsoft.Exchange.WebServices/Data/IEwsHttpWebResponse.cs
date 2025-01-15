using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000CB RID: 203
	internal interface IEwsHttpWebResponse : IDisposable
	{
		// Token: 0x06000918 RID: 2328
		void Close();

		// Token: 0x06000919 RID: 2329
		Stream GetResponseStream();

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600091A RID: 2330
		string ContentEncoding { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600091B RID: 2331
		string ContentType { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600091C RID: 2332
		WebHeaderCollection Headers { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600091D RID: 2333
		Uri ResponseUri { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600091E RID: 2334
		HttpStatusCode StatusCode { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600091F RID: 2335
		string StatusDescription { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000920 RID: 2336
		Version ProtocolVersion { get; }
	}
}
