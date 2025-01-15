using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F3 RID: 243
	internal sealed class DataSetPlannerExpressionTranslationResult
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x000255EF File Offset: 0x000237EF
		internal DataSetPlannerExpressionTranslationResult(ReadOnlyExpressionTable outputExpressionTable, List<DataSetPlan> subQueryDataSetPlans)
		{
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_subQueryPlans = subQueryDataSetPlans;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00025605 File Offset: 0x00023805
		public ReadOnlyExpressionTable OutputExpressionTable
		{
			get
			{
				return this.m_outputExpressionTable;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002560D File Offset: 0x0002380D
		public List<DataSetPlan> SubQueryPlans
		{
			get
			{
				return this.m_subQueryPlans;
			}
		}

		// Token: 0x040004AA RID: 1194
		private readonly ReadOnlyExpressionTable m_outputExpressionTable;

		// Token: 0x040004AB RID: 1195
		private readonly List<DataSetPlan> m_subQueryPlans;
	}
}
