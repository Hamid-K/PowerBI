using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000079 RID: 121
	internal sealed class SubQuery
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x000152C4 File Offset: 0x000134C4
		internal SubQuery(DataSetPlan dataSetPlan, QueryExpression subQueryExpression, string expressionName, IEnumerable<QueryBaseDeclarationExpression> declarations, ExpressionId? targetExpressionId)
		{
			this.m_targetExpressionId = targetExpressionId;
			this.m_dataSetPlan = dataSetPlan;
			this.m_subQueryExpression = subQueryExpression;
			this.m_expressionName = expressionName;
			this.m_declarations = declarations;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000152F1 File Offset: 0x000134F1
		public QueryExpression SubQueryExpression
		{
			get
			{
				return this.m_subQueryExpression;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000152F9 File Offset: 0x000134F9
		public string ExpressionName
		{
			get
			{
				return this.m_expressionName;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00015301 File Offset: 0x00013501
		public IEnumerable<QueryBaseDeclarationExpression> Declarations
		{
			get
			{
				return this.m_declarations;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001530C File Offset: 0x0001350C
		public bool Matches(string expressionName, ExpressionId? targetExpressionId, DataSetPlan dataSetPlan)
		{
			if (this.m_dataSetPlan != dataSetPlan)
			{
				return false;
			}
			if (this.m_targetExpressionId == null || targetExpressionId == null)
			{
				return this.m_expressionName == expressionName;
			}
			return this.m_targetExpressionId.Value == targetExpressionId.Value;
		}

		// Token: 0x040002F1 RID: 753
		private readonly ExpressionId? m_targetExpressionId;

		// Token: 0x040002F2 RID: 754
		private readonly DataSetPlan m_dataSetPlan;

		// Token: 0x040002F3 RID: 755
		private readonly QueryExpression m_subQueryExpression;

		// Token: 0x040002F4 RID: 756
		private readonly string m_expressionName;

		// Token: 0x040002F5 RID: 757
		private readonly IEnumerable<QueryBaseDeclarationExpression> m_declarations;
	}
}
