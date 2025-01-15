using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200014D RID: 333
	internal class CyclicComplexType : BadComplexType
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public CyclicComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicComplex, Strings.Bad_CyclicComplex(qualifiedName))
			})
		{
		}
	}
}
