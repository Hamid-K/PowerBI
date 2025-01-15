using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;
using Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization
{
	// Token: 0x020000F9 RID: 249
	internal static class QueryOptimizer
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x000215BC File Offset: 0x0001F7BC
		public static bool TryOptimizeQuery(ResolvedSemanticQueryDataShapeCommand command, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations, DataShapeGenerationTelemetry telemetry, DataShapeGenerationErrorContext errorContext, ITracer tracer, out ResolvedSemanticQueryDataShapeCommand newCommand, out SemanticQueryDataShapeAnnotations newAnnotations)
		{
			ResolvedQueryDefinition query = command.QueryDataShape.Query;
			ResolvedSemanticQueryDataShape queryDataShape = command.QueryDataShape;
			IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> readOnlyList;
			if (queryDataShape == null)
			{
				readOnlyList = null;
			}
			else
			{
				DataShapeBinding binding = queryDataShape.Binding;
				if (binding == null)
				{
					readOnlyList = null;
				}
				else
				{
					IList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName = binding.SuppressedJoinPredicatesByName;
					readOnlyList = ((suppressedJoinPredicatesByName != null) ? suppressedJoinPredicatesByName.AsReadOnlyList<DataShapeBindingSuppressedJoinPredicate>() : null);
				}
			}
			ResolvedQueryDefinition resolvedQueryDefinition = QueryOptimizer.RunOptimizers(query, readOnlyList, featureSwitchProvider, annotations, telemetry, tracer);
			if (resolvedQueryDefinition != command.QueryDataShape.Query)
			{
				newCommand = command.Clone(command.QueryDataShape.Clone(resolvedQueryDefinition), null);
				return SemanticQueryDataShapeAnnotationAnalyzer.TryCreateAnnotations(newCommand, out newAnnotations);
			}
			newCommand = command;
			newAnnotations = annotations;
			return true;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00021646 File Offset: 0x0001F846
		public static bool TryOptimizeQuery(ResolvedQueryDefinition query, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations, DataShapeGenerationTelemetry telemetry, DataShapeGenerationErrorContext errorContext, ITracer tracer, out ResolvedQueryDefinition newQuery, out SemanticQueryDataShapeAnnotations newAnnotations)
		{
			newQuery = QueryOptimizer.RunOptimizers(query, suppressedJoinPredicatesByName, featureSwitchProvider, annotations, telemetry, tracer);
			if (newQuery != query)
			{
				return SemanticQueryDataShapeAnnotationAnalyzer.TryCreateAnnotations(newQuery, out newAnnotations);
			}
			newAnnotations = annotations;
			return true;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00021674 File Offset: 0x0001F874
		private static ResolvedQueryDefinition RunOptimizers(ResolvedQueryDefinition query, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations, DataShapeGenerationTelemetry telemetry, ITracer tracer)
		{
			CommonSubqueryEliminationTelemetry commonSubqueryEliminationTelemetry;
			ResolvedQueryDefinition resolvedQueryDefinition = QueryOptimizer.EliminateCommonSubqueries(query, featureSwitchProvider, annotations, suppressedJoinPredicatesByName, tracer, out commonSubqueryEliminationTelemetry);
			if (commonSubqueryEliminationTelemetry != null)
			{
				telemetry.Optimization = new QueryOptimizerTelemetry
				{
					CommonSubqueryElimination = commonSubqueryEliminationTelemetry
				};
			}
			return resolvedQueryDefinition;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000216A4 File Offset: 0x0001F8A4
		public static ResolvedQueryDefinition EliminateCommonSubqueries(ResolvedQueryDefinition query, IFeatureSwitchProvider featureSwitchProvider, SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, ITracer tracer, out CommonSubqueryEliminationTelemetry cseTelemetry)
		{
			if (annotations.SubqueryCount < 2)
			{
				cseTelemetry = null;
				return query;
			}
			ResolvedQueryDataShapeEquivalenceComparer resolvedQueryDataShapeEquivalenceComparer = new ResolvedQueryDataShapeEquivalenceComparer(annotations, suppressedJoinPredicatesByName, tracer);
			CommonSubqueryEliminationResult commonSubqueryEliminationResult = CommonSubqueryEliminationOptimizer.Run(query, resolvedQueryDataShapeEquivalenceComparer);
			cseTelemetry = new CommonSubqueryEliminationTelemetry
			{
				Duration = commonSubqueryEliminationResult.Duration,
				ConsideredSubqueryCount = commonSubqueryEliminationResult.ConsideredSubqueryCount,
				EliminatedSubqueryCount = commonSubqueryEliminationResult.EliminatedSubqueryCount,
				NewLetBindingCount = commonSubqueryEliminationResult.NewLetBindingCount,
				ComparedSubqueryCount = commonSubqueryEliminationResult.ComparedSubqueryCount
			};
			return commonSubqueryEliminationResult.Query;
		}
	}
}
