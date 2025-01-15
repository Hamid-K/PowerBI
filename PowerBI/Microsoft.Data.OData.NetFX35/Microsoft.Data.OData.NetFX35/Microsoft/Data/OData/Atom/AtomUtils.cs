using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027F RID: 639
	internal static class AtomUtils
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x0004A04C File Offset: 0x0004824C
		internal static string ComputeODataNavigationLinkRelation(ODataNavigationLink navigationLink)
		{
			return string.Join("/", new string[] { "http://schemas.microsoft.com/ado/2007/08/dataservices", "related", navigationLink.Name });
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004A084 File Offset: 0x00048284
		internal static string ComputeODataNavigationLinkType(ODataNavigationLink navigationLink)
		{
			if (!navigationLink.IsCollection.Value)
			{
				return "application/atom+xml;type=entry";
			}
			return "application/atom+xml;type=feed";
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0004A0AC File Offset: 0x000482AC
		internal static string ComputeODataAssociationLinkRelation(ODataAssociationLink associationLink)
		{
			return string.Join("/", new string[] { "http://schemas.microsoft.com/ado/2007/08/dataservices", "relatedlinks", associationLink.Name });
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004A0E4 File Offset: 0x000482E4
		internal static string ComputeStreamPropertyRelation(ODataProperty streamProperty, bool forEditLink)
		{
			string text = (forEditLink ? "edit-media" : "mediaresource");
			return string.Join("/", new string[] { "http://schemas.microsoft.com/ado/2007/08/dataservices", text, streamProperty.Name });
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0004A128 File Offset: 0x00048328
		internal static string UnescapeAtomLinkRelationAttribute(string relation)
		{
			Uri uri;
			if (!string.IsNullOrEmpty(relation) && Uri.TryCreate(relation, 0, ref uri) && uri.IsAbsoluteUri)
			{
				return uri.GetComponents(127, 3);
			}
			return null;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0004A15B File Offset: 0x0004835B
		internal static string GetNameFromAtomLinkRelationAttribute(string relation, string namespacePrefix)
		{
			if (relation != null && relation.StartsWith(namespacePrefix, 4))
			{
				return relation.Substring(namespacePrefix.Length);
			}
			return null;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0004A178 File Offset: 0x00048378
		internal static bool IsExactNavigationLinkTypeMatch(string navigationLinkType, out bool hasEntryType, out bool hasFeedType)
		{
			hasEntryType = false;
			hasFeedType = false;
			if (!navigationLinkType.StartsWith("application/atom+xml", 4))
			{
				return false;
			}
			int length = navigationLinkType.Length;
			int num = length;
			switch (num)
			{
			case 20:
				return true;
			case 21:
				return navigationLinkType.get_Chars(length - 1) == ';';
			default:
				switch (num)
				{
				case 30:
					hasFeedType = string.Compare(";type=feed", 0, navigationLinkType, 20, ";type=feed".Length, 4) == 0;
					return hasFeedType;
				case 31:
					hasEntryType = string.Compare(";type=entry", 0, navigationLinkType, 20, ";type=entry".Length, 4) == 0;
					return hasEntryType;
				default:
					return false;
				}
				break;
			}
		}

		// Token: 0x040007BD RID: 1981
		private const int MimeApplicationAtomXmlLength = 20;

		// Token: 0x040007BE RID: 1982
		private const int MimeApplicationAtomXmlLengthWithSemicolon = 21;

		// Token: 0x040007BF RID: 1983
		private const int MimeApplicationAtomXmlTypeEntryLength = 31;

		// Token: 0x040007C0 RID: 1984
		private const int MimeApplicationAtomXmlTypeFeedLength = 30;

		// Token: 0x040007C1 RID: 1985
		private const string MimeApplicationAtomXmlTypeEntryParameter = ";type=entry";

		// Token: 0x040007C2 RID: 1986
		private const string MimeApplicationAtomXmlTypeFeedParameter = ";type=feed";
	}
}
