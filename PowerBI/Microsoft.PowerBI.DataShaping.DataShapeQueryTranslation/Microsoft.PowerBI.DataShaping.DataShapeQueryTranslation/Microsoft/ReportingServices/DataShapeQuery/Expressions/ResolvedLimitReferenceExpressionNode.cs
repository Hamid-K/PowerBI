using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000031 RID: 49
	internal sealed class ResolvedLimitReferenceExpressionNode : ResolvedStructureReferenceExpressionNode
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00007315 File Offset: 0x00005515
		internal ResolvedLimitReferenceExpressionNode(Limit limit)
		{
			this.m_limit = limit;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00007324 File Offset: 0x00005524
		public override IIdentifiable Target
		{
			get
			{
				return this.m_limit;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000732C File Offset: 0x0000552C
		public Limit Limit
		{
			get
			{
				return this.m_limit;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007334 File Offset: 0x00005534
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedLimitReference;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007338 File Offset: 0x00005538
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedLimitReferenceExpressionNode resolvedLimitReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedLimitReferenceExpressionNode>(this, other, out flag, out resolvedLimitReferenceExpressionNode))
			{
				return flag;
			}
			return this.Limit == resolvedLimitReferenceExpressionNode.Limit;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007362 File Offset: 0x00005562
		protected override int GetHashCodeImpl()
		{
			return this.Limit.GetHashCode();
		}

		// Token: 0x040000A3 RID: 163
		private readonly Limit m_limit;
	}
}
