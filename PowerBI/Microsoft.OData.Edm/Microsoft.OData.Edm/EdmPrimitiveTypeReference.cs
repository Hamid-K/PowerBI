using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CE RID: 206
	public class EdmPrimitiveTypeReference : EdmTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0000319F File Offset: 0x0000139F
		public EdmPrimitiveTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: base(definition, isNullable)
		{
		}
	}
}
