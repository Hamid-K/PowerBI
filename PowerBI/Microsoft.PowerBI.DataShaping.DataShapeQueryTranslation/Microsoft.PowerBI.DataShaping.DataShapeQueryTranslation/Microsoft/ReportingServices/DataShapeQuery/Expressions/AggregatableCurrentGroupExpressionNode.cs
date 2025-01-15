using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000010 RID: 16
	internal sealed class AggregatableCurrentGroupExpressionNode : ExpressionNode
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000041A2 File Offset: 0x000023A2
		internal AggregatableCurrentGroupExpressionNode(ExpressionNode projection)
		{
			this.m_projection = projection;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000041B1 File Offset: 0x000023B1
		public ExpressionNode Projection
		{
			get
			{
				return this.m_projection;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000041B9 File Offset: 0x000023B9
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.AggregatableCurrentGroupExpression;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000041BC File Offset: 0x000023BC
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			AggregatableCurrentGroupExpressionNode aggregatableCurrentGroupExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<AggregatableCurrentGroupExpressionNode>(this, other, out flag, out aggregatableCurrentGroupExpressionNode))
			{
				return flag;
			}
			return this.Projection.Equals(aggregatableCurrentGroupExpressionNode.Projection);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041E9 File Offset: 0x000023E9
		protected override int GetHashCodeImpl()
		{
			return this.Projection.GetHashCode();
		}

		// Token: 0x0400003B RID: 59
		private readonly ExpressionNode m_projection;
	}
}
