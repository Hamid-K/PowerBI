using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C8 RID: 456
	internal sealed class ExpandTreeNormalizer
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x0003B47C File Offset: 0x0003967C
		public ExpandToken NormalizeExpandTree(ExpandToken treeToNormalize)
		{
			ExpandToken expandToken = this.NormalizePaths(treeToNormalize);
			return this.CombineTerms(expandToken);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0003B498 File Offset: 0x00039698
		public ExpandToken NormalizePaths(ExpandToken treeToInvert)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			foreach (ExpandTermToken expandTermToken in treeToInvert.ExpandTerms)
			{
				PathReverser pathReverser = new PathReverser();
				PathSegmentToken pathSegmentToken = expandTermToken.PathToNavProp.Accept<PathSegmentToken>(pathReverser);
				SelectToken selectToken = expandTermToken.SelectOption;
				if (expandTermToken.SelectOption != null)
				{
					SelectTreeNormalizer selectTreeNormalizer = new SelectTreeNormalizer();
					selectToken = selectTreeNormalizer.NormalizeSelectTree(expandTermToken.SelectOption);
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
				ExpandTermToken expandTermToken2 = new ExpandTermToken(pathSegmentToken, expandTermToken.FilterOption, expandTermToken.OrderByOptions, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.CountQueryOption, expandTermToken.LevelsOption, expandTermToken.SearchOption, selectToken, expandToken);
				list.Add(expandTermToken2);
			}
			return new ExpandToken(list);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003B588 File Offset: 0x00039788
		public ExpandToken CombineTerms(ExpandToken treeToCollapse)
		{
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			foreach (ExpandTermToken expandTermToken in treeToCollapse.ExpandTerms)
			{
				ExpandTermToken expandTermToken2 = expandTermToken;
				if (expandTermToken.ExpandOption != null)
				{
					ExpandToken expandToken = this.CombineTerms(expandTermToken.ExpandOption);
					expandTermToken2 = new ExpandTermToken(expandTermToken.PathToNavProp, expandTermToken.FilterOption, expandTermToken.OrderByOptions, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.CountQueryOption, expandTermToken.LevelsOption, expandTermToken.SearchOption, ExpandTreeNormalizer.RemoveDuplicateSelect(expandTermToken.SelectOption), expandToken);
				}
				this.AddOrCombine(dictionary, expandTermToken2);
			}
			return new ExpandToken(dictionary.Values);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0003B64C File Offset: 0x0003984C
		public ExpandTermToken CombineTerms(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			List<ExpandTermToken> list = Enumerable.ToList<ExpandTermToken>(this.CombineChildNodes(existingToken, newToken));
			SelectToken selectToken = ExpandTreeNormalizer.CombineSelects(existingToken, newToken);
			return new ExpandTermToken(existingToken.PathToNavProp, existingToken.FilterOption, existingToken.OrderByOptions, existingToken.TopOption, existingToken.SkipOption, existingToken.CountQueryOption, existingToken.LevelsOption, existingToken.SearchOption, selectToken, new ExpandToken(list));
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003B6AC File Offset: 0x000398AC
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

		// Token: 0x06001102 RID: 4354 RVA: 0x0003B700 File Offset: 0x00039900
		private void AddChildOptionsToDictionary(ExpandTermToken newToken, Dictionary<PathSegmentToken, ExpandTermToken> combinedTerms)
		{
			foreach (ExpandTermToken expandTermToken in newToken.ExpandOption.ExpandTerms)
			{
				this.AddOrCombine(combinedTerms, expandTermToken);
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003B754 File Offset: 0x00039954
		private void AddOrCombine(IDictionary<PathSegmentToken, ExpandTermToken> combinedTerms, ExpandTermToken expandedTerm)
		{
			ExpandTermToken expandTermToken;
			if (combinedTerms.TryGetValue(expandedTerm.PathToNavProp, ref expandTermToken))
			{
				combinedTerms[expandedTerm.PathToNavProp] = this.CombineTerms(expandedTerm, expandTermToken);
				return;
			}
			combinedTerms.Add(expandedTerm.PathToNavProp, expandedTerm);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003B794 File Offset: 0x00039994
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

		// Token: 0x06001105 RID: 4357 RVA: 0x0003B7F1 File Offset: 0x000399F1
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
