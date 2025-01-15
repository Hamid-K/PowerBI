using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000E1 RID: 225
	internal static class WriterUtils
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0001C37C File Offset: 0x0001A57C
		internal static string PrefixTypeNameForWriting(string typeName, ODataVersion version)
		{
			if (!string.IsNullOrEmpty(typeName))
			{
				string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
				if (collectionItemTypeName == null)
				{
					IEdmSchemaType edmSchemaType = EdmLibraryExtensions.ResolvePrimitiveTypeName(typeName);
					if (edmSchemaType != null)
					{
						typeName = edmSchemaType.ShortQualifiedName();
						if (version >= ODataVersion.V401)
						{
							return typeName;
						}
						return WriterUtils.PrefixTypeName(typeName);
					}
				}
				else
				{
					IEdmSchemaType edmSchemaType2 = EdmLibraryExtensions.ResolvePrimitiveTypeName(collectionItemTypeName);
					if (edmSchemaType2 != null)
					{
						typeName = EdmLibraryExtensions.GetCollectionTypeName(edmSchemaType2.ShortQualifiedName());
						if (version >= ODataVersion.V401)
						{
							return typeName;
						}
						return WriterUtils.PrefixTypeName(typeName);
					}
				}
			}
			return WriterUtils.PrefixTypeName(typeName);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		private static string PrefixTypeName(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return typeName;
			}
			return "#" + typeName;
		}
	}
}
