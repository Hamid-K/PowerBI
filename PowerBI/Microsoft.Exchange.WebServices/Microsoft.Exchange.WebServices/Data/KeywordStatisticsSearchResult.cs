using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000271 RID: 625
	public sealed class KeywordStatisticsSearchResult
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0003D4EC File Offset: 0x0003C4EC
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x0003D4F4 File Offset: 0x0003C4F4
		public string Keyword { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0003D4FD File Offset: 0x0003C4FD
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0003D505 File Offset: 0x0003C505
		public int ItemHits { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0003D50E File Offset: 0x0003C50E
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0003D516 File Offset: 0x0003C516
		[CLSCompliant(false)]
		public ulong Size { get; set; }
	}
}
