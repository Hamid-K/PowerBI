using System;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000095 RID: 149
	public sealed class ODataMessageInfo
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000CE52 File Offset: 0x0000B052
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000CE5A File Offset: 0x0000B05A
		public ODataMediaType MediaType { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000CE63 File Offset: 0x0000B063
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0000CE6B File Offset: 0x0000B06B
		public Encoding Encoding { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000CE74 File Offset: 0x0000B074
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000CE7C File Offset: 0x0000B07C
		public IEdmModel Model { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000CE85 File Offset: 0x0000B085
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000CE8D File Offset: 0x0000B08D
		public bool IsResponse { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000CE96 File Offset: 0x0000B096
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000CE9E File Offset: 0x0000B09E
		public IODataPayloadUriConverter PayloadUriConverter { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000CEA7 File Offset: 0x0000B0A7
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000CEAF File Offset: 0x0000B0AF
		public IServiceProvider Container { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		public bool IsAsync { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000CEC9 File Offset: 0x0000B0C9
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000CED1 File Offset: 0x0000B0D1
		public Stream MessageStream { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000CEE2 File Offset: 0x0000B0E2
		internal ODataPayloadKind PayloadKind { get; set; }
	}
}
