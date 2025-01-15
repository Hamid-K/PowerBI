using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001D9 RID: 473
	public class EdmCollectionTypeReference : EdmTypeReference, IEdmCollectionTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00019BBE File Offset: 0x00017DBE
		public EdmCollectionTypeReference(IEdmCollectionType collectionType)
			: base(collectionType, EdmCollectionTypeReference.GetIsNullable(collectionType))
		{
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00019BD0 File Offset: 0x00017DD0
		private static bool GetIsNullable(IEdmCollectionType collectionType)
		{
			IEdmTypeReference elementType = collectionType.ElementType;
			return elementType == null || elementType is IEdmEntityTypeReference || collectionType.ElementType.IsNullable;
		}
	}
}
