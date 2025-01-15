using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C9 RID: 457
	internal class CsdlLabeledExpression : CsdlExpressionBase
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x000239DD File Offset: 0x00021BDD
		public CsdlLabeledExpression(string label, CsdlExpressionBase element, CsdlLocation location)
			: base(location)
		{
			this.label = label;
			this.element = element;
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00008DED File Offset: 0x00006FED
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000239F4 File Offset: 0x00021BF4
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x000239FC File Offset: 0x00021BFC
		public CsdlExpressionBase Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x040006DA RID: 1754
		private readonly string label;

		// Token: 0x040006DB RID: 1755
		private readonly CsdlExpressionBase element;
	}
}
