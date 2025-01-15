using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A8 RID: 424
	internal class UnresolvedPrimitiveType : BadPrimitiveType, IUnresolvedElement
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x00020F64 File Offset: 0x0001F164
		public UnresolvedPrimitiveType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, EdmPrimitiveTypeKind.None, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedPrimitiveType, Strings.Bad_UnresolvedPrimitiveType(qualifiedName))
			})
		{
		}
	}
}
