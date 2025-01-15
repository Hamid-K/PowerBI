using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001BC RID: 444
	internal sealed class BatchDataSetPlannerJoinPredicates
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0003F1AB File Offset: 0x0003D3AB
		public BatchDataSetPlannerJoinPredicates(IReadOnlyList<Calculation> calculations, IReadOnlyList<PlanDataTransformColumnMeasure> measureTransformColumns)
		{
			this.Calculations = calculations;
			this.MeasureTransformColumns = measureTransformColumns;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003F1C1 File Offset: 0x0003D3C1
		internal IReadOnlyList<Calculation> Calculations { get; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0003F1C9 File Offset: 0x0003D3C9
		internal IReadOnlyList<PlanDataTransformColumnMeasure> MeasureTransformColumns { get; }

		// Token: 0x04000755 RID: 1877
		internal static readonly BatchDataSetPlannerJoinPredicates Empty = new BatchDataSetPlannerJoinPredicates(Util.EmptyReadOnlyList<Calculation>(), Util.EmptyReadOnlyList<PlanDataTransformColumnMeasure>());
	}
}
