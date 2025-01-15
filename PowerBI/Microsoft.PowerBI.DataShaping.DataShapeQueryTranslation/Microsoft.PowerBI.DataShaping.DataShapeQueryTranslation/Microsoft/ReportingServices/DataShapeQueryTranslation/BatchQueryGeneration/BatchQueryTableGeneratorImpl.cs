using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200014A RID: 330
	internal sealed class BatchQueryTableGeneratorImpl : IPlanOperationVisitor<GeneratedTable>
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00031508 File Offset: 0x0002F708
		internal BatchQueryTableGeneratorImpl(BatchQueryGenerationContext context, IQueryExpressionGenerator expressionGenerator, GeneratedDeclarationCollection declarations, BatchQueryExpressionReferenceContext referenceContext, IList<string> reservedColumnNames, BatchQueryGenerationNamingContext sharedNamingContext)
		{
			this.m_context = context;
			this.m_expressionGenerator = expressionGenerator;
			this.m_declarations = declarations;
			this.m_referenceContext = referenceContext;
			this.m_reservedColumnNames = reservedColumnNames;
			this.m_namingContextManager = new BatchQueryGenerationNamingContextManager();
			if (sharedNamingContext != null)
			{
				this.m_namingContextManager.SetActiveNamingContext(sharedNamingContext);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003155C File Offset: 0x0002F75C
		internal bool UseConceptualSchema
		{
			get
			{
				return this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00031570 File Offset: 0x0002F770
		public static GeneratedTable Generate(BatchQueryGenerationContext context, IQueryExpressionGenerator expressionGenerator, GeneratedDeclarationCollection declarations, BatchQueryExpressionReferenceContext referenceContext, IList<string> reservedColumnNames, PlanOperation table, BatchQueryGenerationNamingContext sharedNamingContext)
		{
			BatchQueryTableGeneratorImpl batchQueryTableGeneratorImpl = new BatchQueryTableGeneratorImpl(context, expressionGenerator, declarations, referenceContext, reservedColumnNames, sharedNamingContext);
			return table.Accept<GeneratedTable>(batchQueryTableGeneratorImpl);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00031593 File Offset: 0x0002F793
		public static IReadOnlyList<GeneratedTable> GenerateTables(BatchQueryGenerationContext context, IQueryExpressionGenerator expressionGenerator, GeneratedDeclarationCollection declarations, BatchQueryExpressionReferenceContext referenceContext, IList<string> reservedColumnNames, PlanOperation table, BatchQueryGenerationNamingContext sharedNamingContext)
		{
			return new BatchQueryTableGeneratorImpl(context, expressionGenerator, declarations, referenceContext, reservedColumnNames, sharedNamingContext).GenerateTables(table, context.Model);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000315B0 File Offset: 0x0002F7B0
		private IReadOnlyList<GeneratedTable> GenerateTables(PlanOperation operation, FederatedEntityDataModel model)
		{
			PlanOperationCreateFilterContextTable planOperationCreateFilterContextTable = operation as PlanOperationCreateFilterContextTable;
			if (planOperationCreateFilterContextTable != null)
			{
				return this.GenerateFilterContextTables(planOperationCreateFilterContextTable);
			}
			PlanOperationClearDefaultContext planOperationClearDefaultContext = operation as PlanOperationClearDefaultContext;
			if (planOperationClearDefaultContext == null)
			{
				PlanOperationDeclarationReference planOperationDeclarationReference = operation as PlanOperationDeclarationReference;
				if (planOperationDeclarationReference != null)
				{
					if (planOperationDeclarationReference.CanExpandToMultiTables)
					{
						return this.GenerateMultiTableDeclarations(planOperationDeclarationReference);
					}
				}
				return new List<GeneratedTable> { operation.Accept<GeneratedTable>(this) };
			}
			return BatchQueryGenerationUtils.GenerateFilterContextTables(planOperationClearDefaultContext.DefaultContextManager, model, this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00031628 File Offset: 0x0002F828
		public GeneratedTable Visit(PlanOperationAddMissingItems operation)
		{
			GeneratedTable generatedTable = operation.Table.Accept<GeneratedTable>(this);
			return BatchAddMissingItemsTableGenerator.Generate(operation, generatedTable, this.m_context, this.m_declarations, this.m_expressionGenerator);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003165B File Offset: 0x0002F85B
		public GeneratedTable Visit(PlanOperationAddMissingItemsCompatPattern operation)
		{
			return BatchAddMissingItemsCompatPatternTableGenerator.Generate(operation, this.m_context, this.m_declarations, this.m_expressionGenerator);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00031678 File Offset: 0x0002F878
		public GeneratedTable Visit(PlanOperationGroupAndJoin join)
		{
			BatchQueryConstraintTelemetry batchQueryConstraintTelemetry;
			GeneratedTable generatedTable = BatchGroupAndJoinQueryTableGenerator.Generate(join, this.m_context, this.m_declarations, this.m_expressionGenerator, this.m_reservedColumnNames, out batchQueryConstraintTelemetry);
			if (join.ShouldBeConsideredForTelemetry && batchQueryConstraintTelemetry != null)
			{
				this.m_context.Telemetry.RegisterBatchQueryConstraint(batchQueryConstraintTelemetry, join.TelemetryName);
			}
			return generatedTable;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public GeneratedTable Visit(PlanOperationDataTransform operation)
		{
			GeneratedTable generatedTable = operation.Input.Accept<GeneratedTable>(this);
			return BatchQueryDataTransformTableGenerator.Generate(operation, generatedTable, this.m_context, this.m_expressionGenerator);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000316F8 File Offset: 0x0002F8F8
		public GeneratedTable Visit(PlanOperationSortBy planSort)
		{
			GeneratedTable generatedTable = planSort.Input.Accept<GeneratedTable>(this);
			IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> readOnlyList = BatchQuerySortItemGenerator.Generate(generatedTable, planSort.Sorts, this.m_context.ExpressionTable, this.m_context.Model, this.UseConceptualSchema, false);
			return new GeneratedTable(generatedTable.QueryTable.Sort(readOnlyList), generatedTable.ColumnMap);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00031754 File Offset: 0x0002F954
		public GeneratedTable Visit(PlanOperationTopN planTopN)
		{
			GeneratedTable generatedTable = planTopN.Input.Accept<GeneratedTable>(this);
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(planTopN.RowCount.Expression, planTopN.RowCount.Context);
			IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> readOnlyList = BatchQuerySortItemGenerator.Generate(generatedTable, planTopN.Sorts, this.m_context.ExpressionTable, this.m_context.Model, this.UseConceptualSchema, planTopN.ReverseSortOrder);
			return new GeneratedTable(generatedTable.QueryTable.TopN(queryExpressionContext.QueryExpression, readOnlyList), generatedTable.ColumnMap);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x000317DC File Offset: 0x0002F9DC
		public GeneratedTable Visit(PlanOperationTopNSkip topNSkip)
		{
			GeneratedTable generatedTable = topNSkip.Input.Accept<GeneratedTable>(this);
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(topNSkip.RowCount.Expression, topNSkip.RowCount.Context);
			QueryExpressionContext queryExpressionContext2 = this.m_expressionGenerator.TranslateExpression(topNSkip.SkipCount.Expression, topNSkip.SkipCount.Context);
			IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> readOnlyList = BatchQuerySortItemGenerator.Generate(generatedTable, topNSkip.Sorts, this.m_context.ExpressionTable, this.m_context.Model, this.UseConceptualSchema, topNSkip.ReverseSortOrder);
			return new GeneratedTable(generatedTable.QueryTable.TopNSkip(queryExpressionContext.QueryExpression, queryExpressionContext2.QueryExpression, readOnlyList), generatedTable.ColumnMap);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003188C File Offset: 0x0002FA8C
		public GeneratedTable Visit(PlanOperationSample planSample)
		{
			GeneratedTable generatedTable = planSample.Input.Accept<GeneratedTable>(this);
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(planSample.RowCount.Expression, planSample.RowCount.Context);
			IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> readOnlyList = BatchQuerySortItemGenerator.Generate(generatedTable, planSample.Sorts, this.m_context.ExpressionTable, this.m_context.Model, this.UseConceptualSchema, false);
			return new GeneratedTable(generatedTable.QueryTable.Sample(queryExpressionContext.QueryExpression, readOnlyList), generatedTable.ColumnMap);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00031910 File Offset: 0x0002FB10
		public GeneratedTable Visit(PlanOperationApplyStartPosition startAt)
		{
			GeneratedTable generatedTable = startAt.Input.Accept<GeneratedTable>(this);
			QueryItemCollection<QueryIsOnOrAfterArgument> queryItemCollection = new QueryItemCollection<QueryIsOnOrAfterArgument>();
			foreach (DataMember dataMember in startAt.Members)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_context.Annotations.TryGetBatchSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation))
				{
					if (batchSubtotalAnnotation.Usage.IsIncludeInOutput())
					{
						QueryTableColumn queryTableColumn = generatedTable.ColumnMap[batchSubtotalAnnotation.SubtotalIndicatorColumnName];
						QueryIsOnOrAfterArgument queryIsOnOrAfterArgument = this.CreateIsOnOrAfterArgument(dataMember, queryTableColumn.QdmReference(), dataMember.SubtotalStartPosition.Value, batchSubtotalAnnotation.SortDirection);
						queryItemCollection.Add(queryTableColumn.Name, queryIsOnOrAfterArgument);
					}
				}
				else
				{
					List<SortKey> sortKeys = dataMember.Group.SortKeys;
					List<ScopeValue> values = dataMember.Group.StartPosition.Values;
					Microsoft.DataShaping.Contract.RetailAssert(sortKeys != null, "Missing SortKeys collection on group.");
					for (int i = 0; i < sortKeys.Count; i++)
					{
						SortKey sortKey = sortKeys[i];
						ScopeValue scopeValue = values[i];
						QueryTableColumn queryTableColumn2 = generatedTable.ColumnMap[sortKey.Value.ExpressionId.Value];
						QueryIsOnOrAfterArgument queryIsOnOrAfterArgument2 = this.CreateIsOnOrAfterArgument(dataMember, queryTableColumn2.QdmReference(), scopeValue.Value.Value, sortKey.SortDirection.Value);
						queryItemCollection.Add(queryTableColumn2.Name, queryIsOnOrAfterArgument2);
					}
				}
			}
			RestartMatchingBehavior? restartMatchingBehavior = startAt.RestartMatchingBehavior;
			RestartMatchingBehavior restartMatchingBehavior2 = RestartMatchingBehavior.IsAfter;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if ((restartMatchingBehavior.GetValueOrDefault() == restartMatchingBehavior2) & (restartMatchingBehavior != null))
			{
				queryExpression = QueryExpressionBuilder.IsAfter(queryItemCollection);
			}
			else
			{
				queryExpression = QueryExpressionBuilder.IsOnOrAfter(queryItemCollection);
			}
			return new GeneratedTable(generatedTable.QueryTable.Filter(queryExpression), generatedTable.ColumnMap);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00031AF4 File Offset: 0x0002FCF4
		private QueryIsOnOrAfterArgument CreateIsOnOrAfterArgument(DataMember member, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, ScalarValue value, Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection sortDirection)
		{
			this.m_expressionGenerator.ValidateLiteralType(expression, value, new ExpressionContext(this.m_context.ErrorContext, ObjectType.Group, member.Id, "StartPosition"), EngineMessageSeverity.Warning);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = QueryExpressionBuilder.ToExpressionFromDeclaredType(value, expression.ConceptualResultType);
			return new QueryIsOnOrAfterArgument(expression, queryExpression, sortDirection.ToQdmSortDirection());
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00031B48 File Offset: 0x0002FD48
		public GeneratedTable Visit(PlanOperationDeclarationReference reference)
		{
			if (reference.CanExpandToMultiTables)
			{
				Microsoft.DataShaping.Contract.RetailFail("Unexpected use of PlanOperationDeclarationReference");
				throw new NotSupportedException();
			}
			return this.m_declarations.GetSingleTableDeclaration(reference.DeclarationName).Table;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00031B78 File Offset: 0x0002FD78
		public GeneratedTable Visit(PlanOperationTableScan tableScan)
		{
			WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
			EntitySet entitySet;
			IConceptualEntity conceptualEntity;
			if (tableScan.TargetEntity != null)
			{
				if (this.UseConceptualSchema)
				{
					entitySet = null;
					conceptualEntity = tableScan.TargetEntity;
				}
				else
				{
					entitySet = this.m_context.Model.BaseModel.EntitySets.FindByEdmReferenceName(tableScan.TargetEntity.Name);
					conceptualEntity = null;
				}
			}
			else
			{
				if (tableScan.TargetEntityPlanName == null)
				{
					Microsoft.DataShaping.Contract.RetailFail("PlanOperationTableScan must have either a TargetEntity or a TargetEntityPlanName");
					throw new NotSupportedException();
				}
				entitySet = null;
				GeneratedEntityDeclaration entityDeclaration = this.m_declarations.GetEntityDeclaration(tableScan.TargetEntityPlanName);
				conceptualEntity = entityDeclaration.Entity;
				writableGeneratedColumnMap.AddColumnMap(entityDeclaration.ColumnMap, null);
			}
			List<QueryTableColumn> list = new List<QueryTableColumn>(tableScan.ExpectedProjections.Count);
			foreach (ExpressionId expressionId in tableScan.ExpectedProjections)
			{
				this.AddProjectionExpressionToColumnMap(expressionId, writableGeneratedColumnMap, list);
			}
			return new GeneratedTable(entitySet.TableScan(list, conceptualEntity), writableGeneratedColumnMap);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00031C80 File Offset: 0x0002FE80
		private void AddProjectionExpressionToColumnMap(ExpressionId expressionId, WritableGeneratedColumnMap expressionToColumnMap, List<QueryTableColumn> columns)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = this.m_context.ExpressionTable.GetNode(expressionId) as ResolvedPropertyExpressionNode;
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.DataMember, new Identifier(expressionId.ToString()), "Value");
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(expressionId, expressionContext);
			QueryTableColumn queryTableColumn = new QueryTableColumn(resolvedPropertyExpressionNode.Property.EdmName, queryExpressionContext.QueryExpression);
			expressionToColumnMap.Add(expressionId, queryTableColumn);
			columns.Add(queryTableColumn);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00031D00 File Offset: 0x0002FF00
		public GeneratedTable Visit(PlanOperationSingleRow operation)
		{
			WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
			BatchQueryGenerationNamingContext orCreateNamingContext = this.m_namingContextManager.GetOrCreateNamingContext();
			FederatedEntityDataModel model = this.m_context.Model;
			SingleRowTableBuilder singleRowTableBuilder = new SingleRowTableBuilder((model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.FeatureSwitchProvider, this.m_declarations.ExpressionReferenceNameToTableMapping, orCreateNamingContext.RegisteredNames);
			foreach (Calculation calculation in operation.Calculations)
			{
				foreach (KeyValuePair<ExpressionId, QueryExpressionContext> keyValuePair in this.m_expressionGenerator.TranslateCalculation(calculation))
				{
					QueryExpressionContext value = keyValuePair.Value;
					QueryTableColumn queryTableColumn = singleRowTableBuilder.AddOrReuseColumn(value.QueryExpression, calculation.Id.Value);
					writableGeneratedColumnMap.Add(keyValuePair.Key, queryTableColumn);
					orCreateNamingContext.RegisterName(queryTableColumn.Name);
				}
			}
			IEnumerable<global::System.ValueTuple<QueryTable, bool>> enumerable = BatchQueryGenerationUtils.GenerateContextTables(operation.ContextTables, this.m_declarations, this.m_context, this.m_expressionGenerator, this.m_referenceContext);
			singleRowTableBuilder.AddContextTables(enumerable);
			foreach (ExistsFilterItem existsFilterItem in operation.ExistsFilters)
			{
				IQueryExpressionGenerator expressionGenerator = this.m_expressionGenerator;
				FederatedEntityDataModel model2 = this.m_context.Model;
				QueryExistsFilter queryExistsFilter = BatchQueryGenerationUtils.TranslateExistsFilter(existsFilterItem, expressionGenerator, (model2 != null) ? model2.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
				singleRowTableBuilder.AddExistsFilter(queryExistsFilter);
			}
			foreach (SingleRowAdditionalColumn singleRowAdditionalColumn in operation.AdditionalColumns)
			{
				QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(singleRowAdditionalColumn.ExpressionId, singleRowAdditionalColumn.ExpressionContext);
				QueryTableColumn queryTableColumn2 = singleRowTableBuilder.AddOrReuseColumn(queryExpressionContext.QueryExpression, singleRowAdditionalColumn.PlanName);
				orCreateNamingContext.RegisterName(queryTableColumn2.Name);
				writableGeneratedColumnMap.Add(singleRowAdditionalColumn.PlanName, queryTableColumn2);
				writableGeneratedColumnMap.Add(singleRowAdditionalColumn.ExpressionId, queryTableColumn2);
			}
			return new GeneratedTable(singleRowTableBuilder.ToQueryTable(), writableGeneratedColumnMap);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00031F88 File Offset: 0x00030188
		private IReadOnlyList<GeneratedTable> GenerateMultiTableDeclarations(PlanOperationDeclarationReference operation)
		{
			return (from g in this.m_declarations.GetMultiTableDeclarations(operation.DeclarationName)
				select g.Table).ToReadOnlyList<GeneratedTable>();
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00031FC4 File Offset: 0x000301C4
		private IReadOnlyList<GeneratedTable> GenerateFilterContextTables(PlanOperationCreateFilterContextTable filter)
		{
			Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filterCondition = QueryFilterGenerator.CreateFilter(filter.Condition, this.m_expressionGenerator, this.m_context.ErrorContext, this.m_context.DataShape.Id, this.m_context.CancellationToken);
			IEnumerable<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition> enumerable = new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition[] { filterCondition };
			FederatedEntityDataModel model = this.m_context.Model;
			IConceptualModel conceptualModel = ((model != null) ? model.BaseModel : null);
			IConceptualSchema defaultSchema = this.m_context.Schema.GetDefaultSchema();
			DaxCapabilities daxCapabilities = this.m_context.DaxCapabilities;
			IFeatureSwitchProvider featureSwitchProvider = this.m_context.FeatureSwitchProvider;
			FederatedEntityDataModel model2 = this.m_context.Model;
			return BatchQueryGenerationUtils.GenerateFilterContextTables(enumerable.QdmFilters(conceptualModel, defaultSchema, daxCapabilities, featureSwitchProvider, EntityDataModelExtensions.GetComparer((model2 != null) ? model2.BaseModel : null, this.m_context.Schema, this.m_context.FeatureSwitchProvider), this.m_context.CancellationToken, ScanKind.InheritFilterContextIncludeBlankRow, true));
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0003209C File Offset: 0x0003029C
		public GeneratedTable Visit(PlanOperationFilterBy operation)
		{
			GeneratedTable generatedTable = operation.Input.Accept<GeneratedTable>(this);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (operation.Condition != null)
			{
				queryExpression = QueryFilterGenerator.CreateFilter(operation.Condition, this.m_expressionGenerator, this.m_context.ErrorContext, generatedTable.QueryTable.BindingVariableNameSuggestion, this.m_context.CancellationToken).ToPredicate();
			}
			else
			{
				PlanExpression predicate = operation.Predicate;
				queryExpression = this.m_expressionGenerator.TranslateExpression(predicate.Expression, predicate.Context).QueryExpression;
			}
			this.m_referenceContext.PopReferenceTable();
			return new GeneratedTable(generatedTable.QueryTable.Filter(queryExpression), generatedTable.ColumnMap);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00032150 File Offset: 0x00030350
		public GeneratedTable Visit(PlanOperationDistinctRows operation)
		{
			GeneratedTable generatedTable = operation.Input.Accept<GeneratedTable>(this);
			return new GeneratedTable(generatedTable.QueryTable.Distinct(), generatedTable.ColumnMap);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00032180 File Offset: 0x00030380
		public GeneratedTable Visit(PlanOperationGroupBy operation)
		{
			GeneratedTable generatedTable = operation.Input.Accept<GeneratedTable>(this);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			IEnumerable<QueryTableColumn> enumerable = BatchQueryGroupItemGenerator.Generate(this.m_context.Annotations, this.m_context.CalculationExpressionMapping, generatedTable, operation.Groups);
			BatchQueryAggregateItemGenerationResult batchQueryAggregateItemGenerationResult = BatchQueryAggregateItemGenerator.Generate(this.m_expressionGenerator, enumerable, operation.Aggregates, this.m_namingContextManager.GetOrCreateNamingContext());
			this.m_referenceContext.PopReferenceTable();
			QueryTableDefinition queryTableDefinition = generatedTable.QueryTable.GroupBy(enumerable, batchQueryAggregateItemGenerationResult.Columns);
			WritableGeneratedColumnMap writableGeneratedColumnMap = generatedTable.ColumnMap.UnionColumns(batchQueryAggregateItemGenerationResult.ColumnMap, queryTableDefinition.Columns);
			return new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00032228 File Offset: 0x00030428
		public GeneratedTable Visit(PlanOperationSubstituteWithIndex operation)
		{
			GeneratedTable generatedTable = operation.Table.Accept<GeneratedTable>(this);
			GeneratedTable generatedTable2 = operation.IndexTable.Accept<GeneratedTable>(this);
			return IndexInjectionTableGenerator.Generate(generatedTable, generatedTable2, this.m_context.ExpressionTable, operation.IndexTableSorts, this.m_namingContextManager.GetOrCreateNamingContext(), this.m_context.Model, this.m_context.Schema, this.m_context.FeatureSwitchProvider, false, operation.IndexColumnName, null);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0003229C File Offset: 0x0003049C
		public GeneratedTable Visit(PlanOperationAddJoinIndex operation)
		{
			GeneratedTable generatedTable = operation.Table.Accept<GeneratedTable>(this);
			GeneratedTable generatedTable2 = operation.IndexTable.Accept<GeneratedTable>(this);
			return IndexInjectionTableGenerator.Generate(generatedTable, generatedTable2, this.m_context.ExpressionTable, operation.IndexTableSorts, this.m_namingContextManager.GetOrCreateNamingContext(), this.m_context.Model, this.m_context.Schema, this.m_context.FeatureSwitchProvider, true, null, operation.IndexColumnCalculation);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00032310 File Offset: 0x00030510
		public GeneratedTable Visit(PlanOperationCalculateTableInFilterContext operation)
		{
			GeneratedTable generatedTable = operation.Input.Accept<GeneratedTable>(this);
			IEnumerable<global::System.ValueTuple<QueryTable, bool>> enumerable = BatchQueryGenerationUtils.GenerateContextTables(operation.Filters, this.m_declarations, this.m_context, this.m_expressionGenerator, new BatchQueryExpressionReferenceContext());
			return new GeneratedTable(generatedTable.QueryTable.Calculate(enumerable.Select(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryTable", "ShouldCrossFilterGroupColumns" })] global::System.ValueTuple<QueryTable, bool> c) => c.Item1.Expression)), generatedTable.ColumnMap);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00032388 File Offset: 0x00030588
		public GeneratedTable Visit(PlanOperationClearDefaultContext operation)
		{
			Microsoft.DataShaping.Contract.RetailFail("Unexpected use of PlanOperationClearDefaultContext");
			throw new NotSupportedException();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00032399 File Offset: 0x00030599
		public GeneratedTable Visit(PlanOperationCreateFilterContextTable operation)
		{
			Microsoft.DataShaping.Contract.RetailFail("Unexpected use of PlanOperationCreateFilterContextTable");
			throw new NotSupportedException();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000323AA File Offset: 0x000305AA
		public GeneratedTable Visit(PlanOperationCrossJoin operation)
		{
			IReadOnlyList<PlanOperation> tables = operation.Tables;
			bool flag = false;
			Func<IList<QueryTable>, QueryTableDefinition> func;
			if ((func = BatchQueryTableGeneratorImpl.<>O.<0>__CrossJoin) == null)
			{
				func = (BatchQueryTableGeneratorImpl.<>O.<0>__CrossJoin = new Func<IList<QueryTable>, QueryTableDefinition>(QueryTableBuilder.CrossJoin));
			}
			return this.GenerateCrossJoinTable(tables, flag, func);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000323D4 File Offset: 0x000305D4
		public GeneratedTable Visit(PlanOperationFullOuterCrossJoin operation)
		{
			IReadOnlyList<PlanOperation> tables = operation.Tables;
			bool flag = true;
			Func<IList<QueryTable>, QueryTableDefinition> func;
			if ((func = BatchQueryTableGeneratorImpl.<>O.<1>__GenerateAll) == null)
			{
				func = (BatchQueryTableGeneratorImpl.<>O.<1>__GenerateAll = new Func<IList<QueryTable>, QueryTableDefinition>(QueryTableBuilder.GenerateAll));
			}
			return this.GenerateCrossJoinTable(tables, flag, func);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00032400 File Offset: 0x00030600
		private GeneratedTable GenerateCrossJoinTable(IReadOnlyList<PlanOperation> inputTables, bool ensureFirstTableIsNotEmpty, Func<IList<QueryTable>, QueryTableDefinition> crossJoinFunction)
		{
			bool hasActiveNamingContext = this.m_namingContextManager.HasActiveNamingContext;
			if (!hasActiveNamingContext)
			{
				this.m_namingContextManager.SetActiveNamingContext(new BatchQueryGenerationNamingContext());
			}
			List<GeneratedTable> list = new List<GeneratedTable>(inputTables.Count);
			List<QueryTable> list2 = new List<QueryTable>(inputTables.Count);
			foreach (PlanOperation planOperation in inputTables)
			{
				GeneratedTable generatedTable = planOperation.Accept<GeneratedTable>(this);
				list.Add(generatedTable);
				list2.Add(generatedTable.QueryTable);
			}
			if (ensureFirstTableIsNotEmpty)
			{
				this.EnsureFirstTableIsNotEmpty(list, list2);
			}
			QueryTableDefinition queryTableDefinition = crossJoinFunction(list2);
			WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
			foreach (GeneratedTable generatedTable2 in list)
			{
				writableGeneratedColumnMap.AddColumnMap(generatedTable2.ColumnMap, null);
			}
			if (!hasActiveNamingContext)
			{
				this.m_namingContextManager.ResetActiveNamingContext();
			}
			return new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00032510 File Offset: 0x00030710
		private void EnsureFirstTableIsNotEmpty(IList<GeneratedTable> generatedTables, IList<QueryTable> queryTables)
		{
			int num = -1;
			for (int i = 0; i < queryTables.Count; i++)
			{
				if (QueryTableUtils.IsNonEmptyTable(queryTables[i].Expression))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				string text = this.m_namingContextManager.GetOrCreateNamingContext().CreateAndRegisterUniqueName("Placeholder");
				QueryTableDefinition queryTableDefinition = QueryTableBuilder.Row(new QueryTableColumn(text, Literals.Null), Array.Empty<QueryTableColumn>());
				queryTables.Insert(0, queryTableDefinition);
				WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
				writableGeneratedColumnMap.Add(text, queryTableDefinition.Columns[0]);
				generatedTables.Add(new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap));
				return;
			}
			QueryTable queryTable = queryTables[num];
			queryTables[num] = queryTables[0];
			queryTables[0] = queryTable;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000325CC File Offset: 0x000307CC
		public GeneratedTable Visit(PlanOperationInnerJoin operation)
		{
			GeneratedTable generatedTable = operation.Left.Accept<GeneratedTable>(this);
			GeneratedTable generatedTable2 = operation.Right.Accept<GeneratedTable>(this);
			QueryTableDefinition queryTableDefinition = generatedTable.QueryTable.NaturalInnerJoin(generatedTable2.QueryTable);
			WritableGeneratedColumnMap writableGeneratedColumnMap = generatedTable.ColumnMap.UnionColumns(generatedTable2.ColumnMap, queryTableDefinition.Columns);
			return new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00032624 File Offset: 0x00030824
		public GeneratedTable Visit(PlanOperationLeftOuterJoin operation)
		{
			GeneratedTable generatedTable = operation.Left.Accept<GeneratedTable>(this);
			GeneratedTable generatedTable2 = operation.Right.Accept<GeneratedTable>(this);
			if (generatedTable.QueryTable.Columns.SetEquals(generatedTable2.QueryTable.Columns))
			{
				WritableGeneratedColumnMap writableGeneratedColumnMap = generatedTable.ColumnMap.UnionColumns(generatedTable2.ColumnMap, generatedTable.QueryTable.Columns);
				return new GeneratedTable(generatedTable.QueryTable, writableGeneratedColumnMap);
			}
			QueryTableDefinition queryTableDefinition = generatedTable.QueryTable.NaturalLeftOuterJoin(generatedTable2.QueryTable);
			WritableGeneratedColumnMap writableGeneratedColumnMap2 = generatedTable.ColumnMap.UnionColumns(generatedTable2.ColumnMap, queryTableDefinition.Columns);
			return new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap2);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000326C8 File Offset: 0x000308C8
		public GeneratedTable Visit(PlanOperationProject operation)
		{
			GeneratedTable generatedTable = operation.Input.Accept<GeneratedTable>(this);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			BatchQueryGenerationNamingContext orCreateNamingContext = this.m_namingContextManager.GetOrCreateNamingContext();
			BatchQueryProjectItemGenerator.BatchQueryProjectItemGenerationResult batchQueryProjectItemGenerationResult = BatchQueryProjectItemGenerator.Generate(this.m_expressionGenerator, generatedTable, operation.Projections, orCreateNamingContext, this.m_context.CalculationExpressionMapping, this.m_context.Annotations);
			this.m_referenceContext.PopReferenceTable();
			if (batchQueryProjectItemGenerationResult.ProjectedColumns.SetEquals(generatedTable.QueryTable.Columns) && (!operation.EnforceColumnOrder || batchQueryProjectItemGenerationResult.ProjectedColumns.SequenceEqual(generatedTable.QueryTable.Columns)))
			{
				return new GeneratedTable(generatedTable.QueryTable, batchQueryProjectItemGenerationResult.OutputColumnMap);
			}
			return new GeneratedTable(generatedTable.QueryTable.Project(batchQueryProjectItemGenerationResult.ProjectedColumns, ProjectSubsetStrategy.Default), batchQueryProjectItemGenerationResult.OutputColumnMap);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00032799 File Offset: 0x00030999
		public GeneratedTable Visit(PlanOperationEnsureUniqueUnqualifiedNames operation)
		{
			return EnsureUniqueUnqualifiedNamesGenerator.Generate(operation.Input.Accept<GeneratedTable>(this), operation.ForceRename);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000327B4 File Offset: 0x000309B4
		public GeneratedTable Visit(PlanOperationUnion operation)
		{
			IList<GeneratedTable> list = operation.Tables.Select((PlanOperation table) => table.Accept<GeneratedTable>(this)).Evaluate<GeneratedTable>();
			IEnumerable<QueryTable> enumerable = list.Select((GeneratedTable table) => table.QueryTable);
			QueryTableDefinition queryTableDefinition = QueryTableBuilder.UnionAll(enumerable);
			enumerable.Select((QueryTable table) => table.Columns);
			GeneratedColumnMap generatedColumnMap = null;
			foreach (GeneratedTable generatedTable in list)
			{
				if (generatedColumnMap == null)
				{
					generatedColumnMap = generatedTable.ColumnMap;
				}
				else
				{
					generatedColumnMap = generatedColumnMap.UnionColumns(generatedTable.ColumnMap, queryTableDefinition.Columns);
				}
			}
			return new GeneratedTable(queryTableDefinition, generatedColumnMap);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0003288C File Offset: 0x00030A8C
		public GeneratedTable Visit(PlanOperationBinnedLineSample planSample)
		{
			GeneratedTable generatedTable = planSample.Input.Accept<GeneratedTable>(this);
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(planSample.TargetPointCount.Expression, planSample.TargetPointCount.Context);
			QueryExpressionContext queryExpressionContext2 = this.m_expressionGenerator.TranslateExpression(planSample.MinPointsPerSeries.Expression, planSample.MinPointsPerSeries.Context);
			QueryExpressionContext queryExpressionContext3 = this.m_expressionGenerator.TranslateExpression(planSample.MaxPointsPerSeries.Expression, planSample.MaxPointsPerSeries.Context);
			QueryExpressionContext queryExpressionContext4 = this.m_expressionGenerator.TranslateExpression(planSample.MaxDynamicSeriesCount.Expression, planSample.MaxDynamicSeriesCount.Context);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = BatchQueryBinnedLineSampleItemGenerator.GenerateSingleColumn(generatedTable, planSample.Axis, this.m_context.CalculationExpressionMapping);
			List<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection> list;
			IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> readOnlyList = BatchQueryBinnedLineSampleItemGenerator.Generate(generatedTable, planSample.Measures, this.m_context.CalculationExpressionMapping, out list);
			IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> readOnlyList2 = ((planSample.Series == null) ? null : BatchQueryBinnedLineSampleItemGenerator.Generate(generatedTable, planSample.Series, this.m_context.CalculationExpressionMapping, out list));
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection sortDirection = Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection.Ascending;
			if (!readOnlyList2.IsNullOrEmpty<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>())
			{
				if (list.Distinct<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection>().Count<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection>() > 1)
				{
					this.m_context.ErrorContext.Register(TranslationMessages.InconsistentSortDirectionForBinnedLineSampleSeries(EngineMessageSeverity.Error, ObjectType.DataMember, planSample.Series[0].Member.Id, "SortKey"));
				}
				sortDirection = list[0];
			}
			return new GeneratedTable(generatedTable.QueryTable.SampleAxisWithLocalMinMax(queryExpressionContext.QueryExpression, queryExpression, readOnlyList, queryExpressionContext2.QueryExpression, readOnlyList2, DynamicSeriesSelectionCriteria.Alphabetical, sortDirection, queryExpressionContext3.QueryExpression, queryExpressionContext4.QueryExpression), generatedTable.ColumnMap);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00032A1C File Offset: 0x00030C1C
		public GeneratedTable Visit(PlanOperationTopNPerLevelSample planTopNPerLevelSample)
		{
			GeneratedTable generatedTable = planTopNPerLevelSample.Input.Accept<GeneratedTable>(this);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			GeneratedTable generatedTable2 = BatchTopNPerLevelQueryTableGenerator.Generate(planTopNPerLevelSample, this.m_expressionGenerator, generatedTable, this.m_context.ExpressionTable, this.m_context.Annotations, this.m_context.ErrorContext, this.m_context.Model, this.m_context.FeatureSwitchProvider);
			this.m_referenceContext.PopReferenceTable();
			return generatedTable2;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00032A94 File Offset: 0x00030C94
		public GeneratedTable Visit(PlanOperationOverlappingPointsSample planSample)
		{
			GeneratedTable generatedTable = planSample.Input.Accept<GeneratedTable>(this);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(planSample.TargetPointCount.Expression, planSample.TargetPointCount.Context);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = ((planSample.X != null) ? this.m_expressionGenerator.TranslateExpression(planSample.X.Expression, planSample.X.Context).QueryExpression : null);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = ((planSample.Y != null) ? this.m_expressionGenerator.TranslateExpression(planSample.Y.Expression, planSample.Y.Context).QueryExpression : null);
			QueryTable queryTable = generatedTable.QueryTable.SampleCartesianPointsByCover(queryExpressionContext.QueryExpression, queryExpression, queryExpression2, null, null, null);
			this.m_referenceContext.PopReferenceTable();
			return new GeneratedTable(queryTable, generatedTable.ColumnMap);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00032B6C File Offset: 0x00030D6C
		public GeneratedTable Visit(PlanOperationVisualCalculationReferenceableTable planOperationVisualCalculationReferenceableTable)
		{
			return VisualCalculationReferenceableTableGenerator.Generate(planOperationVisualCalculationReferenceableTable.Table.Accept<GeneratedTable>(this), planOperationVisualCalculationReferenceableTable.ExplicitlyNamedColumns, this.m_namingContextManager.GetOrCreateNamingContext());
		}

		// Token: 0x04000622 RID: 1570
		private readonly BatchQueryGenerationContext m_context;

		// Token: 0x04000623 RID: 1571
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x04000624 RID: 1572
		private readonly GeneratedDeclarationCollection m_declarations;

		// Token: 0x04000625 RID: 1573
		private readonly BatchQueryExpressionReferenceContext m_referenceContext;

		// Token: 0x04000626 RID: 1574
		private readonly BatchQueryGenerationNamingContextManager m_namingContextManager;

		// Token: 0x04000627 RID: 1575
		private readonly IList<string> m_reservedColumnNames;

		// Token: 0x020002E2 RID: 738
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000AB8 RID: 2744
			public static Func<IList<QueryTable>, QueryTableDefinition> <0>__CrossJoin;

			// Token: 0x04000AB9 RID: 2745
			public static Func<IList<QueryTable>, QueryTableDefinition> <1>__GenerateAll;
		}
	}
}
