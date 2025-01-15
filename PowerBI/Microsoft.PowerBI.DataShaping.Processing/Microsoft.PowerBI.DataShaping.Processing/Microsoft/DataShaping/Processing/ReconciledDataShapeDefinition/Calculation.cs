using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000022 RID: 34
	internal sealed class Calculation
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004EAB File Offset: 0x000030AB
		internal Calculation(string id, ExpressionNode value)
		{
			this._id = id;
			this._value = value;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004EC1 File Offset: 0x000030C1
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004EC9 File Offset: 0x000030C9
		internal ExpressionNode Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040000A2 RID: 162
		private readonly string _id;

		// Token: 0x040000A3 RID: 163
		private readonly ExpressionNode _value;
	}
}
