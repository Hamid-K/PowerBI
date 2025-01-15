using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DD RID: 477
	internal class CsdlDecimalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x00023CE4 File Offset: 0x00021EE4
		public CsdlDecimalTypeReference(int? precision, int? scale, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Decimal, typeName, isNullable, location)
		{
			base.Precision = precision;
			base.Scale = scale;
		}
	}
}
