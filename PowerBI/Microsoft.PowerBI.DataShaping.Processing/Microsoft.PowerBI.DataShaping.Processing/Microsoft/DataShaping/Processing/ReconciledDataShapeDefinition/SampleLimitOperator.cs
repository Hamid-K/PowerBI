using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200002B RID: 43
	internal sealed class SampleLimitOperator : DataLimitOperator
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00005110 File Offset: 0x00003310
		internal SampleLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
			: base(count, dbCount, isExceededDbCount, kind, warningCount)
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000511F File Offset: 0x0000331F
		internal override bool SkipInstancesWhenExceeded
		{
			get
			{
				return false;
			}
		}
	}
}
