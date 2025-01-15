using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000012 RID: 18
	public class EdmPathTypeReference : EdmTypeReference, IEdmPathTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000319F File Offset: 0x0000139F
		public EdmPathTypeReference(IEdmPathType definition, bool isNullable)
			: base(definition, isNullable)
		{
		}
	}
}
