using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DB RID: 475
	internal class CsdlBinaryTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00023CA5 File Offset: 0x00021EA5
		public CsdlBinaryTypeReference(bool isUnbounded, int? maxLength, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Binary, typeName, isNullable, location)
		{
			base.IsUnbounded = isUnbounded;
			base.MaxLength = maxLength;
		}
	}
}
