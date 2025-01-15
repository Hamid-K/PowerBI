using System;

namespace Model
{
	// Token: 0x0200000F RID: 15
	public sealed class DataSetParameterInfo
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000226A File Offset: 0x0000046A
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002272 File Offset: 0x00000472
		public string Name { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000227B File Offset: 0x0000047B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002283 File Offset: 0x00000483
		public string DefaultValue { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002294 File Offset: 0x00000494
		public bool Nullable { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000229D File Offset: 0x0000049D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000022A5 File Offset: 0x000004A5
		public ReportParameterType DataType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000022AE File Offset: 0x000004AE
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000022B6 File Offset: 0x000004B6
		public bool IsExpression { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000022BF File Offset: 0x000004BF
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000022C7 File Offset: 0x000004C7
		public bool IsMultiValued { get; set; }
	}
}
