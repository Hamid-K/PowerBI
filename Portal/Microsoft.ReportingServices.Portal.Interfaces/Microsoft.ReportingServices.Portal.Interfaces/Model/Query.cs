using System;

namespace Model
{
	// Token: 0x02000019 RID: 25
	public sealed class Query
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000024D5 File Offset: 0x000006D5
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000024DD File Offset: 0x000006DD
		public string CommandText { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000024E6 File Offset: 0x000006E6
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000024EE File Offset: 0x000006EE
		public int? Timeout { get; set; }
	}
}
