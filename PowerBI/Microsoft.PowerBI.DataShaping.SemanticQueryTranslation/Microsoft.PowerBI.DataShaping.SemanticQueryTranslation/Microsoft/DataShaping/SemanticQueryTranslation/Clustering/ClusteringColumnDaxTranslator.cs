using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Clustering
{
	// Token: 0x02000025 RID: 37
	internal static class ClusteringColumnDaxTranslator
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00006644 File Offset: 0x00004844
		internal static bool TryTranslate(SemanticQueryTranslatorContext context, ResolvedPartitionTable partitionTable, out string columnExpression)
		{
			if (!ClusteringColumnDaxTranslator.ValidatePartitionTable(context, partitionTable))
			{
				columnExpression = null;
				return false;
			}
			ResolvedPartitionTableResult result = partitionTable.Result;
			List<ClusteringLookupTuple> list;
			if (!ClusteringColumnDaxTranslator.TryGenerateClusterLookupTuples(context, result, out list))
			{
				columnExpression = null;
				return false;
			}
			IReadOnlyList<KeyValuePair<ResolvedQueryExpression, string>> readOnlyList = SemanticQueryTranslationUtils.ConvertDisplayNames(partitionTable.Definition.Partitions);
			if (!ClusteringColumnGenerator.TryGenerate(context, result.TableName, result.PartitionIdColumn, list, readOnlyList, partitionTable.Definition.DefaultPartitionPrefix, out columnExpression))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.ClusteringColumnGenerationError(EngineMessageSeverity.Error));
				columnExpression = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000066C0 File Offset: 0x000048C0
		private static bool ValidatePartitionTable(SemanticQueryTranslatorContext context, ResolvedPartitionTable partitionTable)
		{
			if (partitionTable == null || partitionTable.Definition == null || partitionTable.Result == null || string.IsNullOrEmpty(partitionTable.Result.TableName) || string.IsNullOrEmpty(partitionTable.Result.PartitionIdColumn))
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.MissingPartitionTableContentError(EngineMessageSeverity.Error));
				return false;
			}
			ResolvedPartitionTableResult result = partitionTable.Result;
			if (result.ItemIdMappings == null || result.ItemIdMappings.Count != 1 || result.ItemIdMappings[0].IsNullOrEmpty<ResolvedPartitionTableIdentityMapping>())
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.MissingPartitionTableMappingsError(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000675C File Offset: 0x0000495C
		private static bool TryGenerateClusterLookupTuples(SemanticQueryTranslatorContext context, ResolvedPartitionTableResult tableResult, out List<ClusteringLookupTuple> tuples)
		{
			IReadOnlyList<ResolvedPartitionTableIdentityMapping> readOnlyList = tableResult.ItemIdMappings[0];
			tuples = new List<ClusteringLookupTuple>(readOnlyList.Count);
			foreach (ResolvedPartitionTableIdentityMapping resolvedPartitionTableIdentityMapping in readOnlyList)
			{
				ResolvedQueryColumnExpression resolvedQueryColumnExpression = resolvedPartitionTableIdentityMapping.PartitionTableColumn as ResolvedQueryColumnExpression;
				ResolvedQueryColumnExpression resolvedQueryColumnExpression2 = resolvedPartitionTableIdentityMapping.SourceTableColumn as ResolvedQueryColumnExpression;
				if (resolvedQueryColumnExpression == null || resolvedQueryColumnExpression2 == null)
				{
					SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.InvalidPartitionTableColumnsError(EngineMessageSeverity.Error));
					tuples = null;
					return false;
				}
				tuples.Add(new ClusteringLookupTuple(resolvedQueryColumnExpression.Column.Name, resolvedQueryColumnExpression2));
			}
			return true;
		}
	}
}
