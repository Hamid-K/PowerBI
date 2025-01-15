using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000C9 RID: 201
	internal static class ReaderUtils
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x00016974 File Offset: 0x00014B74
		internal static EdmTypeKind GetExpectedTypeKind(IEdmTypeReference expectedTypeReference, bool enablePrimitiveTypeConversion)
		{
			IEdmType definition;
			if (expectedTypeReference == null || (definition = expectedTypeReference.Definition) == null)
			{
				return EdmTypeKind.None;
			}
			EdmTypeKind typeKind = definition.TypeKind;
			if (!enablePrimitiveTypeConversion && typeKind == EdmTypeKind.Primitive && !definition.IsStream())
			{
				return EdmTypeKind.None;
			}
			return typeKind;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000169A9 File Offset: 0x00014BA9
		internal static ODataResource CreateNewResource()
		{
			return new ODataResource
			{
				Properties = new ReadOnlyEnumerable<ODataProperty>()
			};
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000169BB File Offset: 0x00014BBB
		internal static ODataDeletedResource CreateDeletedResource(Uri id, DeltaDeletedEntryReason reason)
		{
			return new ODataDeletedResource(id, reason)
			{
				Properties = new ReadOnlyEnumerable<ODataProperty>()
			};
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000169D0 File Offset: 0x00014BD0
		internal static void CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(PropertyAndAnnotationCollector propertyAndAnnotationCollector, ODataNestedResourceInfo nestedResourceInfo)
		{
			Uri uri = propertyAndAnnotationCollector.ValidatePropertyUniquenessAndGetAssociationLink(nestedResourceInfo);
			if (uri != null && nestedResourceInfo.AssociationLinkUrl == null)
			{
				nestedResourceInfo.AssociationLinkUrl = uri;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00016A04 File Offset: 0x00014C04
		internal static void CheckForDuplicateAssociationLinkAndUpdateNestedResourceInfo(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string associationLinkName, Uri associationLinkUrl)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = propertyAndAnnotationCollector.ValidatePropertyOpenForAssociationLinkAndGetNestedResourceInfo(associationLinkName, associationLinkUrl);
			if (odataNestedResourceInfo != null && odataNestedResourceInfo.AssociationLinkUrl == null && associationLinkUrl != null)
			{
				odataNestedResourceInfo.AssociationLinkUrl = associationLinkUrl;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00016A3B File Offset: 0x00014C3B
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Ignoring violation because of Debug.Assert.")]
		internal static string GetExpectedPropertyName(IEdmStructuralProperty expectedProperty)
		{
			if (expectedProperty == null)
			{
				return null;
			}
			return expectedProperty.Name;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00016A48 File Offset: 0x00014C48
		internal static string RemovePrefixOfTypeName(string typeName)
		{
			string text = typeName;
			if (!string.IsNullOrEmpty(typeName) && typeName.StartsWith("#", StringComparison.Ordinal))
			{
				text = typeName.Substring("#".Length);
			}
			return text;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00016A80 File Offset: 0x00014C80
		internal static string AddEdmPrefixOfTypeName(string typeName)
		{
			if (!string.IsNullOrEmpty(typeName))
			{
				string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
				if (collectionItemTypeName == null)
				{
					IEdmSchemaType edmSchemaType = EdmLibraryExtensions.ResolvePrimitiveTypeName(typeName);
					if (edmSchemaType != null)
					{
						return edmSchemaType.FullName();
					}
				}
				else
				{
					IEdmSchemaType edmSchemaType2 = EdmLibraryExtensions.ResolvePrimitiveTypeName(collectionItemTypeName);
					if (edmSchemaType2 != null)
					{
						return EdmLibraryExtensions.GetCollectionTypeName(edmSchemaType2.FullName());
					}
				}
			}
			return typeName;
		}
	}
}
