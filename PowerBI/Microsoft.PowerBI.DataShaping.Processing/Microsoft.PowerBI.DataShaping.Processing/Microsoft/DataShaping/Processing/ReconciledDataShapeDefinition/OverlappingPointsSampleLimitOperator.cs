using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200002D RID: 45
	internal sealed class OverlappingPointsSampleLimitOperator : DataLimitOperator
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00005134 File Offset: 0x00003334
		internal OverlappingPointsSampleLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
			: base(count, dbCount, isExceededDbCount, kind, warningCount)
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005143 File Offset: 0x00003343
		internal override bool SkipInstancesWhenExceeded
		{
			get
			{
				return false;
			}
		}
	}
}
