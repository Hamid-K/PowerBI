using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000044 RID: 68
	internal class CyclicEntityType : BadEntityType
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00009868 File Offset: 0x00007A68
		public CyclicEntityType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicEntity, Strings.Bad_CyclicEntity(qualifiedName))
			})
		{
		}
	}
}
