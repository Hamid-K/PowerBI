using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000485 RID: 1157
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class RouterAttribute : Attribute
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00080A9E File Offset: 0x0007EC9E
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x00080AA6 File Offset: 0x0007ECA6
		public SupportedKeys SupportedKeys { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00080AAF File Offset: 0x0007ECAF
		// (set) Token: 0x060023B4 RID: 9140 RVA: 0x00080AB7 File Offset: 0x0007ECB7
		public ProviderCount ProviderCount { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00080AC0 File Offset: 0x0007ECC0
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00080AC8 File Offset: 0x0007ECC8
		public bool IsUnicast { get; set; }

		// Token: 0x060023B7 RID: 9143 RVA: 0x00080AD1 File Offset: 0x0007ECD1
		public RouterAttribute()
		{
			this.SupportedKeys = SupportedKeys.None;
			this.ProviderCount = ProviderCount.Single;
			this.IsUnicast = true;
		}
	}
}
