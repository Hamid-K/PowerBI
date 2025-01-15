using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CA RID: 458
	internal class CsdlLabeledExpressionReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x00023A04 File Offset: 0x00021C04
		public CsdlLabeledExpressionReferenceExpression(string label, CsdlLocation location)
			: base(location)
		{
			this.label = label;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00013B87 File Offset: 0x00011D87
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00023A14 File Offset: 0x00021C14
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x040006DC RID: 1756
		private readonly string label;
	}
}
