using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200002A RID: 42
	internal sealed class BottomLimitOperator : DataLimitOperator
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000050FE File Offset: 0x000032FE
		internal BottomLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
			: base(count, dbCount, isExceededDbCount, kind, warningCount)
		{
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000510D File Offset: 0x0000330D
		internal override bool SkipInstancesWhenExceeded
		{
			get
			{
				return false;
			}
		}
	}
}
