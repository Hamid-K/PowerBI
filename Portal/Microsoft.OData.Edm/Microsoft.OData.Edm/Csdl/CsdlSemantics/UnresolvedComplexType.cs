using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B1 RID: 433
	internal class UnresolvedComplexType : BadComplexType, IUnresolvedElement
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x00021E70 File Offset: 0x00020070
		public UnresolvedComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedComplexType, Strings.Bad_UnresolvedComplexType(qualifiedName))
			})
		{
		}
	}
}
