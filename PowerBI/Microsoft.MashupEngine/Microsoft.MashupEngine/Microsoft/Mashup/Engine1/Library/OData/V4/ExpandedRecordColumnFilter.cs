using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200084B RID: 2123
	internal class ExpandedRecordColumnFilter : ExpandedColumnFilter
	{
		// Token: 0x06003D3E RID: 15678 RVA: 0x000C7301 File Offset: 0x000C5501
		public ExpandedRecordColumnFilter(EntityRangeVariable topIterator, SingleValueNode expressionNode, SingleNavigationNode source)
		{
			this.topIterator = topIterator;
			this.outerFilterNode = expressionNode;
			this.singleSource = source;
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000020FA File Offset: 0x000002FA
		public override FilterClause InnerFilterClause
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x000C731E File Offset: 0x000C551E
		public override SingleValueNode OuterFilterNode
		{
			get
			{
				return this.outerFilterNode;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000C7326 File Offset: 0x000C5526
		public override FilterClause OuterFilterClause
		{
			get
			{
				if (this.outerFilterNode == null)
				{
					return null;
				}
				return new FilterClause(this.outerFilterNode, this.topIterator);
			}
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000C7344 File Offset: 0x000C5544
		public override QueryNode ExpandInnerNavigationPropertyNode(bool isRecordExpansion, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty)
		{
			QueryNode queryNode;
			if (isRecordExpansion)
			{
				queryNode = new SingleNavigationNode(navigationProperty, this.singleSource);
			}
			else
			{
				queryNode = new CollectionNavigationNode(navigationProperty, this.singleSource);
			}
			return queryNode;
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000C7374 File Offset: 0x000C5574
		public override ExpandedColumnFilter AppendOuterFilter(ExpandedColumnFilter right)
		{
			if (right.OuterFilterNode == null)
			{
				return this;
			}
			SingleValueNode singleValueNode;
			if (this.outerFilterNode != null)
			{
				singleValueNode = new BinaryOperatorNode(BinaryOperatorKind.And, this.outerFilterNode, right.OuterFilterNode);
			}
			else
			{
				singleValueNode = right.OuterFilterNode;
			}
			singleValueNode = ODataExpression.AppendNullRowsExprToExpandedRecordFilter(singleValueNode, this.singleSource);
			return new ExpandedRecordColumnFilter(this.topIterator, singleValueNode, this.singleSource);
		}

		// Token: 0x04002009 RID: 8201
		private readonly EntityRangeVariable topIterator;

		// Token: 0x0400200A RID: 8202
		private readonly SingleNavigationNode singleSource;

		// Token: 0x0400200B RID: 8203
		private readonly SingleValueNode outerFilterNode;
	}
}
