using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B5 RID: 437
	internal class UnresolvedProperty : BadProperty, IUnresolvedElement
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x00021F08 File Offset: 0x00020108
		public UnresolvedProperty(IEdmStructuredType declaringType, string name, EdmLocation location)
			: base(declaringType, name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedProperty, Strings.Bad_UnresolvedProperty(name))
			})
		{
		}
	}
}
