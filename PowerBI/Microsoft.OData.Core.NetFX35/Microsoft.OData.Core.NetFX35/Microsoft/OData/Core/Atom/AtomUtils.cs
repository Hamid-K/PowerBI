using System;
using System.Globalization;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200001C RID: 28
	internal static class AtomUtils
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000355C File Offset: 0x0000175C
		internal static string ComputeODataNavigationLinkRelation(ODataNavigationLink navigationLink)
		{
			return string.Join("", new string[] { "http://docs.oasis-open.org/odata/ns/related/", navigationLink.Name });
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000358C File Offset: 0x0000178C
		internal static string ComputeODataNavigationLinkType(ODataNavigationLink navigationLink)
		{
			if (!navigationLink.IsCollection.Value)
			{
				return "application/atom+xml;type=entry";
			}
			return "application/atom+xml;type=feed";
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000035B4 File Offset: 0x000017B4
		internal static string ComputeODataAssociationLinkRelation(string navigationPropertyName)
		{
			return string.Join("", new string[] { "http://docs.oasis-open.org/odata/ns/relatedlinks/", navigationPropertyName });
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000035E0 File Offset: 0x000017E0
		internal static string ComputeStreamPropertyRelation(ODataProperty streamProperty, bool forEditLink)
		{
			string text = (forEditLink ? "http://docs.oasis-open.org/odata/ns/edit-media/" : "http://docs.oasis-open.org/odata/ns/mediaresource/");
			return string.Join("", new string[] { text, streamProperty.Name });
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000361C File Offset: 0x0000181C
		internal static string UnescapeAtomLinkRelationAttribute(string relation)
		{
			Uri uri;
			if (!string.IsNullOrEmpty(relation) && Uri.TryCreate(relation, 0, ref uri) && uri.IsAbsoluteUri)
			{
				return uri.GetComponents(127, 3);
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000364F File Offset: 0x0000184F
		internal static string GetNameFromAtomLinkRelationAttribute(string relation, string namespacePrefix)
		{
			if (relation != null && relation.StartsWith(namespacePrefix, 4))
			{
				return relation.Substring(namespacePrefix.Length);
			}
			return null;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000366C File Offset: 0x0000186C
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

		// Token: 0x060000DC RID: 220 RVA: 0x00003710 File Offset: 0x00001910
		internal static string GetTransientId()
		{
			return string.Format(CultureInfo.InvariantCulture, "odata:transient:{{{0}}}", new object[] { Guid.NewGuid().ToString() });
		}

		// Token: 0x040000CE RID: 206
		private const int MimeApplicationAtomXmlLength = 20;

		// Token: 0x040000CF RID: 207
		private const int MimeApplicationAtomXmlLengthWithSemicolon = 21;

		// Token: 0x040000D0 RID: 208
		private const int MimeApplicationAtomXmlTypeEntryLength = 31;

		// Token: 0x040000D1 RID: 209
		private const int MimeApplicationAtomXmlTypeFeedLength = 30;

		// Token: 0x040000D2 RID: 210
		private const string MimeApplicationAtomXmlTypeEntryParameter = ";type=entry";

		// Token: 0x040000D3 RID: 211
		private const string MimeApplicationAtomXmlTypeFeedParameter = ";type=feed";
	}
}
