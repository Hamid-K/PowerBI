using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007C RID: 124
	public class EdmEnumTypeReference : EdmTypeReference, IEdmEnumTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000319F File Offset: 0x0000139F
		public EdmEnumTypeReference(IEdmEnumType enumType, bool isNullable)
			: base(enumType, isNullable)
		{
		}
	}
}
