using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x020000FC RID: 252
	internal static class CommonSubqueryEliminationOptimizer
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x00021B34 File Offset: 0x0001FD34
		public static CommonSubqueryEliminationResult Run(ResolvedQueryDefinition query, ResolvedQueryDefinitionEquivalenceComparer queryComparer)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			MergeableSubqueryCollectorResult mergeableSubqueryCollectorResult = MergeableSubqueryCollector.Collect(query, queryComparer);
			if (mergeableSubqueryCollectorResult.Groups == null || !mergeableSubqueryCollectorResult.UsedLetNames.IsNullOrEmptyCollection<string>())
			{
				return new CommonSubqueryEliminationResult(query, stopwatch.ElapsedMilliseconds, mergeableSubqueryCollectorResult.ConsideredSubqueryCount, mergeableSubqueryCollectorResult.ComparedSubqueryCount, 0, 0);
			}
			Dictionary<ResolvedQueryDefinition, ResolvedQueryLetRefExpression> dictionary;
			List<ResolvedQueryLetBinding> list = LetBindingGenerator.CreateLetBindingsForMergedQueries(mergeableSubqueryCollectorResult.Groups, mergeableSubqueryCollectorResult.UsedLetNames, out dictionary);
			return new CommonSubqueryEliminationResult(CommonSubqueryEliminationQueryRewriter.Rewrite(query, list, dictionary), stopwatch.ElapsedMilliseconds, mergeableSubqueryCollectorResult.ConsideredSubqueryCount, mergeableSubqueryCollectorResult.ComparedSubqueryCount, dictionary.Count - list.Count, list.Count);
		}
	}
}
