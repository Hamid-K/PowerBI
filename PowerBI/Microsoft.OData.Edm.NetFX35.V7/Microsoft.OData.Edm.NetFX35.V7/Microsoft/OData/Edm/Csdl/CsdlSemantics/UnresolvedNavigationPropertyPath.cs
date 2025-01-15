using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A7 RID: 423
	internal class UnresolvedNavigationPropertyPath : BadNavigationProperty, IUnresolvedElement
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x0001FB04 File Offset: 0x0001DD04
		public UnresolvedNavigationPropertyPath(IEdmStructuredType startingType, string path, EdmLocation location)
			: base(startingType, path, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedNavigationPropertyPath, Strings.Bad_UnresolvedNavigationPropertyPath(path, startingType.FullTypeName()))
			})
		{
		}
	}
}
