using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000033 RID: 51
	internal class CsdlAnnotation : CsdlElement
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000037B8 File Offset: 0x000019B8
		public CsdlAnnotation(string term, string qualifier, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.expression = expression;
			this.qualifier = qualifier;
			this.term = term;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000037D7 File Offset: 0x000019D7
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000037DF File Offset: 0x000019DF
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000037E7 File Offset: 0x000019E7
		public string Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly CsdlExpressionBase expression;

		// Token: 0x0400004D RID: 77
		private readonly string qualifier;

		// Token: 0x0400004E RID: 78
		private readonly string term;
	}
}
