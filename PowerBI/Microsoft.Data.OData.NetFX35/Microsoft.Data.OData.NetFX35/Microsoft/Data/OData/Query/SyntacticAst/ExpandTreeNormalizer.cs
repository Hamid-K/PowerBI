using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000027 RID: 39
	internal static class ExpandTreeNormalizer
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004C84 File Offset: 0x00002E84
		public static ExpandToken NormalizeExpandTree(ExpandToken treeToNormalize)
		{
			ExpandToken expandToken = ExpandTreeNormalizer.InvertPaths(treeToNormalize);
			return ExpandTreeNormalizer.CombineTerms(expandToken);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public static ExpandToken InvertPaths(ExpandToken treeToInvert)
		{
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			foreach (ExpandTermToken expandTermToken in treeToInvert.ExpandTerms)
			{
				PathReverser pathReverser = new PathReverser();
				PathSegmentToken pathSegmentToken = expandTermToken.PathToNavProp.Accept<PathSegmentToken>(pathReverser);
				ExpandTermToken expandTermToken2 = new ExpandTermToken(pathSegmentToken, expandTermToken.FilterOption, expandTermToken.OrderByOption, expandTermToken.TopOption, expandTermToken.SkipOption, expandTermToken.InlineCountOption, expandTermToken.SelectOption, expandTermToken.ExpandOption);
				list.Add(expandTermToken2);
			}
			return new ExpandToken(list);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004D44 File Offset: 0x00002F44
		public static ExpandToken CombineTerms(ExpandToken treeToCollapse)
		{
			Dictionary<PathSegmentToken, ExpandTermToken> dictionary = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
			foreach (ExpandTermToken expandTermToken in treeToCollapse.ExpandTerms)
			{
				ExpandTermToken expandTermToken2 = ExpandTreeNormalizer.BuildSubExpandTree(expandTermToken);
				ExpandTreeNormalizer.AddOrCombine(dictionary, expandTermToken2);
			}
			return new ExpandToken(dictionary.Values);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static ExpandTermToken BuildSubExpandTree(ExpandTermToken termToExpand)
		{
			if (termToExpand.PathToNavProp.NextToken == null)
			{
				return termToExpand;
			}
			PathSegmentToken pathToNavProp = termToExpand.PathToNavProp;
			PathSegmentToken pathSegmentToken = pathToNavProp;
			while (pathSegmentToken.IsNamespaceOrContainerQualified())
			{
				pathSegmentToken = pathSegmentToken.NextToken;
				if (pathSegmentToken == null)
				{
					throw new ODataException(Strings.ExpandTreeNormalizer_NonPathInPropertyChain);
				}
			}
			PathSegmentToken nextToken = pathSegmentToken.NextToken;
			pathSegmentToken.SetNextToken(null);
			ExpandToken expandToken;
			if (nextToken != null)
			{
				ExpandTermToken expandTermToken = new ExpandTermToken(nextToken, termToExpand.FilterOption, termToExpand.OrderByOption, termToExpand.TopOption, termToExpand.SkipOption, termToExpand.InlineCountOption, termToExpand.SelectOption, null);
				ExpandTermToken expandTermToken2 = ExpandTreeNormalizer.BuildSubExpandTree(expandTermToken);
				expandToken = new ExpandToken(new ExpandTermToken[] { expandTermToken2 });
			}
			else
			{
				expandToken = new ExpandToken(new ExpandTermToken[0]);
			}
			return new ExpandTermToken(pathToNavProp, termToExpand.FilterOption, termToExpand.OrderByOption, termToExpand.TopOption, termToExpand.SkipOption, termToExpand.InlineCountOption, termToExpand.SelectOption, expandToken);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004E8C File Offset: 0x0000308C
		public static ExpandTermToken CombineTerms(ExpandTermToken existingToken, ExpandTermToken newToken)
		{
			List<ExpandTermToken> list = Enumerable.ToList<ExpandTermToken>(ExpandTreeNormalizer.CombineChildNodes(existingToken, newToken));
			return new ExpandTermToken(existingToken.PathToNavProp, existingToken.FilterOption, existingToken.OrderByOption, existingToken.TopOption, existingToken.SkipOption, existingToken.InlineCountOption, existingToken.SelectOption, new ExpandToken(list));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004EDC File Offset: 0x000030DC
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

		// Token: 0x06000103 RID: 259 RVA: 0x00004F30 File Offset: 0x00003130
		private static void AddChildOptionsToDictionary(ExpandTermToken newToken, Dictionary<PathSegmentToken, ExpandTermToken> combinedTerms)
		{
			foreach (ExpandTermToken expandTermToken in newToken.ExpandOption.ExpandTerms)
			{
				ExpandTreeNormalizer.AddOrCombine(combinedTerms, expandTermToken);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004F84 File Offset: 0x00003184
		private static void AddOrCombine(IDictionary<PathSegmentToken, ExpandTermToken> combinedTerms, ExpandTermToken expandedTerm)
		{
			ExpandTermToken expandTermToken;
			if (combinedTerms.TryGetValue(expandedTerm.PathToNavProp, ref expandTermToken))
			{
				combinedTerms[expandedTerm.PathToNavProp] = ExpandTreeNormalizer.CombineTerms(expandedTerm, expandTermToken);
				return;
			}
			combinedTerms.Add(expandedTerm.PathToNavProp, expandedTerm);
		}
	}
}
