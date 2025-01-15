using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000DB RID: 219
	internal sealed class IntermediateQueryTransformParameter
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001CB9A File Offset: 0x0001AD9A
		internal IntermediateQueryTransformParameter(string name, ExpressionNode expression)
		{
			this._name = name;
			this._expression = expression;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		internal ExpressionNode Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x040003F9 RID: 1017
		private readonly string _name;

		// Token: 0x040003FA RID: 1018
		private readonly ExpressionNode _expression;
	}
}
