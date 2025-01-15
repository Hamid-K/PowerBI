using System;
using Microsoft.Data.OData.Atom;

namespace Microsoft.Data.OData
{
	// Token: 0x02000215 RID: 533
	internal static class AtomMetadataReaderUtils
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x00039A14 File Offset: 0x00037C14
		internal static AtomEntryMetadata CreateNewAtomEntryMetadata()
		{
			return new AtomEntryMetadata
			{
				Authors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Categories = ReadOnlyEnumerable<AtomCategoryMetadata>.Empty(),
				Contributors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Links = ReadOnlyEnumerable<AtomLinkMetadata>.Empty()
			};
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00039A54 File Offset: 0x00037C54
		internal static AtomFeedMetadata CreateNewAtomFeedMetadata()
		{
			return new AtomFeedMetadata
			{
				Authors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Categories = ReadOnlyEnumerable<AtomCategoryMetadata>.Empty(),
				Contributors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Links = ReadOnlyEnumerable<AtomLinkMetadata>.Empty()
			};
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00039A94 File Offset: 0x00037C94
		internal static void AddAuthor(this AtomEntryMetadata entryMetadata, AtomPersonMetadata authorMetadata)
		{
			entryMetadata.Authors = entryMetadata.Authors.ConcatToReadOnlyEnumerable("Authors", authorMetadata);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00039AAD File Offset: 0x00037CAD
		internal static void AddContributor(this AtomEntryMetadata entryMetadata, AtomPersonMetadata contributorMetadata)
		{
			entryMetadata.Contributors = entryMetadata.Contributors.ConcatToReadOnlyEnumerable("Contributors", contributorMetadata);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00039AC6 File Offset: 0x00037CC6
		internal static void AddLink(this AtomEntryMetadata entryMetadata, AtomLinkMetadata linkMetadata)
		{
			entryMetadata.Links = entryMetadata.Links.ConcatToReadOnlyEnumerable("Links", linkMetadata);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00039ADF File Offset: 0x00037CDF
		internal static void AddLink(this AtomFeedMetadata feedMetadata, AtomLinkMetadata linkMetadata)
		{
			feedMetadata.Links = feedMetadata.Links.ConcatToReadOnlyEnumerable("Links", linkMetadata);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00039AF8 File Offset: 0x00037CF8
		internal static void AddCategory(this AtomEntryMetadata entryMetadata, AtomCategoryMetadata categoryMetadata)
		{
			entryMetadata.Categories = entryMetadata.Categories.ConcatToReadOnlyEnumerable("Categories", categoryMetadata);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00039B11 File Offset: 0x00037D11
		internal static void AddCategory(this AtomFeedMetadata feedMetadata, AtomCategoryMetadata categoryMetadata)
		{
			feedMetadata.Categories = feedMetadata.Categories.ConcatToReadOnlyEnumerable("Categories", categoryMetadata);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00039B2A File Offset: 0x00037D2A
		internal static void AddAuthor(this AtomFeedMetadata feedMetadata, AtomPersonMetadata authorMetadata)
		{
			feedMetadata.Authors = feedMetadata.Authors.ConcatToReadOnlyEnumerable("Authors", authorMetadata);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00039B43 File Offset: 0x00037D43
		internal static void AddContributor(this AtomFeedMetadata feedMetadata, AtomPersonMetadata contributorMetadata)
		{
			feedMetadata.Contributors = feedMetadata.Contributors.ConcatToReadOnlyEnumerable("Contributors", contributorMetadata);
		}
	}
}
