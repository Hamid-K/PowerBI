using System;

namespace Model
{
	// Token: 0x0200002A RID: 42
	public sealed class ParameterValue
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002922 File Offset: 0x00000B22
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000292A File Offset: 0x00000B2A
		public string Name { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002933 File Offset: 0x00000B33
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000293B File Offset: 0x00000B3B
		public string Value { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002944 File Offset: 0x00000B44
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000294C File Offset: 0x00000B4C
		public bool IsValueFieldReference { get; set; }
	}
}
