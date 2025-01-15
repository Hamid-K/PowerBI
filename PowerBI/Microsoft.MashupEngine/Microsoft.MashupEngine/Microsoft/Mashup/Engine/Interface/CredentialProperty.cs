using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000014 RID: 20
	public sealed class CredentialProperty
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000024F0 File Offset: 0x000006F0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000024F8 File Offset: 0x000006F8
		public string Name { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002501 File Offset: 0x00000701
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002509 File Offset: 0x00000709
		public string Label { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002512 File Offset: 0x00000712
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000251A File Offset: 0x0000071A
		public bool IsRequired { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002523 File Offset: 0x00000723
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000252B File Offset: 0x0000072B
		public bool IsSecret { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002534 File Offset: 0x00000734
		// (set) Token: 0x0600004B RID: 75 RVA: 0x0000253C File Offset: 0x0000073C
		public Type PropertyType { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002545 File Offset: 0x00000745
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000254D File Offset: 0x0000074D
		public bool AllowNull { get; set; }
	}
}
