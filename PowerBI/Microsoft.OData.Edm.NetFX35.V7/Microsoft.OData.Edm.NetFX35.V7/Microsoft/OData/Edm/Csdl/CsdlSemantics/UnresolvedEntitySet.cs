using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A4 RID: 420
	internal class UnresolvedEntitySet : BadEntitySet, IUnresolvedElement
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public UnresolvedEntitySet(string name, IEdmEntityContainer container, EdmLocation location)
			: base(name, container, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntitySet, Strings.Bad_UnresolvedEntitySet(name))
			})
		{
		}
	}
}
