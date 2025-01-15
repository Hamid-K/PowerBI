using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000212 RID: 530
	public class EdmEntityReferenceTypeReference : EdmTypeReference, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000C61 RID: 3169 RVA: 0x00022EF9 File Offset: 0x000210F9
		public EdmEntityReferenceTypeReference(IEdmEntityReferenceType entityReferenceType, bool isNullable)
			: base(entityReferenceType, isNullable)
		{
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00022F03 File Offset: 0x00021103
		public IEdmEntityReferenceType EntityReferenceDefinition
		{
			get
			{
				return (IEdmEntityReferenceType)base.Definition;
			}
		}
	}
}
