using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A2 RID: 418
	internal class UnresolvedComplexType : BadComplexType, IUnresolvedElement
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x0001FA3C File Offset: 0x0001DC3C
		public UnresolvedComplexType(string qualifiedName, EdmLocation location)
			: base(qualifiedName, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedComplexType, Strings.Bad_UnresolvedComplexType(qualifiedName))
			})
		{
		}
	}
}
