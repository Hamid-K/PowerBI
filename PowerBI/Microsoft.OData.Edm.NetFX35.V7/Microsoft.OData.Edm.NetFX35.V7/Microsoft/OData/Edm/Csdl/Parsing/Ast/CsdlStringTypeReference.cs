using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F8 RID: 504
	internal class CsdlStringTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x0002448F File Offset: 0x0002268F
		public CsdlStringTypeReference(bool isUnbounded, int? maxLength, bool? isUnicode, string typeName, bool isNullable, CsdlLocation location)
			: base(EdmPrimitiveTypeKind.String, typeName, isNullable, location)
		{
			base.IsUnbounded = isUnbounded;
			base.MaxLength = maxLength;
			base.IsUnicode = isUnicode;
		}
	}
}
