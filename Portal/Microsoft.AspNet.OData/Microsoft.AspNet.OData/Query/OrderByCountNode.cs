using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000A8 RID: 168
	public class OrderByCountNode : OrderByNode
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00015140 File Offset: 0x00013340
		public OrderByCountNode(OrderByClause orderByClause)
		{
			if (orderByClause == null)
			{
				throw Error.ArgumentNull("orderByClause");
			}
			this.OrderByClause = orderByClause;
			base.Direction = orderByClause.Direction;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00015169 File Offset: 0x00013369
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00015171 File Offset: 0x00013371
		public OrderByClause OrderByClause { get; private set; }
	}
}
