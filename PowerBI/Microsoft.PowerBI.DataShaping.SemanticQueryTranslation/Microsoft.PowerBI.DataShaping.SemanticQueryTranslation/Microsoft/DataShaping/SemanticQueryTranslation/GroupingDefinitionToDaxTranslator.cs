using System;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.SemanticQueryTranslation.Binning;
using Microsoft.DataShaping.SemanticQueryTranslation.Clustering;
using Microsoft.DataShaping.SemanticQueryTranslation.Grouping;
using Microsoft.DataShaping.SemanticQueryTranslation.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x0200000D RID: 13
	internal sealed class GroupingDefinitionToDaxTranslator : IGroupingDefinitionToDaxTranslator
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B17 File Offset: 0x00000D17
		private GroupingDefinitionToDaxTranslator()
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B20 File Offset: 0x00000D20
		public SemanticQueryToDaxTranslationResult Translate(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			Func<SemanticQueryToDaxTranslationResult> func = () => GroupingDefinitionToDaxTranslator.TranslateInternal(command, context);
			SemanticQueryTranslatorContext context2 = context;
			Func<SemanticQueryToDaxTranslationResult, bool> func2;
			if ((func2 = GroupingDefinitionToDaxTranslator.<>O.<0>__IsResultValid) == null)
			{
				func2 = (GroupingDefinitionToDaxTranslator.<>O.<0>__IsResultValid = new Func<SemanticQueryToDaxTranslationResult, bool>(SemanticQueryTranslationUtils.IsResultValid));
			}
			return SemanticQueryTranslationUtils.EnsureTranslatorContract<SemanticQueryToDaxTranslationResult>(func, context2, func2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B74 File Offset: 0x00000D74
		public SemanticQueryToDaxTranslationResult TranslatePartitionColumn(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			Func<SemanticQueryToDaxTranslationResult> func = () => GroupingDefinitionToDaxTranslator.TranslatePartitionColumnInternal(command, context);
			SemanticQueryTranslatorContext context2 = context;
			Func<SemanticQueryToDaxTranslationResult, bool> func2;
			if ((func2 = GroupingDefinitionToDaxTranslator.<>O.<0>__IsResultValid) == null)
			{
				func2 = (GroupingDefinitionToDaxTranslator.<>O.<0>__IsResultValid = new Func<SemanticQueryToDaxTranslationResult, bool>(SemanticQueryTranslationUtils.IsResultValid));
			}
			return SemanticQueryTranslationUtils.EnsureTranslatorContract<SemanticQueryToDaxTranslationResult>(func, context2, func2);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private static SemanticQueryToDaxTranslationResult TranslateInternal(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			ResolvedGroupingDefinition resolvedGroupingDefinition;
			if (!GroupingDefinitionToDaxTranslator.TryValidateAndResolve(command, context, out resolvedGroupingDefinition))
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			SemanticQueryToDaxTranslationResult semanticQueryToDaxTranslationResult;
			switch (resolvedGroupingDefinition.DefinitionKind)
			{
			case GroupingDefinitionKind.GroupItems:
				semanticQueryToDaxTranslationResult = GroupingDefinitionToDaxTranslator.TranslateGroupItems(context, resolvedGroupingDefinition);
				break;
			case GroupingDefinitionKind.BinItem:
				semanticQueryToDaxTranslationResult = GroupingDefinitionToDaxTranslator.TranslateBinItem(context, resolvedGroupingDefinition);
				break;
			case GroupingDefinitionKind.PartitionTable:
				semanticQueryToDaxTranslationResult = GroupingDefinitionToDaxTranslator.TranslatePartitionTable(context, resolvedGroupingDefinition);
				break;
			default:
				throw new NotImplementedException("Translation of anything except a PartitionTable, GroupItems, or BinItem is not implemented");
			}
			SemanticQueryTranslationUtils.TraceResult(context, semanticQueryToDaxTranslationResult);
			return semanticQueryToDaxTranslationResult;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C38 File Offset: 0x00000E38
		private static SemanticQueryToDaxTranslationResult TranslatePartitionColumnInternal(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			ResolvedGroupingDefinition resolvedGroupingDefinition;
			if (!GroupingDefinitionToDaxTranslator.TryValidateAndResolve(command, context, out resolvedGroupingDefinition))
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			SemanticQueryToDaxTranslationResult semanticQueryToDaxTranslationResult = GroupingDefinitionToDaxTranslator.TranslatePartitionColumn(context, resolvedGroupingDefinition);
			SemanticQueryTranslationUtils.TraceResult(context, semanticQueryToDaxTranslationResult);
			return semanticQueryToDaxTranslationResult;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C6C File Offset: 0x00000E6C
		private static SemanticQueryToDaxTranslationResult TranslateGroupItems(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			return context.TelemetryService.RunInActivity<SemanticQueryToDaxTranslationResult>(ActivityKind.TranslateGrouping, () => GroupingDefinitionToDaxTranslator.TranslateGroupItemsInternal(context, resolvedGroupingDefinition));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CAC File Offset: 0x00000EAC
		private static SemanticQueryToDaxTranslationResult TranslateBinItem(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			return context.TelemetryService.RunInActivity<SemanticQueryToDaxTranslationResult>(ActivityKind.TranslateGrouping, () => GroupingDefinitionToDaxTranslator.TranslateBinItemInternal(context, resolvedGroupingDefinition.BinItem));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CEC File Offset: 0x00000EEC
		private static SemanticQueryToDaxTranslationResult TranslateGroupItemsInternal(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			SemanticQueryToDaxTranslationResult semanticQueryToDaxTranslationResult;
			if (!GroupingDaxTranslator.TryTranslate(context, resolvedGroupingDefinition, out semanticQueryToDaxTranslationResult))
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			return semanticQueryToDaxTranslationResult;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D14 File Offset: 0x00000F14
		private static SemanticQueryToDaxTranslationResult TranslateBinItemInternal(SemanticQueryTranslatorContext context, ResolvedBinItem resolvedBinItem)
		{
			SemanticQueryToDaxTranslationResult semanticQueryToDaxTranslationResult;
			if (!BinningDaxTranslator.TryTranslate(context, resolvedBinItem, out semanticQueryToDaxTranslationResult))
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			return semanticQueryToDaxTranslationResult;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D3C File Offset: 0x00000F3C
		private static SemanticQueryToDaxTranslationResult TranslatePartitionTable(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			return context.TelemetryService.RunInActivity<SemanticQueryToDaxTranslationResult>(ActivityKind.TranslateClustering, () => GroupingDefinitionToDaxTranslator.TranslatePartitionTableInternal(context, resolvedGroupingDefinition));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D7C File Offset: 0x00000F7C
		private static SemanticQueryToDaxTranslationResult TranslatePartitionTableInternal(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			ClusteringTranslationRequest clusteringTranslationRequest = new ClusteringTranslationRequest(resolvedGroupingDefinition.GroupedColumns, resolvedGroupingDefinition.PartitionTable);
			ClusteringTranslationResult clusteringTranslationResult;
			if (!ClusteringDaxTranslator.TryTranslate(context, clusteringTranslationRequest, out clusteringTranslationResult))
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			return SemanticQueryToDaxTranslationResultUtils.ForClustering(clusteringTranslationResult, context.ErrorContext);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private static SemanticQueryToDaxTranslationResult TranslatePartitionColumn(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition groupingDefinition)
		{
			return context.TelemetryService.RunInActivity<SemanticQueryToDaxTranslationResult>(ActivityKind.TranslateClusteringColumn, () => GroupingDefinitionToDaxTranslator.TranslatePartitionColumnInternal(context, groupingDefinition));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E00 File Offset: 0x00001000
		private static SemanticQueryToDaxTranslationResult TranslatePartitionColumnInternal(SemanticQueryTranslatorContext context, ResolvedGroupingDefinition groupingDefinition)
		{
			string text;
			if (!ClusteringColumnDaxTranslator.TryTranslate(context, groupingDefinition.PartitionTable, out text))
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			return SemanticQueryToDaxTranslationResultUtils.ForClusteringColumn(text, context.ErrorContext);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E35 File Offset: 0x00001035
		private static bool TryValidateAndResolve(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context, out ResolvedGroupingDefinition resolvedDefinition)
		{
			SemanticQueryTranslationUtils.TraceCommand(context, command);
			if (!GroupingDefinitionToDaxTranslator.TryValidateGroupingDefinition(context, command.GroupingDefinition))
			{
				resolvedDefinition = null;
				return false;
			}
			return GroupingDefinitionToDaxTranslator.TryResolveGroupingDefinition(context, command.GroupingDefinition, out resolvedDefinition);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E60 File Offset: 0x00001060
		private static bool TryResolveGroupingDefinition(SemanticQueryTranslatorContext context, GroupingDefinition groupingDefinition, out ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(context.ErrorContext.CreateAdapter("GroupingResolutionError", ErrorSourceCategory.InputDoesNotMatchModel));
			if (GroupingDefinitionResolver.TryResolveGroupingDefinition(groupingDefinition, context.Schema, queryResolutionErrorContext, out resolvedGroupingDefinition))
			{
				return true;
			}
			SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.GroupingResolutionError(EngineMessageSeverity.Error));
			return false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EAC File Offset: 0x000010AC
		private static bool TryValidateGroupingDefinition(SemanticQueryTranslatorContext context, GroupingDefinition groupingDefinition)
		{
			if (groupingDefinition.IsValid())
			{
				return true;
			}
			SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.InvalidGroupingError(EngineMessageSeverity.Error));
			return false;
		}

		// Token: 0x0400003C RID: 60
		internal static readonly GroupingDefinitionToDaxTranslator Instance = new GroupingDefinitionToDaxTranslator();

		// Token: 0x0200002C RID: 44
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000086 RID: 134
			public static Func<SemanticQueryToDaxTranslationResult, bool> <0>__IsResultValid;
		}
	}
}
