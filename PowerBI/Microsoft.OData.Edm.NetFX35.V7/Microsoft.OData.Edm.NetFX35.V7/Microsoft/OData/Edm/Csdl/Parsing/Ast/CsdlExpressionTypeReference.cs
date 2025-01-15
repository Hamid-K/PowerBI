using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E5 RID: 485
	internal class CsdlExpressionTypeReference : CsdlTypeReference
	{
		// Token: 0x06000CEB RID: 3307 RVA: 0x00023F30 File Offset: 0x00022130
		public CsdlExpressionTypeReference(ICsdlTypeExpression typeExpression, bool isNullable, CsdlLocation location)
			: base(isNullable, location)
		{
			this.typeExpression = typeExpression;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00023F41 File Offset: 0x00022141
		public ICsdlTypeExpression TypeExpression
		{
			get
			{
				return this.typeExpression;
			}
		}

		// Token: 0x04000704 RID: 1796
		private readonly ICsdlTypeExpression typeExpression;
	}
}
