using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x0200001D RID: 29
	internal class HttpUnsortedRequest
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000043BA File Offset: 0x000025BA
		public HttpUnsortedRequest()
		{
			this.HttpHeaders = new HttpUnsortedHeaders();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000043CD File Offset: 0x000025CD
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000043D5 File Offset: 0x000025D5
		public HttpMethod Method { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000043DE File Offset: 0x000025DE
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000043E6 File Offset: 0x000025E6
		public string RequestUri { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000043EF File Offset: 0x000025EF
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000043F7 File Offset: 0x000025F7
		public Version Version { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004400 File Offset: 0x00002600
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004408 File Offset: 0x00002608
		public HttpHeaders HttpHeaders { get; private set; }
	}
}
