using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000038 RID: 56
	internal sealed class RollupExpressionNode : ExpressionNode
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000759D File Offset: 0x0000579D
		internal RollupExpressionNode(ResolvedCalculationReferenceExpressionNode argument)
		{
			this.m_argument = argument;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000075AC File Offset: 0x000057AC
		public ResolvedCalculationReferenceExpressionNode Argument
		{
			get
			{
				return this.m_argument;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000075B4 File Offset: 0x000057B4
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.Rollup;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000075B8 File Offset: 0x000057B8
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			RollupExpressionNode rollupExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<RollupExpressionNode>(this, other, out flag, out rollupExpressionNode))
			{
				return flag;
			}
			return this.Argument.Equals(rollupExpressionNode.Argument);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000075E5 File Offset: 0x000057E5
		protected override int GetHashCodeImpl()
		{
			return this.Argument.GetHashCode();
		}

		// Token: 0x040000A9 RID: 169
		private readonly ResolvedCalculationReferenceExpressionNode m_argument;
	}
}
