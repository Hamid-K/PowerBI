using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200020C RID: 524
	internal class UnresolvedEntityContainer : BadEntityContainer, IUnresolvedElement
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x00022D98 File Offset: 0x00020F98
		public UnresolvedEntityContainer(string name, EdmLocation location)
			: base(name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEntityContainer, Strings.Bad_UnresolvedEntityContainer(name))
			})
		{
		}
	}
}
