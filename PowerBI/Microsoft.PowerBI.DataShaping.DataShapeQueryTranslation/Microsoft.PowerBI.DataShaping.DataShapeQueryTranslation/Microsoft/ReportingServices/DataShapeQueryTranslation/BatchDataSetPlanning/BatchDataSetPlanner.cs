using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000162 RID: 354
	internal sealed class BatchDataSetPlanner
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x000351B0 File Offset: 0x000333B0
		private BatchDataSetPlanner(BatchDataSetPlannerContext context, WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations, ContextTableManager attributeFilterContextTableManager, IFilterDeclarationCollection highlightFilterDeclarations, DataShapeQueryTranslationTelemetry telemetryInfo, bool omitOrderBy)
		{
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_highlightFilterDeclarations = highlightFilterDeclarations;
			this.m_telemetryInfo = telemetryInfo;
			this.m_omitOrderBy = omitOrderBy;
			this.m_planOpGenerator = new PlanOperationTreeGenerator(context, outputExpressionTable, declarations, attributeFilterContextTableManager);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00035200 File Offset: 0x00033400
		public static BatchDataSetPlanningResult DeterminePlans(DataShapeContext dsContext, IFederatedConceptualSchema schema, ExpressionTable expressionTable, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, DataShapeQueryTranslationTelemetry telemetryInfo, double? enhancedSamplingAdditionalKeyPointsRatio, bool applyTransformsInQuery, bool omitOrderBy, bool generateComposableQueryColumnNames, IFeatureSwitchProvider featureSwitches)
		{
			PlanDeclarationCollection planDeclarationCollection = new PlanDeclarationCollection(expressionTable);
			WritableExpressionTable writableExpressionTable = expressionTable.CopyTable();
			GroupDetailAnalyzer.Result result = GroupDetailAnalyzer.Analyze(dsContext.DataShape, annotations, scopeTree, writableExpressionTable, schema);
			DataTransformReferenceMap dataTransformReferenceMap = DataTransformReferenceAnalyzer.Analyze(dsContext.DataShape, writableExpressionTable, annotations);
			BatchSortByMeasureExpressionMappings batchSortByMeasureExpressionMappings = SortByMeasureTranslator.CreateSortByMeasureExpressionMappings(dsContext.DataShape, scopeTree, annotations, writableExpressionTable, errorContext, dataTransformReferenceMap, applyTransformsInQuery);
			IDataShapeDefaultValueContextManager dataShapeDefaultValueContextManager = DataShapeDefaultValueContextManager.Create(schema, featureSwitches, annotations, scopeTree, expressionTable);
			BatchDataSetPlanningResult batchDataSetPlanningResult;
			try
			{
				batchDataSetPlanningResult = BatchDataSetPlanner.CreatePlanner(dsContext, schema, planDeclarationCollection, writableExpressionTable, result.CalculationMap, result.DetailMap, dataTransformReferenceMap, annotations, scopeTree, batchSortByMeasureExpressionMappings, errorContext, telemetryInfo, enhancedSamplingAdditionalKeyPointsRatio, applyTransformsInQuery, omitOrderBy, generateComposableQueryColumnNames, featureSwitches, dataShapeDefaultValueContextManager).InternalDeterminePlans(dsContext);
			}
			catch (DataSetPlanningException)
			{
				batchDataSetPlanningResult = null;
			}
			return batchDataSetPlanningResult;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000352B0 File Offset: 0x000334B0
		private static BatchDataSetPlanner CreatePlanner(DataShapeContext dsContext, IFederatedConceptualSchema schema, PlanDeclarationCollection declarations, WritableExpressionTable outputExpressionTable, CalculationExpressionMap calculationMap, GroupDetailMap groupDetailMap, DataTransformReferenceMap transformReferenceMap, DataShapeAnnotations annotations, ScopeTree scopeTree, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, TranslationErrorContext errorContext, DataShapeQueryTranslationTelemetry telemetryInfo, double? enhancedSamplingAdditionalKeyPointsRatio, bool applyTransformsInQuery, bool omitOrderBy, bool generateComposableQueryColumnNames, IFeatureSwitchProvider featureSwitches, IDataShapeDefaultValueContextManager defaultValueContextManager)
		{
			BatchDataSetPlannerContext batchDataSetPlannerContext = new BatchDataSetPlannerContext(outputExpressionTable, schema, annotations, scopeTree, errorContext, sortByMeasureExpressionMappings, calculationMap, groupDetailMap, transformReferenceMap, telemetryInfo, enhancedSamplingAdditionalKeyPointsRatio, applyTransformsInQuery, generateComposableQueryColumnNames, featureSwitches, defaultValueContextManager);
			IFilterDeclarationCollection filterDeclarationCollection = HighlightDeclarationCollector.Analyze(batchDataSetPlannerContext.OutputExpressionTable, batchDataSetPlannerContext.FeatureSwitches, batchDataSetPlannerContext.Schema, batchDataSetPlannerContext.ErrorContext, declarations, dsContext.DataShape);
			ContextTableManager contextTableManager = ContextTableCollector.Analyze(batchDataSetPlannerContext, declarations, dsContext);
			return new BatchDataSetPlanner(batchDataSetPlannerContext, outputExpressionTable, declarations, contextTableManager, filterDeclarationCollection, telemetryInfo, omitOrderBy);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003531C File Offset: 0x0003351C
		private BatchDataSetPlanningResult InternalDeterminePlans(DataShapeContext dsContext)
		{
			if (!this.m_context.Annotations.ValidateBatchSubtotalAnnotations(this.m_context.ErrorContext))
			{
				return null;
			}
			BatchDataSetPlanner.IntermediatePlan intermediatePlan = this.DeterminePlanForDataShape(dsContext);
			this.m_telemetryInfo.InstanceFilters = this.m_planOpGenerator.InstanceFiltersTelemetry;
			if (!intermediatePlan.OutputTableMapping.HasOutputTable)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidDataShapeNoOutputData(EngineMessageSeverity.Error, dsContext.DataShape.ObjectType, dsContext.DataShape.Id, "Calculations"));
				return null;
			}
			List<BatchDataSetPlan> list = new List<BatchDataSetPlan>(1);
			BatchDataSetPlan batchDataSetPlan = new BatchDataSetPlan(intermediatePlan.OutputTableMapping.OutputTables, this.m_declarations.GetDeclarations(), list.Count, dsContext.Id.Value, dsContext.DataShape.ExtensionSchema, dsContext.DataShape.DataSourceVariables, dsContext.DataShape.ModelParameters, dsContext.DataShape.QueryParameters);
			list.Add(batchDataSetPlan);
			BatchDataSetPlannerExpressionTranslator.Translate(this.m_outputExpressionTable, this.m_context, dsContext.DataShape, this.m_highlightFilterDeclarations);
			BatchDataBindingMapping batchDataBindingMapping = new BatchDataBindingMapping();
			foreach (KeyValuePair<IDataBoundItem, int> keyValuePair in intermediatePlan.OutputTableMapping.Mapping)
			{
				batchDataBindingMapping.Add(keyValuePair.Key, new BatchDataBinding(batchDataSetPlan, keyValuePair.Value));
			}
			IntersectionCorrelations intersectionCorrelations = null;
			IntermediateCorrelations correlations = intermediatePlan.Correlations;
			if (correlations != null)
			{
				intersectionCorrelations = correlations.Bind(batchDataSetPlan, intermediatePlan.OutputTableMapping);
			}
			BatchMemberMatchConditions batchMemberMatchConditions = null;
			IntermediateMemberMatchConditions memberMatchConditions = intermediatePlan.MemberMatchConditions;
			if (memberMatchConditions != null)
			{
				batchMemberMatchConditions = memberMatchConditions.Bind(batchDataSetPlan, intermediatePlan.OutputTableMapping);
			}
			BatchMemberDiscardConditions batchMemberDiscardConditions = null;
			IntermediateMemberDiscardConditions memberDiscardConditions = intermediatePlan.MemberDiscardConditions;
			if (memberDiscardConditions != null)
			{
				batchMemberDiscardConditions = memberDiscardConditions.Bind(batchDataSetPlan, intermediatePlan.OutputTableMapping);
			}
			BatchRestartIndicator restartIndicator = intermediatePlan.RestartIndicator;
			if (restartIndicator != null)
			{
				restartIndicator.Bind(batchDataSetPlan, intermediatePlan.OutputTableMapping);
			}
			return new BatchDataSetPlanningResult(list, this.m_outputExpressionTable.AsReadOnly(), batchDataBindingMapping, intersectionCorrelations, batchMemberMatchConditions, batchMemberDiscardConditions, this.m_context.SortByMeasureExpressionMappings, this.m_context.GroupDetailMap, this.m_context.CalculationMap, intermediatePlan.CalculationsWithSharedValues, intermediatePlan.LimitInfo, restartIndicator);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003554C File Offset: 0x0003374C
		private BatchDataSetPlanner.IntermediatePlan DeterminePlanForDataShape(DataShapeContext dsContext)
		{
			BatchPlanningStrategy batchPlanningStrategy = dsContext.DeterminePlanningStrategy();
			this.ValidateTranslationStrategyPrerequisites(dsContext, batchPlanningStrategy);
			PlanOperationTreeGeneratorResult planOperationTreeGeneratorResult = this.m_planOpGenerator.GenerateTables(dsContext, this.m_omitOrderBy, false, batchPlanningStrategy);
			string text = PlanNames.Body(dsContext.Id);
			string text2 = PlanNames.Secondary(dsContext.Id);
			if (planOperationTreeGeneratorResult.RestartIndicator != null)
			{
				planOperationTreeGeneratorResult.RestartIndicator.OutputTableName = text;
			}
			IntermediateCorrelations intermediateCorrelations = null;
			if (planOperationTreeGeneratorResult.ColumnIndexSort != null)
			{
				intermediateCorrelations = this.CreateCorrelations(dsContext, text, planOperationTreeGeneratorResult.ColumnIndexSort.Name);
			}
			IntermediateMemberMatchConditions intermediateMemberMatchConditions = this.CreateMatchConditions(dsContext, batchPlanningStrategy, text, text2);
			return new BatchDataSetPlanner.IntermediatePlan(planOperationTreeGeneratorResult.OutputTables, planOperationTreeGeneratorResult.CalculationsWithSharedValues, intermediateCorrelations, intermediateMemberMatchConditions, planOperationTreeGeneratorResult.DiscardConditions, planOperationTreeGeneratorResult.LimitInfo, planOperationTreeGeneratorResult.RestartIndicator);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000355FC File Offset: 0x000337FC
		private void ValidateTranslationStrategyPrerequisites(DataShapeContext dsContext, BatchPlanningStrategy translationStrategy)
		{
			switch (translationStrategy)
			{
			case BatchPlanningStrategy.DetailGroups:
				this.CheckNoInlineDataTransform(dsContext);
				this.CheckUnsupportedOmitOrderBy();
				this.CheckUnsupportedGenerateComposableQueryColumnNames();
				this.CheckUnsupportedInstanceFilters(dsContext);
				return;
			case BatchPlanningStrategy.AggregatesOnly:
				return;
			case BatchPlanningStrategy.PrimaryOnly:
				return;
			case BatchPlanningStrategy.SecondaryOnly:
				this.CheckUnsupportedTopNPerLevel(dsContext);
				this.CheckNoInlineDataTransform(dsContext);
				return;
			case BatchPlanningStrategy.PrimaryAndSecondary:
				this.CheckNoInlineDataTransform(dsContext);
				this.CheckUnsupportedTopNPerLevel(dsContext);
				return;
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unrecognized BatchTranslationStrategy: {0}", translationStrategy);
				return;
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00035670 File Offset: 0x00033870
		private void CheckNoInlineDataTransform(DataShapeContext dsContext)
		{
			Microsoft.DataShaping.Contract.RetailAssert(!this.m_context.ApplyTransformsInQuery || !dsContext.HasDataTransforms, "The selected data set plan strategy does not support inline DataTransforms");
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00035695 File Offset: 0x00033895
		private void CheckUnsupportedOmitOrderBy()
		{
			Microsoft.DataShaping.Contract.RetailAssert(!this.m_omitOrderBy, "The selected data set plan strategy does not support OmitOrderBy.");
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000356AA File Offset: 0x000338AA
		private void CheckUnsupportedGenerateComposableQueryColumnNames()
		{
			Microsoft.DataShaping.Contract.RetailAssert(!this.m_context.GenerateComposableQueryColumnNames, "The selected data set plan strategy does not support GenerateComposableQueryColumnNames.");
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000356C4 File Offset: 0x000338C4
		private void CheckUnsupportedInstanceFilters(DataShapeContext dsContext)
		{
			Microsoft.DataShaping.Contract.RetailAssert(!dsContext.HasInstanceFilters, "The selected data set plan strategy does not support InstanceFilters");
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000356D9 File Offset: 0x000338D9
		private void CheckUnsupportedTopNPerLevel(DataShapeContext dsContext)
		{
			Microsoft.DataShaping.Contract.RetailAssert(!dsContext.HasTopNPerLevelSampleLimit, "Secondary hierarchy should not be present when TopNPerLevel reduction is used");
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000356F0 File Offset: 0x000338F0
		private IntermediateMemberMatchConditions CreateMatchConditions(DataShapeContext dsContext, BatchPlanningStrategy translationStrategy, string bodyTableName, string secondaryTableName)
		{
			switch (translationStrategy)
			{
			case BatchPlanningStrategy.DetailGroups:
			case BatchPlanningStrategy.AggregatesOnly:
				return null;
			case BatchPlanningStrategy.PrimaryOnly:
				return MemberMatchConditionCollector.CollectMatchConditions(this.m_context.Annotations, dsContext.DataShape.PrimaryHierarchy, bodyTableName);
			case BatchPlanningStrategy.SecondaryOnly:
				return MemberMatchConditionCollector.CollectMatchConditions(this.m_context.Annotations, dsContext.DataShape.SecondaryHierarchy, bodyTableName);
			case BatchPlanningStrategy.PrimaryAndSecondary:
			{
				IntermediateMemberMatchConditions intermediateMemberMatchConditions = MemberMatchConditionCollector.CollectMatchConditions(this.m_context.Annotations, dsContext.DataShape.PrimaryHierarchy, bodyTableName);
				IntermediateMemberMatchConditions intermediateMemberMatchConditions2 = MemberMatchConditionCollector.CollectMatchConditions(this.m_context.Annotations, dsContext.DataShape.SecondaryHierarchy, secondaryTableName);
				return IntermediateMemberMatchConditions.Merge(intermediateMemberMatchConditions, intermediateMemberMatchConditions2);
			}
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unrecognized BatchTranslationStrategy: {0}", translationStrategy);
				return null;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000357A7 File Offset: 0x000339A7
		private IntermediateCorrelations CreateCorrelations(DataShapeContext dsContext, string bodyTableName, string columnIndexSortName)
		{
			return new IntermediateCorrelations { 
			{
				dsContext.Id,
				new IntermediateColumnReference(columnIndexSortName, bodyTableName)
			} };
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000357C4 File Offset: 0x000339C4
		private bool RequiresUnsupportedLimitsOnHierarchies(DataShapeContext dsContext)
		{
			IReadOnlyList<Limit> primaryHierarchyLimits = dsContext.PrimaryHierarchyLimits;
			IReadOnlyList<Limit> secondaryHierarchyLimits = dsContext.SecondaryHierarchyLimits;
			return (!primaryHierarchyLimits.IsNullOrEmpty<Limit>() || !secondaryHierarchyLimits.IsNullOrEmpty<Limit>()) && (this.HasUnsupportedWithinOnLimit(primaryHierarchyLimits, dsContext.DataShape) || this.HasUnsupportedWithinOnLimit(secondaryHierarchyLimits, dsContext.DataShape));
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00035810 File Offset: 0x00033A10
		private bool HasUnsupportedWithinOnLimit(IReadOnlyList<Limit> limits, DataShape containingDataShape)
		{
			foreach (Limit limit in limits)
			{
				ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = (ResolvedScopeReferenceExpressionNode)this.m_context.OutputExpressionTable.GetNode(limit.Within);
				if (!this.m_context.ScopeTree.AreSameScope(containingDataShape, resolvedScopeReferenceExpressionNode.Scope))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400065F RID: 1631
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000660 RID: 1632
		private readonly BatchDataSetPlannerContext m_context;

		// Token: 0x04000661 RID: 1633
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000662 RID: 1634
		private readonly IFilterDeclarationCollection m_highlightFilterDeclarations;

		// Token: 0x04000663 RID: 1635
		private readonly PlanOperationTreeGenerator m_planOpGenerator;

		// Token: 0x04000664 RID: 1636
		private readonly DataShapeQueryTranslationTelemetry m_telemetryInfo;

		// Token: 0x04000665 RID: 1637
		private readonly bool m_omitOrderBy;

		// Token: 0x020002E9 RID: 745
		internal sealed class IntermediatePlan
		{
			// Token: 0x060016B9 RID: 5817 RVA: 0x00051EF3 File Offset: 0x000500F3
			internal IntermediatePlan(OutputTableMapping outputTableMapping, CalculationsWithSharedValues calculationsWithSharedValues, IntermediateCorrelations correlations = null, IntermediateMemberMatchConditions memberMatchConditions = null, IntermediateMemberDiscardConditions memberDiscardConditions = null, PlanLimitInfo limitInfo = null, BatchRestartIndicator restartIndicator = null)
			{
				this.OutputTableMapping = outputTableMapping;
				this.Correlations = correlations;
				this.MemberMatchConditions = memberMatchConditions;
				this.MemberDiscardConditions = memberDiscardConditions;
				this.LimitInfo = limitInfo;
				this.RestartIndicator = restartIndicator;
				this.CalculationsWithSharedValues = calculationsWithSharedValues;
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x060016BA RID: 5818 RVA: 0x00051F30 File Offset: 0x00050130
			public OutputTableMapping OutputTableMapping { get; }

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x060016BB RID: 5819 RVA: 0x00051F38 File Offset: 0x00050138
			public IntermediateCorrelations Correlations { get; }

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x060016BC RID: 5820 RVA: 0x00051F40 File Offset: 0x00050140
			public IntermediateMemberMatchConditions MemberMatchConditions { get; }

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060016BD RID: 5821 RVA: 0x00051F48 File Offset: 0x00050148
			public IntermediateMemberDiscardConditions MemberDiscardConditions { get; }

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060016BE RID: 5822 RVA: 0x00051F50 File Offset: 0x00050150
			public PlanLimitInfo LimitInfo { get; }

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x060016BF RID: 5823 RVA: 0x00051F58 File Offset: 0x00050158
			public BatchRestartIndicator RestartIndicator { get; }

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00051F60 File Offset: 0x00050160
			public CalculationsWithSharedValues CalculationsWithSharedValues { get; }
		}
	}
}
