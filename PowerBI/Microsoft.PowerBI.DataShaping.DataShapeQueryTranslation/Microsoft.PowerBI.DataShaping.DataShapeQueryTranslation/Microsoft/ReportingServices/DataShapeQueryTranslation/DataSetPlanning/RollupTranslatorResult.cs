using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x0200010B RID: 267
	internal sealed class RollupTranslatorResult
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x0002826C File Offset: 0x0002646C
		internal RollupTranslatorResult(IList<DataSetPlan> dataSetPlans, ReadOnlyExpressionTable expressionTable)
		{
			this.m_dataSetPlans = dataSetPlans.ToReadOnlyCollection<DataSetPlan>();
			this.m_expressionTable = expressionTable;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00028287 File Offset: 0x00026487
		public ReadOnlyCollection<DataSetPlan> DataSetPlans
		{
			get
			{
				return this.m_dataSetPlans;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002828F File Offset: 0x0002648F
		public ReadOnlyExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x04000510 RID: 1296
		private readonly ReadOnlyCollection<DataSetPlan> m_dataSetPlans;

		// Token: 0x04000511 RID: 1297
		private readonly ReadOnlyExpressionTable m_expressionTable;
	}
}
