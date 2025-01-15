using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000036 RID: 54
	internal sealed class ResolvedSortKeyReferenceExpressionNode : ResolvedGroupElementReferenceExpressionNode
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000752D File Offset: 0x0000572D
		internal ResolvedSortKeyReferenceExpressionNode(SortKey sortKey)
		{
			this.m_sortKey = sortKey;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000753C File Offset: 0x0000573C
		public override IIdentifiable Target
		{
			get
			{
				return this.m_sortKey;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007544 File Offset: 0x00005744
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedSortKeyReference;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00007548 File Offset: 0x00005748
		public SortKey SortKey
		{
			get
			{
				return this.m_sortKey;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007550 File Offset: 0x00005750
		protected override Expression ReferencedExpression
		{
			get
			{
				return this.m_sortKey.Value;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007560 File Offset: 0x00005760
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedSortKeyReferenceExpressionNode resolvedSortKeyReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedSortKeyReferenceExpressionNode>(this, other, out flag, out resolvedSortKeyReferenceExpressionNode))
			{
				if (resolvedSortKeyReferenceExpressionNode == null)
				{
					flag = base.Equals(other);
				}
				return flag;
			}
			return this.SortKey == resolvedSortKeyReferenceExpressionNode.SortKey;
		}

		// Token: 0x040000A8 RID: 168
		private readonly SortKey m_sortKey;
	}
}
