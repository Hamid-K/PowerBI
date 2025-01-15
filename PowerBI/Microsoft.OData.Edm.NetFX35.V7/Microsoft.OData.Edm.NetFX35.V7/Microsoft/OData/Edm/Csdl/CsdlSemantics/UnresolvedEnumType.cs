using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000156 RID: 342
	internal class UnresolvedEnumType : BadEnumType, IUnresolvedElement
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x00019430 File Offset: 0x00017630
		public UnresolvedEnumType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumType, Strings.Bad_UnresolvedEnumType(qualifiedName))
			})
		{
		}
	}
}
