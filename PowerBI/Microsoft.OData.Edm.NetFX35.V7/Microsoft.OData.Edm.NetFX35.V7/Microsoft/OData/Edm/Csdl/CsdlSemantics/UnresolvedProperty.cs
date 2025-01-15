using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A6 RID: 422
	internal class UnresolvedProperty : BadProperty, IUnresolvedElement
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		public UnresolvedProperty(IEdmStructuredType declaringType, string name, EdmLocation location)
			: base(declaringType, name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedProperty, Strings.Bad_UnresolvedProperty(name))
			})
		{
		}
	}
}
