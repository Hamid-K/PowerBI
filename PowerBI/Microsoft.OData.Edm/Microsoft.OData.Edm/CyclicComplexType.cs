using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006B RID: 107
	internal class CyclicComplexType : BadComplexType
	{
		// Token: 0x0600022E RID: 558 RVA: 0x000055BA File Offset: 0x000037BA
		public CyclicComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicComplex, Strings.Bad_CyclicComplex(qualifiedName))
			})
		{
		}
	}
}
