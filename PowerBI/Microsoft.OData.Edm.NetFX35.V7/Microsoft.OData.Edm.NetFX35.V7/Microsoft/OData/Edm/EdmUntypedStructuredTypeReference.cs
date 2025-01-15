using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007F RID: 127
	public class EdmUntypedStructuredTypeReference : EdmTypeReference, IEdmUntypedTypeReference, IEdmTypeReference, IEdmElement, IEdmStructuredTypeReference
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0000C879 File Offset: 0x0000AA79
		public EdmUntypedStructuredTypeReference(IEdmStructuredType definition)
			: base(definition, true)
		{
		}
	}
}
