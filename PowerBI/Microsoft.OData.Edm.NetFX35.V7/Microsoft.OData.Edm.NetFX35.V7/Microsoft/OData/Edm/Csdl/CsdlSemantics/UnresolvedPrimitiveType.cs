using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000197 RID: 407
	internal class UnresolvedPrimitiveType : BadPrimitiveType, IUnresolvedElement
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public UnresolvedPrimitiveType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, EdmPrimitiveTypeKind.None, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedPrimitiveType, Strings.Bad_UnresolvedPrimitiveType(qualifiedName))
			})
		{
		}
	}
}
