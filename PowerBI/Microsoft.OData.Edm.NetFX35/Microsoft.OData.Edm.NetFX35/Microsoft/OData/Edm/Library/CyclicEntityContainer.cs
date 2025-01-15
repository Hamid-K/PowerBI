using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200014E RID: 334
	internal class CyclicEntityContainer : BadEntityContainer
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public CyclicEntityContainer(string name, EdmLocation location)
			: base(name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicEntityContainer, Strings.Bad_CyclicEntityContainer(name))
			})
		{
		}
	}
}
