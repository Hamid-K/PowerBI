using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200014C RID: 332
	internal sealed class BatchQueryDataTransformTableGenerator
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x00033170 File Offset: 0x00031370
		internal static GeneratedTable Generate(PlanOperationDataTransform operation, GeneratedTable input, BatchQueryGenerationContext context, IQueryExpressionGenerator expressionGenerator)
		{
			DataTransform transform = operation.Transform;
			IDaxDataTransformMetadata transformMetadata = BatchQueryDataTransformTableGenerator.GetTransformMetadata(transform, context);
			QueryTable queryTable = BatchQueryDataTransformTableGenerator.CreateInputRoleMapping(transform, input);
			QueryTable queryTable2;
			List<QueryTableColumn> list;
			List<global::System.ValueTuple<string, int>> list2;
			WritableGeneratedColumnMap writableGeneratedColumnMap;
			BatchQueryDataTransformTableGenerator.DetermineOutputSchema(transform, input, context, out queryTable2, out list, out list2, out writableGeneratedColumnMap);
			IReadOnlyList<QueryExpression> readOnlyList = BatchQueryDataTransformTableGenerator.CreateArguments(input.QueryTable, queryTable, queryTable2, transform.Input.Parameters, transformMetadata.Parameters, context.ErrorContext, expressionGenerator);
			return new GeneratedTable(QueryTableBuilder.InvokeExtensionFunction(list, transformMetadata.DaxFunctionName, readOnlyList, list2), writableGeneratedColumnMap);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000331E4 File Offset: 0x000313E4
		private static IDaxDataTransformMetadata GetTransformMetadata(DataTransform transform, BatchQueryGenerationContext context)
		{
			Contract.RetailAssert(context.TransformMetadataFactory != null, "An IDaxDataTransformMetadata is required to apply transforms inside the query");
			IDaxDataTransformMetadata daxDataTransformMetadata = context.TransformMetadataFactory.Create(transform.Algorithm.Value);
			if (daxDataTransformMetadata == null)
			{
				context.ErrorContext.Register(TranslationMessages.UnknownDataTransformAlgorithm(EngineMessageSeverity.Error, transform.ObjectType, transform.Id, "Algorithm", transform.Algorithm.Value));
				throw new QueryGenerationException("Unknown data transform algorithm.");
			}
			return daxDataTransformMetadata;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00033255 File Offset: 0x00031455
		private static IReadOnlyList<QueryExpression> CreateArguments(QueryTable inputTable, QueryTable inputRoleMapping, QueryTable outputRoleMapping, IReadOnlyList<DataTransformParameter> transformParams, IReadOnlyList<IDaxDataTransformParameterMetadata> functionParams, TranslationErrorContext errorContext, IQueryExpressionGenerator expressionGenerator)
		{
			DataTransformFunctionArgumentsBuilder dataTransformFunctionArgumentsBuilder = new DataTransformFunctionArgumentsBuilder(errorContext, expressionGenerator);
			dataTransformFunctionArgumentsBuilder.AddConventionalArguments(inputTable, inputRoleMapping, outputRoleMapping);
			dataTransformFunctionArgumentsBuilder.AddCustomArguments(transformParams, functionParams);
			return dataTransformFunctionArgumentsBuilder.ToList();
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00033278 File Offset: 0x00031478
		private static QueryTable CreateInputRoleMapping(DataTransform transform, GeneratedTable input)
		{
			DataTransformRoleMappingTableBuilder dataTransformRoleMappingTableBuilder = new DataTransformRoleMappingTableBuilder();
			foreach (DataTransformTableColumn dataTransformTableColumn in transform.Input.Table.Columns)
			{
				if (!string.IsNullOrEmpty(dataTransformTableColumn.Role.GetValueOrDefault<string>()))
				{
					QueryTableColumn queryColumn = BatchQueryDataTransformTableGenerator.GetQueryColumn(input, dataTransformTableColumn);
					dataTransformRoleMappingTableBuilder.AddExistingColumn(input.QueryTable, queryColumn, dataTransformTableColumn.Role.Value);
				}
			}
			return dataTransformRoleMappingTableBuilder.ToQueryTable();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0003330C File Offset: 0x0003150C
		private static void DetermineOutputSchema(DataTransform transform, GeneratedTable input, BatchQueryGenerationContext context, out QueryTable outputRoleMapping, out List<QueryTableColumn> outputColumns, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })] out List<global::System.ValueTuple<string, int>> outputColumnLineage, out WritableGeneratedColumnMap outputColumnMap)
		{
			DataTransformRoleMappingTableBuilder dataTransformRoleMappingTableBuilder = new DataTransformRoleMappingTableBuilder();
			BatchQueryGenerationNamingContext batchQueryGenerationNamingContext = new BatchQueryGenerationNamingContext();
			List<DataTransformTableColumn> columns = transform.Output.Table.Columns;
			outputColumns = new List<QueryTableColumn>(columns.Count);
			outputColumnLineage = new List<global::System.ValueTuple<string, int>>(columns.Count);
			outputColumnMap = new WritableGeneratedColumnMap();
			List<global::System.ValueTuple<ExpressionId, string>> list = new List<global::System.ValueTuple<ExpressionId, string>>();
			foreach (DataTransformTableColumn dataTransformTableColumn in columns)
			{
				ExpressionNode node = context.ExpressionTable.GetNode(dataTransformTableColumn.Value);
				DataTransformTableColumn dataTransformTableColumn2;
				if (ExpressionAnalysisUtils.TryExtractDataTransformColumnReference(node, out dataTransformTableColumn2))
				{
					QueryTableColumn queryColumn = BatchQueryDataTransformTableGenerator.GetQueryColumn(input, dataTransformTableColumn2);
					dataTransformRoleMappingTableBuilder.AddExistingColumn(input.QueryTable, queryColumn, null);
					batchQueryGenerationNamingContext.RegisterName(queryColumn.Name);
					QueryTableColumn queryTableColumn = queryColumn.ToReferenceColumn();
					outputColumns.Add(queryTableColumn);
					outputColumnLineage.Add(new global::System.ValueTuple<string, int>(queryTableColumn.Name, 0));
					outputColumnMap.Add(dataTransformTableColumn.Value.ExpressionId.Value, queryTableColumn);
				}
				else
				{
					string text = BatchQueryDataTransformTableGenerator.ExtractOutputRole(node);
					list.Add(new global::System.ValueTuple<ExpressionId, string>(dataTransformTableColumn.Value.ExpressionId.Value, text));
				}
			}
			foreach (global::System.ValueTuple<ExpressionId, string> valueTuple in list)
			{
				string text2 = batchQueryGenerationNamingContext.CreateAndRegisterUniqueName(DataTransformQueryUtils.SanitizeRoleForColumnName(valueTuple.Item2));
				dataTransformRoleMappingTableBuilder.AddNewColumn(text2, valueTuple.Item2);
				QueryTableColumn queryTableColumn2 = new QueryTableColumn(text2, DataTransformConstants.StringType);
				outputColumns.Add(queryTableColumn2);
				outputColumnMap.Add(valueTuple.Item1, queryTableColumn2);
			}
			outputRoleMapping = dataTransformRoleMappingTableBuilder.ToQueryTable();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000334E8 File Offset: 0x000316E8
		private static QueryTableColumn GetQueryColumn(GeneratedTable table, DataTransformTableColumn transformColumn)
		{
			QueryTableColumn queryTableColumn;
			if (!table.ColumnMap.TryGetColumn(transformColumn.Value.ExpressionId.Value, out queryTableColumn))
			{
				Contract.RetailFail("Could not find expression for DataTransformColumn {0} in input query table.", transformColumn.Id.Value);
			}
			return queryTableColumn;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00033530 File Offset: 0x00031730
		private static string ExtractOutputRole(ExpressionNode transformOutputColumnExpr)
		{
			FunctionCallExpressionNode functionCallExpressionNode = transformOutputColumnExpr as FunctionCallExpressionNode;
			Contract.RetailAssert(functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.Name == "TransformOutputRoleRef", "A transform output column should only refer to an input column or the TransformOutputRoleRef function.");
			Contract.RetailAssert(functionCallExpressionNode.Arguments.Count > 0, "Missing role name in TransformOutputRoleRef function call.");
			LiteralExpressionNode literalExpressionNode = functionCallExpressionNode.Arguments[0] as LiteralExpressionNode;
			Contract.RetailAssert(literalExpressionNode != null, "Expected role name to be a literal.");
			return literalExpressionNode.Value.CastValue<string>();
		}
	}
}
