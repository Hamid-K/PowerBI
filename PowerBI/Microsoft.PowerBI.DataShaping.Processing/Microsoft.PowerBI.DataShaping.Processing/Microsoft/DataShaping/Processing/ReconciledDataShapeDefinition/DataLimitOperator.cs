using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000028 RID: 40
	internal abstract class DataLimitOperator
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00005097 File Offset: 0x00003297
		protected DataLimitOperator(ExpressionNode count, ExpressionNode dbCount, ExpressionNode isExceededDbCount, ExceededDetectionKind kind, ExpressionNode warningCount)
		{
			this.Count = count;
			this.DbCount = dbCount;
			this.IsExceededDbCount = isExceededDbCount;
			this.Kind = kind;
			this.WarningCount = warningCount;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000050C4 File Offset: 0x000032C4
		internal ExpressionNode Count { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000050CC File Offset: 0x000032CC
		internal ExpressionNode DbCount { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000050D4 File Offset: 0x000032D4
		internal ExpressionNode IsExceededDbCount { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000050DC File Offset: 0x000032DC
		internal ExpressionNode WarningCount { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013C RID: 316
		internal abstract bool SkipInstancesWhenExceeded { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000050E4 File Offset: 0x000032E4
		internal ExceededDetectionKind Kind { get; }
	}
}
