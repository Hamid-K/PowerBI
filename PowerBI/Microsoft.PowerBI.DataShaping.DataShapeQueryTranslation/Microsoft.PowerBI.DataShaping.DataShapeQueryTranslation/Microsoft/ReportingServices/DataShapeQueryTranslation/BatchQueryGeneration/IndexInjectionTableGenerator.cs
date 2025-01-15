using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200015B RID: 347
	internal static class IndexInjectionTableGenerator
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x00034498 File Offset: 0x00032698
		public static GeneratedTable Generate(GeneratedTable table, GeneratedTable indexTable, ExpressionTable expressionTable, IReadOnlyList<PlanSortItem> indexTableSorts, BatchQueryGenerationNamingContext namingContext, FederatedEntityDataModel model, IFederatedConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider, bool preserveSharedColumns, string indexColumnName = null, Calculation indexColumnCalculation = null)
		{
			if (preserveSharedColumns)
			{
				global::System.ValueTuple<GeneratedTable, GeneratedTable> valueTuple = IndexInjectionTableGenerator.PreserveColumnsForJoinOperation(table, indexTable, namingContext);
				table = valueTuple.Item1;
				indexTable = valueTuple.Item2;
			}
			else
			{
				foreach (QueryTableColumn queryTableColumn in table.QueryTable.Columns)
				{
					namingContext.RegisterName(queryTableColumn.Name);
				}
			}
			string text = namingContext.CreateAndRegisterUniqueName(indexColumnName ?? indexColumnCalculation.Id.Value);
			QueryTableColumn queryTableColumn2;
			QueryTableDefinition queryTableDefinition = table.QueryTable.SubstituteWithIndex(text, indexTable.QueryTable, BatchQuerySortItemGenerator.Generate(indexTable, indexTableSorts, expressionTable, model, featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), false), out queryTableColumn2, true);
			WritableGeneratedColumnMap writableGeneratedColumnMap = table.ColumnMap.FilterColumns(queryTableDefinition.Columns);
			if (indexColumnName != null)
			{
				writableGeneratedColumnMap.Add(indexColumnName, queryTableColumn2);
			}
			if (indexColumnCalculation != null)
			{
				writableGeneratedColumnMap.Add(indexColumnCalculation.Value.ExpressionId.Value, queryTableColumn2);
			}
			return new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000345A0 File Offset: 0x000327A0
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Table", "IndexTable" })]
		private static global::System.ValueTuple<GeneratedTable, GeneratedTable> PreserveColumnsForJoinOperation(GeneratedTable table, GeneratedTable indexTable, BatchQueryGenerationNamingContext namingContext)
		{
			IReadOnlyList<QueryTableColumn> columns = table.QueryTable.Columns;
			IReadOnlyList<QueryTableColumn> columns2 = indexTable.QueryTable.Columns;
			List<QueryTableColumn> list = new List<QueryTableColumn>(columns2.Count);
			List<QueryTableColumn> list2 = new List<QueryTableColumn>(columns.Count + columns2.Count);
			Dictionary<string, QueryTableColumn> dictionary = new Dictionary<string, QueryTableColumn>(columns.Count, QueryNamingContext.NameComparer);
			Dictionary<QueryTableColumn, QueryTableColumn> dictionary2 = new Dictionary<QueryTableColumn, QueryTableColumn>(columns2.Count);
			Dictionary<QueryTableColumn, QueryTableColumn> dictionary3 = new Dictionary<QueryTableColumn, QueryTableColumn>(columns2.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				QueryTableColumn queryTableColumn = columns[i];
				dictionary.Add(queryTableColumn.Name, queryTableColumn);
				namingContext.RegisterName(queryTableColumn.Name);
				QueryTableColumn queryTableColumn2 = queryTableColumn.ToReferenceColumn();
				list2.Add(queryTableColumn2);
				dictionary3[queryTableColumn] = queryTableColumn2;
			}
			for (int j = 0; j < columns2.Count; j++)
			{
				QueryTableColumn queryTableColumn3 = columns2[j];
				QueryTableColumn queryTableColumn4;
				Microsoft.DataShaping.Contract.RetailAssert(dictionary.TryGetValue(queryTableColumn3.Name, out queryTableColumn4), "Unexpected unique column found in indexTable");
				string text = namingContext.CreateAndRegisterUniqueName(QdmNames.Cloned(queryTableColumn3.Name));
				QueryTableColumn queryTableColumn5 = queryTableColumn3.QdmReference().ToQueryTableColumn(text);
				list.Add(queryTableColumn5);
				dictionary2[queryTableColumn3] = queryTableColumn5;
				QueryTableColumn queryTableColumn6 = queryTableColumn4.QdmReference().ToQueryTableColumn(text);
				list2.Add(queryTableColumn6);
			}
			WritableGeneratedColumnMap writableGeneratedColumnMap = indexTable.ColumnMap.GenerateWithReplacements(dictionary2, false);
			GeneratedTable generatedTable = new GeneratedTable(indexTable.QueryTable.Project(list, ProjectSubsetStrategy.Default), writableGeneratedColumnMap);
			WritableGeneratedColumnMap writableGeneratedColumnMap2 = table.ColumnMap.GenerateWithReplacements(dictionary3, false);
			return new global::System.ValueTuple<GeneratedTable, GeneratedTable>(new GeneratedTable(table.QueryTable.Project(list2, ProjectSubsetStrategy.Default), writableGeneratedColumnMap2), generatedTable);
		}
	}
}
