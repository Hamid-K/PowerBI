using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001EA RID: 490
	internal sealed class PlanExpression : IEquatable<PlanExpression>, IStructuredToString
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x000464E2 File Offset: 0x000446E2
		internal PlanExpression(ExpressionNode expression, ExpressionContext context)
		{
			this.m_expression = expression;
			this.m_context = context;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x000464F8 File Offset: 0x000446F8
		public ExpressionNode Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00046500 File Offset: 0x00044700
		public ExpressionContext Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00046508 File Offset: 0x00044708
		public override int GetHashCode()
		{
			return this.m_expression.GetHashCode();
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00046515 File Offset: 0x00044715
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanExpression);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00046523 File Offset: 0x00044723
		public bool Equals(PlanExpression other)
		{
			return other != null && this.m_expression.Equals(other.m_expression);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0004653B File Offset: 0x0004473B
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanExpression");
			builder.WriteProperty<ExpressionNode>("Expression", this.m_expression, false);
			builder.EndObject();
		}

		// Token: 0x040007E2 RID: 2018
		private readonly ExpressionNode m_expression;

		// Token: 0x040007E3 RID: 2019
		private readonly ExpressionContext m_context;
	}
}
