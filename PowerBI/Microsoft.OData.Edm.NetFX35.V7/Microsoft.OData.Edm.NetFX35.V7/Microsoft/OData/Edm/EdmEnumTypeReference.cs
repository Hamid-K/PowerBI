using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005E RID: 94
	public class EdmEnumTypeReference : EdmTypeReference, IEdmEnumTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00009D2E File Offset: 0x00007F2E
		public EdmEnumTypeReference(IEdmEnumType enumType, bool isNullable)
			: base(enumType, isNullable)
		{
		}
	}
}
