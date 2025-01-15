using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200020E RID: 526
	internal class UnresolvedEntityType : BadEntityType, IUnresolvedElement
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x00022DFC File Offset: 0x00020FFC
		public UnresolvedEntityType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntityType, Strings.Bad_UnresolvedEntityType(qualifiedName))
			})
		{
		}
	}
}
