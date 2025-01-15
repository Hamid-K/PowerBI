using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000210 RID: 528
	internal class UnresolvedNavigationPropertyPath : BadNavigationProperty, IUnresolvedElement
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x00022E60 File Offset: 0x00021060
		public UnresolvedNavigationPropertyPath(IEdmEntityType startingType, string path, EdmLocation location)
			: base(startingType, path, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedNavigationPropertyPath, Strings.Bad_UnresolvedNavigationPropertyPath(path, startingType.FullName()))
			})
		{
		}
	}
}
