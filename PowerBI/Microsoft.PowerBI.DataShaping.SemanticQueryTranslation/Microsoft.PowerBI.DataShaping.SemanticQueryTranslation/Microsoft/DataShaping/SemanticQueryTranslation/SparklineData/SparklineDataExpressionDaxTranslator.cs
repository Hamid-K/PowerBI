using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.SparklineData
{
	// Token: 0x02000021 RID: 33
	internal static class SparklineDataExpressionDaxTranslator
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000055E0 File Offset: 0x000037E0
		internal static QueryExpression Translate(QueryExpression measureExpression, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, int pointsPerSparkline, QueryExpression scalarKeyExpression, bool includeMinGroupingInterval, IConceptualSchema schema)
		{
			QueryFieldExpression queryFieldExpression = ((columnToGroupingColumnsMapping.Item1 != null) ? SparklineDataExpressionDaxTranslator.GetFieldExpression(columnToGroupingColumnsMapping.Item1) : SparklineDataExpressionDaxTranslator.GetFieldExpression(fieldToGroupingFieldsMapping.Item1));
			bool flag;
			if (scalarKeyExpression == null)
			{
				ConceptualPrimitiveResultType conceptualPrimitiveResultType = queryFieldExpression.ConceptualResultType as ConceptualPrimitiveResultType;
				flag = conceptualPrimitiveResultType != null && conceptualPrimitiveResultType.ConceptualDataType.IsScalar();
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				return SparklineDataExpressionDaxTranslator.TranslateWithCategoricalGrouping(measureExpression, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, pointsPerSparkline, schema);
			}
			return SparklineDataExpressionDaxTranslator.TranslateWithScalarGrouping(measureExpression, queryFieldExpression, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, pointsPerSparkline, scalarKeyExpression, includeMinGroupingInterval, schema);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005650 File Offset: 0x00003850
		private static QueryExpression TranslateWithScalarGrouping(QueryExpression measureExpression, QueryExpression groupingExpression, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, int pointsPerSparkline, QueryExpression scalarKeyExpression, bool includeMinGroupingInterval, IConceptualSchema schema)
		{
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QueryTable queryTable = SparklineDataExpressionDaxTranslator.DistinctCategories(fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, schema).AsTableDefinition();
			QueryTable queryTable2 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable, "Categories");
			QueryExpression queryExpression = SparklineDataExpressionDaxTranslator.PrepareScalarKeyExpression(groupingExpression, scalarKeyExpression);
			QueryTableColumn queryTableColumn;
			QueryTableColumn queryTableColumn2;
			QueryExpression queryExpression2;
			QueryTable queryTable3 = SparklineDataExpressionDaxTranslator.CreateDataTable(queryTable2, measureExpression, queryExpression, out queryTableColumn, out queryTableColumn2, out queryExpression2);
			queryTable3 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable3, "Data");
			QueryExpression queryExpression3 = SparklineDataExpressionDaxTranslator.TranslateWithScalarGroupingResult(queryTable, queryTable3, groupingExpression, queryExpression2, queryExpression, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, queryTableColumn, queryTableColumn2, pointsPerSparkline, includeMinGroupingInterval);
			QueryFunctionExpression queryFunctionExpression = queryTable3.Expression.IsEmpty().If(Literals.NullString, queryExpression3);
			queryExpressionWithLocalVariablesBuilder.SetResult(queryFunctionExpression);
			return queryExpressionWithLocalVariablesBuilder.ToQueryExpression();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000056E0 File Offset: 0x000038E0
		private static QueryExpression TranslateWithCategoricalGrouping(QueryExpression measureExpression, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, int pointsPerSparkline, IConceptualSchema schema)
		{
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QueryTable queryTable = SparklineDataExpressionDaxTranslator.DistinctCategories(fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, schema).AsTableDefinition();
			QueryTableColumn queryTableColumn;
			QueryTableColumn queryTableColumn2;
			QueryExpression queryExpression;
			QueryTable queryTable2 = SparklineDataExpressionDaxTranslator.CreateDataTable(queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable, "Categories"), measureExpression, null, out queryTableColumn, out queryTableColumn2, out queryExpression);
			queryTable2 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable2, "Data");
			QueryExpression queryExpression2 = SparklineDataExpressionDaxTranslator.TranslateWithCategoricalGroupingResult(queryTable, queryTable2, queryExpression, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, queryTableColumn, pointsPerSparkline);
			QueryFunctionExpression queryFunctionExpression = queryTable2.Expression.IsEmpty().If(Literals.NullString, queryExpression2);
			queryExpressionWithLocalVariablesBuilder.SetResult(queryFunctionExpression);
			return queryExpressionWithLocalVariablesBuilder.ToQueryExpression();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000575C File Offset: 0x0000395C
		private static QueryExpression TranslateWithScalarGroupingResult(QueryTable categoriesTable, QueryTable dataTable, QueryExpression groupingExpression, QueryExpression measureNotBlankPredicate, QueryExpression scalarKeyExpr, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, QueryTableColumn valueColumn, QueryTableColumn scalarKeyColumn, int pointsPerSparkline, bool includeMinGroupingInterval)
		{
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QueryTable queryTable = SparklineDataExpressionDaxTranslator.UnfilteredCategoriesWithDataForScalarGrouping(categoriesTable, measureNotBlankPredicate, groupingExpression, scalarKeyExpr);
			queryTable = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable, "All_Categories");
			QueryTable queryTable2 = SparklineDataExpressionDaxTranslator.SampleAllCategories(queryTable, "ScalarKey", pointsPerSparkline);
			queryTable2 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable2, "Sample_Categories");
			QueryExpression queryExpression = SparklineDataExpressionDaxTranslator.GetMinGroupingInterval(queryTable2, scalarKeyColumn, includeMinGroupingInterval);
			if (queryExpression != null)
			{
				queryExpression = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryExpression, "Min_Interval");
			}
			QueryTableColumn queryTableColumn;
			QueryTable queryTable3 = SparklineDataExpressionDaxTranslator.SyncSampledCategoriesWithDataTable(queryTable2, dataTable, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, true, out queryTableColumn);
			queryTable3 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable3, "Sync_Data");
			global::System.ValueTuple<QueryExpression, QueryExpression, QueryExpression> valueTuple = SparklineDataExpressionDaxTranslator.ExtractYMinMaxAndRange(queryExpressionWithLocalVariablesBuilder, queryTable3, valueColumn);
			global::System.ValueTuple<QueryExpression, QueryExpression> valueTuple2 = SparklineDataExpressionDaxTranslator.ExtractXMinAndMax(queryExpressionWithLocalVariablesBuilder, queryTable2, scalarKeyColumn, true);
			QueryTable queryTable4 = SparklineDataExpressionDaxTranslator.FilterOutBlanks(queryTable3, valueColumn);
			queryTable4 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable4, "Non_Blank_Sync_Data");
			return SparklineDataExpressionDaxTranslator.CreateFinalResult(queryExpressionWithLocalVariablesBuilder, queryTable3, queryTable4, valueColumn, scalarKeyColumn, valueTuple, valueTuple2, true, includeMinGroupingInterval, true, queryExpression);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005828 File Offset: 0x00003A28
		private static QueryExpression TranslateWithCategoricalGroupingResult(QueryTable categoriesTable, QueryTable dataTable, QueryExpression measureNotBlankPredicate, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, QueryTableColumn valueColumn, int pointsPerSparkline)
		{
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QueryTable queryTable = SparklineDataExpressionDaxTranslator.UnfilteredCategoriesWithDataForCategoricalGrouping(categoriesTable, measureNotBlankPredicate);
			QueryTable queryTable2 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable, "All_Categories");
			IConceptualColumn item = columnToGroupingColumnsMapping.Item1;
			QueryTable queryTable3 = SparklineDataExpressionDaxTranslator.SampleAllCategories(queryTable2, ((item != null) ? item.EdmName : null) ?? fieldToGroupingFieldsMapping.Item1.Field.Name, pointsPerSparkline);
			queryTable3 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable3, "Sample_Categories");
			QueryTableColumn queryTableColumn;
			QueryTable queryTable4 = SparklineDataExpressionDaxTranslator.SyncSampledCategoriesWithDataTable(queryTable3, dataTable, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, false, out queryTableColumn);
			queryTable4 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable4, "Sync_Data");
			global::System.ValueTuple<QueryExpression, QueryExpression, QueryExpression> valueTuple = SparklineDataExpressionDaxTranslator.ExtractYMinMaxAndRange(queryExpressionWithLocalVariablesBuilder, queryTable4, valueColumn);
			global::System.ValueTuple<QueryExpression, QueryExpression> valueTuple2 = SparklineDataExpressionDaxTranslator.ExtractXMinAndMax(queryExpressionWithLocalVariablesBuilder, queryTable3, null, false);
			QueryTable queryTable5 = SparklineDataExpressionDaxTranslator.FilterOutBlanks(queryTable4, valueColumn);
			queryTable5 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryTable5, "Non_Blank_Sync_Data");
			return SparklineDataExpressionDaxTranslator.CreateFinalResult(queryExpressionWithLocalVariablesBuilder, queryTable4, queryTable5, valueColumn, queryTableColumn, valueTuple, valueTuple2, false, false, false, null);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000058E7 File Offset: 0x00003AE7
		private static QueryFieldExpression GetFieldExpression(IEdmFieldInstance field)
		{
			return field.Entity.ScalarEntity(null).Field(field.Field.Name);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005905 File Offset: 0x00003B05
		private static QueryFieldExpression GetFieldExpression(IConceptualColumn column)
		{
			return column.Entity.ScalarEntity().Field(column.EdmName);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005920 File Offset: 0x00003B20
		private static QueryGroupByExpression DistinctCategories([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, IConceptualSchema schema)
		{
			List<IEdmFieldInstance> list = null;
			List<IConceptualColumn> list2 = null;
			if (columnToGroupingColumnsMapping.Item1 != null)
			{
				list2 = new List<IConceptualColumn>(columnToGroupingColumnsMapping.Item2.Count + 1);
				if (columnToGroupingColumnsMapping.Item2.IsNotEmpty<IConceptualColumn>())
				{
					list2.AddRange(columnToGroupingColumnsMapping.Item2);
				}
				list2.Add(columnToGroupingColumnsMapping.Item1);
			}
			else
			{
				list = new List<IEdmFieldInstance>(fieldToGroupingFieldsMapping.Item2.Count + 1);
				if (fieldToGroupingFieldsMapping.Item2.IsNotEmpty<IEdmFieldInstance>())
				{
					list.AddRange(fieldToGroupingFieldsMapping.Item2);
				}
				list.Add(fieldToGroupingFieldsMapping.Item1);
			}
			return list.QdmFilterGroupBy(schema, columnToGroupingColumnsMapping.Item1 != null, list2, ScanKind.InheritFilterContextIncludeBlankRow);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000059C0 File Offset: 0x00003BC0
		private static QueryTable UnfilteredCategoriesWithDataForScalarGrouping(QueryTable categoriesTable, QueryExpression measureNotBlankPredicate, QueryExpression groupingExpression, QueryExpression scalarKeyExpression)
		{
			QueryFunctionExpression queryFunctionExpression = groupingExpression.IsNull().Not();
			QueryTable queryTable = categoriesTable.Filter(measureNotBlankPredicate.And(queryFunctionExpression));
			QueryTableColumn queryTableColumn = scalarKeyExpression.ToQueryTableColumn("ScalarKey");
			return queryTable.AddProjection(queryTableColumn.ListWrap<QueryTableColumn>()).Calculate(QueryExpressionBuilder.AllSelected().AsList<QueryAllExpression>()).Project(queryTableColumn.ToReferenceColumn().ListWrap<QueryTableColumn>(), ProjectSubsetStrategy.Default);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A1D File Offset: 0x00003C1D
		private static QueryTable SampleAllCategories(QueryTable allCategoriesTable, string sortColumnName, int pointsPerSparkline)
		{
			return allCategoriesTable.Sample(QueryExpressionBuilder.Literal(pointsPerSparkline), sortColumnName, SortDirection.Ascending);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005A34 File Offset: 0x00003C34
		private static QueryExpression GetMinGroupingInterval(QueryTable sampleCategoriesTable, QueryTableColumn scalarKeyColumn, bool includeMinGroupingInterval)
		{
			if (!includeMinGroupingInterval)
			{
				return null;
			}
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QdmTableColumnReferenceExpression qdmTableColumnReferenceExpression = scalarKeyColumn.QdmReference();
			QueryExpressionBinding queryExpressionBinding = sampleCategoriesTable.ToBinding();
			QueryExpression queryExpression = qdmTableColumnReferenceExpression.Earlier().RewriteColumnReferences(sampleCategoriesTable.Columns, queryExpressionBinding.Variable);
			QueryComparisonExpression queryComparisonExpression = qdmTableColumnReferenceExpression.LessThan(queryExpression);
			QueryFunctionExpression queryFunctionExpression = sampleCategoriesTable.Filter(queryComparisonExpression).Project(qdmTableColumnReferenceExpression).Max();
			QueryVariableReferenceExpression queryVariableReferenceExpression = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryFunctionExpression, "Previous");
			QueryFunctionExpression queryFunctionExpression2 = qdmTableColumnReferenceExpression.Minus(queryVariableReferenceExpression);
			QueryFunctionExpression queryFunctionExpression3 = queryVariableReferenceExpression.IsNull().If(queryFunctionExpression2.ConceptualResultType.Null(), queryFunctionExpression2);
			queryExpressionWithLocalVariablesBuilder.SetResult(queryFunctionExpression3);
			QueryExpression queryExpression2 = queryExpressionWithLocalVariablesBuilder.ToQueryExpression().RewriteColumnReferences(sampleCategoriesTable.Columns, queryExpressionBinding.Variable);
			return queryExpressionBinding.Project(queryExpression2, ProjectSubsetStrategy.Default).Min();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005AF0 File Offset: 0x00003CF0
		private static QueryTable UnfilteredCategoriesWithDataForCategoricalGrouping(QueryTable categoriesTable, QueryExpression measureNotBlankPredicate)
		{
			return categoriesTable.Filter(measureNotBlankPredicate).Calculate(QueryExpressionBuilder.AllSelected().AsList<QueryAllExpression>());
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005B08 File Offset: 0x00003D08
		private static QueryExpression PrepareScalarKeyExpression(QueryExpression groupingExpression, QueryExpression scalarKeyExpression)
		{
			if (scalarKeyExpression != null)
			{
				return scalarKeyExpression.Calculate(Array.Empty<QueryExpression>());
			}
			return groupingExpression;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005B1C File Offset: 0x00003D1C
		private static QueryTable CreateDataTable(QueryTable categoriesVar, QueryExpression measureExpression, QueryExpression scalarKeyExpression, out QueryTableColumn valueColumn, out QueryTableColumn scalarKeyColumn, out QueryExpression measureNotBlankPredicate)
		{
			scalarKeyColumn = null;
			measureNotBlankPredicate = measureExpression.CalculateIfNeeded().IsNull().Not();
			QueryTable queryTable = categoriesVar.Filter(measureNotBlankPredicate);
			List<QueryTableColumn> list = new List<QueryTableColumn>(2);
			if (scalarKeyExpression != null)
			{
				scalarKeyColumn = scalarKeyExpression.ToQueryTableColumn("ScalarKey");
				list.Add(scalarKeyColumn);
			}
			valueColumn = measureExpression.CalculateIfNeeded().ToQueryTableColumn("Value");
			list.Add(valueColumn);
			return queryTable.AddProjection(list);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005B8A File Offset: 0x00003D8A
		private static QueryTable SyncSampledCategoriesWithDataTable(QueryTable sampleCategoriesTable, QueryTable dataTable, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, bool isScalarGrouping, out QueryTableColumn groupingIndexColumn)
		{
			sampleCategoriesTable = sampleCategoriesTable.BackfillTableColumnsIfNeeded();
			dataTable = dataTable.BackfillTableColumnsIfNeeded();
			if (isScalarGrouping)
			{
				groupingIndexColumn = null;
				return sampleCategoriesTable.NaturalInnerJoin(dataTable);
			}
			return SparklineDataExpressionDaxTranslator.GetSubstituteWithIndexForCategoricalGrouping(dataTable, sampleCategoriesTable, fieldToGroupingFieldsMapping, columnToGroupingColumnsMapping, out groupingIndexColumn);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005BB8 File Offset: 0x00003DB8
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "MinY", "MaxY", "YRange" })]
		private static global::System.ValueTuple<QueryExpression, QueryExpression, QueryExpression> ExtractYMinMaxAndRange(QueryExpressionWithLocalVariablesBuilder variableExpressionBuilder, QueryTable syncDataTable, QueryTableColumn valueColumn)
		{
			QueryTableColumn queryTableColumn = valueColumn.ToReferenceColumn();
			QueryFunctionExpression queryFunctionExpression = syncDataTable.Project(queryTableColumn).Min();
			QueryVariableReferenceExpression queryVariableReferenceExpression = variableExpressionBuilder.DeclareVariable(queryFunctionExpression, "MinY_Value");
			QueryFunctionExpression queryFunctionExpression2 = syncDataTable.Project(queryTableColumn).Max();
			QueryVariableReferenceExpression queryVariableReferenceExpression2 = variableExpressionBuilder.DeclareVariable(queryFunctionExpression2, "MaxY_Value");
			QueryFunctionExpression queryFunctionExpression3 = queryVariableReferenceExpression2.Minus(queryVariableReferenceExpression);
			QueryVariableReferenceExpression queryVariableReferenceExpression3 = variableExpressionBuilder.DeclareVariable(queryFunctionExpression3, "RangeY");
			return new global::System.ValueTuple<QueryExpression, QueryExpression, QueryExpression>(queryVariableReferenceExpression, queryVariableReferenceExpression2, queryVariableReferenceExpression3);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005C24 File Offset: 0x00003E24
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "MinX", "MaxX" })]
		private static global::System.ValueTuple<QueryExpression, QueryExpression> ExtractXMinAndMax(QueryExpressionWithLocalVariablesBuilder variableExpressionBuilder, QueryTable sampleCategoriesTable, QueryTableColumn scalarKeyColumn, bool isScalarGrouping)
		{
			if (isScalarGrouping)
			{
				QueryTableColumn queryTableColumn = scalarKeyColumn.ToReferenceColumn();
				QueryFunctionExpression queryFunctionExpression = sampleCategoriesTable.Project(queryTableColumn).Min();
				QueryExpression queryExpression = variableExpressionBuilder.DeclareVariable(queryFunctionExpression, "MinX_Value");
				QueryFunctionExpression queryFunctionExpression2 = sampleCategoriesTable.Project(queryTableColumn).Max();
				QueryVariableReferenceExpression queryVariableReferenceExpression = variableExpressionBuilder.DeclareVariable(queryFunctionExpression2, "MaxX_Value");
				return new global::System.ValueTuple<QueryExpression, QueryExpression>(queryExpression, queryVariableReferenceExpression);
			}
			QueryLiteralExpression zero = Literals.Zero;
			QueryExpression queryExpression2 = variableExpressionBuilder.DeclareVariable(zero, "MinX_Value");
			QueryFunctionExpression queryFunctionExpression3 = sampleCategoriesTable.Expression.CountRows().Minus(Literals.One);
			QueryVariableReferenceExpression queryVariableReferenceExpression2 = variableExpressionBuilder.DeclareVariable(queryFunctionExpression3, "MaxX_Value");
			return new global::System.ValueTuple<QueryExpression, QueryExpression>(queryExpression2, queryVariableReferenceExpression2);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005CB6 File Offset: 0x00003EB6
		private static QueryTable FilterOutBlanks(QueryTable syncDataTable, QueryTableColumn valueColumn)
		{
			return syncDataTable.Filter(valueColumn.QdmReference().IsNull().Not());
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005CD0 File Offset: 0x00003ED0
		private static QueryExpression CreateFinalResult(QueryExpressionWithLocalVariablesBuilder variableExpressionBuilder, QueryTable syncDataTable, QueryTable nonBlankSyncDataTable, QueryTableColumn valueColumn, QueryTableColumn formatAndSortColumn, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "MinY", "MaxY", "YRange" })] global::System.ValueTuple<QueryExpression, QueryExpression, QueryExpression> yMinMaxAndRange, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "MinX", "MaxX" })] global::System.ValueTuple<QueryExpression, QueryExpression> xMinMax, bool isScalarGrouping, bool includeMinGroupingInterval, bool formatXValuesAsNumber = false, QueryExpression minIntervalExpr = null)
		{
			QdmTableColumnReferenceExpression qdmTableColumnReferenceExpression = valueColumn.QdmReference();
			QdmTableColumnReferenceExpression qdmTableColumnReferenceExpression2 = formatAndSortColumn.QdmReference();
			QueryFormatExpression queryFormatExpression = qdmTableColumnReferenceExpression2.Format("General Number", "en-US");
			QueryFormatExpression queryFormatExpression2 = qdmTableColumnReferenceExpression.Minus(yMinMaxAndRange.Item1).Divide(yMinMaxAndRange.Item3, Literals.Zero).Multiply(QueryExpressionBuilder.Literal(100))
				.Format("0.0", "en-US");
			QueryConcatenateExpression queryConcatenateExpression = QueryExpressionBuilder.Literal(",").Concatenate(queryFormatExpression2);
			QueryFunctionExpression queryFunctionExpression = qdmTableColumnReferenceExpression.IsNull().If(Literals.EmptyString, queryConcatenateExpression);
			QueryConcatenateExpression queryConcatenateExpression2 = new List<QueryExpression>
			{
				QueryExpressionBuilder.Literal("["),
				queryFormatExpression,
				queryFunctionExpression,
				QueryExpressionBuilder.Literal("]")
			}.Concatenate();
			QueryConcatenateXExpression queryConcatenateXExpression = syncDataTable.ConcatenateX(queryConcatenateExpression2, QueryExpressionBuilder.Literal(","), qdmTableColumnReferenceExpression2.ToSortClause(SortDirection.Ascending));
			List<QueryExpression> list = new List<QueryExpression>
			{
				QueryExpressionBuilder.Literal("{\"p\":["),
				queryConcatenateXExpression,
				QueryExpressionBuilder.Literal("],\"yl\":"),
				yMinMaxAndRange.Item1.Format("General Number", "en-US"),
				QueryExpressionBuilder.Literal(",\"yh\":"),
				yMinMaxAndRange.Item2.Format("General Number", "en-US"),
				QueryExpressionBuilder.Literal(",\"xl\":"),
				formatXValuesAsNumber ? xMinMax.Item1.Format("General Number", "en-US") : xMinMax.Item1,
				QueryExpressionBuilder.Literal(",\"xh\":"),
				formatXValuesAsNumber ? xMinMax.Item2.Format("General Number", "en-US") : xMinMax.Item2
			};
			if (isScalarGrouping && includeMinGroupingInterval && minIntervalExpr != null)
			{
				QueryConcatenateExpression queryConcatenateExpression3 = QueryExpressionBuilder.Literal(",\"md\":").Concatenate(minIntervalExpr.Format("General Number", "en-US"));
				list.Add(minIntervalExpr.IsNull().If(Literals.EmptyString, queryConcatenateExpression3));
			}
			list.Add(QueryExpressionBuilder.Literal("}"));
			QueryConcatenateExpression queryConcatenateExpression4 = list.Concatenate();
			QueryVariableReferenceExpression queryVariableReferenceExpression = variableExpressionBuilder.DeclareVariable(queryConcatenateExpression4, "Result");
			QueryFunctionExpression queryFunctionExpression2 = nonBlankSyncDataTable.Expression.IsEmpty().If(Literals.NullString, queryVariableReferenceExpression);
			variableExpressionBuilder.SetResult(queryFunctionExpression2);
			return variableExpressionBuilder.ToQueryExpression();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005F6C File Offset: 0x0000416C
		private static QueryTable GetSubstituteWithIndexForCategoricalGrouping(QueryTable dataTable, QueryTable sampleCategoriesTable, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, out QueryTableColumn groupingIndexColumn)
		{
			QueryExpressionBinding queryExpressionBinding = sampleCategoriesTable.ToBinding();
			List<QuerySortClause> list = new List<QuerySortClause>();
			if (columnToGroupingColumnsMapping.Item1 != null)
			{
				foreach (IConceptualColumn conceptualColumn in columnToGroupingColumnsMapping.Item2)
				{
					QueryFieldExpression queryFieldExpression = queryExpressionBinding.Variable.Field(conceptualColumn.EdmName);
					list.Add(queryFieldExpression.ToSortClause(SortDirection.Ascending));
				}
				IConceptualColumn item = columnToGroupingColumnsMapping.Item1;
				QueryFieldExpression queryFieldExpression2 = queryExpressionBinding.Variable.Field(item.EdmName);
				list.Add(queryFieldExpression2.ToSortClause(SortDirection.Ascending));
				return dataTable.SubstituteWithIndex("GroupingIndex", sampleCategoriesTable, list, out groupingIndexColumn, false);
			}
			foreach (IEdmFieldInstance edmFieldInstance in fieldToGroupingFieldsMapping.Item2)
			{
				QueryFieldExpression queryFieldExpression3 = queryExpressionBinding.Variable.Field(edmFieldInstance.Field.Name);
				list.Add(queryFieldExpression3.ToSortClause(SortDirection.Ascending));
			}
			IEdmFieldInstance item2 = fieldToGroupingFieldsMapping.Item1;
			QueryFieldExpression queryFieldExpression4 = queryExpressionBinding.Variable.Field(item2.Field.Name);
			list.Add(queryFieldExpression4.ToSortClause(SortDirection.Ascending));
			return dataTable.SubstituteWithIndex("GroupingIndex", sampleCategoriesTable, list, out groupingIndexColumn, false);
		}

		// Token: 0x04000061 RID: 97
		private const string CategoriesVariableName = "Categories";

		// Token: 0x04000062 RID: 98
		private const string SampleCategoriesVariableName = "Sample_Categories";

		// Token: 0x04000063 RID: 99
		private const string AllCategoriesVariableName = "All_Categories";

		// Token: 0x04000064 RID: 100
		private const string DataVariableName = "Data";

		// Token: 0x04000065 RID: 101
		private const string SyncDataVariableName = "Sync_Data";

		// Token: 0x04000066 RID: 102
		private const string MaxXValueVariableName = "MaxX_Value";

		// Token: 0x04000067 RID: 103
		private const string MinXValueVariableName = "MinX_Value";

		// Token: 0x04000068 RID: 104
		private const string MaxYValueVariableName = "MaxY_Value";

		// Token: 0x04000069 RID: 105
		private const string MinYValueVariableName = "MinY_Value";

		// Token: 0x0400006A RID: 106
		private const string MinIntervalVariableName = "Min_Interval";

		// Token: 0x0400006B RID: 107
		private const string RangeYVariableName = "RangeY";

		// Token: 0x0400006C RID: 108
		private const string NonBlankSyncDataVariableName = "Non_Blank_Sync_Data";

		// Token: 0x0400006D RID: 109
		private const string ResultVariableName = "Result";

		// Token: 0x0400006E RID: 110
		private const string PreviousVariableName = "Previous";

		// Token: 0x0400006F RID: 111
		private const string USEnglishLocale = "en-US";

		// Token: 0x04000070 RID: 112
		private const string GeneralNumberFormatString = "General Number";

		// Token: 0x04000071 RID: 113
		private const string DoubleZeroFormatString = "0.0";

		// Token: 0x04000072 RID: 114
		private const string ScalarKeyFieldName = "ScalarKey";

		// Token: 0x04000073 RID: 115
		private const string ValueFieldName = "Value";

		// Token: 0x04000074 RID: 116
		private const string GroupingIndexFieldName = "GroupingIndex";

		// Token: 0x04000075 RID: 117
		private const string SampleTableBindingVariableName = "SampleTable";
	}
}
