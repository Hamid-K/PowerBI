using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000158 RID: 344
	internal sealed class GeneratedScalarDeclaration
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x000343D6 File Offset: 0x000325D6
		internal GeneratedScalarDeclaration(string planName, QueryVariableReferenceExpression expression)
		{
			this.m_planName = planName;
			this.m_expression = expression;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000343EC File Offset: 0x000325EC
		public string PlanName
		{
			get
			{
				return this.m_planName;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000343F4 File Offset: 0x000325F4
		public QueryVariableReferenceExpression Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x04000649 RID: 1609
		private readonly string m_planName;

		// Token: 0x0400064A RID: 1610
		private readonly QueryVariableReferenceExpression m_expression;
	}
}
