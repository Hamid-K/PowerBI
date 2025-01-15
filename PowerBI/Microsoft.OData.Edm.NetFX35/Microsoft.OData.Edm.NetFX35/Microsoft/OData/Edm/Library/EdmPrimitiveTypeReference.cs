using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000122 RID: 290
	public class EdmPrimitiveTypeReference : EdmTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0000E06D File Offset: 0x0000C26D
		public EdmPrimitiveTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: base(definition, isNullable)
		{
		}
	}
}
