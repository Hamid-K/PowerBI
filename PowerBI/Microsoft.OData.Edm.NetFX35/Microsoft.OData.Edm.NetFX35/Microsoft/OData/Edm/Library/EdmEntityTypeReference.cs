using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000138 RID: 312
	public class EdmEntityTypeReference : EdmTypeReference, IEdmEntityTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0000E41E File Offset: 0x0000C61E
		public EdmEntityTypeReference(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
		}
	}
}
