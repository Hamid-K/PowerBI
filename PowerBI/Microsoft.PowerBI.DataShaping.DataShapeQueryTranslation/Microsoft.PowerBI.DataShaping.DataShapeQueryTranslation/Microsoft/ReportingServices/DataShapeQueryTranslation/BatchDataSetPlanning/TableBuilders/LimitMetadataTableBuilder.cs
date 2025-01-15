using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D8 RID: 472
	internal sealed class LimitMetadataTableBuilder
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x000441BE File Offset: 0x000423BE
		internal LimitMetadataTableBuilder(WritableExpressionTable expressionTable, TranslationErrorContext errorContext)
		{
			this.m_expressionTable = expressionTable;
			this.m_errorContext = errorContext;
			this.m_columns = new List<SingleRowAdditionalColumn>();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000441E0 File Offset: 0x000423E0
		internal ExpressionId AddColumn(string planName, ExpressionNode value, ObjectType objectType, Identifier objectId = null)
		{
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, objectType, objectId ?? planName, planName);
			ExpressionId expressionId = this.m_expressionTable.Add(value);
			SingleRowAdditionalColumn singleRowAdditionalColumn = new SingleRowAdditionalColumn(planName, expressionId, expressionContext);
			this.m_columns.Add(singleRowAdditionalColumn);
			return expressionId;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004422A File Offset: 0x0004242A
		internal PlanOperationContext ToTableContext(IScope rowScope)
		{
			if (this.m_columns.IsNullOrEmpty<SingleRowAdditionalColumn>())
			{
				return null;
			}
			return new PlanOperationContext(PlanOperationBuilder.SingleRow(null, null, null, this.m_columns), rowScope.AsReadOnlyList<IScope>(), null);
		}

		// Token: 0x040007A9 RID: 1961
		private readonly List<SingleRowAdditionalColumn> m_columns;

		// Token: 0x040007AA RID: 1962
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x040007AB RID: 1963
		private readonly TranslationErrorContext m_errorContext;
	}
}
