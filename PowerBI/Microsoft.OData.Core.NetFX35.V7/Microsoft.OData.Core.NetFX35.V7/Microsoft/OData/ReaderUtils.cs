using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000AB RID: 171
	internal static class ReaderUtils
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x000120B4 File Offset: 0x000102B4
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

		// Token: 0x06000690 RID: 1680 RVA: 0x000120E9 File Offset: 0x000102E9
		internal static ODataResource CreateNewResource()
		{
			return new ODataResource
			{
				Properties = new ReadOnlyEnumerable<ODataProperty>()
			};
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000120FC File Offset: 0x000102FC
		internal static void CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(PropertyAndAnnotationCollector propertyAndAnnotationCollector, ODataNestedResourceInfo nestedResourceInfo)
		{
			Uri uri = propertyAndAnnotationCollector.ValidatePropertyUniquenessAndGetAssociationLink(nestedResourceInfo);
			if (uri != null && nestedResourceInfo.AssociationLinkUrl == null)
			{
				nestedResourceInfo.AssociationLinkUrl = uri;
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00012130 File Offset: 0x00010330
		internal static void CheckForDuplicateAssociationLinkAndUpdateNestedResourceInfo(PropertyAndAnnotationCollector propertyAndAnnotationCollector, string associationLinkName, Uri associationLinkUrl)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = propertyAndAnnotationCollector.ValidatePropertyOpenForAssociationLinkAndGetNestedResourceInfo(associationLinkName, associationLinkUrl);
			if (odataNestedResourceInfo != null && odataNestedResourceInfo.AssociationLinkUrl == null && associationLinkUrl != null)
			{
				odataNestedResourceInfo.AssociationLinkUrl = associationLinkUrl;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00012167 File Offset: 0x00010367
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Ignoring violation because of Debug.Assert.")]
		internal static string GetExpectedPropertyName(IEdmStructuralProperty expectedProperty)
		{
			if (expectedProperty == null)
			{
				return null;
			}
			return expectedProperty.Name;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00012174 File Offset: 0x00010374
		internal static string RemovePrefixOfTypeName(string typeName)
		{
			string text = typeName;
			if (!string.IsNullOrEmpty(typeName) && typeName.StartsWith("#", 4))
			{
				text = typeName.Substring("#".Length);
			}
			return text;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000121AC File Offset: 0x000103AC
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
