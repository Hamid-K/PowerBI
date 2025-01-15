using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000047 RID: 71
	internal sealed class MatchCondition
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00006061 File Offset: 0x00004261
		internal MatchCondition(FieldValueExpressionNode field, bool value)
		{
			this._field = field;
			this._value = value;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006077 File Offset: 0x00004277
		internal FieldValueExpressionNode Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000607F File Offset: 0x0000427F
		internal bool Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000129 RID: 297
		private readonly FieldValueExpressionNode _field;

		// Token: 0x0400012A RID: 298
		private readonly bool _value;
	}
}
