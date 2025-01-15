using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200002E RID: 46
	internal sealed class TopNPerLevelLimitOperator : DataLimitOperator
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005146 File Offset: 0x00003346
		internal TopNPerLevelLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
			: base(count, dbCount, isExceededDbCount, kind, warningCount)
		{
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005155 File Offset: 0x00003355
		internal override bool SkipInstancesWhenExceeded
		{
			get
			{
				return false;
			}
		}
	}
}
