using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B2 RID: 434
	internal class UnresolvedEntityContainer : BadEntityContainer, IUnresolvedElement
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x00021E90 File Offset: 0x00020090
		public UnresolvedEntityContainer(string name, EdmLocation location)
			: base(name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntityContainer, Strings.Bad_UnresolvedEntityContainer(name))
			})
		{
		}
	}
}
