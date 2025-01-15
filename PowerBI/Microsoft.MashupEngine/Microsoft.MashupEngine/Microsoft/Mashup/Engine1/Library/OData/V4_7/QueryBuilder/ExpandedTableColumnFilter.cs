using System;
using System.Collections.ObjectModel;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C4 RID: 1988
	internal class ExpandedTableColumnFilter : ExpandedColumnFilter
	{
		// Token: 0x060039D2 RID: 14802 RVA: 0x000BAA3A File Offset: 0x000B8C3A
		public ExpandedTableColumnFilter(ResourceRangeVariable topIterator, ResourceRangeVariable currentIterator, ResourceRangeVariableReferenceNode currentIteratorReference, FilterClause innerFilter, SingleValueNode expressionNode, CollectionNavigationNode source)
		{
			this.topIterator = topIterator;
			this.currentIterator = currentIterator;
			this.currentIteratorReference = currentIteratorReference;
			this.innerFilter = innerFilter;
			this.outerFilterNode = expressionNode;
			this.source = source;
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000BAA6F File Offset: 0x000B8C6F
		public override SingleResourceNode Source
		{
			get
			{
				return this.currentIteratorReference;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000BAA77 File Offset: 0x000B8C77
		public override SingleValueNode OuterFilterNode
		{
			get
			{
				return this.outerFilterNode;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000BAA7F File Offset: 0x000B8C7F
		public override FilterClause InnerFilterClause
		{
			get
			{
				return this.innerFilter;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000BAA87 File Offset: 0x000B8C87
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

		// Token: 0x060039D7 RID: 14807 RVA: 0x000BAAA4 File Offset: 0x000B8CA4
		public override ExpandedColumnFilter AppendOuterFilter(ExpandedColumnFilter right)
		{
			if (right.OuterFilterNode == null)
			{
				return this;
			}
			SingleValueNode singleValueNode;
			if (this.outerFilterNode != null)
			{
				BinaryOperatorNode binaryOperatorNode = (BinaryOperatorNode)this.outerFilterNode;
				AnyNode anyNode = ((binaryOperatorNode.Left is AnyNode) ? (binaryOperatorNode.Left as AnyNode) : (binaryOperatorNode.Right as AnyNode));
				singleValueNode = new BinaryOperatorNode(BinaryOperatorKind.And, anyNode.Body, right.OuterFilterNode);
			}
			else
			{
				singleValueNode = right.OuterFilterNode;
			}
			SingleValueNode singleValueNode2 = ODataExpression.AppendNullRowsExprToExpandedTableFilter(new AnyNode(new Collection<RangeVariable> { this.currentIterator }, this.currentIterator)
			{
				Body = singleValueNode,
				Source = this.source
			}, this.source);
			return new ExpandedTableColumnFilter(this.topIterator, this.currentIterator, this.currentIteratorReference, this.innerFilter, singleValueNode2, this.source);
		}

		// Token: 0x04001DF4 RID: 7668
		private readonly ResourceRangeVariable topIterator;

		// Token: 0x04001DF5 RID: 7669
		private readonly ResourceRangeVariable currentIterator;

		// Token: 0x04001DF6 RID: 7670
		private readonly ResourceRangeVariableReferenceNode currentIteratorReference;

		// Token: 0x04001DF7 RID: 7671
		private readonly FilterClause innerFilter;

		// Token: 0x04001DF8 RID: 7672
		private readonly CollectionNavigationNode source;

		// Token: 0x04001DF9 RID: 7673
		private readonly SingleValueNode outerFilterNode;
	}
}
