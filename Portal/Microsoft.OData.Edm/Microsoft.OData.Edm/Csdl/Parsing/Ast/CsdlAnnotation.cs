using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DC RID: 476
	internal class CsdlAnnotation : CsdlElement
	{
		// Token: 0x06000D55 RID: 3413 RVA: 0x00025BF9 File Offset: 0x00023DF9
		public CsdlAnnotation(string term, string qualifier, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.expression = expression;
			this.qualifier = qualifier;
			this.term = term;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00025C18 File Offset: 0x00023E18
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00025C20 File Offset: 0x00023E20
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00025C28 File Offset: 0x00023E28
		public string Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x04000757 RID: 1879
		private readonly CsdlExpressionBase expression;

		// Token: 0x04000758 RID: 1880
		private readonly string qualifier;

		// Token: 0x04000759 RID: 1881
		private readonly string term;
	}
}
