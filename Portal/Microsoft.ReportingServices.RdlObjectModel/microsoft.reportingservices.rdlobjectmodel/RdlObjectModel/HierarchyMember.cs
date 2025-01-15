using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B8 RID: 184
	public abstract class HierarchyMember : ReportObject
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0001B9BB File Offset: 0x00019BBB
		public HierarchyMember()
		{
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001B9C3 File Offset: 0x00019BC3
		internal HierarchyMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007AF RID: 1967
		// (set) Token: 0x060007B0 RID: 1968
		public abstract Group Group { get; set; }
	}
}
