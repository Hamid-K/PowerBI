using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000142 RID: 322
	internal sealed class BatchQueryGenerator
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0002F768 File Offset: 0x0002D968
		private BatchQueryGenerator(BatchQueryGenerationContext context, BatchDataSetPlan dataSetPlan, IntersectionCorrelations correlations, BatchMemberMatchConditions memberMatchConditions, BatchMemberDiscardConditions memberDiscardConditions, BatchRestartIndicator restartIndicator)
		{
			this.m_context = context;
			this.m_dataSetPlan = dataSetPlan;
			this.m_intersectionCorrelations = correlations;
			this.m_memberMatchConditions = memberMatchConditions;
			this.m_memberDiscardConditions = memberDiscardConditions;
			this.m_restartIndicator = restartIndicator;
			this.m_outputExpressionTable = this.m_context.ExpressionTable.CreateEmptyWritableTable();
			this.m_declarations = new WritableGeneratedDeclarationCollection();
			this.m_queryParameterMap = new WritableGeneratedQueryParameterMap();
			if (BatchQueryGenerator.NeedsGlobalNamingContext(dataSetPlan))
			{
				this.m_globalNamingContext = new BatchQueryGenerationNamingContext();
			}
			ReadOnlyGeneratedDeclarationCollection readOnlyGeneratedDeclarationCollection = this.m_declarations.AsReadOnly();
			BatchQueryExpressionReferenceContext batchQueryExpressionReferenceContext = new BatchQueryExpressionReferenceContext();
			this.m_tableGenerator = new BatchQueryTableGenerator(context, readOnlyGeneratedDeclarationCollection, batchQueryExpressionReferenceContext);
			this.m_expressionGenerator = new BatchQueryExpressionGenerator(context, this.m_outputExpressionTable, this.m_tableGenerator, batchQueryExpressionReferenceContext, readOnlyGeneratedDeclarationCollection, this.m_queryParameterMap);
			this.m_entityGenerator = new BatchQueryEntityDeclarationGenerator(context, this.m_expressionGenerator, this.m_tableGenerator, batchQueryExpressionReferenceContext);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002F840 File Offset: 0x0002DA40
		private static bool NeedsGlobalNamingContext(BatchDataSetPlan dataSetPlan)
		{
			foreach (IPlanNamedItem planNamedItem in dataSetPlan.Declarations)
			{
				PlanNamedTable planNamedTable = planNamedItem as PlanNamedTable;
				if (planNamedTable != null && planNamedTable.UseGlobalNamingContext)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
		public static ReadOnlyCollection<BatchQueryGenerationResult> GenerateAll(BatchQueryGenerationContext context, BatchDataSetPlanningResult dataSetPlanningResult)
		{
			List<BatchQueryGenerationResult> list = new List<BatchQueryGenerationResult>(dataSetPlanningResult.DataSetPlans.Count);
			foreach (BatchDataSetPlan batchDataSetPlan in dataSetPlanningResult.DataSetPlans)
			{
				BatchQueryGenerationResult batchQueryGenerationResult = BatchQueryGenerator.Generate(context, batchDataSetPlan, dataSetPlanningResult.IntersectionCorrelations, dataSetPlanningResult.MemberMatchConditions, dataSetPlanningResult.MemberDiscardConditions, dataSetPlanningResult.RestartIndicator);
				list.Add(batchQueryGenerationResult);
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002F924 File Offset: 0x0002DB24
		public static BatchQueryGenerationResult Generate(BatchQueryGenerationContext context, BatchDataSetPlan dataSetPlan, IntersectionCorrelations correlations, BatchMemberMatchConditions memberMatchConditions, BatchMemberDiscardConditions memberDiscardConditions, BatchRestartIndicator restartIndicator)
		{
			BatchQueryGenerationResult batchQueryGenerationResult;
			try
			{
				batchQueryGenerationResult = new BatchQueryGenerator(context, dataSetPlan, correlations, memberMatchConditions, memberDiscardConditions, restartIndicator).InternalGenerate();
			}
			catch (QueryGenerationException)
			{
				batchQueryGenerationResult = null;
			}
			catch (CommandTreeTranslationException ex)
			{
				if (!QueryGenerationUtils.TryHandleCommandTreeTranslationException(ex, context.ErrorContext, dataSetPlan.Name))
				{
					throw;
				}
				batchQueryGenerationResult = null;
			}
			return batchQueryGenerationResult;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002F984 File Offset: 0x0002DB84
		private BatchQueryGenerationResult InternalGenerate()
		{
			IConceptualSchema defaultSchema = this.m_context.Schema.GetDefaultSchema();
			FederatedEntityDataModel model = this.m_context.Model;
			EntityDataModel entityDataModel = ((model != null) ? model.BaseModel : null);
			IConceptualSchema conceptualSchema = defaultSchema;
			IFeatureSwitchProvider featureSwitchProvider = this.m_context.FeatureSwitchProvider;
			FederatedEntityDataModel model2 = this.m_context.Model;
			BatchQueryBuilder batchQueryBuilder = new BatchQueryBuilder(entityDataModel, conceptualSchema, featureSwitchProvider, DaxCapabilitiesBuilder.BuildCapabilities((model2 != null) ? model2.BaseModel : null, defaultSchema, this.m_context.FeatureSwitchProvider), this.m_context.SuppressModelGrouping);
			BatchQueryExtensionSchemaExpressionGenerator batchQueryExtensionSchemaExpressionGenerator = new BatchQueryExtensionSchemaExpressionGenerator(this.m_context.ExpressionTable, this.m_context.ErrorContext, this.m_context.DaxCapabilities, this.m_context.SuppressModelGrouping, batchQueryBuilder, this.m_context.Model, this.m_context.Schema.GetDefaultSchema(), this.m_context.CancellationToken, this.m_context.FeatureSwitchProvider);
			BatchQueryExtensionSchemaTranslator.Translate(this.m_dataSetPlan.ExtensionSchema, batchQueryBuilder, this.m_context.Model, this.m_context.Schema, batchQueryExtensionSchemaExpressionGenerator, this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
			this.m_context.CancellationToken.ThrowIfCancellationRequested();
			this.DeclareQueryParameters(batchQueryBuilder);
			this.DeclareDataSourceVariables(batchQueryBuilder);
			this.DeclareMParameters(batchQueryBuilder);
			this.m_context.CancellationToken.ThrowIfCancellationRequested();
			this.GenerateDeclarations(batchQueryBuilder);
			this.m_context.CancellationToken.ThrowIfCancellationRequested();
			List<GeneratedTable> list = this.GenerateOutputTables(batchQueryBuilder);
			this.m_context.CancellationToken.ThrowIfCancellationRequested();
			Dictionary<Identifier, ExpressionId> dictionary = this.GenerateIntersectionCorrelation(list);
			BatchQueryMemberMatchConditions batchQueryMemberMatchConditions = this.GenerateMemberMatchConditions(list);
			BatchQueryMemberDiscardConditions batchQueryMemberDiscardConditions = this.GenerateMemberDiscardConditions(list);
			BatchQueryRestartIndicator batchQueryRestartIndicator = this.GenerateRestartIndicator(list);
			this.m_context.CancellationToken.ThrowIfCancellationRequested();
			BatchQueryDefinition batchQueryDefinition;
			if (this.m_context.GenerateComposableQuery)
			{
				batchQueryDefinition = batchQueryBuilder.ToComposableQueryDefinition();
			}
			else
			{
				batchQueryDefinition = batchQueryBuilder.ToQueryDefinition();
			}
			return new BatchQueryGenerationResult(this.m_dataSetPlan, batchQueryDefinition, this.m_outputExpressionTable, dictionary, batchQueryMemberMatchConditions, batchQueryMemberDiscardConditions, batchQueryRestartIndicator, this.m_queryParameterMap);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002FB88 File Offset: 0x0002DD88
		private void DeclareQueryParameters(BatchQueryBuilder batchBuilder)
		{
			QueryParameterGenerator.Generate(this.m_dataSetPlan.QueryParameters, batchBuilder, this.m_queryParameterMap);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002FBA4 File Offset: 0x0002DDA4
		private void GenerateDeclarations(BatchQueryBuilder batchBuilder)
		{
			Dictionary<string, BatchQueryGenerationNamingContext> dictionary = null;
			foreach (IPlanNamedItem planNamedItem in this.m_dataSetPlan.Declarations)
			{
				switch (planNamedItem.Kind)
				{
				case PlanNamedItemKind.Entity:
					this.GenerateEntityDeclaration(batchBuilder, (PlanNamedEntity)planNamedItem);
					break;
				case PlanNamedItemKind.Scalar:
					this.GenerateScalarDeclaration(batchBuilder, (PlanNamedScalar)planNamedItem);
					break;
				case PlanNamedItemKind.Table:
					this.GenerateTableDeclaration(batchBuilder, (PlanNamedTable)planNamedItem, ref dictionary);
					break;
				default:
					Microsoft.DataShaping.Contract.RetailFail("Unexpected declaration type: {0}", planNamedItem.GetType().Name);
					break;
				}
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002FC50 File Offset: 0x0002DE50
		private void GenerateEntityDeclaration(BatchQueryBuilder batchBuilder, PlanNamedEntity entityDecl)
		{
			string text = batchBuilder.NamingContext.CreateNameForTableDeclaration(entityDecl.Name);
			global::System.ValueTuple<QueryTableDeclarationExpression, GeneratedColumnMap> valueTuple = this.m_entityGenerator.Generate(entityDecl, text, null);
			QueryTableDeclarationExpression item = valueTuple.Item1;
			GeneratedColumnMap item2 = valueTuple.Item2;
			IConceptualEntity conceptualEntity = batchBuilder.DeclareTable(item);
			GeneratedEntityDeclaration generatedEntityDeclaration = new GeneratedEntityDeclaration(entityDecl.Name, conceptualEntity, item2);
			this.m_declarations.AddEntityDeclaration(generatedEntityDeclaration);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002FCB0 File Offset: 0x0002DEB0
		private void GenerateTableDeclaration(BatchQueryBuilder batchBuilder, PlanNamedTable tableDecl, ref Dictionary<string, BatchQueryGenerationNamingContext> namingContexts)
		{
			BatchQueryGenerationNamingContext batchQueryGenerationNamingContext = null;
			if (tableDecl.NamingContextId != null)
			{
				if (!Microsoft.DataShaping.Util.TryGetFromLazyDictionary<string, BatchQueryGenerationNamingContext>(namingContexts, tableDecl.NamingContextId, out batchQueryGenerationNamingContext))
				{
					batchQueryGenerationNamingContext = new BatchQueryGenerationNamingContext();
					Microsoft.DataShaping.Util.AddToLazyDictionary<string, BatchQueryGenerationNamingContext>(ref namingContexts, tableDecl.NamingContextId, batchQueryGenerationNamingContext, null);
				}
			}
			else if (tableDecl.UseGlobalNamingContext)
			{
				batchQueryGenerationNamingContext = this.m_globalNamingContext;
			}
			if (tableDecl.CanExpandToMultiTables)
			{
				using (IEnumerator<GeneratedTable> enumerator = this.m_tableGenerator.GenerateTables(tableDecl.Table, this.m_expressionGenerator, batchQueryGenerationNamingContext).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GeneratedTable generatedTable = enumerator.Current;
						if (!tableDecl.IsFragmentOfExistingDeclaration || !this.m_declarations.TryReconcileAgainstExistingDeclaration(tableDecl.Name, generatedTable))
						{
							string text;
							GeneratedTable generatedTable2 = this.DeclareInQuery(batchBuilder, generatedTable, tableDecl.Name, out text);
							generatedTable2.ShouldCrossFilterGroupColumns = generatedTable.ShouldCrossFilterGroupColumns;
							GeneratedTableDeclaration generatedTableDeclaration = new GeneratedTableDeclaration(tableDecl.Name, text, generatedTable2, generatedTable);
							this.m_declarations.AddMultiTableDeclaration(generatedTableDeclaration);
							this.UpdateGlobalNamingContext(generatedTable);
						}
					}
					return;
				}
			}
			GeneratedTable generatedTable3 = this.m_tableGenerator.Generate(tableDecl.Table, this.m_expressionGenerator, batchQueryGenerationNamingContext);
			if (tableDecl.IsFragmentOfExistingDeclaration && this.m_declarations.TryReconcileAgainstExistingDeclaration(tableDecl.Name, generatedTable3))
			{
				return;
			}
			if (this.TryRemapAliasToExistingDeclaration(generatedTable3, tableDecl.Name))
			{
				return;
			}
			string text2;
			GeneratedTable generatedTable4 = this.DeclareInQuery(batchBuilder, generatedTable3, tableDecl.Name, out text2);
			this.m_declarations.AddSingleTableDeclaration(new GeneratedTableDeclaration(tableDecl.Name, text2, generatedTable4, generatedTable3));
			this.UpdateGlobalNamingContext(generatedTable3);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002FE38 File Offset: 0x0002E038
		private void UpdateGlobalNamingContext(GeneratedTable table)
		{
			if (this.m_globalNamingContext == null)
			{
				return;
			}
			foreach (QueryTableColumn queryTableColumn in table.QueryTable.Columns)
			{
				this.m_globalNamingContext.RegisterName(queryTableColumn.Name);
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		private GeneratedTable DeclareInQuery(BatchQueryBuilder batchBuilder, GeneratedTable originalTable, string declarationName, out string variableName)
		{
			QueryTableReference queryTableReference = batchBuilder.DeclareVariable(originalTable.QueryTable, declarationName);
			variableName = queryTableReference.VariableName;
			return new GeneratedTable(queryTableReference, originalTable.ColumnMap);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002FED0 File Offset: 0x0002E0D0
		private bool TryRemapAliasToExistingDeclaration(GeneratedTable newDeclTable, string planDeclName)
		{
			QueryTableReference queryTableReference = newDeclTable.QueryTable as QueryTableReference;
			if (queryTableReference != null)
			{
				GeneratedTableDeclaration tableDeclarationByQueryName = this.m_declarations.GetTableDeclarationByQueryName(queryTableReference.VariableName);
				GeneratedTable generatedTable = new GeneratedTable(tableDeclarationByQueryName.Table.QueryTable, newDeclTable.ColumnMap);
				GeneratedTable generatedTable2 = new GeneratedTable(tableDeclarationByQueryName.OriginalTable.QueryTable, newDeclTable.ColumnMap);
				GeneratedTableDeclaration generatedTableDeclaration = new GeneratedTableDeclaration(planDeclName, tableDeclarationByQueryName.QueryName, generatedTable, generatedTable2);
				this.m_declarations.AddReconciledDeclaration(generatedTableDeclaration);
				return true;
			}
			return false;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002FF4C File Offset: 0x0002E14C
		private void GenerateScalarDeclaration(BatchQueryBuilder batchBuilder, PlanNamedScalar scalarDecl)
		{
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(scalarDecl.Expression.Expression, scalarDecl.Expression.Context);
			QueryVariableReferenceExpression queryVariableReferenceExpression = batchBuilder.DeclareVariable(queryExpressionContext.QueryExpression, scalarDecl.Name);
			this.m_declarations.AddScalarDeclaration(new GeneratedScalarDeclaration(scalarDecl.Name, queryVariableReferenceExpression));
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002FFA8 File Offset: 0x0002E1A8
		private List<GeneratedTable> GenerateOutputTables(BatchQueryBuilder batchBuilder)
		{
			List<GeneratedTable> list = new List<GeneratedTable>(this.m_dataSetPlan.OutputTables.Count);
			foreach (PlanNamedTableContext planNamedTableContext in this.m_dataSetPlan.OutputTables)
			{
				GeneratedTable generatedTable = this.m_tableGenerator.Generate(planNamedTableContext.Table, this.m_expressionGenerator, null);
				list.Add(generatedTable);
				batchBuilder.AddOutputTable(generatedTable.QueryTable);
				foreach (KeyValuePair<ExpressionId, QueryTableColumn> keyValuePair in generatedTable.ColumnMap.ExpressionMap)
				{
					this.m_outputExpressionTable.SetNode(keyValuePair.Key, new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, keyValuePair.Value.Name, planNamedTableContext));
				}
			}
			return list;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000300A8 File Offset: 0x0002E2A8
		private void DeclareDataSourceVariables(BatchQueryBuilder batchBuilder)
		{
			if (string.IsNullOrEmpty(this.m_dataSetPlan.DataSourceVariables))
			{
				return;
			}
			batchBuilder.DeclareDataSourceVariables(this.m_dataSetPlan.DataSourceVariables);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000300D0 File Offset: 0x0002E2D0
		private void DeclareMParameters(BatchQueryBuilder batchBuilder)
		{
			if (this.m_dataSetPlan.ModelParameters.IsNullOrEmpty<ModelParameter>())
			{
				return;
			}
			foreach (ModelParameter modelParameter in this.m_dataSetPlan.ModelParameters)
			{
				QueryMParameterDeclarationExpression queryMParameterDeclarationExpression = QueryGenerationUtils.ConvertToQueryMParameterDeclarationExpression(modelParameter, this.m_context.ErrorContext, this.m_expressionGenerator);
				batchBuilder.DeclareMParameter(queryMParameterDeclarationExpression);
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0003014C File Offset: 0x0002E34C
		private Dictionary<Identifier, ExpressionId> GenerateIntersectionCorrelation(List<GeneratedTable> outputTables)
		{
			if (this.m_intersectionCorrelations == null)
			{
				return null;
			}
			Dictionary<Identifier, ExpressionId> dictionary = new Dictionary<Identifier, ExpressionId>();
			foreach (KeyValuePair<Identifier, BatchColumnReference> keyValuePair in this.m_intersectionCorrelations)
			{
				BatchColumnReference batchColumnReference = this.m_intersectionCorrelations[keyValuePair.Key];
				int outputTableIndex = batchColumnReference.DataBinding.OutputTableIndex;
				ExpressionId expressionId = this.ResolveColumnAndCreateFieldReferenceExpression(outputTables, outputTableIndex, batchColumnReference.ColumnName);
				dictionary.Add(keyValuePair.Key, expressionId);
			}
			return dictionary;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000301EC File Offset: 0x0002E3EC
		private BatchQueryMemberMatchConditions GenerateMemberMatchConditions(List<GeneratedTable> outputTables)
		{
			if (this.m_memberMatchConditions == null)
			{
				return null;
			}
			BatchQueryMemberMatchConditions batchQueryMemberMatchConditions = new BatchQueryMemberMatchConditions(this.m_memberMatchConditions.Count);
			foreach (KeyValuePair<DataMember, BatchMatchCondition> keyValuePair in this.m_memberMatchConditions)
			{
				ExpressionId expressionId = this.ResolveColumnAndCreateFieldReferenceExpression(outputTables, keyValuePair.Value.DataBinding.OutputTableIndex, keyValuePair.Value.ColumnName);
				batchQueryMemberMatchConditions.Add(keyValuePair.Key, new BatchQueryMemberMatchCondition(expressionId, keyValuePair.Value.MatchValue));
			}
			return batchQueryMemberMatchConditions;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00030298 File Offset: 0x0002E498
		private BatchQueryMemberDiscardConditions GenerateMemberDiscardConditions(List<GeneratedTable> outputTables)
		{
			if (this.m_memberDiscardConditions == null)
			{
				return null;
			}
			BatchQueryMemberDiscardConditions batchQueryMemberDiscardConditions = new BatchQueryMemberDiscardConditions(this.m_memberDiscardConditions.Count);
			foreach (KeyValuePair<DataMember, BatchDiscardCondition> keyValuePair in this.m_memberDiscardConditions)
			{
				ExpressionId expressionId = this.ResolveColumnAndCreateFieldReferenceExpression(outputTables, keyValuePair.Value.DataBinding.OutputTableIndex, keyValuePair.Value.ColumnName);
				batchQueryMemberDiscardConditions.Add(keyValuePair.Key, new BatchQueryMemberDiscardCondition(expressionId, keyValuePair.Value.MatchValue, keyValuePair.Value.Operator));
			}
			return batchQueryMemberDiscardConditions;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00030350 File Offset: 0x0002E550
		private BatchQueryRestartIndicator GenerateRestartIndicator(List<GeneratedTable> outputTables)
		{
			if (this.m_restartIndicator == null)
			{
				return null;
			}
			return new BatchQueryRestartIndicator(this.ResolveColumnAndCreateFieldReferenceExpression(outputTables, this.m_restartIndicator.RestartIndicatorColumn.DataBinding.OutputTableIndex, this.m_restartIndicator.RestartIndicatorColumn.ColumnName), this.m_restartIndicator.DataMembersToRestart);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000303A4 File Offset: 0x0002E5A4
		private ExpressionId ResolveColumnAndCreateFieldReferenceExpression(List<GeneratedTable> outputTables, int outputTableIndex, string columnName)
		{
			QueryTableColumn queryTableColumn = outputTables[outputTableIndex].ColumnMap[columnName];
			return this.m_outputExpressionTable.Add(new DataSetFieldReferenceExpressionNode(this.m_dataSetPlan, queryTableColumn.Name, this.m_dataSetPlan.OutputTables[outputTableIndex]));
		}

		// Token: 0x040005FB RID: 1531
		private readonly BatchQueryGenerationContext m_context;

		// Token: 0x040005FC RID: 1532
		private readonly BatchDataSetPlan m_dataSetPlan;

		// Token: 0x040005FD RID: 1533
		private readonly IntersectionCorrelations m_intersectionCorrelations;

		// Token: 0x040005FE RID: 1534
		private readonly BatchMemberMatchConditions m_memberMatchConditions;

		// Token: 0x040005FF RID: 1535
		private readonly BatchMemberDiscardConditions m_memberDiscardConditions;

		// Token: 0x04000600 RID: 1536
		private readonly BatchRestartIndicator m_restartIndicator;

		// Token: 0x04000601 RID: 1537
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000602 RID: 1538
		private readonly WritableGeneratedDeclarationCollection m_declarations;

		// Token: 0x04000603 RID: 1539
		private readonly WritableGeneratedQueryParameterMap m_queryParameterMap;

		// Token: 0x04000604 RID: 1540
		private readonly BatchQueryGenerationNamingContext m_globalNamingContext;

		// Token: 0x04000605 RID: 1541
		private readonly BatchQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x04000606 RID: 1542
		private readonly BatchQueryTableGenerator m_tableGenerator;

		// Token: 0x04000607 RID: 1543
		private readonly BatchQueryEntityDeclarationGenerator m_entityGenerator;
	}
}
