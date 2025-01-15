using System;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000243 RID: 579
	internal static class ReaderUtils
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x000428E4 File Offset: 0x00040AE4
		internal static ODataEntry CreateNewEntry()
		{
			return new ODataEntry
			{
				Properties = new ReadOnlyEnumerable<ODataProperty>(),
				AssociationLinks = ReadOnlyEnumerable<ODataAssociationLink>.Empty(),
				Actions = ReadOnlyEnumerable<ODataAction>.Empty(),
				Functions = ReadOnlyEnumerable<ODataFunction>.Empty()
			};
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00042924 File Offset: 0x00040B24
		internal static void CheckForDuplicateNavigationLinkNameAndSetAssociationLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			ODataAssociationLink odataAssociationLink = duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(navigationLink, isExpanded, isCollection);
			if (odataAssociationLink != null && odataAssociationLink.Url != null && navigationLink.AssociationLinkUrl == null)
			{
				navigationLink.AssociationLinkUrl = odataAssociationLink.Url;
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00042968 File Offset: 0x00040B68
		internal static void CheckForDuplicateAssociationLinkAndUpdateNavigationLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ODataAssociationLink associationLink)
		{
			ODataNavigationLink odataNavigationLink = duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(associationLink);
			if (odataNavigationLink != null && odataNavigationLink.AssociationLinkUrl == null && associationLink.Url != null)
			{
				odataNavigationLink.AssociationLinkUrl = associationLink.Url;
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000429C8 File Offset: 0x00040BC8
		internal static ODataAssociationLink GetOrCreateAssociationLinkForNavigationProperty(ODataEntry entry, IEdmNavigationProperty navigationProperty)
		{
			ODataAssociationLink odataAssociationLink = Enumerable.FirstOrDefault<ODataAssociationLink>(entry.AssociationLinks, (ODataAssociationLink al) => al.Name == navigationProperty.Name);
			if (odataAssociationLink == null)
			{
				odataAssociationLink = new ODataAssociationLink
				{
					Name = navigationProperty.Name
				};
				entry.AddAssociationLink(odataAssociationLink);
			}
			return odataAssociationLink;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00042A1E File Offset: 0x00040C1E
		internal static bool HasFlag(this ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds, ODataUndeclaredPropertyBehaviorKinds flag)
		{
			return (undeclaredPropertyBehaviorKinds & flag) == flag;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00042A26 File Offset: 0x00040C26
		internal static string GetExpectedPropertyName(IEdmStructuralProperty expectedProperty)
		{
			if (expectedProperty == null)
			{
				return null;
			}
			return expectedProperty.Name;
		}
	}
}
