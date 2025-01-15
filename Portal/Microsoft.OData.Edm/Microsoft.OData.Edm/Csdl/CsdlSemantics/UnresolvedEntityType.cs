using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B4 RID: 436
	internal class UnresolvedEntityType : BadEntityType, IUnresolvedElement
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x00021EE3 File Offset: 0x000200E3
		public UnresolvedEntityType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntityType, Strings.Bad_UnresolvedEntityType(qualifiedName))
			})
		{
		}
	}
}
