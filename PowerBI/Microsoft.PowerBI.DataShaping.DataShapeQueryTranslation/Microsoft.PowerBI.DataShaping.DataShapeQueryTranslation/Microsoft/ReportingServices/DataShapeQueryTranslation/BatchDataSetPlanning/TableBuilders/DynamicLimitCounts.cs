using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C8 RID: 456
	internal readonly struct DynamicLimitCounts
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x00041BD0 File Offset: 0x0003FDD0
		internal DynamicLimitCounts(ExpressionNode actualIntersectionCount, ExpressionNode actualPrimaryCount, ExpressionNode targetPrimaryCount, ExpressionNode actualSecondaryCount, ExpressionNode targetSecondaryCount)
		{
			this.ActualIntersectionCount = actualIntersectionCount;
			this.ActualPrimaryCount = actualPrimaryCount;
			this.TargetPrimaryCount = targetPrimaryCount;
			this.ActualSecondaryCount = actualSecondaryCount;
			this.TargetSecondaryCount = targetSecondaryCount;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00041BF7 File Offset: 0x0003FDF7
		internal ExpressionNode ActualIntersectionCount { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00041BFF File Offset: 0x0003FDFF
		internal ExpressionNode ActualPrimaryCount { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00041C07 File Offset: 0x0003FE07
		internal ExpressionNode TargetPrimaryCount { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00041C0F File Offset: 0x0003FE0F
		internal ExpressionNode ActualSecondaryCount { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00041C17 File Offset: 0x0003FE17
		internal ExpressionNode TargetSecondaryCount { get; }
	}
}
