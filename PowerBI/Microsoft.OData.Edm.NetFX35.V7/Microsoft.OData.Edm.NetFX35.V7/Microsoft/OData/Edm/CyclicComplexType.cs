using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000042 RID: 66
	internal class CyclicComplexType : BadComplexType
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00009822 File Offset: 0x00007A22
		public CyclicComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicComplex, Strings.Bad_CyclicComplex(qualifiedName))
			})
		{
		}
	}
}
