using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006C RID: 108
	internal class CyclicEntityContainer : BadEntityContainer
	{
		// Token: 0x0600022F RID: 559 RVA: 0x000055DD File Offset: 0x000037DD
		public CyclicEntityContainer(string name, EdmLocation location)
			: base(name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicEntityContainer, Strings.Bad_CyclicEntityContainer(name))
			})
		{
		}
	}
}
