using System;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200002E RID: 46
	internal static class QueryTokenUtils
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00005DA0 File Offset: 0x00003FA0
		internal static InlineCountKind? ParseInlineCountKind(string inlineCount)
		{
			if (inlineCount == null)
			{
				return default(InlineCountKind?);
			}
			if (string.Equals(inlineCount, "allpages", 5))
			{
				return new InlineCountKind?(InlineCountKind.AllPages);
			}
			if (string.Equals(inlineCount, "none", 5))
			{
				return new InlineCountKind?(InlineCountKind.None);
			}
			throw new ODataException(Strings.QueryDescriptorQueryToken_InvalidInlineCountQueryOptionValue(inlineCount, string.Join(", ", new string[] { "none", "allpages" })));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005E14 File Offset: 0x00004014
		internal static KeywordKind? ParseKeywordKind(string segment)
		{
			if (segment != null)
			{
				if (segment == "$metadata")
				{
					return new KeywordKind?(KeywordKind.Metadata);
				}
				if (segment == "$count")
				{
					return new KeywordKind?(KeywordKind.Count);
				}
				if (segment == "$value")
				{
					return new KeywordKind?(KeywordKind.Value);
				}
				if (segment == "$batch")
				{
					return new KeywordKind?(KeywordKind.Batch);
				}
				if (segment == "$links")
				{
					return new KeywordKind?(KeywordKind.Links);
				}
			}
			return default(KeywordKind?);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005E98 File Offset: 0x00004098
		internal static string GetNameFromKeywordKind(KeywordKind keyword)
		{
			switch (keyword)
			{
			case KeywordKind.Metadata:
				return "$metadata";
			case KeywordKind.Value:
				return "$value";
			case KeywordKind.Batch:
				return "$batch";
			case KeywordKind.Links:
				return "$links";
			case KeywordKind.Count:
				return "$count";
			default:
				throw new InvalidOperationException("Should not have reached here with kind: " + keyword);
			}
		}
	}
}
