using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000033 RID: 51
	internal sealed class ResolvedRollupExpressionNode : ExpressionNode
	{
		// Token: 0x0600025B RID: 603 RVA: 0x000073D7 File Offset: 0x000055D7
		internal ResolvedRollupExpressionNode(ResolvedCalculationReferenceExpressionNode argument)
		{
			this.m_argument = argument;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000073E6 File Offset: 0x000055E6
		public ResolvedCalculationReferenceExpressionNode Argument
		{
			get
			{
				return this.m_argument;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000073EE File Offset: 0x000055EE
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedRollup;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000073F4 File Offset: 0x000055F4
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedRollupExpressionNode resolvedRollupExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedRollupExpressionNode>(this, other, out flag, out resolvedRollupExpressionNode))
			{
				return flag;
			}
			return this.Argument.Equals(resolvedRollupExpressionNode.Argument);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007421 File Offset: 0x00005621
		protected override int GetHashCodeImpl()
		{
			return this.Argument.GetHashCode();
		}

		// Token: 0x040000A5 RID: 165
		private readonly ResolvedCalculationReferenceExpressionNode m_argument;
	}
}
