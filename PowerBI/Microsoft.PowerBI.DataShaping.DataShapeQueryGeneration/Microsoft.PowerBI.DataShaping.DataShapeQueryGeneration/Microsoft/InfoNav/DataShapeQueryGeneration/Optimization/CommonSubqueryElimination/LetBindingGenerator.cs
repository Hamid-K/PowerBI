using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x02000101 RID: 257
	internal static class LetBindingGenerator
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00021D6C File Offset: 0x0001FF6C
		internal static List<ResolvedQueryLetBinding> CreateLetBindingsForMergedQueries(IReadOnlyList<EquivalentQueryGroup> queryGroups, HashSet<string> usedLetNames, out Dictionary<ResolvedQueryDefinition, ResolvedQueryLetRefExpression> queryRewriteMapping)
		{
			queryRewriteMapping = new Dictionary<ResolvedQueryDefinition, ResolvedQueryLetRefExpression>(queryGroups.Count, ReferenceEqualityComparer<ResolvedQueryDefinition>.Instance);
			List<ResolvedQueryLetBinding> list = new List<ResolvedQueryLetBinding>(queryGroups.Count);
			foreach (EquivalentQueryGroup equivalentQueryGroup in queryGroups)
			{
				ResolvedQueryDefinition resolvedQueryDefinition = equivalentQueryGroup.Exemplar;
				if (queryRewriteMapping.Count > 0)
				{
					resolvedQueryDefinition = CommonSubqueryEliminationQueryRewriter.Rewrite(resolvedQueryDefinition, null, queryRewriteMapping);
				}
				ResolvedQueryLetBinding resolvedQueryLetBinding = new ResolvedQueryLetBinding(LetBindingGenerator.MakeLetName(resolvedQueryDefinition.Name, ref usedLetNames), resolvedQueryDefinition.Subquery());
				list.Add(resolvedQueryLetBinding);
				ResolvedQueryLetRefExpression resolvedQueryLetRefExpression = resolvedQueryLetBinding.LetRef();
				queryRewriteMapping.Add(equivalentQueryGroup.Exemplar, resolvedQueryLetRefExpression);
				foreach (ResolvedQueryDefinition resolvedQueryDefinition2 in equivalentQueryGroup.EquivalentQueries)
				{
					queryRewriteMapping.Add(resolvedQueryDefinition2, resolvedQueryLetRefExpression);
				}
			}
			return list;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00021E70 File Offset: 0x00020070
		private static string MakeLetName(string candidate, ref HashSet<string> usedNames)
		{
			if (usedNames == null)
			{
				usedNames = new HashSet<string>(QueryNameComparer.Instance);
			}
			string text = StringUtil.MakeUniqueName(candidate ?? "let", usedNames);
			usedNames.Add(text);
			return text;
		}

		// Token: 0x0400045B RID: 1115
		internal const string DefaultLetName = "let";
	}
}
