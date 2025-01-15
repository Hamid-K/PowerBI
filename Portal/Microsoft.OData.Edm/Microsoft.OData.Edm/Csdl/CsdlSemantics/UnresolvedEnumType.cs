using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000165 RID: 357
	internal class UnresolvedEnumType : BadEnumType, IUnresolvedElement
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x0001B4EC File Offset: 0x000196EC
		public UnresolvedEnumType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumType, Strings.Bad_UnresolvedEnumType(qualifiedName))
			})
		{
		}
	}
}
