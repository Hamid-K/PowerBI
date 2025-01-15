using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B3 RID: 435
	internal class UnresolvedEntitySet : BadEntitySet, IUnresolvedElement
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x00021EB4 File Offset: 0x000200B4
		public UnresolvedEntitySet(string name, IEdmEntityContainer container, EdmLocation location)
			: base(name, container, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntitySet, Strings.Bad_UnresolvedEntitySet(name))
			})
		{
		}
	}
}
