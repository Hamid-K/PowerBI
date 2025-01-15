using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CA RID: 202
	public class EdmComplexTypeReference : EdmTypeReference, IEdmComplexTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000319F File Offset: 0x0000139F
		public EdmComplexTypeReference(IEdmComplexType complexType, bool isNullable)
			: base(complexType, isNullable)
		{
		}
	}
}
