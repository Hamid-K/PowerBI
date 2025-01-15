using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004C RID: 76
	public class EdmComplexTypeReference : EdmTypeReference, IEdmComplexTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00009D2E File Offset: 0x00007F2E
		public EdmComplexTypeReference(IEdmComplexType complexType, bool isNullable)
			: base(complexType, isNullable)
		{
		}
	}
}
