using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x02000195 RID: 405
	public class EdmEnumTypeReference : EdmTypeReference, IEdmEnumTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001848B File Offset: 0x0001668B
		public EdmEnumTypeReference(IEdmEnumType enumType, bool isNullable)
			: base(enumType, isNullable)
		{
		}
	}
}
