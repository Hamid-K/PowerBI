using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200020D RID: 525
	internal class UnresolvedEntitySet : BadEntitySet, IUnresolvedElement
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00022DC8 File Offset: 0x00020FC8
		public UnresolvedEntitySet(string name, IEdmEntityContainer container, EdmLocation location)
			: base(name, container, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntitySet, Strings.Bad_UnresolvedEntitySet(name))
			})
		{
		}
	}
}
