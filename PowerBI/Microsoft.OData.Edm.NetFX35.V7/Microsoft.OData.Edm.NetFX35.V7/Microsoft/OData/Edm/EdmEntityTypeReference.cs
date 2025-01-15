using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005A RID: 90
	public class EdmEntityTypeReference : EdmTypeReference, IEdmEntityTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600035B RID: 859 RVA: 0x00009D2E File Offset: 0x00007F2E
		public EdmEntityTypeReference(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
		}
	}
}
