using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200047C RID: 1148
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class ECFContractAttribute : Attribute
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x000809E2 File Offset: 0x0007EBE2
		// (set) Token: 0x0600239D RID: 9117 RVA: 0x000809EA File Offset: 0x0007EBEA
		public bool IsExternal { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x000809F3 File Offset: 0x0007EBF3
		// (set) Token: 0x0600239F RID: 9119 RVA: 0x000809FB File Offset: 0x0007EBFB
		public bool FlattenHierarchy { get; set; }

		// Token: 0x060023A0 RID: 9120 RVA: 0x00080A04 File Offset: 0x0007EC04
		public ECFContractAttribute(bool isExternal, bool flattenHierarchy)
		{
			this.IsExternal = isExternal;
			this.FlattenHierarchy = flattenHierarchy;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00080A1A File Offset: 0x0007EC1A
		public ECFContractAttribute(bool isExternal)
			: this(isExternal, false)
		{
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00080A24 File Offset: 0x0007EC24
		public ECFContractAttribute()
			: this(false)
		{
		}
	}
}
