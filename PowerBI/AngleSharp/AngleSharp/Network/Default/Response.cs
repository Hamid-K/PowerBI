using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000AF RID: 175
	public sealed class Response : IResponse, IDisposable
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0001FB51 File Offset: 0x0001DD51
		public Response()
		{
			this.Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.StatusCode = HttpStatusCode.Accepted;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001FB74 File Offset: 0x0001DD74
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		public HttpStatusCode StatusCode { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001FB85 File Offset: 0x0001DD85
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001FB8D File Offset: 0x0001DD8D
		public Url Address { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001FB96 File Offset: 0x0001DD96
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001FB9E File Offset: 0x0001DD9E
		public IDictionary<string, string> Headers { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001FBA7 File Offset: 0x0001DDA7
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001FBAF File Offset: 0x0001DDAF
		public Stream Content { get; set; }

		// Token: 0x0600052C RID: 1324 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		void IDisposable.Dispose()
		{
			Stream content = this.Content;
			if (content != null)
			{
				content.Dispose();
			}
			this.Headers.Clear();
		}
	}
}
