using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000030 RID: 48
	internal sealed class ResolvedGroupKeyReferenceExpressionNode : ResolvedGroupElementReferenceExpressionNode
	{
		// Token: 0x06000249 RID: 585 RVA: 0x000072AF File Offset: 0x000054AF
		internal ResolvedGroupKeyReferenceExpressionNode(GroupKey groupKey)
		{
			this.m_groupKey = groupKey;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000072BE File Offset: 0x000054BE
		public override IIdentifiable Target
		{
			get
			{
				return this.m_groupKey;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000072C6 File Offset: 0x000054C6
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedGroupKeyReference;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000072CA File Offset: 0x000054CA
		public GroupKey GroupKey
		{
			get
			{
				return this.m_groupKey;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000072D2 File Offset: 0x000054D2
		protected override Expression ReferencedExpression
		{
			get
			{
				return this.m_groupKey.Value;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000072E0 File Offset: 0x000054E0
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedGroupKeyReferenceExpressionNode resolvedGroupKeyReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedGroupKeyReferenceExpressionNode>(this, other, out flag, out resolvedGroupKeyReferenceExpressionNode))
			{
				if (resolvedGroupKeyReferenceExpressionNode == null)
				{
					flag = base.Equals(other);
				}
				return flag;
			}
			return this.GroupKey == resolvedGroupKeyReferenceExpressionNode.GroupKey;
		}

		// Token: 0x040000A2 RID: 162
		private readonly GroupKey m_groupKey;
	}
}
