using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EA RID: 490
	internal class CsdlBinaryTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00025E68 File Offset: 0x00024068
		public CsdlBinaryTypeReference(bool isUnbounded, int? maxLength, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.Binary, typeName, isNullable, location)
		{
			base.IsUnbounded = isUnbounded;
			base.MaxLength = maxLength;
		}
	}
}
