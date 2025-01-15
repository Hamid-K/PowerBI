using System;
using System.Collections.ObjectModel;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200084C RID: 2124
	internal class ExpandedTableColumnFilter : ExpandedColumnFilter
	{
		// Token: 0x06003D44 RID: 15684 RVA: 0x000C73CE File Offset: 0x000C55CE
		public ExpandedTableColumnFilter(EntityRangeVariable topIterator, EntityRangeVariable currentIterator, EntityRangeVariableReferenceNode currentIteratorReference, FilterClause innerFilter, SingleValueNode expressionNode, CollectionNavigationNode source)
		{
			this.topIterator = topIterator;
			this.currentIterator = currentIterator;
			this.currentIteratorReference = currentIteratorReference;
			this.innerFilter = innerFilter;
			this.outerFilterNode = expressionNode;
			this.source = source;
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000C7403 File Offset: 0x000C5603
		public override SingleValueNode OuterFilterNode
		{
			get
			{
				return this.outerFilterNode;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000C740B File Offset: 0x000C560B
		public override FilterClause InnerFilterClause
		{
			get
			{
				return this.innerFilter;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000C7413 File Offset: 0x000C5613
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

		// Token: 0x06003D48 RID: 15688 RVA: 0x000C7430 File Offset: 0x000C5630
		public override QueryNode ExpandInnerNavigationPropertyNode(bool isRecordExpansion, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty)
		{
			QueryNode queryNode;
			if (isRecordExpansion)
			{
				queryNode = new SingleNavigationNode(navigationProperty, this.currentIteratorReference);
			}
			else
			{
				queryNode = new CollectionNavigationNode(navigationProperty, this.currentIteratorReference);
			}
			return queryNode;
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x000C7460 File Offset: 0x000C5660
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

		// Token: 0x0400200C RID: 8204
		private readonly EntityRangeVariable topIterator;

		// Token: 0x0400200D RID: 8205
		private readonly EntityRangeVariable currentIterator;

		// Token: 0x0400200E RID: 8206
		private readonly EntityRangeVariableReferenceNode currentIteratorReference;

		// Token: 0x0400200F RID: 8207
		private readonly FilterClause innerFilter;

		// Token: 0x04002010 RID: 8208
		private readonly CollectionNavigationNode source;

		// Token: 0x04002011 RID: 8209
		private readonly SingleValueNode outerFilterNode;
	}
}
