using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000043 RID: 67
	internal class CyclicEntityContainer : BadEntityContainer
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00009845 File Offset: 0x00007A45
		public CyclicEntityContainer(string name, EdmLocation location)
			: base(name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadCyclicEntityContainer, Strings.Bad_CyclicEntityContainer(name))
			})
		{
		}
	}
}
