using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001DA RID: 474
	public sealed class AssociationSetCollection : EntitySetBaseCollection<AssociationSet>
	{
		// Token: 0x060016B4 RID: 5812 RVA: 0x0003EA18 File Offset: 0x0003CC18
		internal AssociationSetCollection(IEnumerable<AssociationSet> items)
			: base(items)
		{
		}
	}
}
