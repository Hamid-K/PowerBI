using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F2 RID: 498
	internal class CsdlExpressionTypeReference : CsdlTypeReference
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x0002607D File Offset: 0x0002427D
		public CsdlExpressionTypeReference(ICsdlTypeExpression typeExpression, bool isNullable, CsdlLocation location)
			: base(isNullable, location)
		{
			this.typeExpression = typeExpression;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002608E File Offset: 0x0002428E
		public ICsdlTypeExpression TypeExpression
		{
			get
			{
				return this.typeExpression;
			}
		}

		// Token: 0x0400077A RID: 1914
		private readonly ICsdlTypeExpression typeExpression;
	}
}
