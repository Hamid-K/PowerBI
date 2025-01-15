using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000056 RID: 86
	public class EdmEntityReferenceTypeReference : EdmTypeReference, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00009D2E File Offset: 0x00007F2E
		public EdmEntityReferenceTypeReference(IEdmEntityReferenceType entityReferenceType, bool isNullable)
			: base(entityReferenceType, isNullable)
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000AA61 File Offset: 0x00008C61
		public IEdmEntityReferenceType EntityReferenceDefinition
		{
			get
			{
				return (IEdmEntityReferenceType)base.Definition;
			}
		}
	}
}
