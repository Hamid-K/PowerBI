using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004A RID: 74
	public class EdmCollectionTypeReference : EdmTypeReference, IEdmCollectionTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00009C82 File Offset: 0x00007E82
		public EdmCollectionTypeReference(IEdmCollectionType collectionType)
			: base(collectionType, EdmCollectionTypeReference.GetIsNullable(collectionType))
		{
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009C94 File Offset: 0x00007E94
		private static bool GetIsNullable(IEdmCollectionType collectionType)
		{
			IEdmTypeReference elementType = collectionType.ElementType;
			return elementType == null || elementType is IEdmEntityTypeReference || collectionType.ElementType.IsNullable;
		}
	}
}
