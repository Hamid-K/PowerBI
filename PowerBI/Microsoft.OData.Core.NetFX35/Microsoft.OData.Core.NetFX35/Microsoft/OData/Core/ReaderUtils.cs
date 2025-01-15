using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B3 RID: 435
	internal static class ReaderUtils
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x00037968 File Offset: 0x00035B68
		internal static ODataEntry CreateNewEntry()
		{
			return new ODataEntry
			{
				Properties = new ReadOnlyEnumerable<ODataProperty>()
			};
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00037988 File Offset: 0x00035B88
		internal static void CheckForDuplicateNavigationLinkNameAndSetAssociationLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			Uri uri = duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(navigationLink, isExpanded, isCollection);
			if (uri != null && navigationLink.AssociationLinkUrl == null)
			{
				navigationLink.AssociationLinkUrl = uri;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000379C0 File Offset: 0x00035BC0
		internal static void CheckForDuplicateAssociationLinkAndUpdateNavigationLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string associationLinkName, Uri associationLinkUrl)
		{
			ODataNavigationLink odataNavigationLink = duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(associationLinkName, associationLinkUrl);
			if (odataNavigationLink != null && odataNavigationLink.AssociationLinkUrl == null && associationLinkUrl != null)
			{
				odataNavigationLink.AssociationLinkUrl = associationLinkUrl;
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000379F7 File Offset: 0x00035BF7
		internal static bool HasFlag(this ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds, ODataUndeclaredPropertyBehaviorKinds flag)
		{
			return (undeclaredPropertyBehaviorKinds & flag) == flag;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000379FF File Offset: 0x00035BFF
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Ignoring violation because of Debug.Assert.")]
		internal static string GetExpectedPropertyName(IEdmStructuralProperty expectedProperty)
		{
			if (expectedProperty == null)
			{
				return null;
			}
			return expectedProperty.Name;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00037A0C File Offset: 0x00035C0C
		internal static string RemovePrefixOfTypeName(string typeName)
		{
			string text = typeName;
			if (!string.IsNullOrEmpty(typeName) && typeName.StartsWith("#", 4))
			{
				text = typeName.Substring("#".Length);
			}
			return text;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00037A44 File Offset: 0x00035C44
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
