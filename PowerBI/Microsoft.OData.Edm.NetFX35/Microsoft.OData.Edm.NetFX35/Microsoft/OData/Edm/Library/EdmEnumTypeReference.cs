using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001C7 RID: 455
	public class EdmEnumTypeReference : EdmTypeReference, IEdmEnumTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x000196EA File Offset: 0x000178EA
		public EdmEnumTypeReference(IEdmEnumType enumType, bool isNullable)
			: base(enumType, isNullable)
		{
		}
	}
}
