using System;

namespace Model
{
	// Token: 0x02000049 RID: 73
	public class DataSetField
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00003049 File Offset: 0x00001249
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00003051 File Offset: 0x00001251
		public string Name { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000305A File Offset: 0x0000125A
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00003062 File Offset: 0x00001262
		public ReportParameterType DataType { get; set; }
	}
}
