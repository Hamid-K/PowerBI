using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D4 RID: 212
	public class OrderByPropertyNode : OrderByNode
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x000180DC File Offset: 0x000162DC
		public OrderByPropertyNode(OrderByClause orderByClause)
			: base(orderByClause)
		{
			if (orderByClause == null)
			{
				throw Error.ArgumentNull("orderByClause");
			}
			this.OrderByClause = orderByClause;
			base.Direction = orderByClause.Direction;
			SingleValuePropertyAccessNode singleValuePropertyAccessNode = orderByClause.Expression as SingleValuePropertyAccessNode;
			if (singleValuePropertyAccessNode == null)
			{
				throw new ODataException(SRResources.OrderByClauseNotSupported);
			}
			this.Property = singleValuePropertyAccessNode.Property;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00018137 File Offset: 0x00016337
		public OrderByPropertyNode(IEdmProperty property, OrderByDirection direction)
			: base(direction)
		{
			if (property == null)
			{
				throw Error.ArgumentNull("property");
			}
			this.Property = property;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00018155 File Offset: 0x00016355
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001815D File Offset: 0x0001635D
		public OrderByClause OrderByClause { get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00018166 File Offset: 0x00016366
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001816E File Offset: 0x0001636E
		public IEdmProperty Property { get; private set; }
	}
}
