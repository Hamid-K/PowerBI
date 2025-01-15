using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000124 RID: 292
	internal static class ExpandTreeNormalizer
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00027C68 File Offset: 0x00025E68
		public static ExpandToken NormalizeExpandTree(ExpandToken treeToNormalize)
		{
			if (treeToNormalize == null)
			{
				return null;
			}
			ExpandToken expandToken = ExpandTreeNormalizer.NormalizePaths(treeToNormalize);
			return ExpandTreeNormalizer.CombineTerms(expandToken);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00027C88 File Offset: 0x00025E88
		public static ExpandToken NormalizePaths(ExpandToken treeToInvert)
		{
			if (treeToInvert != null)
			{
				foreach (ExpandTermToken expandTermToken in treeToInvert.ExpandTerms)
				{
					expandTermToken.PathToProperty = expandTermToken.PathToProperty.Reverse();
					if (expandTermToken.SelectOption != null)
					{
						expandTermToken.SelectOption = SelectTreeNormalizer.NormalizeSelectTree(expandTermToken.SelectOption);
					}
					if (expandTermToken.ExpandOption != null)
					{
						expandTermToken.ExpandOption = ExpandTreeNormalizer.NormalizePaths(expandTermToken.ExpandOption);
					}
				}
			}
			return treeToInvert;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00027D18 File Offset: 0x00025F18
		public static ExpandToken CombineTerms(ExpandToken treeToCollapse)
		{
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			foreach (ExpandTermToken expandTermToken in treeToCollapse.ExpandTerms)
			{
				ExpandTermToken expandTermToken2 = expandTermToken;
				if (expandTermToken.ExpandOption != null)
				{
					ExpandToken expandToken = ExpandTreeNormalizer.CombineTerms(expandTermToken.ExpandOption);
					expandTermToken2 = new ExpandTermToken(expandTermToken.PathToNavigationProp, expandTermToken.FilterOption, expandTermToken.OrderByOptions, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.CountQueryOption, expandTermToken.LevelsOption, expandTermToken.SearchOption, ExpandTreeNormalizer.RemoveDuplicateSelect(expandTermToken.SelectOption), expandToken, expandTermToken.ComputeOption, expandTermToken.ApplyOptions);
				}
				ExpandTreeNormalizer.AddOrCombine(dictionary, expandTermToken2);
			}
			return new ExpandToken(dictionary.Values);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00027DE4 File Offset: 0x00025FE4
		public static ExpandTermToken CombineTerms(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			List<ExpandTermToken> list = ExpandTreeNormalizer.CombineChildNodes(existingToken, newToken).ToList<ExpandTermToken>();
			SelectToken selectToken = ExpandTreeNormalizer.CombineSelects(existingToken, newToken);
			return new ExpandTermToken(existingToken.PathToNavigationProp, existingToken.FilterOption, existingToken.OrderByOptions, existingToken.TopOption, existingToken.SkipOption, existingToken.CountQueryOption, existingToken.LevelsOption, existingToken.SearchOption, selectToken, new ExpandToken(list), existingToken.ComputeOption, existingToken.ApplyOptions);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00027E50 File Offset: 0x00026050
		public static IEnumerable<ExpandTermToken> CombineChildNodes(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			if (existingToken.ExpandOption == null && newToken.ExpandOption == null)
			{
				return new List<ExpandTermToken>();
			}
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			if (existingToken.ExpandOption != null)
			{
				ExpandTreeNormalizer.AddChildOptionsToDictionary(existingToken, dictionary);
			}
			if (newToken.ExpandOption != null)
			{
				ExpandTreeNormalizer.AddChildOptionsToDictionary(newToken, dictionary);
			}
			return dictionary.Values;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00027EA4 File Offset: 0x000260A4
		private static void AddChildOptionsToDictionary(ExpandTermToken newToken, Dictionary<PathSegmentToken, ExpandTermToken> combinedTerms)
		{
			foreach (ExpandTermToken expandTermToken in newToken.ExpandOption.ExpandTerms)
			{
				ExpandTreeNormalizer.AddOrCombine(combinedTerms, expandTermToken);
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00027EF8 File Offset: 0x000260F8
		private static void AddOrCombine(IDictionary<PathSegmentToken, ExpandTermToken> combinedTerms, ExpandTermToken expandedTerm)
		{
			ExpandTermToken expandTermToken;
			if (combinedTerms.TryGetValue(expandedTerm.PathToNavigationProp, out expandTermToken))
			{
				combinedTerms[expandedTerm.PathToNavigationProp] = ExpandTreeNormalizer.CombineTerms(expandedTerm, expandTermToken);
				return;
			}
			combinedTerms.Add(expandedTerm.PathToNavigationProp, expandedTerm);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00027F38 File Offset: 0x00026138
		private static SelectToken CombineSelects(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			if (existingToken.SelectOption == null)
			{
				return newToken.SelectOption;
			}
			if (newToken.SelectOption == null)
			{
				return existingToken.SelectOption;
			}
			List<PathSegmentToken> list = existingToken.SelectOption.Properties.ToList<PathSegmentToken>();
			list.AddRange(newToken.SelectOption.Properties);
			return new SelectToken(list.Distinct(new PathSegmentTokenEqualityComparer()));
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00027F95 File Offset: 0x00026195
		private static SelectToken RemoveDuplicateSelect(SelectToken selectToken)
		{
			if (selectToken == null)
			{
				return null;
			}
			return new SelectToken(selectToken.Properties.Distinct(new PathSegmentTokenEqualityComparer()));
		}
	}
}
