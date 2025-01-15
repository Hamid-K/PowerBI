using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000108 RID: 264
	public sealed class ComputeClause
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x00025FAF File Offset: 0x000241AF
		public ComputeClause(IEnumerable<ComputeExpression> computedItems)
		{
			this.computedItems = ((computedItems != null) ? new ReadOnlyCollection<ComputeExpression>(computedItems.ToList<ComputeExpression>()) : new ReadOnlyCollection<ComputeExpression>(new List<ComputeExpression>()));
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00025FD7 File Offset: 0x000241D7
		public IEnumerable<ComputeExpression> ComputedItems
		{
			get
			{
				return this.computedItems.AsEnumerable<ComputeExpression>();
			}
		}

		// Token: 0x04000777 RID: 1911
		private ReadOnlyCollection<ComputeExpression> computedItems;
	}
}
