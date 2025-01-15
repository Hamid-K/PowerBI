using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000041 RID: 65
	internal sealed class DiscardCondition
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00005F8C File Offset: 0x0000418C
		internal DiscardCondition(FieldValueExpressionNode field, bool value, DiscardConditionComparisonOperator op)
		{
			this.Field = field;
			this.Value = value;
			this.Operator = op;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005FA9 File Offset: 0x000041A9
		internal FieldValueExpressionNode Field { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00005FB1 File Offset: 0x000041B1
		internal bool Value { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00005FB9 File Offset: 0x000041B9
		internal DiscardConditionComparisonOperator Operator { get; }
	}
}
