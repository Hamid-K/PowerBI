using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200002F RID: 47
	internal class CsdlLabeledExpressionReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003747 File Offset: 0x00001947
		public CsdlLabeledExpressionReferenceExpression(string label, CsdlLocation location)
			: base(location)
		{
			this.label = label;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003757 File Offset: 0x00001957
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.LabeledExpressionReference;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000375B File Offset: 0x0000195B
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x04000048 RID: 72
		private readonly string label;
	}
}
