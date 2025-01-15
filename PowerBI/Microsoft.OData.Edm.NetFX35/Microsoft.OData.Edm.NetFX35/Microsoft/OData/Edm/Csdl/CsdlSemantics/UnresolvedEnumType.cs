using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000050 RID: 80
	internal class UnresolvedEnumType : BadEnumType, IUnresolvedElement
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00003B70 File Offset: 0x00001D70
		public UnresolvedEnumType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumType, Strings.Bad_UnresolvedEnumType(qualifiedName))
			})
		{
		}
	}
}
