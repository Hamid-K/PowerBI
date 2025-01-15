using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200023D RID: 573
	public sealed class EntitySetCollection : EntitySetBaseCollection<EntitySet>
	{
		// Token: 0x06001948 RID: 6472 RVA: 0x00044EA7 File Offset: 0x000430A7
		internal EntitySetCollection(IEnumerable<EntitySet> items)
			: base(items)
		{
		}
	}
}
