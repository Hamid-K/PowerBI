using System;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C7 RID: 199
	internal sealed class RegularExpressionTableLookup : IExpressionTableLookup
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0001FE11 File Offset: 0x0001E011
		internal RegularExpressionTableLookup(DataSetPlanningResult dataSetPlanningResult, ReadOnlyCollection<QueryGenerationResult> queryGenerationResults)
		{
			this.m_dataSetPlanningResult = dataSetPlanningResult;
			this.m_queryGenerationResults = queryGenerationResults;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001FE27 File Offset: 0x0001E027
		public int Count
		{
			get
			{
				return this.m_queryGenerationResults.Count;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001FE34 File Offset: 0x0001E034
		public int GetExpressionTableIndex(IContextItem item)
		{
			return this.m_dataSetPlanningResult.GetOutputDataSetPlanForItem(item).PlanIndex;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001FE47 File Offset: 0x0001E047
		public ExpressionTable GetExpressionTable(int index)
		{
			return this.m_queryGenerationResults[index].ExpressionTable;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001FE5C File Offset: 0x0001E05C
		public ExpressionTable GetExpressionTable(IContextItem item)
		{
			int expressionTableIndex = this.GetExpressionTableIndex(item);
			return this.GetExpressionTable(expressionTableIndex);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001FE78 File Offset: 0x0001E078
		public ExpressionTable GetFallbackExpressionTable()
		{
			return this.m_dataSetPlanningResult.ExpressionTable;
		}

		// Token: 0x0400041B RID: 1051
		private readonly DataSetPlanningResult m_dataSetPlanningResult;

		// Token: 0x0400041C RID: 1052
		private readonly ReadOnlyCollection<QueryGenerationResult> m_queryGenerationResults;
	}
}
