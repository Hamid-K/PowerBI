using System;
using Microsoft.OData.Core.Atom;

namespace Microsoft.OData.Core
{
	// Token: 0x02000016 RID: 22
	internal static class AtomMetadataReaderUtils
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000032F0 File Offset: 0x000014F0
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

		// Token: 0x060000AE RID: 174 RVA: 0x00003330 File Offset: 0x00001530
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

		// Token: 0x060000AF RID: 175 RVA: 0x00003370 File Offset: 0x00001570
		internal static void AddAuthor(this AtomEntryMetadata entryMetadata, AtomPersonMetadata authorMetadata)
		{
			entryMetadata.Authors = entryMetadata.Authors.ConcatToReadOnlyEnumerable("Authors", authorMetadata);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003389 File Offset: 0x00001589
		internal static void AddContributor(this AtomEntryMetadata entryMetadata, AtomPersonMetadata contributorMetadata)
		{
			entryMetadata.Contributors = entryMetadata.Contributors.ConcatToReadOnlyEnumerable("Contributors", contributorMetadata);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000033A2 File Offset: 0x000015A2
		internal static void AddLink(this AtomEntryMetadata entryMetadata, AtomLinkMetadata linkMetadata)
		{
			entryMetadata.Links = entryMetadata.Links.ConcatToReadOnlyEnumerable("Ref", linkMetadata);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000033BB File Offset: 0x000015BB
		internal static void AddLink(this AtomFeedMetadata feedMetadata, AtomLinkMetadata linkMetadata)
		{
			feedMetadata.Links = feedMetadata.Links.ConcatToReadOnlyEnumerable("Ref", linkMetadata);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000033D4 File Offset: 0x000015D4
		internal static void AddCategory(this AtomEntryMetadata entryMetadata, AtomCategoryMetadata categoryMetadata)
		{
			entryMetadata.Categories = entryMetadata.Categories.ConcatToReadOnlyEnumerable("Categories", categoryMetadata);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000033ED File Offset: 0x000015ED
		internal static void AddCategory(this AtomFeedMetadata feedMetadata, AtomCategoryMetadata categoryMetadata)
		{
			feedMetadata.Categories = feedMetadata.Categories.ConcatToReadOnlyEnumerable("Categories", categoryMetadata);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003406 File Offset: 0x00001606
		internal static void AddAuthor(this AtomFeedMetadata feedMetadata, AtomPersonMetadata authorMetadata)
		{
			feedMetadata.Authors = feedMetadata.Authors.ConcatToReadOnlyEnumerable("Authors", authorMetadata);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000341F File Offset: 0x0000161F
		internal static void AddContributor(this AtomFeedMetadata feedMetadata, AtomPersonMetadata contributorMetadata)
		{
			feedMetadata.Contributors = feedMetadata.Contributors.ConcatToReadOnlyEnumerable("Contributors", contributorMetadata);
		}
	}
}
