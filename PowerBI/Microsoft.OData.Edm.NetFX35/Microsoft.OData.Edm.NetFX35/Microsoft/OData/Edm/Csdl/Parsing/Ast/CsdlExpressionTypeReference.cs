using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000182 RID: 386
	internal class CsdlExpressionTypeReference : CsdlTypeReference
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x000118EE File Offset: 0x0000FAEE
		public CsdlExpressionTypeReference(ICsdlTypeExpression typeExpression, bool isNullable, CsdlLocation location)
			: base(isNullable, location)
		{
			this.typeExpression = typeExpression;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000118FF File Offset: 0x0000FAFF
		public ICsdlTypeExpression TypeExpression
		{
			get
			{
				return this.typeExpression;
			}
		}

		// Token: 0x040003C4 RID: 964
		private readonly ICsdlTypeExpression typeExpression;
	}
}
