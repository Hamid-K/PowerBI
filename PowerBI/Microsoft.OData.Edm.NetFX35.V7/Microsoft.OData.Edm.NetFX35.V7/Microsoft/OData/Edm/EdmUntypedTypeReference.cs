using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000080 RID: 128
	public class EdmUntypedTypeReference : EdmTypeReference, IEdmUntypedTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x0000C879 File Offset: 0x0000AA79
		public EdmUntypedTypeReference(IEdmUntypedType definition)
			: base(definition, true)
		{
		}
	}
}
