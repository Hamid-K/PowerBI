using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200020B RID: 523
	internal class UnresolvedComplexType : BadComplexType, IUnresolvedElement
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x00022D68 File Offset: 0x00020F68
		public UnresolvedComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedComplexType, Strings.Bad_UnresolvedComplexType(qualifiedName))
			})
		{
		}
	}
}
