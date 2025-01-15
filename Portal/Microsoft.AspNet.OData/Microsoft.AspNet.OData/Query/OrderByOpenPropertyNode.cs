using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000BC RID: 188
	public class OrderByOpenPropertyNode : OrderByNode
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x00016140 File Offset: 0x00014340
		public OrderByOpenPropertyNode(OrderByClause orderByClause)
			: base(orderByClause)
		{
			if (orderByClause == null)
			{
				throw Error.ArgumentNull("orderByClause");
			}
			this.OrderByClause = orderByClause;
			SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = orderByClause.Expression as SingleValueOpenPropertyAccessNode;
			if (singleValueOpenPropertyAccessNode == null)
			{
				throw new ODataException(SRResources.OrderByClauseNotSupported);
			}
			this.PropertyName = singleValueOpenPropertyAccessNode.Name;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001618F File Offset: 0x0001438F
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00016197 File Offset: 0x00014397
		public OrderByClause OrderByClause { get; private set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x000161A0 File Offset: 0x000143A0
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x000161A8 File Offset: 0x000143A8
		public string PropertyName { get; private set; }
	}
}
