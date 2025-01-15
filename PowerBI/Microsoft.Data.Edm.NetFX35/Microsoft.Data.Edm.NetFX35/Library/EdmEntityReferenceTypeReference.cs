using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001E6 RID: 486
	public class EdmEntityReferenceTypeReference : EdmTypeReference, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x000213B9 File Offset: 0x0001F5B9
		public EdmEntityReferenceTypeReference(IEdmEntityReferenceType entityReferenceType, bool isNullable)
			: base(entityReferenceType, isNullable)
		{
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000213C3 File Offset: 0x0001F5C3
		public IEdmEntityReferenceType EntityReferenceDefinition
		{
			get
			{
				return (IEdmEntityReferenceType)base.Definition;
			}
		}
	}
}
