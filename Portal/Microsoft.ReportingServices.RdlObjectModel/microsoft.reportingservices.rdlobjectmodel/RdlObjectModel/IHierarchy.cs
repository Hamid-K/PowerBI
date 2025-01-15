using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B5 RID: 181
	public interface IHierarchy
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007A5 RID: 1957
		IEnumerable<IHierarchyMember> Members { get; }
	}
}
