using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001D6 RID: 470
	internal class UnresolvedPrimitiveType : BadPrimitiveType, IUnresolvedElement
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x00019AC4 File Offset: 0x00017CC4
		public UnresolvedPrimitiveType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, EdmPrimitiveTypeKind.None, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedPrimitiveType, Strings.Bad_UnresolvedPrimitiveType(qualifiedName))
			})
		{
		}
	}
}
