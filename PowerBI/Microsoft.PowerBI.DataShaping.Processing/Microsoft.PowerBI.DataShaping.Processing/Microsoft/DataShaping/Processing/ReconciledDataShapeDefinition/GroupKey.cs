using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000043 RID: 67
	internal sealed class GroupKey
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00005FE7 File Offset: 0x000041E7
		internal GroupKey(ExpressionNode value)
		{
			this._value = value;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005FF6 File Offset: 0x000041F6
		internal ExpressionNode Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000123 RID: 291
		private readonly ExpressionNode _value;
	}
}
