using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200020F RID: 527
	internal class UnresolvedProperty : BadProperty, IUnresolvedElement
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x00022E2C File Offset: 0x0002102C
		public UnresolvedProperty(IEdmStructuredType declaringType, string name, EdmLocation location)
			: base(declaringType, name, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedProperty, Strings.Bad_UnresolvedProperty(name))
			})
		{
		}
	}
}
