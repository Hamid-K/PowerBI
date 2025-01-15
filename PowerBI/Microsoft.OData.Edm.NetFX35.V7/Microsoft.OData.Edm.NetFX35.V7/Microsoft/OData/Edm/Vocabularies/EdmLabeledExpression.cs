using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F5 RID: 245
	public class EdmLabeledExpression : EdmElement, IEdmLabeledExpression, IEdmNamedElement, IEdmElement, IEdmExpression
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x00013AFE File Offset: 0x00011CFE
		public EdmLabeledExpression(string name, IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			this.name = name;
			this.expression = expression;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00013B2C File Offset: 0x00011D2C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00013B34 File Offset: 0x00011D34
		public IEdmExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00008DED File Offset: 0x00006FED
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Labeled;
			}
		}

		// Token: 0x0400041B RID: 1051
		private readonly string name;

		// Token: 0x0400041C RID: 1052
		private readonly IEdmExpression expression;
	}
}
