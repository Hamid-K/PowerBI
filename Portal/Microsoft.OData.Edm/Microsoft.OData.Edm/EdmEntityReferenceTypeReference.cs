using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C8 RID: 200
	public class EdmEntityReferenceTypeReference : EdmTypeReference, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0000319F File Offset: 0x0000139F
		public EdmEntityReferenceTypeReference(IEdmEntityReferenceType entityReferenceType, bool isNullable)
			: base(entityReferenceType, isNullable)
		{
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000C321 File Offset: 0x0000A521
		public IEdmEntityReferenceType EntityReferenceDefinition
		{
			get
			{
				return (IEdmEntityReferenceType)base.Definition;
			}
		}
	}
}
