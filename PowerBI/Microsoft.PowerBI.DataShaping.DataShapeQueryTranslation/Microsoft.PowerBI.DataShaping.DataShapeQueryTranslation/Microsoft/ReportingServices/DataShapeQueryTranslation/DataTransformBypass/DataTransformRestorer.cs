using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C1 RID: 193
	internal sealed class DataTransformRestorer
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x0001F930 File Offset: 0x0001DB30
		public static DataTransformRestorationResult RestoreTransforms(DataShape dataShape, BatchDataSetPlanningResult dataSetPlanningResult, ReadOnlyCollection<BatchQueryGenerationResult> queryGenerationResults, DataTransformInliningResult inliningResult)
		{
			BatchExpressionTableLookup batchExpressionTableLookup = new BatchExpressionTableLookup(dataSetPlanningResult, queryGenerationResults);
			return DataTransformRestorer.RestoreTransforms(dataShape, batchExpressionTableLookup, inliningResult);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001F950 File Offset: 0x0001DB50
		public static DataTransformRestorationResult RestoreTransforms(DataShape dataShape, DataSetPlanningResult dataSetPlanningResult, ReadOnlyCollection<QueryGenerationResult> queryGenerationResults, DataTransformInliningResult inliningResult)
		{
			RegularExpressionTableLookup regularExpressionTableLookup = new RegularExpressionTableLookup(dataSetPlanningResult, queryGenerationResults);
			return DataTransformRestorer.RestoreTransforms(dataShape, regularExpressionTableLookup, inliningResult);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001F970 File Offset: 0x0001DB70
		private static DataTransformRestorationResult RestoreTransforms(DataShape dataShape, IExpressionTableLookup expressionTableLookup, DataTransformInliningResult inliningResult)
		{
			if (!inliningResult.RequiresRestoration)
			{
				return DataTransformRestorer.CreateNoOpResult(expressionTableLookup);
			}
			WritableExpressionTable writableExpressionTable = DataTransformRestorerRemapper.RemapTransforms(dataShape, inliningResult.ExpressionTable, inliningResult.SourceColumns, expressionTableLookup);
			Dictionary<ResultSetReference, DataTransformTable> dictionary;
			return DataTransformRestorer.CreateRestoredResult(DataTransformRestorer.RestoreInlinedExpressions(expressionTableLookup, inliningResult.ExpressionsToRestore, out dictionary), writableExpressionTable, dictionary);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001F9B8 File Offset: 0x0001DBB8
		private static List<WritableExpressionTable> RestoreInlinedExpressions(IExpressionTableLookup expressionTableLookup, IReadOnlyList<ExpressionRestorationInfo> expressionsToRestore, out Dictionary<ResultSetReference, DataTransformTable> resultSetToTableMapping)
		{
			resultSetToTableMapping = new Dictionary<ResultSetReference, DataTransformTable>();
			List<WritableExpressionTable> list = DataTransformRestorer.CreateOutputExpressionTables(expressionTableLookup);
			foreach (ExpressionRestorationInfo expressionRestorationInfo in expressionsToRestore)
			{
				int expressionTableIndex = expressionTableLookup.GetExpressionTableIndex(expressionRestorationInfo.Expression.Owner);
				ExpressionNode expressionNode = expressionTableLookup.GetExpressionTable(expressionTableIndex).GetNodeOrDefault(expressionRestorationInfo.Expression.ExpressionId);
				if (expressionNode == null)
				{
					expressionNode = expressionTableLookup.GetFallbackExpressionTable().GetNode(expressionRestorationInfo.Expression.ExpressionId);
				}
				DataTransformTable dataTransformTable = DataTransformRestorer.DetermineTargetTable(expressionRestorationInfo.OriginalNode);
				ResultSetReference resultSetReference = DataTransformRestorer.DetermineTargetResultSet(expressionNode);
				if (resultSetReference != null)
				{
					resultSetToTableMapping[resultSetReference] = dataTransformTable;
				}
				list[expressionTableIndex].SetNode(expressionRestorationInfo.Expression.ExpressionId, expressionRestorationInfo.OriginalNode);
			}
			return list;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001FA98 File Offset: 0x0001DC98
		private static DataTransformTable DetermineTargetTable(ExpressionNode node)
		{
			ResolvedDataTransformTableColumnReferenceExpressionNode resolvedDataTransformTableColumnReferenceExpressionNode = node as ResolvedDataTransformTableColumnReferenceExpressionNode;
			Contract.RetailAssert(resolvedDataTransformTableColumnReferenceExpressionNode != null, "Expected a column reference expression");
			return resolvedDataTransformTableColumnReferenceExpressionNode.Table;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		private static ResultSetReference DetermineTargetResultSet(ExpressionNode node)
		{
			DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode = node as DataSetFieldReferenceExpressionNode;
			if (dataSetFieldReferenceExpressionNode == null)
			{
				FunctionCallExpressionNode functionCallExpressionNode = node as FunctionCallExpressionNode;
				if (functionCallExpressionNode == null || functionCallExpressionNode.Descriptor.BackingFunctionName != "TransformOutputRoleRef")
				{
					Contract.RetailFail("Expected a field reference expression or a transform output role reference expression");
				}
				return null;
			}
			return new ResultSetReference(dataSetFieldReferenceExpressionNode.DataSetPlan, dataSetFieldReferenceExpressionNode.TablePlan);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001FB0C File Offset: 0x0001DD0C
		private static List<WritableExpressionTable> CreateOutputExpressionTables(IExpressionTableLookup expressionTableLookup)
		{
			List<WritableExpressionTable> list = new List<WritableExpressionTable>(expressionTableLookup.Count);
			for (int i = 0; i < expressionTableLookup.Count; i++)
			{
				ExpressionTable expressionTable = expressionTableLookup.GetExpressionTable(i);
				list.Add(expressionTable.CopyTable());
			}
			return list;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001FB4C File Offset: 0x0001DD4C
		private static DataTransformRestorationResult CreateNoOpResult(IExpressionTableLookup expressionTableLookup)
		{
			List<ReadOnlyExpressionTable> list = new List<ReadOnlyExpressionTable>(expressionTableLookup.Count);
			for (int i = 0; i < expressionTableLookup.Count; i++)
			{
				ExpressionTable expressionTable = expressionTableLookup.GetExpressionTable(i);
				list.Add(expressionTable.AsReadOnly());
			}
			return new DataTransformRestorationResult(list.AsReadOnly());
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001FB98 File Offset: 0x0001DD98
		private static DataTransformRestorationResult CreateRestoredResult(List<WritableExpressionTable> outputExprTables, WritableExpressionTable transformExprTable, Dictionary<ResultSetReference, DataTransformTable> resultSetToTableMapping)
		{
			List<ReadOnlyExpressionTable> list = new List<ReadOnlyExpressionTable>(outputExprTables.Count);
			for (int i = 0; i < outputExprTables.Count; i++)
			{
				ReadOnlyExpressionTable readOnlyExpressionTable = outputExprTables[i].AsReadOnly();
				list.Add(readOnlyExpressionTable);
			}
			return new DataTransformRestorationResult(list, transformExprTable.AsReadOnly(), resultSetToTableMapping);
		}
	}
}
