using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C8 RID: 200
	public abstract class OrderByNode
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00017505 File Offset: 0x00015705
		protected OrderByNode(OrderByDirection direction)
		{
			this.Direction = direction;
			this.PropertyPath = string.Empty;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001751F File Offset: 0x0001571F
		protected OrderByNode(OrderByClause orderByClause)
		{
			if (orderByClause == null)
			{
				throw Error.ArgumentNull("orderByClause");
			}
			this.Direction = orderByClause.Direction;
			this.PropertyPath = OrderByNode.RestorePropertyPath(orderByClause.Expression);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00002557 File Offset: 0x00000757
		internal OrderByNode()
		{
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00017552 File Offset: 0x00015752
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001755A File Offset: 0x0001575A
		public OrderByDirection Direction { get; internal set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00017563 File Offset: 0x00015763
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001756B File Offset: 0x0001576B
		internal string PropertyPath { get; set; }

		// Token: 0x060006BF RID: 1727 RVA: 0x00017574 File Offset: 0x00015774
		public static IList<OrderByNode> CreateCollection(OrderByClause orderByClause)
		{
			List<OrderByNode> list = new List<OrderByNode>();
			for (OrderByClause orderByClause2 = orderByClause; orderByClause2 != null; orderByClause2 = orderByClause2.ThenBy)
			{
				if (orderByClause2.Expression is CountNode)
				{
					list.Add(new OrderByCountNode(orderByClause2));
				}
				else if (orderByClause2.Expression is NonResourceRangeVariableReferenceNode || orderByClause2.Expression is ResourceRangeVariableReferenceNode)
				{
					list.Add(new OrderByItNode(orderByClause2.Direction));
				}
				else if (orderByClause2.Expression is SingleValueOpenPropertyAccessNode)
				{
					list.Add(new OrderByOpenPropertyNode(orderByClause2));
				}
				else
				{
					list.Add(new OrderByPropertyNode(orderByClause2));
				}
			}
			return list;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00017608 File Offset: 0x00015808
		internal static string RestorePropertyPath(SingleValueNode expression)
		{
			if (expression == null)
			{
				return string.Empty;
			}
			string text = string.Empty;
			SingleValueNode singleValueNode = null;
			SingleValuePropertyAccessNode singleValuePropertyAccessNode = expression as SingleValuePropertyAccessNode;
			if (singleValuePropertyAccessNode != null)
			{
				text = singleValuePropertyAccessNode.Property.Name;
				singleValueNode = singleValuePropertyAccessNode.Source;
			}
			else
			{
				SingleComplexNode singleComplexNode = expression as SingleComplexNode;
				if (singleComplexNode != null)
				{
					text = singleComplexNode.Property.Name;
					singleValueNode = singleComplexNode.Source;
				}
				else
				{
					SingleNavigationNode singleNavigationNode = expression as SingleNavigationNode;
					if (singleNavigationNode != null)
					{
						text = singleNavigationNode.NavigationProperty.Name;
						singleValueNode = singleNavigationNode.Source;
					}
				}
			}
			string text2 = OrderByNode.RestorePropertyPath(singleValueNode);
			if (string.IsNullOrEmpty(text2))
			{
				return text;
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}/{1}", new object[] { text2, text });
		}
	}
}
