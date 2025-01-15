using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000080 RID: 128
	internal sealed class RdmQueryLiteralExpression : IRdmQueryExpression
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000C507 File Offset: 0x0000A707
		internal RdmQueryLiteralExpression(PrimitiveValue value)
		{
			this._value = value;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000C516 File Offset: 0x0000A716
		internal PrimitiveValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C51E File Offset: 0x0000A71E
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.Literal = this._value;
		}

		// Token: 0x0400019C RID: 412
		private readonly PrimitiveValue _value;
	}
}
