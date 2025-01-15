using System;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000012 RID: 18
	internal sealed class ApplyContextFilterExpressionNode : ExpressionNode
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000042BE File Offset: 0x000024BE
		internal ApplyContextFilterExpressionNode(ExpressionNode expression, ReadOnlyCollection<PlanOperation> contextTables)
		{
			this.m_expression = expression;
			this.m_contextTables = contextTables;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000042D4 File Offset: 0x000024D4
		public ExpressionNode Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000042DC File Offset: 0x000024DC
		public ReadOnlyCollection<PlanOperation> ContextTables
		{
			get
			{
				return this.m_contextTables;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000042E4 File Offset: 0x000024E4
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ApplyContextFilter;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042E8 File Offset: 0x000024E8
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ApplyContextFilterExpressionNode applyContextFilterExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ApplyContextFilterExpressionNode>(this, other, out flag, out applyContextFilterExpressionNode))
			{
				return flag;
			}
			return this.Expression.Equals(applyContextFilterExpressionNode.Expression) && this.ContextTables.SetEquals(applyContextFilterExpressionNode.ContextTables);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000432A File Offset: 0x0000252A
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), Hashing.CombineHashUnordered<PlanOperation>(this.ContextTables));
		}

		// Token: 0x0400003F RID: 63
		private readonly ExpressionNode m_expression;

		// Token: 0x04000040 RID: 64
		private readonly ReadOnlyCollection<PlanOperation> m_contextTables;
	}
}
