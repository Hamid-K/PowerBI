using System;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000069 RID: 105
	public class PBIProject
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000852B File Offset: 0x0000672B
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00008533 File Offset: 0x00006733
		public ArtifactShortcutSettings Settings { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000853C File Offset: 0x0000673C
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00008544 File Offset: 0x00006744
		public string DollarVeryUniqueSchemaProperty { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000854D File Offset: 0x0000674D
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00008555 File Offset: 0x00006755
		public string ReportPath { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000855E File Offset: 0x0000675E
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00008566 File Offset: 0x00006766
		public PBIProjectReport Report { get; set; } = new PBIProjectReport();
	}
}
