using System;

namespace System.Web.Http.Description
{
	// Token: 0x020000E4 RID: 228
	public class ResponseDescription
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000E949 File Offset: 0x0000CB49
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000E951 File Offset: 0x0000CB51
		public Type DeclaredType { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000E95A File Offset: 0x0000CB5A
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000E962 File Offset: 0x0000CB62
		public Type ResponseType { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000E96B File Offset: 0x0000CB6B
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000E973 File Offset: 0x0000CB73
		public string Documentation { get; set; }
	}
}
