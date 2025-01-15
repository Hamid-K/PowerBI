using System;
using System.Collections;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200018D RID: 397
	internal static class EdmObjectHelper
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x00033DE0 File Offset: 0x00031FE0
		public static IEdmObject ConvertToEdmObject(this IEnumerable enumerable, IEdmCollectionTypeReference collectionType)
		{
			IEdmTypeReference edmTypeReference = collectionType.ElementType();
			if (edmTypeReference.IsEntity())
			{
				EdmEntityObjectCollection edmEntityObjectCollection = new EdmEntityObjectCollection(collectionType);
				foreach (object obj in enumerable)
				{
					EdmEntityObject edmEntityObject = (EdmEntityObject)obj;
					edmEntityObjectCollection.Add(edmEntityObject);
				}
				return edmEntityObjectCollection;
			}
			if (edmTypeReference.IsComplex())
			{
				EdmComplexObjectCollection edmComplexObjectCollection = new EdmComplexObjectCollection(collectionType);
				foreach (object obj2 in enumerable)
				{
					EdmComplexObject edmComplexObject = (EdmComplexObject)obj2;
					edmComplexObjectCollection.Add(edmComplexObject);
				}
				return edmComplexObjectCollection;
			}
			if (edmTypeReference.IsEnum())
			{
				EdmEnumObjectCollection edmEnumObjectCollection = new EdmEnumObjectCollection(collectionType);
				foreach (object obj3 in enumerable)
				{
					EdmEnumObject edmEnumObject = (EdmEnumObject)obj3;
					edmEnumObjectCollection.Add(edmEnumObject);
				}
				return edmEnumObjectCollection;
			}
			return null;
		}
	}
}
