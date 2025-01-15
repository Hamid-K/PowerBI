using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006F RID: 111
	public class EdmPrimitiveTypeReference : EdmTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00009D2E File Offset: 0x00007F2E
		public EdmPrimitiveTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: base(definition, isNullable)
		{
		}
	}
}
