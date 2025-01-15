using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A5 RID: 421
	internal class UnresolvedEntityType : BadEntityType, IUnresolvedElement
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0001FAAF File Offset: 0x0001DCAF
		public UnresolvedEntityType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntityType, Strings.Bad_UnresolvedEntityType(qualifiedName))
			})
		{
		}
	}
}
