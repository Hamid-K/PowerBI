using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CD RID: 461
	internal class CsdlAnnotation : CsdlElement
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00023A34 File Offset: 0x00021C34
		public CsdlAnnotation(string term, string qualifier, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.expression = expression;
			this.qualifier = qualifier;
			this.term = term;
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00023A53 File Offset: 0x00021C53
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00023A5B File Offset: 0x00021C5B
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00023A63 File Offset: 0x00021C63
		public string Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x040006DE RID: 1758
		private readonly CsdlExpressionBase expression;

		// Token: 0x040006DF RID: 1759
		private readonly string qualifier;

		// Token: 0x040006E0 RID: 1760
		private readonly string term;
	}
}
