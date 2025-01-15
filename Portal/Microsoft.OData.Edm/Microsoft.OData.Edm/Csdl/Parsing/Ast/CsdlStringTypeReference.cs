using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000205 RID: 517
	internal class CsdlStringTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x000265C1 File Offset: 0x000247C1
		public CsdlStringTypeReference(bool isUnbounded, int? maxLength, bool? isUnicode, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.String, typeName, isNullable, location)
		{
			base.IsUnbounded = isUnbounded;
			base.MaxLength = maxLength;
			base.IsUnicode = isUnicode;
		}
	}
}
