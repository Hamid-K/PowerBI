using System;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200006F RID: 111
	public sealed class ODataMessageInfo
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000A976 File Offset: 0x00008B76
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000A97E File Offset: 0x00008B7E
		public ODataMediaType MediaType { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000A987 File Offset: 0x00008B87
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000A98F File Offset: 0x00008B8F
		public Encoding Encoding { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000A998 File Offset: 0x00008B98
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000A9A0 File Offset: 0x00008BA0
		public IEdmModel Model { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A9A9 File Offset: 0x00008BA9
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000A9B1 File Offset: 0x00008BB1
		public bool IsResponse { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A9BA File Offset: 0x00008BBA
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000A9C2 File Offset: 0x00008BC2
		public IODataPayloadUriConverter PayloadUriConverter { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A9CB File Offset: 0x00008BCB
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		public IServiceProvider Container { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A9DC File Offset: 0x00008BDC
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public bool IsAsync { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000A9ED File Offset: 0x00008BED
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000A9F5 File Offset: 0x00008BF5
		public Stream MessageStream { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000A9FE File Offset: 0x00008BFE
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000AA06 File Offset: 0x00008C06
		internal ODataPayloadKind PayloadKind { get; set; }
	}
}
