using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B6 RID: 438
	internal class UnresolvedNavigationPropertyPath : BadNavigationProperty, IUnresolvedElement
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00021F38 File Offset: 0x00020138
		public UnresolvedNavigationPropertyPath(IEdmStructuredType startingType, string path, EdmLocation location)
			: base(startingType, path, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedNavigationPropertyPath, Strings.Bad_UnresolvedNavigationPropertyPath(path, startingType.FullTypeName()))
			})
		{
		}
	}
}
