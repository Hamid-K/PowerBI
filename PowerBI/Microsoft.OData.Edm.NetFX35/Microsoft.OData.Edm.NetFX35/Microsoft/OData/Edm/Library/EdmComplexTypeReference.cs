using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200012D RID: 301
	public class EdmComplexTypeReference : EdmTypeReference, IEdmComplexTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0000E1E7 File Offset: 0x0000C3E7
		public EdmComplexTypeReference(IEdmComplexType complexType, bool isNullable)
			: base(complexType, isNullable)
		{
		}
	}
}
