using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020002A0 RID: 672
	internal static class WriterUtils
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x0004F3C6 File Offset: 0x0004D5C6
		internal static bool ShouldSkipProperty(this ProjectedPropertiesAnnotation projectedProperties, string propertyName)
		{
			return projectedProperties != null && (object.ReferenceEquals(ProjectedPropertiesAnnotation.EmptyProjectedPropertiesInstance, projectedProperties) || (!object.ReferenceEquals(ProjectedPropertiesAnnotation.AllProjectedPropertiesInstance, projectedProperties) && !projectedProperties.IsPropertyProjected(propertyName)));
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004F3F8 File Offset: 0x0004D5F8
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
