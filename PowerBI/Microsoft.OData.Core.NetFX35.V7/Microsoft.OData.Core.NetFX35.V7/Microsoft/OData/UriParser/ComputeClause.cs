using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AF RID: 431
	public sealed class ComputeClause
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x00030693 File Offset: 0x0002E893
		public ComputeClause(IEnumerable<ComputeExpression> computedItems)
		{
			this.computedItems = ((computedItems != null) ? new ReadOnlyCollection<ComputeExpression>(Enumerable.ToList<ComputeExpression>(computedItems)) : new ReadOnlyCollection<ComputeExpression>(new List<ComputeExpression>()));
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000306BB File Offset: 0x0002E8BB
		public IEnumerable<ComputeExpression> ComputedItems
		{
			get
			{
				return Enumerable.AsEnumerable<ComputeExpression>(this.computedItems);
			}
		}

		// Token: 0x040008D2 RID: 2258
		private ReadOnlyCollection<ComputeExpression> computedItems;
	}
}
