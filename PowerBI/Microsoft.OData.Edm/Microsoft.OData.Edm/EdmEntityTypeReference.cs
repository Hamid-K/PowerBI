using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CD RID: 205
	public class EdmEntityTypeReference : EdmTypeReference, IEdmEntityTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000319F File Offset: 0x0000139F
		public EdmEntityTypeReference(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
		}
	}
}
