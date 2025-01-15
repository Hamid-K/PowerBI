using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000046 RID: 70
	public class QueryableRestrictionsAnnotation
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00007BE2 File Offset: 0x00005DE2
		public QueryableRestrictionsAnnotation(QueryableRestrictions restrictions)
		{
			if (restrictions == null)
			{
				throw Error.ArgumentNull("restrictions");
			}
			this.Restrictions = restrictions;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007BFF File Offset: 0x00005DFF
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007C07 File Offset: 0x00005E07
		public QueryableRestrictions Restrictions { get; private set; }
	}
}
