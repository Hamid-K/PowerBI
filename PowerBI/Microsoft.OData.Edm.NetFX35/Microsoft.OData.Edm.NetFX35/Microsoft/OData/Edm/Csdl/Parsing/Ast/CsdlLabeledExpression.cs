using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200002E RID: 46
	internal class CsdlLabeledExpression : CsdlExpressionBase
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000371C File Offset: 0x0000191C
		public CsdlLabeledExpression(string label, CsdlExpressionBase element, CsdlLocation location)
			: base(location)
		{
			this.label = label;
			this.element = element;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003733 File Offset: 0x00001933
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003737 File Offset: 0x00001937
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000373F File Offset: 0x0000193F
		public CsdlExpressionBase Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x04000046 RID: 70
		private readonly string label;

		// Token: 0x04000047 RID: 71
		private readonly CsdlExpressionBase element;
	}
}
