using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E6 RID: 230
	internal sealed class ExpandTreeNormalizer
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
		public ExpandToken NormalizeExpandTree(ExpandToken treeToNormalize)
		{
			ExpandToken expandToken = this.NormalizePaths(treeToNormalize);
			return this.CombineTerms(expandToken);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001C4DC File Offset: 0x0001A6DC
		public ExpandToken NormalizePaths(ExpandToken treeToInvert)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			foreach (ExpandTermToken expandTermToken in treeToInvert.ExpandTerms)
			{
				PathReverser pathReverser = new PathReverser();
				PathSegmentToken pathSegmentToken = expandTermToken.PathToNavigationProp.Accept<PathSegmentToken>(pathReverser);
				SelectToken selectToken = expandTermToken.SelectOption;
				if (expandTermToken.SelectOption != null)
				{
					selectToken = SelectTreeNormalizer.NormalizeSelectTree(expandTermToken.SelectOption);
				}
				ExpandToken expandToken;
				if (expandTermToken.ExpandOption != null)
				{
					expandToken = this.NormalizePaths(expandTermToken.ExpandOption);
				}
				else
				{
					expandToken = null;
				}
				ExpandTermToken expandTermToken2 = new ExpandTermToken(pathSegmentToken, expandTermToken.FilterOption, expandTermToken.OrderByOptions, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.CountQueryOption, expandTermToken.LevelsOption, expandTermToken.SearchOption, selectToken, expandToken, expandTermToken.ComputeOption);
				list.Add(expandTermToken2);
			}
			return new ExpandToken(list);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
		public ExpandToken CombineTerms(ExpandToken treeToCollapse)
		{
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			foreach (ExpandTermToken expandTermToken in treeToCollapse.ExpandTerms)
			{
				ExpandTermToken expandTermToken2 = expandTermToken;
				if (expandTermToken.ExpandOption != null)
				{
					ExpandToken expandToken = this.CombineTerms(expandTermToken.ExpandOption);
					expandTermToken2 = new ExpandTermToken(expandTermToken.PathToNavigationProp, expandTermToken.FilterOption, expandTermToken.OrderByOptions, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.CountQueryOption, expandTermToken.LevelsOption, expandTermToken.SearchOption, ExpandTreeNormalizer.RemoveDuplicateSelect(expandTermToken.SelectOption), expandToken, expandTermToken.ComputeOption);
				}
				this.AddOrCombine(dictionary, expandTermToken2);
			}
			return new ExpandToken(dictionary.Values);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001C690 File Offset: 0x0001A890
		public ExpandTermToken CombineTerms(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			List<ExpandTermToken> list = Enumerable.ToList<ExpandTermToken>(this.CombineChildNodes(existingToken, newToken));
			SelectToken selectToken = ExpandTreeNormalizer.CombineSelects(existingToken, newToken);
			return new ExpandTermToken(existingToken.PathToNavigationProp, existingToken.FilterOption, existingToken.OrderByOptions, existingToken.TopOption, existingToken.SkipOption, existingToken.CountQueryOption, existingToken.LevelsOption, existingToken.SearchOption, selectToken, new ExpandToken(list), existingToken.ComputeOption);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		public IEnumerable<ExpandTermToken> CombineChildNodes(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			if (existingToken.ExpandOption == null && newToken.ExpandOption == null)
			{
				return new List<ExpandTermToken>();
			}
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			if (existingToken.ExpandOption != null)
			{
				this.AddChildOptionsToDictionary(existingToken, dictionary);
			}
			if (newToken.ExpandOption != null)
			{
				this.AddChildOptionsToDictionary(newToken, dictionary);
			}
			return dictionary.Values;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001C74C File Offset: 0x0001A94C
		private void AddChildOptionsToDictionary(ExpandTermToken newToken, Dictionary<PathSegmentToken, ExpandTermToken> combinedTerms)
		{
			foreach (ExpandTermToken expandTermToken in newToken.ExpandOption.ExpandTerms)
			{
				this.AddOrCombine(combinedTerms, expandTermToken);
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
		private void AddOrCombine(IDictionary<PathSegmentToken, ExpandTermToken> combinedTerms, ExpandTermToken expandedTerm)
		{
			ExpandTermToken expandTermToken;
			if (combinedTerms.TryGetValue(expandedTerm.PathToNavigationProp, ref expandTermToken))
			{
				combinedTerms[expandedTerm.PathToNavigationProp] = this.CombineTerms(expandedTerm, expandTermToken);
				return;
			}
			combinedTerms.Add(expandedTerm.PathToNavigationProp, expandedTerm);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
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
			List<PathSegmentToken> list = Enumerable.ToList<PathSegmentToken>(existingToken.SelectOption.Properties);
			list.AddRange(newToken.SelectOption.Properties);
			return new SelectToken(Enumerable.Distinct<PathSegmentToken>(list, new PathSegmentTokenEqualityComparer()));
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001C83D File Offset: 0x0001AA3D
		private static SelectToken RemoveDuplicateSelect(SelectToken selectToken)
		{
			if (selectToken == null)
			{
				return null;
			}
			return new SelectToken(Enumerable.Distinct<PathSegmentToken>(selectToken.Properties, new PathSegmentTokenEqualityComparer()));
		}
	}
}
