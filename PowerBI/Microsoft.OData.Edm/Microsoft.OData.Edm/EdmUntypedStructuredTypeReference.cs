using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000011 RID: 17
	public class EdmUntypedStructuredTypeReference : EdmTypeReference, IEdmUntypedTypeReference, IEdmTypeReference, IEdmElement, IEdmStructuredTypeReference
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003195 File Offset: 0x00001395
		public EdmUntypedStructuredTypeReference(IEdmStructuredType definition)
			: base(definition, true)
		{
		}
	}
}
