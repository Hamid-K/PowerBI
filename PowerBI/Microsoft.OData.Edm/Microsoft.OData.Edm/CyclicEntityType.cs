using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006D RID: 109
	internal class CyclicEntityType : BadEntityType
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00005600 File Offset: 0x00003800
		public CyclicEntityType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicEntity, Strings.Bad_CyclicEntity(qualifiedName))
			})
		{
		}
	}
}
