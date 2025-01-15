using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B6 RID: 182
	public interface IHierarchyMember
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007A6 RID: 1958
		// (set) Token: 0x060007A7 RID: 1959
		Group Group { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007A8 RID: 1960
		// (set) Token: 0x060007A9 RID: 1961
		IList<SortExpression> SortExpressions { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007AA RID: 1962
		IEnumerable<IHierarchyMember> Members { get; }
	}
}
