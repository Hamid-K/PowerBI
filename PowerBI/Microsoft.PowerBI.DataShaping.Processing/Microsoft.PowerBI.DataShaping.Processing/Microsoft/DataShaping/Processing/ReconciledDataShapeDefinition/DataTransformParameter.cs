using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200003C RID: 60
	internal sealed class DataTransformParameter
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00005B28 File Offset: 0x00003D28
		internal DataTransformParameter(string name, ExpressionNode value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005B3E File Offset: 0x00003D3E
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005B46 File Offset: 0x00003D46
		internal ExpressionNode Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0400010D RID: 269
		private readonly string _name;

		// Token: 0x0400010E RID: 270
		private readonly ExpressionNode _value;
	}
}
