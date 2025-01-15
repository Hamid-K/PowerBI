using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000103 RID: 259
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class AutoExpandAttribute : Attribute
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00025DD1 File Offset: 0x00023FD1
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00025DD9 File Offset: 0x00023FD9
		public bool DisableWhenSelectPresent { get; set; }
	}
}
