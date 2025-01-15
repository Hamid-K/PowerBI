using System;
using System.Collections.Generic;
using System.IO;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000AD RID: 173
	public sealed class Request : IRequest
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0001FA70 File Offset: 0x0001DC70
		public Request()
		{
			this.Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001FA88 File Offset: 0x0001DC88
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public HttpMethod Method { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001FA99 File Offset: 0x0001DC99
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0001FAA1 File Offset: 0x0001DCA1
		public Url Address { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001FAAA File Offset: 0x0001DCAA
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0001FAB2 File Offset: 0x0001DCB2
		public IDictionary<string, string> Headers { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001FABB File Offset: 0x0001DCBB
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0001FAC3 File Offset: 0x0001DCC3
		public Stream Content { get; set; }
	}
}
