using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200002C RID: 44
	internal sealed class BinnedLineSampleLimitOperator : DataLimitOperator
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005122 File Offset: 0x00003322
		internal BinnedLineSampleLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
			: base(count, dbCount, isExceededDbCount, kind, warningCount)
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005131 File Offset: 0x00003331
		internal override bool SkipInstancesWhenExceeded
		{
			get
			{
				return false;
			}
		}
	}
}
