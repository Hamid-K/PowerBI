using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EC RID: 492
	internal class CsdlDecimalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00025E97 File Offset: 0x00024097
		public CsdlDecimalTypeReference(int? precision, int? scale, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Decimal, typeName, isNullable, location)
		{
			base.Precision = precision;
			base.Scale = scale;
		}
	}
}
