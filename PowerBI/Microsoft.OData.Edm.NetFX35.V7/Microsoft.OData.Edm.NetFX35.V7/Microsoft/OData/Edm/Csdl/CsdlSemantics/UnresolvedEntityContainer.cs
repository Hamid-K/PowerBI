using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A3 RID: 419
	internal class UnresolvedEntityContainer : BadEntityContainer, IUnresolvedElement
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0001FA5C File Offset: 0x0001DC5C
		public UnresolvedEntityContainer(string name, EdmLocation location)
			: base(name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntityContainer, Strings.Bad_UnresolvedEntityContainer(name))
			})
		{
		}
	}
}
