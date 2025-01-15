using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000B5 RID: 181
	internal static class WriterUtils
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x000140A8 File Offset: 0x000122A8
		internal static string RemoveEdmPrefixFromTypeName(string typeName)
		{
			if (!string.IsNullOrEmpty(typeName))
			{
				string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
				if (collectionItemTypeName == null)
				{
					IEdmSchemaType edmSchemaType = EdmLibraryExtensions.ResolvePrimitiveTypeName(typeName);
					if (edmSchemaType != null)
					{
						return edmSchemaType.ShortQualifiedName();
					}
				}
				else
				{
					IEdmSchemaType edmSchemaType2 = EdmLibraryExtensions.ResolvePrimitiveTypeName(collectionItemTypeName);
					if (edmSchemaType2 != null)
					{
						return EdmLibraryExtensions.GetCollectionTypeName(edmSchemaType2.ShortQualifiedName());
					}
				}
			}
			return typeName;
		}
	}
}
