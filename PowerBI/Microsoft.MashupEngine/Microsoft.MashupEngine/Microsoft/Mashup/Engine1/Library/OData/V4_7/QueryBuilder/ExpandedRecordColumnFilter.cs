using System;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C3 RID: 1987
	internal class ExpandedRecordColumnFilter : ExpandedColumnFilter
	{
		// Token: 0x060039CC RID: 14796 RVA: 0x000BA996 File Offset: 0x000B8B96
		public ExpandedRecordColumnFilter(ResourceRangeVariable topIterator, SingleValueNode expressionNode, SingleNavigationNode source)
		{
			this.topIterator = topIterator;
			this.outerFilterNode = expressionNode;
			this.singleSource = source;
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000BA9B3 File Offset: 0x000B8BB3
		public override SingleResourceNode Source
		{
			get
			{
				return this.singleSource;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000020FA File Offset: 0x000002FA
		public override FilterClause InnerFilterClause
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000BA9BB File Offset: 0x000B8BBB
		public override SingleValueNode OuterFilterNode
		{
			get
			{
				return this.outerFilterNode;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000BA9C3 File Offset: 0x000B8BC3
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

		// Token: 0x060039D1 RID: 14801 RVA: 0x000BA9E0 File Offset: 0x000B8BE0
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

		// Token: 0x04001DF1 RID: 7665
		private readonly ResourceRangeVariable topIterator;

		// Token: 0x04001DF2 RID: 7666
		private readonly SingleNavigationNode singleSource;

		// Token: 0x04001DF3 RID: 7667
		private readonly SingleValueNode outerFilterNode;
	}
}
