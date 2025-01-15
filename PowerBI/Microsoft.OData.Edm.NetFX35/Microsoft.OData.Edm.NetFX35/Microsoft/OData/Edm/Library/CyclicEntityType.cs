using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200014F RID: 335
	internal class CyclicEntityType : BadEntityType
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public CyclicEntityType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicEntity, Strings.Bad_CyclicEntity(qualifiedName))
			})
		{
		}
	}
}
