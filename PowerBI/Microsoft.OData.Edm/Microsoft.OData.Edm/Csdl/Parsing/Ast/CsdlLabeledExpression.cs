using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D8 RID: 472
	internal class CsdlLabeledExpression : CsdlExpressionBase
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00025BA2 File Offset: 0x00023DA2
		public CsdlLabeledExpression(string label, CsdlExpressionBase element, CsdlLocation location)
			: base(location)
		{
			this.label = label;
			this.element = element;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00004C41 File Offset: 0x00002E41
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00025BB9 File Offset: 0x00023DB9
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00025BC1 File Offset: 0x00023DC1
		public CsdlExpressionBase Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x04000753 RID: 1875
		private readonly string label;

		// Token: 0x04000754 RID: 1876
		private readonly CsdlExpressionBase element;
	}
}
