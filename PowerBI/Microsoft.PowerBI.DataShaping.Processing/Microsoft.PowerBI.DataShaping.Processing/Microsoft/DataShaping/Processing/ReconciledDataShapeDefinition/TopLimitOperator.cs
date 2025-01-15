using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000029 RID: 41
	internal sealed class TopLimitOperator : DataLimitOperator
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000050EC File Offset: 0x000032EC
		internal TopLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
			: base(count, dbCount, isExceededDbCount, kind, warningCount)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000050FB File Offset: 0x000032FB
		internal override bool SkipInstancesWhenExceeded
		{
			get
			{
				return true;
			}
		}
	}
}
