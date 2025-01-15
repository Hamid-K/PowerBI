using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x0200001E RID: 30
	internal class HttpUnsortedResponse
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004411 File Offset: 0x00002611
		public HttpUnsortedResponse()
		{
			this.HttpHeaders = new HttpUnsortedHeaders();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004424 File Offset: 0x00002624
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000442C File Offset: 0x0000262C
		public Version Version { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004435 File Offset: 0x00002635
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000443D File Offset: 0x0000263D
		public HttpStatusCode StatusCode { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004446 File Offset: 0x00002646
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000444E File Offset: 0x0000264E
		public string ReasonPhrase { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004457 File Offset: 0x00002657
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000445F File Offset: 0x0000265F
		public HttpHeaders HttpHeaders { get; private set; }
	}
}
