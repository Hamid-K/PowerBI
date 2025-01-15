using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007E RID: 126
	public class EdmCollectionTypeReference : EdmTypeReference, IEdmCollectionTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000276 RID: 630 RVA: 0x000060C1 File Offset: 0x000042C1
		public EdmCollectionTypeReference(IEdmCollectionType collectionType)
			: base(collectionType, EdmCollectionTypeReference.GetIsNullable(collectionType))
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000060D0 File Offset: 0x000042D0
		private static bool GetIsNullable(IEdmCollectionType collectionType)
		{
			IEdmTypeReference elementType = collectionType.ElementType;
			return elementType == null || elementType is IEdmEntityTypeReference || collectionType.ElementType.IsNullable;
		}
	}
}
