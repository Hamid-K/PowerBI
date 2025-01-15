using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000045 RID: 69
	internal sealed class ScopeKey
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00006015 File Offset: 0x00004215
		internal ScopeKey(ExpressionNode value)
		{
			this._value = value;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00006024 File Offset: 0x00004224
		internal ExpressionNode Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000125 RID: 293
		private readonly ExpressionNode _value;
	}
}
