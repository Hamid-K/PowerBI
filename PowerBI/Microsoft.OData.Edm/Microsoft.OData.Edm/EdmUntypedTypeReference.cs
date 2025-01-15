using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000013 RID: 19
	public class EdmUntypedTypeReference : EdmTypeReference, IEdmUntypedTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003195 File Offset: 0x00001395
		public EdmUntypedTypeReference(IEdmUntypedType definition)
			: base(definition, true)
		{
		}
	}
}
