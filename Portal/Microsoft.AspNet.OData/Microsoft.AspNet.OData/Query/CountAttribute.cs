using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000AB RID: 171
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class CountAttribute : Attribute
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00015294 File Offset: 0x00013494
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0001529C File Offset: 0x0001349C
		public bool Disabled { get; set; }
	}
}
