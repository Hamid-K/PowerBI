using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000133 RID: 307
	internal sealed class BatchQueryMemberDiscardCondition
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x0002DD62 File Offset: 0x0002BF62
		internal BatchQueryMemberDiscardCondition(ExpressionId expressionId, bool matchValue, BatchDiscardConditionOperator op)
		{
			this.ExpressionId = expressionId;
			this.MatchValue = matchValue;
			this.Operator = op;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002DD7F File Offset: 0x0002BF7F
		public ExpressionId ExpressionId { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0002DD87 File Offset: 0x0002BF87
		public bool MatchValue { get; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002DD8F File Offset: 0x0002BF8F
		public BatchDiscardConditionOperator Operator { get; }
	}
}
