using System;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C5 RID: 197
	public sealed class QueryParameter
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x00012AFF File Offset: 0x00010CFF
		internal QueryParameter()
		{
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00012B07 File Offset: 0x00010D07
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00012B0F File Offset: 0x00010D0F
		public string Name { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00012B18 File Offset: 0x00010D18
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00012B20 File Offset: 0x00010D20
		public string Value { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00012B29 File Offset: 0x00010D29
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00012B31 File Offset: 0x00010D31
		public string TypeName { get; set; }
	}
}
