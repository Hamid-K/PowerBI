using System;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C6 RID: 198
	internal sealed class BatchExpressionTableLookup : IExpressionTableLookup
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0001FDAF File Offset: 0x0001DFAF
		internal BatchExpressionTableLookup(BatchDataSetPlanningResult batchDataSetPlanningResult, ReadOnlyCollection<BatchQueryGenerationResult> queryGenerationResults)
		{
			this.m_batchDataSetPlanningResult = batchDataSetPlanningResult;
			this.m_queryGenerationResults = queryGenerationResults;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001FDC5 File Offset: 0x0001DFC5
		public int Count
		{
			get
			{
				return this.m_queryGenerationResults.Count;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001FDD2 File Offset: 0x0001DFD2
		public int GetExpressionTableIndex(IContextItem item)
		{
			return 0;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001FDD5 File Offset: 0x0001DFD5
		public ExpressionTable GetExpressionTable(int index)
		{
			return this.m_queryGenerationResults[index].ExpressionTable;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		public ExpressionTable GetExpressionTable(IContextItem item)
		{
			int expressionTableIndex = this.GetExpressionTableIndex(item);
			return this.GetExpressionTable(expressionTableIndex);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001FE04 File Offset: 0x0001E004
		public ExpressionTable GetFallbackExpressionTable()
		{
			return this.m_batchDataSetPlanningResult.ExpressionTable;
		}

		// Token: 0x04000419 RID: 1049
		private readonly BatchDataSetPlanningResult m_batchDataSetPlanningResult;

		// Token: 0x0400041A RID: 1050
		private readonly ReadOnlyCollection<BatchQueryGenerationResult> m_queryGenerationResults;
	}
}
