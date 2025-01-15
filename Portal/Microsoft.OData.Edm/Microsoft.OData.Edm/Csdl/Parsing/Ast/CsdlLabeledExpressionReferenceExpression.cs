using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D9 RID: 473
	internal class CsdlLabeledExpressionReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00025BC9 File Offset: 0x00023DC9
		public CsdlLabeledExpressionReferenceExpression(string label, CsdlLocation location)
			: base(location)
		{
			this.label = label;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0001206B File Offset: 0x0001026B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00025BD9 File Offset: 0x00023DD9
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x04000755 RID: 1877
		private readonly string label;
	}
}
