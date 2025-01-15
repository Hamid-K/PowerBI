using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000199 RID: 409
	internal sealed class PlanOperationTreeGenerator : IPlanOperationTreeGenerator
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x000393B8 File Offset: 0x000375B8
		internal PlanOperationTreeGenerator(BatchDataSetPlannerContext context, WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations, ContextTableManager attributeFilterContextTableManager)
		{
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_attributeFilterContextTableManager = attributeFilterContextTableManager;
			this.m_hierarchyPlanner = new HierarchyPlanOperationApplier(this.m_outputExpressionTable, this.m_declarations);
			this.m_subqueryGenerator = new SubqueryPlanOperationGenerator(this.m_context, this.m_declarations, this);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00039417 File Offset: 0x00037617
		internal List<InstanceFilterTelemetry> InstanceFiltersTelemetry
		{
			get
			{
				return this.m_instanceFiltersTelemetry;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003941F File Offset: 0x0003761F
		private GroupSynchronizationApplier GroupSyncApplier
		{
			get
			{
				if (this.m_groupSyncApplier == null)
				{
					this.m_groupSyncApplier = new GroupSynchronizationApplier(this.m_context, null);
				}
				return this.m_groupSyncApplier;
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00039444 File Offset: 0x00037644
		public PlanOperationTreeGeneratorResult GenerateTables(DataShapeContext dsContext, bool omitOrderBy, bool suppressCoreTableUnconstrainedJoinCheck, BatchPlanningStrategy translationStrategy)
		{
			AggregatesInputTableCollector aggregatesInputTableCollector = new AggregatesInputTableCollector(this.m_context.ScopeTree, this.m_context.Annotations, this.m_context.SortByMeasureExpressionMappings, this.m_declarations, this.m_outputExpressionTable);
			InstanceFilterTelemetry instanceFilterTelemetry;
			InstanceFiltersContext instanceFiltersContext = InstanceFiltersContext.Create(dsContext, this.m_context.Schema, this.m_declarations, this.m_outputExpressionTable, this.m_context.FeatureSwitches, out instanceFilterTelemetry);
			if (instanceFilterTelemetry != null)
			{
				Util.AddToLazyList<InstanceFilterTelemetry>(ref this.m_instanceFiltersTelemetry, instanceFilterTelemetry);
			}
			IntermediateMemberDiscardConditions intermediateMemberDiscardConditions = new IntermediateMemberDiscardConditions();
			switch (translationStrategy)
			{
			case BatchPlanningStrategy.DetailGroups:
				return new PlanOperationTreeGeneratorResult(this.GenerateDetailTables(dsContext, aggregatesInputTableCollector, instanceFiltersContext, suppressCoreTableUnconstrainedJoinCheck));
			case BatchPlanningStrategy.AggregatesOnly:
				return new PlanOperationTreeGeneratorResult(this.GenerateAggregatesOnlyTables(dsContext, aggregatesInputTableCollector, instanceFiltersContext, suppressCoreTableUnconstrainedJoinCheck));
			case BatchPlanningStrategy.PrimaryOnly:
			{
				PlanLimitInfo planLimitInfo;
				BatchRestartIndicator batchRestartIndicator;
				OutputTableMapping outputTableMapping = this.GeneratePrimaryOnlyTables(dsContext, instanceFiltersContext, aggregatesInputTableCollector, intermediateMemberDiscardConditions, omitOrderBy, suppressCoreTableUnconstrainedJoinCheck, out planLimitInfo, out batchRestartIndicator);
				PlanLimitInfo planLimitInfo2 = planLimitInfo;
				BatchRestartIndicator batchRestartIndicator2 = batchRestartIndicator;
				PlanColumnSortItem planColumnSortItem = null;
				IntermediateMemberDiscardConditions intermediateMemberDiscardConditions2 = intermediateMemberDiscardConditions;
				GroupSynchronizationApplier groupSyncApplier = this.GroupSyncApplier;
				return new PlanOperationTreeGeneratorResult(outputTableMapping, planLimitInfo2, batchRestartIndicator2, planColumnSortItem, intermediateMemberDiscardConditions2, (groupSyncApplier != null) ? groupSyncApplier.CalculationsWithSharedValues : null);
			}
			case BatchPlanningStrategy.SecondaryOnly:
			{
				PlanLimitInfo planLimitInfo;
				OutputTableMapping outputTableMapping2 = this.GenerateSecondaryOnlyTables(dsContext, instanceFiltersContext, aggregatesInputTableCollector, intermediateMemberDiscardConditions, omitOrderBy, suppressCoreTableUnconstrainedJoinCheck, out planLimitInfo);
				PlanLimitInfo planLimitInfo3 = planLimitInfo;
				BatchRestartIndicator batchRestartIndicator3 = null;
				PlanColumnSortItem planColumnSortItem2 = null;
				IntermediateMemberDiscardConditions intermediateMemberDiscardConditions3 = intermediateMemberDiscardConditions;
				GroupSynchronizationApplier groupSyncApplier2 = this.GroupSyncApplier;
				return new PlanOperationTreeGeneratorResult(outputTableMapping2, planLimitInfo3, batchRestartIndicator3, planColumnSortItem2, intermediateMemberDiscardConditions3, (groupSyncApplier2 != null) ? groupSyncApplier2.CalculationsWithSharedValues : null);
			}
			case BatchPlanningStrategy.PrimaryAndSecondary:
			{
				PlanLimitInfo planLimitInfo;
				PlanColumnSortItem planColumnSortItem3;
				OutputTableMapping outputTableMapping3 = this.GeneratePrimaryAndSecondaryTables(dsContext, instanceFiltersContext, aggregatesInputTableCollector, intermediateMemberDiscardConditions, suppressCoreTableUnconstrainedJoinCheck, out planLimitInfo, out planColumnSortItem3);
				PlanLimitInfo planLimitInfo4 = planLimitInfo;
				BatchRestartIndicator batchRestartIndicator4 = null;
				PlanColumnSortItem planColumnSortItem4 = planColumnSortItem3;
				IntermediateMemberDiscardConditions intermediateMemberDiscardConditions4 = intermediateMemberDiscardConditions;
				GroupSynchronizationApplier groupSyncApplier3 = this.GroupSyncApplier;
				return new PlanOperationTreeGeneratorResult(outputTableMapping3, planLimitInfo4, batchRestartIndicator4, planColumnSortItem4, intermediateMemberDiscardConditions4, (groupSyncApplier3 != null) ? groupSyncApplier3.CalculationsWithSharedValues : null);
			}
			default:
				Contract.RetailFail("Unrecognized BatchTranslationStrategy: {0}", translationStrategy);
				return null;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003959C File Offset: 0x0003779C
		private OutputTableMapping GenerateDetailTables(DataShapeContext dsContext, AggregatesInputTableCollector aggregatesInputTableCollector, InstanceFiltersContext instanceFiltersContext, bool suppressCoreTableUnconstrainedJoinCheck)
		{
			Contract.RetailAssert(!dsContext.HasAnyVisualCalculations, "VisualCalculations are not supported for detail tables.");
			ReadOnlyCollection<PlanOperation> contextTables = this.m_attributeFilterContextTableManager.GetContextTables(dsContext.DataShape);
			PlanOperation planOperation = DetailTableBuilder.CreateDetailTable(this.m_context, dsContext, contextTables, this.m_outputExpressionTable);
			OutputTableMapping outputTableMapping = new OutputTableMapping();
			if (dsContext.HasDataShapeAggregatesAndProjections)
			{
				dsContext.DataShapeAggregatesAndProjections.Where((Calculation c) => this.m_context.Annotations.IsMeasure(c)).Evaluate<Calculation>();
				this.CreateAggregateOnlyTableAndOutputTableMapping(dsContext, contextTables, aggregatesInputTableCollector, outputTableMapping, instanceFiltersContext, suppressCoreTableUnconstrainedJoinCheck);
			}
			PlanOperationContext planOperationContext = new PlanOperationContext(planOperation, dsContext.DataShape.AsReadOnlyList<DataShape>(), null);
			outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.DetailTable(dsContext.Id), planOperationContext, false, false, false), dsContext.DataShape, dsContext.FirstPrimaryMember);
			return outputTableMapping;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00039654 File Offset: 0x00037854
		private OutputTableMapping GenerateAggregatesOnlyTables(DataShapeContext dsContext, AggregatesInputTableCollector aggregatesInputTableCollector, InstanceFiltersContext instanceFiltersContext, bool suppressCoreTableUnconstrainedJoinCheck)
		{
			IReadOnlyList<PlanOperation> contextTables = this.m_attributeFilterContextTableManager.GetContextTables(dsContext.DataShape);
			PlanOperationClearDefaultContext planOperationClearDefaultContext = this.m_context.DefaultValueContextManager.ToPlanOperation(dsContext.DataShape);
			IReadOnlyList<PlanOperation> readOnlyList = contextTables;
			if (planOperationClearDefaultContext != null)
			{
				readOnlyList = new List<PlanOperation>(readOnlyList) { planOperationClearDefaultContext };
			}
			OutputTableMapping outputTableMapping = new OutputTableMapping();
			this.CreateAggregateOnlyTableAndOutputTableMapping(dsContext, readOnlyList, aggregatesInputTableCollector, outputTableMapping, instanceFiltersContext, suppressCoreTableUnconstrainedJoinCheck);
			return outputTableMapping;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000396B0 File Offset: 0x000378B0
		private OutputTableMapping GenerateSecondaryOnlyTables(DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, IntermediateMemberDiscardConditions memberDiscardConditions, bool omitOrderBy, bool suppressCoreTableUnconstrainedJoinCheck, out PlanLimitInfo limitInfo)
		{
			global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts> valueTuple = this.CreateCoreTable(dsContext, instanceFiltersContext, aggregatesInputTableCollector, suppressCoreTableUnconstrainedJoinCheck);
			PlanOperationContext planOperationContext = valueTuple.Item1;
			CoreTableArtifacts item = valueTuple.Item2;
			planOperationContext = this.ApplyVisualCalculations(dsContext, planOperationContext);
			planOperationContext = this.RemoveContextOnlyCalculations(dsContext, planOperationContext);
			planOperationContext = this.ApplyInstanceFiltersToCoreTable(planOperationContext, dsContext, instanceFiltersContext, aggregatesInputTableCollector, true);
			PlanOperationContext planOperationContext2 = this.m_hierarchyPlanner.ApplySecondaryHierarchyPreLimitOperations(this.m_context, dsContext, instanceFiltersContext, this.m_attributeFilterContextTableManager, planOperationContext, aggregatesInputTableCollector, item, planOperationContext.RowScopes);
			LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(this.m_context.OutputExpressionTable, this.m_context.ErrorContext);
			PlanLimitInfoBuilder planLimitInfoBuilder = new PlanLimitInfoBuilder();
			planOperationContext2 = LimitApplier.ApplyHierarchyStaticLimitsAndWindows(this.m_context, this.m_declarations, planOperationContext2, dsContext, memberDiscardConditions, limitMetadataTableBuilder, planLimitInfoBuilder, this.GroupSyncApplier, false, true);
			limitInfo = planLimitInfoBuilder.ToLimitInfo();
			PlanOperationContext planOperationContext3 = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
			if (BatchDataSetPlanningUtils.DetermineScopedMeasureShowAllRestorationMode(dsContext) == ScopedMeasureShowAllRestorationMode.PostLimit)
			{
				planOperationContext2 = BatchDataSetPlanningUtils.RestoreScopedMeasureShowAllValues(dsContext, item.CoreTableFragments, planOperationContext2, this.m_declarations, planOperationContext.RowScopes);
			}
			global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext> valueTuple2 = this.GroupSyncApplier.ProcessSyncDataShapes(dsContext, this.m_declarations, this.m_outputExpressionTable, planOperationContext2, true);
			List<PlanNamedTableContext> item2 = valueTuple2.Item1;
			planOperationContext2 = valueTuple2.Item2;
			if (!omitOrderBy)
			{
				planOperationContext2 = planOperationContext2.SortBy(dsContext.SecondaryDynamicsExcludingContextOnly.ToSortItems(this.m_context.Annotations, true));
			}
			PlanOperationContext planOperationContext4 = null;
			if (dsContext.HasDataShapeAggregatesAndProjections)
			{
				planOperationContext4 = AggregatesTableBuilder.CreateAggregatesTable(this.m_context, dsContext, this.m_declarations, aggregatesInputTableCollector.References, item.ContextTables, this.m_outputExpressionTable, planOperationContext.RowScopes, dsContext.DataShapeAggregatesAndProjections.ToReadOnlyList<Calculation>(), dsContext.DataShape, this.m_context.TelemetryInfo);
			}
			OutputTableMapping outputTableMapping = new OutputTableMapping();
			if (planOperationContext3 != null)
			{
				outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.LimitMetadata(dsContext.Id), planOperationContext3, false, false, false), dsContext.SecondaryHierarchyLimits[0]);
			}
			this.AddSyncTablesToOutput(item2, outputTableMapping);
			if (planOperationContext4 != null)
			{
				outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.Aggregates(dsContext.Id), planOperationContext4, false, false, false), dsContext.DataShape);
			}
			string text = PlanNames.Body(dsContext.Id);
			outputTableMapping.AddOutputTable(new PlanNamedTableContext(text, planOperationContext2, false, false, false), dsContext.DataShape, dsContext.FirstSecondaryMember);
			memberDiscardConditions.FixupOutputTable(dsContext.SecondaryDynamicsExcludingContextOnly, text);
			return outputTableMapping;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000398D4 File Offset: 0x00037AD4
		private OutputTableMapping GeneratePrimaryOnlyTables(DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, IntermediateMemberDiscardConditions memberDiscardConditions, bool omitOrderBy, bool suppressCoreTableUnconstrainedJoinCheck, out PlanLimitInfo limitInfo, out BatchRestartIndicator restartIndicator)
		{
			global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts> valueTuple = this.CreateCoreTable(dsContext, instanceFiltersContext, aggregatesInputTableCollector, suppressCoreTableUnconstrainedJoinCheck);
			PlanOperationContext planOperationContext = valueTuple.Item1;
			CoreTableArtifacts item = valueTuple.Item2;
			planOperationContext = this.ApplyVisualCalculations(dsContext, planOperationContext);
			planOperationContext = this.RemoveContextOnlyCalculations(dsContext, planOperationContext);
			planOperationContext = this.ApplyInstanceFiltersToCoreTable(planOperationContext, dsContext, instanceFiltersContext, aggregatesInputTableCollector, true);
			if (dsContext.HasGroupingStructureAggregates)
			{
				planOperationContext = this.ApplyScopedAggregatesTransform(dsContext, this.m_declarations, planOperationContext, aggregatesInputTableCollector, true);
			}
			PlanOperationContext planOperationContext2 = this.m_hierarchyPlanner.ApplyPrimaryHierarchyPreLimitOperations(this.m_context, this.m_context.ScopeTree, this.m_context.SortByMeasureExpressionMappings, dsContext, instanceFiltersContext, this.m_attributeFilterContextTableManager, planOperationContext, planOperationContext, aggregatesInputTableCollector, item);
			planOperationContext2 = DataTransformApplier.ApplyDataTransforms(planOperationContext2, this.m_declarations, this.m_context.TransformReferenceMap, dsContext.DataShape.Transforms, aggregatesInputTableCollector, this.m_context.ApplyTransformsInQuery, this.m_context.GenerateComposableQueryColumnNames);
			PlanOperationContext planOperationContext3;
			planOperationContext2 = LimitApplier.ApplyPrimaryHierarchyOnlyLimitsAndWindows(this.m_context, this.m_declarations, planOperationContext2, item.JoinPredicates, dsContext, this.m_context.TelemetryInfo, memberDiscardConditions, this.GroupSyncApplier, out limitInfo, out planOperationContext3, out restartIndicator);
			if (BatchDataSetPlanningUtils.DetermineScopedMeasureShowAllRestorationMode(dsContext) == ScopedMeasureShowAllRestorationMode.PostLimit)
			{
				planOperationContext2 = BatchDataSetPlanningUtils.RestoreScopedMeasureShowAllValues(dsContext, item.CoreTableFragments, planOperationContext2, this.m_declarations, planOperationContext.RowScopes);
			}
			global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext> valueTuple2 = this.GroupSyncApplier.ProcessSyncDataShapes(dsContext, this.m_declarations, this.m_outputExpressionTable, planOperationContext2, true);
			List<PlanNamedTableContext> item2 = valueTuple2.Item1;
			planOperationContext2 = valueTuple2.Item2;
			if (this.m_context.GenerateComposableQueryColumnNames)
			{
				planOperationContext2 = planOperationContext2.EnsureUniqueUnqualifiedNames(false);
			}
			if (!omitOrderBy)
			{
				planOperationContext2 = planOperationContext2.SortBy(dsContext.PrimaryDynamicsExcludingContextOnly.ToSortItems(this.m_context.Annotations, true));
			}
			PlanOperationContext planOperationContext4 = null;
			if (dsContext.HasDataShapeAggregatesAndProjections)
			{
				planOperationContext4 = AggregatesTableBuilder.CreateAggregatesTable(this.m_context, dsContext, this.m_declarations, aggregatesInputTableCollector.References, item.ContextTables, this.m_outputExpressionTable, planOperationContext.RowScopes, dsContext.DataShapeAggregatesAndProjections.ToReadOnlyList<Calculation>(), dsContext.DataShape, this.m_context.TelemetryInfo);
			}
			OutputTableMapping outputTableMapping = new OutputTableMapping();
			if (planOperationContext3 != null)
			{
				outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.LimitMetadata(dsContext.Id), planOperationContext3, false, false, false), dsContext.PrimaryHierarchyLimits[0]);
			}
			this.AddSyncTablesToOutput(item2, outputTableMapping);
			if (planOperationContext4 != null)
			{
				outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.Aggregates(dsContext.DataShape.Id), planOperationContext4, false, false, false), dsContext.DataShape);
			}
			string text = PlanNames.Body(dsContext.Id);
			outputTableMapping.AddOutputTable(new PlanNamedTableContext(text, planOperationContext2, false, false, false), dsContext.DataShape, dsContext.FirstPrimaryMember);
			memberDiscardConditions.FixupOutputTable(dsContext.PrimaryDynamicsExcludingContextOnly, text);
			return outputTableMapping;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00039B48 File Offset: 0x00037D48
		private OutputTableMapping GeneratePrimaryAndSecondaryTables(DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, IntermediateMemberDiscardConditions memberDiscardConditions, bool suppressCoreTableUnconstrainedJoinCheck, out PlanLimitInfo limitInfo, out PlanColumnSortItem columnIndexSort)
		{
			limitInfo = null;
			columnIndexSort = null;
			global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts> valueTuple = this.CreateCoreTable(dsContext, instanceFiltersContext, aggregatesInputTableCollector, suppressCoreTableUnconstrainedJoinCheck);
			PlanOperationContext planOperationContext = valueTuple.Item1;
			CoreTableArtifacts item = valueTuple.Item2;
			planOperationContext = this.ApplyVisualCalculations(dsContext, planOperationContext);
			planOperationContext = this.RemoveContextOnlyCalculations(dsContext, planOperationContext);
			bool flag = instanceFiltersContext.QueryStageForInstanceFilters > QueryStageForInstanceFilters.None;
			planOperationContext = this.ApplyInstanceFiltersToCoreTable(planOperationContext, dsContext, instanceFiltersContext, aggregatesInputTableCollector, flag);
			PlanOperationContext planOperationContext2 = this.RemoveSortByMeasureTotals(dsContext, planOperationContext);
			if (!flag)
			{
				aggregatesInputTableCollector.RegisterCoreNoInstanceFiltersTable(dsContext, planOperationContext2);
			}
			PlanOperationContext planOperationContext3 = null;
			string columnIndex = PlanNames.ColumnIndex;
			IEnumerable<PlanSortItem> enumerable = dsContext.PrimaryDynamicsExcludingContextOnly.ToSortItems(this.m_context.Annotations, true);
			PlanOperationContext planOperationContext5;
			PlanOperationContext planOperationContext4;
			if (dsContext.HasBinnedLineSampleLimit || dsContext.HasOverlappingPointsSampleLimit)
			{
				planOperationContext4 = this.ApplyAdvancedLimitOperations(dsContext, item, planOperationContext2, instanceFiltersContext, aggregatesInputTableCollector, columnIndex, out limitInfo, out columnIndexSort, out planOperationContext5, out planOperationContext3);
				enumerable = enumerable.Concat(new PlanSortItem[] { columnIndexSort });
			}
			else
			{
				Identifier identifier;
				planOperationContext4 = this.ApplyIndividualHierarchyLimitsOperations(planOperationContext, dsContext, item, planOperationContext2, instanceFiltersContext, aggregatesInputTableCollector, memberDiscardConditions, columnIndex, out limitInfo, out columnIndexSort, out planOperationContext5, out planOperationContext3, out identifier);
				enumerable = enumerable.Concat(new PlanSortItem[] { columnIndexSort });
				planOperationContext4 = BatchDataSetPlanningUtils.ApplyInnermostIntersectionLimit(this.m_context, dsContext, planOperationContext4, enumerable, identifier);
			}
			global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext> valueTuple2 = this.GroupSyncApplier.ProcessSyncDataShapes(dsContext, this.m_declarations, this.m_outputExpressionTable, planOperationContext5, false);
			List<PlanNamedTableContext> item2 = valueTuple2.Item1;
			planOperationContext5 = valueTuple2.Item2.SortBy(dsContext.SecondaryDynamicsExcludingContextOnly.ToSortItems(this.m_context.Annotations, true));
			global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext> valueTuple3 = this.GroupSyncApplier.ProcessSyncDataShapes(dsContext, this.m_declarations, this.m_outputExpressionTable, planOperationContext4, true);
			List<PlanNamedTableContext> item3 = valueTuple3.Item1;
			planOperationContext4 = valueTuple3.Item2.SortBy(enumerable);
			PlanOperationContext planOperationContext6 = null;
			if (dsContext.HasDataShapeAggregatesAndProjections)
			{
				planOperationContext6 = AggregatesTableBuilder.CreateAggregatesTable(this.m_context, dsContext, this.m_declarations, aggregatesInputTableCollector.References, item.ContextTables, this.m_outputExpressionTable, planOperationContext.RowScopes, dsContext.DataShapeAggregatesAndProjections.ToReadOnlyList<Calculation>(), dsContext.DataShape, this.m_context.TelemetryInfo);
			}
			return this.PrepareOutputTableMappingsForPrimaryAndSecondaryPlan(dsContext, memberDiscardConditions, planOperationContext6, planOperationContext4, planOperationContext5, planOperationContext3, item2, item3);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00039D38 File Offset: 0x00037F38
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "CoreTable", "coreArtifacts" })]
		private global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts> CreateCoreTable(DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, bool suppressUnconstrainedJoinCheck)
		{
			if (this.m_subqueryGenerator.ShouldPerformSubqueryRegrouping(dsContext))
			{
				global::System.ValueTuple<DataShapeContext, PlanOperationContext> valueTuple = this.m_subqueryGenerator.GenerateSubqueryTables(dsContext).Single("Only 1 subquery is allowed for regrouping", Array.Empty<string>());
				PlanOperationContext item = valueTuple.Item2;
				aggregatesInputTableCollector.RegisterSubqueryReferenceTable(item, valueTuple.Item1.DataShape.Id.Value, dsContext.DataShape.Id.Value);
				return new global::System.ValueTuple<PlanOperationContext, CoreTableArtifacts>(this.m_subqueryGenerator.CreateCoreTableFromSubquery(item, valueTuple.Item1.DataShape.Id.Value, dsContext).DeclareIfNotDeclared(PlanNames.Core(dsContext.Id), this.m_declarations, false, null, false), CoreTableArtifacts.Empty);
			}
			return CoreTableBuilder.CreateCoreTable(this.m_context, dsContext, this.m_declarations, this.m_attributeFilterContextTableManager, instanceFiltersContext, suppressUnconstrainedJoinCheck);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00039E08 File Offset: 0x00038008
		private PlanOperationContext ApplyVisualCalculations(DataShapeContext dsContext, PlanOperationContext coreTable)
		{
			IReadOnlyList<Calculation> visualCalculations = dsContext.Annotations.GetVisualCalculations(dsContext.DataShape);
			if (visualCalculations.Count == 0)
			{
				return coreTable;
			}
			if (coreTable == null)
			{
				coreTable = new SingleRowPlaceholderTableBuilder(this.m_context, dsContext.DataShape, "Placeholder").ToTableContext();
			}
			List<ColumnWithExplicitName> list = dsContext.Annotations.GetCalculationsWithNativeReferenceName(dsContext.DataShape).Select(delegate(Calculation calculation)
			{
				ReadOnlyCollection<ExpressionId> expressions = this.m_context.CalculationMap.GetExpressions(calculation);
				Contract.RetailAssert(expressions.Count == 1, "GetExpressions must return exactly one expression whenever that calcuation has a native reference name. It returned {0} number of expressions", expressions.Count);
				return new ColumnWithExplicitName(expressions[0], calculation.NativeReferenceName);
			}).ToList<ColumnWithExplicitName>();
			PlanOperationContext planOperationContext = this.CreateVisualCalculationReferenceableTable(dsContext.DataShape.Id, coreTable, list);
			List<Calculation> list2 = dsContext.Annotations.GetSubtotalCalculations(dsContext.DataShape).Where(delegate(Calculation calculation)
			{
				Calculation calculation2;
				Contract.RetailAssert(dsContext.Annotations.IsSubtotal(calculation, out calculation2), "The calculation {0} is not a subtotal but was returned by {1}.", calculation.Id, "GetSubtotalCalculations");
				return dsContext.Annotations.IsVisualCalculation(calculation2);
			}).ToList<Calculation>();
			List<VisualAxis> visualCalculationMetadata = dsContext.DataShape.VisualCalculationMetadata;
			string text = (visualCalculationMetadata.IsNullOrEmpty<VisualAxis>() ? null : PlanNames.IsDensifiedRow);
			planOperationContext = this.AddVisualCalculations(dsContext.DataShape.Id, planOperationContext, visualCalculationMetadata, visualCalculations, list2, text);
			if (text != null)
			{
				planOperationContext = this.RemoveUnneededVisualCalcsDensifiedRows(dsContext.DataShape.Id, planOperationContext, visualCalculations, text);
			}
			return this.RemoveContextOnlyLevels(dsContext, planOperationContext);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00039F64 File Offset: 0x00038164
		private PlanOperationContext RemoveContextOnlyLevels(DataShapeContext dsContext, PlanOperationContext planOperationContext)
		{
			if (!dsContext.HasAnyContextOnlyDataMembers)
			{
				return planOperationContext;
			}
			List<Calculation> list = new List<Calculation>();
			List<PlanProjectItem> list2 = new List<PlanProjectItem>();
			List<DataMember> filteringMetadataMembersToExclude = new List<DataMember>();
			List<FilterCondition> list3 = null;
			this.RemoveTopLevelContextOnlySubtotal(dsContext, true, ref list3, ref list2, ref list);
			this.RemoveTopLevelContextOnlySubtotal(dsContext, false, ref list3, ref list2, ref list);
			foreach (DataMember dataMember in dsContext.AllDynamics)
			{
				DataMember dataMember2 = dataMember.DynamicChildDataMemberOrDefault();
				if (dataMember2 != null)
				{
					BatchSubtotalAnnotation batchSubtotalAnnotation;
					bool flag = dsContext.Annotations.TryGetBatchSubtotalAnnotation(dataMember2, out batchSubtotalAnnotation);
					if (!dsContext.Annotations.DataMemberAnnotations.AreContentsIncludedInOutput(dataMember2))
					{
						filteringMetadataMembersToExclude.Add(dataMember2);
						list2.Add(new PlanDataMemberProjectItem(dataMember2, false));
						if (flag)
						{
							list2.Add(new PlanNamedColumnProjectItem(batchSubtotalAnnotation.SubtotalIndicatorColumnName));
							BinaryFilterCondition binaryFilterCondition = FilterUtils.CreateBooleanColumnFilterCondition(this.m_outputExpressionTable, batchSubtotalAnnotation.SubtotalIndicatorColumnName, true);
							Util.AddToLazyList<FilterCondition>(ref list3, binaryFilterCondition);
							if (batchSubtotalAnnotation.Usage == SubtotalUsage.VisualCalculations)
							{
								this.AddCalculationsToExclude(dataMember.StaticChildDataMemberOrDefault(), ref list2, ref list);
							}
						}
						this.AddCalculationsToExclude(dataMember2, ref list2, ref list);
					}
					else if (flag && batchSubtotalAnnotation.Usage == SubtotalUsage.VisualCalculations)
					{
						DataMember dataMember3 = dataMember.StaticChildDataMemberOrDefault();
						filteringMetadataMembersToExclude.Add(dataMember3);
						list2.Add(new PlanNamedColumnProjectItem(batchSubtotalAnnotation.SubtotalIndicatorColumnName));
						BinaryFilterCondition binaryFilterCondition2 = FilterUtils.CreateBooleanColumnFilterCondition(this.m_outputExpressionTable, batchSubtotalAnnotation.SubtotalIndicatorColumnName, false);
						BatchSubtotalAnnotation batchSubtotalAnnotation2;
						if (dsContext.Annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation2))
						{
							BinaryFilterCondition binaryFilterCondition3 = FilterUtils.CreateBooleanColumnFilterCondition(this.m_outputExpressionTable, batchSubtotalAnnotation2.SubtotalIndicatorColumnName, true);
							CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
							{
								Conditions = new List<FilterCondition> { binaryFilterCondition2, binaryFilterCondition3 },
								Operator = CompoundFilterOperator.Any
							};
							Util.AddToLazyList<FilterCondition>(ref list3, compoundFilterCondition);
						}
						else
						{
							Util.AddToLazyList<FilterCondition>(ref list3, binaryFilterCondition2);
						}
						this.AddCalculationsToExclude(dataMember3, ref list2, ref list);
					}
				}
			}
			if (!dsContext.DataShape.DataRows.IsNullOrEmpty<DataRow>())
			{
				foreach (DataRow dataRow in dsContext.DataShape.DataRows)
				{
					foreach (DataIntersection dataIntersection in dataRow.Intersections)
					{
						if (!dsContext.Annotations.AreContentsIncludedInOutput(dataIntersection))
						{
							foreach (Calculation calculation in dataIntersection.Calculations)
							{
								list2.Add(new PlanCalculationProjectItem(calculation));
								list.Add(calculation);
							}
						}
					}
				}
			}
			IReadOnlyList<Calculation> readOnlyList = planOperationContext.Calculations.Except(list).ToReadOnlyList<Calculation>();
			if (list3 != null)
			{
				FilterCondition filterCondition;
				if (list3.Count != 1)
				{
					CompoundFilterCondition compoundFilterCondition2 = new CompoundFilterCondition();
					compoundFilterCondition2.Conditions = list3;
					filterCondition = compoundFilterCondition2;
					compoundFilterCondition2.Operator = CompoundFilterOperator.All;
				}
				else
				{
					filterCondition = list3[0];
				}
				FilterCondition filterCondition2 = filterCondition;
				PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(planOperationContext.FilteringMetadata.TotalsMetadata.Where((SubtotalColumnFilteringMetadata m) => !filteringMetadataMembersToExclude.Contains(m.Member)).ToReadOnlyList<SubtotalColumnFilteringMetadata>(), false);
				planOperationContext = planOperationContext.FilterBy(filterCondition2, planOperationFilteringMetadata);
			}
			IScope innermostScopeExcludingContextOnly = dsContext.InnermostScopeExcludingContextOnly;
			List<IScope> list4 = dsContext.ScopeTree.GetAllParentScopes(innermostScopeExcludingContextOnly).ToList<IScope>();
			PlanOperation planOperation = planOperationContext.Table.Project(new PlanPreserveAllColumnsExceptProjectItem(list2), false);
			planOperationContext = planOperationContext.ReplaceTable(planOperation, list4, null, readOnlyList);
			return planOperationContext.DeclareIfNotDeclared(PlanNames.RemoveContextOnlyLevels(dsContext.DataShape.Id), this.m_declarations, false, null, false);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003A370 File Offset: 0x00038570
		private void RemoveTopLevelContextOnlySubtotal(DataShapeContext dsContext, bool isPrimary, ref List<FilterCondition> conditions, ref List<PlanProjectItem> projectItemsToExclude, ref List<Calculation> calculationsToExclude)
		{
			DataMember dataMember = (isPrimary ? dsContext.DataShape.PrimaryHierarchy : dsContext.DataShape.SecondaryHierarchy).StaticChildDataMemberOrDefault();
			if (dataMember == null)
			{
				return;
			}
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (dsContext.Annotations.TryGetBatchSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation) && !batchSubtotalAnnotation.Usage.IsIncludeInOutput())
			{
				projectItemsToExclude.Add(new PlanNamedColumnProjectItem(batchSubtotalAnnotation.SubtotalIndicatorColumnName));
				BinaryFilterCondition binaryFilterCondition = FilterUtils.CreateBooleanColumnFilterCondition(this.m_outputExpressionTable, batchSubtotalAnnotation.SubtotalIndicatorColumnName, false);
				Util.AddToLazyList<FilterCondition>(ref conditions, binaryFilterCondition);
				this.AddCalculationsToExclude(dataMember, ref projectItemsToExclude, ref calculationsToExclude);
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003A3F8 File Offset: 0x000385F8
		private void AddCalculationsToExclude(DataMember dataMember, ref List<PlanProjectItem> projectItemsToExclude, ref List<Calculation> calculationsToExclude)
		{
			if (!dataMember.Calculations.IsNullOrEmpty<Calculation>())
			{
				foreach (Calculation calculation in dataMember.Calculations)
				{
					projectItemsToExclude.Add(new PlanCalculationProjectItem(calculation));
					calculationsToExclude.Add(calculation);
				}
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003A468 File Offset: 0x00038668
		private PlanOperationContext AddVisualCalculations(Identifier dataShapeId, PlanOperationContext planOperationContext, IReadOnlyList<VisualAxis> visualAxes, IReadOnlyList<Calculation> visualCalculations, IReadOnlyList<Calculation> subtotalsOverVisualCalculations, string isDensifiedColumnName)
		{
			string text = PlanNames.VisualCalcs(dataShapeId);
			this.m_declarations.DeclareEntity(text, planOperationContext.Table, new PlanVisualShape(visualAxes, isDensifiedColumnName), visualCalculations, subtotalsOverVisualCalculations);
			PlanOperation planOperation = PlanOperationBuilder.TableScan(text, Util.EmptyReadOnlyList<ExpressionId>());
			List<Calculation> list = new List<Calculation>(visualCalculations.Count + planOperationContext.Calculations.Count + subtotalsOverVisualCalculations.Count);
			list.AddRange(planOperationContext.Calculations);
			list.AddRange(visualCalculations);
			list.AddRange(subtotalsOverVisualCalculations);
			return planOperationContext.ReplaceTable(planOperation, null, null, list);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003A4F0 File Offset: 0x000386F0
		private PlanOperationContext CreateVisualCalculationReferenceableTable(Identifier dataShapeId, PlanOperationContext planOperationContext, IReadOnlyList<ColumnWithExplicitName> columnsWithNativeReferenceNames)
		{
			planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.Core(dataShapeId), this.m_declarations, false, null, false);
			PlanOperation planOperation = planOperationContext.Table.ReferencableFromVisualCalculation(columnsWithNativeReferenceNames);
			return planOperationContext.ReplaceTable(planOperation, null, null, null).DeclareIfNotDeclared(PlanNames.VisualCalcsInput(dataShapeId), this.m_declarations, false, null, false);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003A540 File Offset: 0x00038740
		private PlanOperationContext RemoveUnneededVisualCalcsDensifiedRows(Identifier dataShapeId, PlanOperationContext visualCalcsTable, IReadOnlyList<Calculation> visualCalculations, string isDensifiedColumnName)
		{
			ExpressionNode expressionNode = ExprNodes.BatchColumnReference(isDensifiedColumnName).Not();
			foreach (Calculation calculation in visualCalculations)
			{
				UnaryOperatorExpressionNode unaryOperatorExpressionNode = ExprNodes.CalculationReference(calculation).IsNull(FunctionUsageKind.Query).Not();
				expressionNode = expressionNode.Or(unaryOperatorExpressionNode);
			}
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.DataShape, "VisualCalcs", "IsDensified");
			PlanExpression planExpression = new PlanExpression(expressionNode, expressionContext);
			return visualCalcsTable.FilterBy(planExpression).DeclareIfNotDeclared(PlanNames.RemoveEmptyDensified(dataShapeId), this.m_declarations, false, null, false);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003A5F0 File Offset: 0x000387F0
		private PlanOperationContext RemoveContextOnlyCalculations(DataShapeContext dsContext, PlanOperationContext coreTable)
		{
			if (coreTable == null || !dsContext.HasAnyContextOnlyCalculations)
			{
				return coreTable;
			}
			List<PlanCalculationProjectItem> list = new List<PlanCalculationProjectItem>();
			List<Calculation> list2 = new List<Calculation>();
			foreach (Calculation calculation in coreTable.Calculations)
			{
				if (calculation.IsContextOnly)
				{
					list.Add(new PlanCalculationProjectItem(calculation));
				}
				else
				{
					list2.Add(calculation);
				}
			}
			if (list.IsNullOrEmpty<PlanCalculationProjectItem>())
			{
				return coreTable;
			}
			PlanPreserveAllColumnsExceptProjectItem planPreserveAllColumnsExceptProjectItem = new PlanPreserveAllColumnsExceptProjectItem(list);
			coreTable = coreTable.ReplaceTable(coreTable.Table.Project(planPreserveAllColumnsExceptProjectItem, false), null, null, list2);
			return coreTable.DeclareIfNotDeclared(PlanNames.RemoveContextOnlyColumns(dsContext.DataShape.Id), this.m_declarations, false, null, false);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003A6B8 File Offset: 0x000388B8
		private PlanOperationContext ApplyInstanceFiltersToCoreTable(PlanOperationContext coreTable, DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, bool registerCoreNoInstanceFiltersForAggregates = true)
		{
			PlanOperationContext planOperationContext = coreTable;
			if (instanceFiltersContext.ShouldApplyInstanceFiltersPostFilterToCoreTable)
			{
				planOperationContext = coreTable.DeclareIfNotDeclared(PlanNames.CoreNoInstanceFilters(dsContext.Id), this.m_declarations, false, null, false);
				coreTable = BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(this.m_context.OutputExpressionTable, this.m_context.ErrorContext, this.m_context.Annotations, planOperationContext, null);
			}
			bool flag = coreTable == planOperationContext;
			if (flag)
			{
				coreTable = coreTable.DeclareIfNotDeclared(PlanNames.Core(dsContext.Id), this.m_declarations, false, null, false);
			}
			else
			{
				coreTable = coreTable.DeclareIfNotDeclared(PlanNames.CoreWithInstanceFilters(dsContext.Id), this.m_declarations, false, null, false);
			}
			if (flag)
			{
				planOperationContext = coreTable;
			}
			if (registerCoreNoInstanceFiltersForAggregates)
			{
				aggregatesInputTableCollector.RegisterCoreNoInstanceFiltersTable(dsContext, planOperationContext);
			}
			aggregatesInputTableCollector.RegisterCoreWithInstanceFiltersTable(dsContext, coreTable);
			return coreTable;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003A770 File Offset: 0x00038970
		private void CreateAggregateOnlyTableAndOutputTableMapping(DataShapeContext dsContext, IReadOnlyList<PlanOperation> contextTables, AggregatesInputTableCollector aggregatesInputTableCollector, OutputTableMapping outputTableMapping, InstanceFiltersContext instanceFiltersContext, bool suppressCoreTableUnconstrainedJoinCheck)
		{
			PlanOperationContext planOperationContext;
			IReadOnlyList<Calculation> readOnlyList;
			if (!dsContext.DataShape.Transforms.IsNullOrEmpty<DataTransform>() && this.m_context.ApplyTransformsInQuery)
			{
				planOperationContext = DataTransformApplier.ApplyDataTransforms(this.CreateCoreTable(dsContext, instanceFiltersContext, aggregatesInputTableCollector, suppressCoreTableUnconstrainedJoinCheck).Item1.DeclareIfNotDeclared(PlanNames.Core(dsContext.Id), this.m_declarations, false, null, false), this.m_declarations, this.m_context.TransformReferenceMap, dsContext.DataShape.Transforms, aggregatesInputTableCollector, this.m_context.ApplyTransformsInQuery, this.m_context.GenerateComposableQueryColumnNames);
				readOnlyList = dsContext.DataShapeAggregatesAndProjections.Except(planOperationContext.Calculations).ToList<Calculation>();
			}
			else
			{
				planOperationContext = null;
				readOnlyList = dsContext.DataShapeAggregatesAndProjections;
				if (dsContext.InputSubqueryDataShapes.Any<DataShape>())
				{
					foreach (global::System.ValueTuple<DataShapeContext, PlanOperationContext> valueTuple in this.m_subqueryGenerator.GenerateSubqueryTables(dsContext))
					{
						aggregatesInputTableCollector.RegisterSubqueryReferenceTable(valueTuple.Item2, valueTuple.Item1.DataShape.Id.Value, dsContext.DataShape.Id.Value);
					}
				}
			}
			PlanOperationContext planOperationContext2 = AggregatesTableBuilder.CreateAggregatesTable(this.m_context, dsContext, this.m_declarations, aggregatesInputTableCollector.References, contextTables, this.m_outputExpressionTable, new RowScopesMetadata(dsContext.RowScopes), readOnlyList.ToReadOnlyList<Calculation>(), dsContext.DataShape, this.m_context.TelemetryInfo);
			planOperationContext2 = this.ApplyVisualCalculations(dsContext, planOperationContext2);
			planOperationContext2 = this.RemoveContextOnlyCalculations(dsContext, planOperationContext2);
			planOperationContext2 = AggregatesTableBuilder.JoinAggregatesTable(dsContext, this.m_declarations, new RowScopesMetadata(dsContext.RowScopes), dsContext.DataShapeAggregatesAndProjections, dsContext.DataShape, planOperationContext, planOperationContext2, readOnlyList.ToReadOnlyList<Calculation>());
			if (planOperationContext2 != null)
			{
				outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.Aggregates(dsContext.Id), planOperationContext2, false, false, false), dsContext.DataShape);
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003A948 File Offset: 0x00038B48
		private PlanOperationContext ApplyAdvancedLimitOperations(DataShapeContext dsContext, CoreTableArtifacts coreTableArtifacts, PlanOperationContext coreTableOnlyOutputTotals, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, string indexColumnName, out PlanLimitInfo limitInfo, out PlanColumnSortItem columnIndexSort, out PlanOperationContext secondaryTable, out PlanOperationContext limitMetadataContext)
		{
			LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(this.m_outputExpressionTable, this.m_context.ErrorContext);
			PlanLimitInfoBuilder planLimitInfoBuilder = new PlanLimitInfoBuilder();
			PlanOperationContext planOperationContext;
			if (dsContext.HasBinnedLineSampleLimit)
			{
				planOperationContext = BatchDataSetPlannerBinnedLineSampleLimitTranslator.Translate(this.m_context, this.m_declarations, dsContext, coreTableOnlyOutputTotals, dsContext.IntersectionLimit, limitMetadataTableBuilder, planLimitInfoBuilder);
				planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.BodyBinnedSample(dsContext.Id), this.m_declarations, false, null, false);
			}
			else
			{
				planOperationContext = BatchDataSetPlannerOverlappingPointsSampleLimitTranslator.Translate(this.m_context, this.m_declarations, this.m_outputExpressionTable, dsContext, coreTableOnlyOutputTotals, dsContext.IntersectionLimit, limitMetadataTableBuilder, planLimitInfoBuilder);
				planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.BodyOverlappingPointsSample(dsContext.Id), this.m_declarations, false, null, false);
			}
			secondaryTable = this.CreateSecondaryTableBase(dsContext, planOperationContext, coreTableArtifacts, instanceFiltersContext, aggregatesInputTableCollector);
			ExpressionNode expressionNode = secondaryTable.CountRows();
			expressionNode = expressionNode.DeclareIfNotDeclared(PlanNames.SecondaryCount(dsContext.Id), this.m_declarations, this.m_context.ErrorContext, ObjectType.Limit, "Secondary");
			ExpressionId expressionId = limitMetadataTableBuilder.AddColumn(PlanNames.SecondaryCount(dsContext.Id), expressionNode, ObjectType.Limit, "Secondary");
			planLimitInfoBuilder.AddTelemetryItem(new LimitTelemetryItem("SecCount", expressionId));
			planOperationContext = this.JoinPrimaryAndSecondaryTables(dsContext, planOperationContext, null, secondaryTable, indexColumnName, false, out columnIndexSort);
			limitMetadataContext = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
			limitInfo = planLimitInfoBuilder.ToLimitInfo();
			return planOperationContext;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003AA98 File Offset: 0x00038C98
		private PlanOperationContext ApplyIndividualHierarchyLimitsOperations(PlanOperationContext coreTable, DataShapeContext dsContext, CoreTableArtifacts coreTableArtifacts, PlanOperationContext coreTableOnlyOutputTotals, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, IntermediateMemberDiscardConditions memberDiscardConditions, string indexColumnName, out PlanLimitInfo limitInfo, out PlanColumnSortItem columnIndexSort, out PlanOperationContext secondaryTable, out PlanOperationContext limitMetadataContext, out Identifier suppressedLimitId)
		{
			limitInfo = null;
			limitMetadataContext = null;
			suppressedLimitId = null;
			PlanOperationContext planOperationContext2;
			PlanOperationContext planOperationContext;
			if (dsContext.DataShape.DynamicLimits != null || dsContext.HasAnyScopedLimits)
			{
				planOperationContext = this.ApplyDynamicAndScopedLimitsInParallel(coreTable, dsContext, coreTableArtifacts, coreTableOnlyOutputTotals, instanceFiltersContext, aggregatesInputTableCollector, memberDiscardConditions, out planOperationContext2, out secondaryTable, out limitInfo, out limitMetadataContext, out suppressedLimitId);
			}
			else
			{
				planOperationContext = this.ApplyStaticStructuralLimitsInSequence(coreTable, dsContext, coreTableArtifacts, coreTableOnlyOutputTotals, instanceFiltersContext, aggregatesInputTableCollector, out planOperationContext2, out secondaryTable);
			}
			if (BatchDataSetPlanningUtils.DetermineScopedMeasureShowAllRestorationMode(dsContext) == ScopedMeasureShowAllRestorationMode.PostLimit)
			{
				if (dsContext.HasAnyPrimaryDynamic)
				{
					planOperationContext2 = BatchDataSetPlanningUtils.RestoreScopedMeasureShowAllValues(dsContext, coreTableArtifacts.CoreTableFragments, planOperationContext2, this.m_declarations, coreTable.RowScopes);
				}
				secondaryTable = BatchDataSetPlanningUtils.RestoreScopedMeasureShowAllValues(dsContext, coreTableArtifacts.CoreTableFragments, secondaryTable, this.m_declarations, coreTable.RowScopes);
			}
			return this.JoinPrimaryAndSecondaryTables(dsContext, planOperationContext, planOperationContext2, secondaryTable, indexColumnName, planOperationContext2 != coreTable, out columnIndexSort);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003AB5C File Offset: 0x00038D5C
		private PlanOperationContext ApplyDynamicAndScopedLimitsInParallel(PlanOperationContext coreTable, DataShapeContext dsContext, CoreTableArtifacts coreTableArtifacts, PlanOperationContext coreTableOnlyOutputTotals, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, IntermediateMemberDiscardConditions memberDiscardConditions, out PlanOperationContext primaryTable, out PlanOperationContext secondaryTable, out PlanLimitInfo limitInfo, out PlanOperationContext limitMetadataContext, out Identifier suppressedLimitId)
		{
			PlanOperationContext planOperationContext = this.CreatePrimaryTableBase(coreTable, dsContext, coreTableArtifacts, instanceFiltersContext, aggregatesInputTableCollector);
			PlanOperationContext planOperationContext2 = this.CreateSecondaryTableBase(dsContext, coreTableOnlyOutputTotals, coreTableArtifacts, instanceFiltersContext, aggregatesInputTableCollector);
			PlanOperationContext planOperationContext3;
			if (dsContext.DataShape.DynamicLimits != null && !dsContext.HasAnyScopedLimits && dsContext.DataShape.DynamicLimits.Blocks == null)
			{
				this.m_context.TelemetryInfo.UsedDynamicLimits = true;
				DynamicLimitCounts dynamicLimitCounts = DynamicLimitsTablesBuilder.ApplyDynamicLimits(this.m_context.Annotations, this.m_context.ErrorContext, this.m_declarations, dsContext, coreTableOnlyOutputTotals, planOperationContext, planOperationContext2, out primaryTable, out secondaryTable);
				List<IntermediateTelemetryItem> list = null;
				if (BatchDataSetPlanningUtils.UseEnhancedSampling(dsContext))
				{
					KeyPointsTable keyPointsTable = EnhancedSamplingKeyPointsTableBuilder.CreatePrimaryEnhancedSamplingFromPrimaryAndSecondary(this.m_context, dsContext, this.m_context.TelemetryInfo, this.m_declarations, coreTableOnlyOutputTotals, secondaryTable, planOperationContext, coreTableArtifacts.JoinPredicates, in dynamicLimitCounts, out planOperationContext3, out list);
					primaryTable = keyPointsTable.ApplyAndDeclare(primaryTable, PlanNames.PrimaryKeyPoints(dsContext.Id));
				}
				else
				{
					planOperationContext3 = coreTableOnlyOutputTotals;
				}
				PlanOperationTreeGenerator.LimitMetadataTableInfo limitMetadataTableInfo = this.BuildLimitMetadataTable(dsContext, dynamicLimitCounts, list);
				limitMetadataContext = limitMetadataTableInfo.TableContext;
				suppressedLimitId = BatchDataSetPlanningUtils.FindSuppressedDynamicIntersectionLimit(dsContext, this.m_context.OutputExpressionTable);
				limitInfo = PlanOperationTreeGenerator.BuildPlanLimitInfo(dsContext, suppressedLimitId, limitMetadataTableInfo, list);
			}
			else
			{
				LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(this.m_context.OutputExpressionTable, this.m_context.ErrorContext);
				PlanLimitInfoBuilder planLimitInfoBuilder = new PlanLimitInfoBuilder();
				global::System.ValueTuple<PlanOperationContext, PlanOperationContext> valueTuple = LimitApplier.ApplyPrimaryAndSecondaryScopedLimits(this.m_context, this.m_declarations, dsContext, planOperationContext, planOperationContext2, this.GroupSyncApplier, memberDiscardConditions, limitMetadataTableBuilder, planLimitInfoBuilder);
				primaryTable = valueTuple.Item1;
				secondaryTable = valueTuple.Item2;
				planOperationContext3 = coreTableOnlyOutputTotals;
				suppressedLimitId = null;
				limitInfo = planLimitInfoBuilder.ToLimitInfo();
				limitMetadataContext = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
			}
			return planOperationContext3;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003AD04 File Offset: 0x00038F04
		private PlanOperationContext CreatePrimaryTableBase(PlanOperationContext coreTable, DataShapeContext dsContext, CoreTableArtifacts coreTableArtifacts, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector)
		{
			PlanOperationContext planOperationContext = BatchDataSetPlanningUtils.ApplyGroupByHierarchy(this.m_context.Annotations, coreTable, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryMembersExcludingContextOnly, dsContext.ScopeTree, SubtotalUsage.SortByMeasure, false, true);
			planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.PrimaryBase(dsContext.Id), this.m_declarations, false, null, false);
			return this.m_hierarchyPlanner.ApplyPrimaryHierarchyPreLimitOperations(this.m_context, this.m_context.ScopeTree, this.m_context.SortByMeasureExpressionMappings, dsContext, instanceFiltersContext, this.m_attributeFilterContextTableManager, coreTable, planOperationContext, aggregatesInputTableCollector, coreTableArtifacts);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003AD8C File Offset: 0x00038F8C
		private PlanOperationContext ApplyStaticStructuralLimitsInSequence(PlanOperationContext coreTable, DataShapeContext dsContext, CoreTableArtifacts coreTableArtifacts, PlanOperationContext coreTableOnlyOutputTotals, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector, out PlanOperationContext primaryTable, out PlanOperationContext secondaryTable)
		{
			PlanOperationContext planOperationContext;
			if (dsContext.HasPrimaryHierarchyLimit)
			{
				primaryTable = this.CreatePrimaryTableBase(coreTable, dsContext, coreTableArtifacts, instanceFiltersContext, aggregatesInputTableCollector);
				primaryTable = LimitApplier.ApplyTotalHierarchyLimit(this.m_context.Annotations, this.m_context.ErrorContext, dsContext, primaryTable, dsContext.PrimaryDynamicsExcludingContextOnly, null);
				primaryTable = primaryTable.DeclareIfNotDeclared(PlanNames.Primary(dsContext.Id), this.m_declarations, false, null, false);
				planOperationContext = primaryTable.InnerJoin(coreTableOnlyOutputTotals);
				planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.BodyWithLimitedPrimary(dsContext.Id), this.m_declarations, false, null, false);
				primaryTable = WindowTableBuilder.ApplyLegacyPrimaryHierarchySegmentation(primaryTable, this.m_declarations, this.m_context.ErrorContext, this.m_context.Annotations, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryHierarchyLimit, dsContext.DataShape.RequestedPrimaryLeafCount, dsContext.DataShape.RestartMatchingBehavior, dsContext.DataShape.Id);
				this.m_context.TelemetryInfo.UsedStaticLimits = true;
			}
			else
			{
				primaryTable = this.CreatePrimaryWithNoLimitTable(dsContext, this.m_declarations, coreTable, coreTableOnlyOutputTotals, coreTableArtifacts, instanceFiltersContext, aggregatesInputTableCollector, true);
				planOperationContext = coreTableOnlyOutputTotals;
			}
			secondaryTable = this.CreateSecondaryTableBase(dsContext, planOperationContext, coreTableArtifacts, instanceFiltersContext, aggregatesInputTableCollector);
			secondaryTable = LimitApplier.ApplyTotalHierarchyLimit(this.m_context.Annotations, this.m_context.ErrorContext, dsContext, secondaryTable, dsContext.SecondaryDynamicsExcludingContextOnly, null);
			secondaryTable = secondaryTable.DeclareIfNotDeclared(PlanNames.Secondary(dsContext.Id), this.m_declarations, false, null, false);
			return planOperationContext;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003AF00 File Offset: 0x00039100
		private PlanOperationContext RemoveSortByMeasureTotals(DataShapeContext dsContext, PlanOperationContext coreTable)
		{
			return CoreTableTotalsTransforms.RemoveUnneededSortByMeasureTotals(coreTable, BatchDataSetPlanningUtils.CreateCoreTableTransformContext(this.m_context.ScopeTree, this.m_context.Annotations, this.m_context.SortByMeasureExpressionMappings, this.m_outputExpressionTable)).DeclareIfNotDeclared(PlanNames.CoreOnlyOutputTotals(dsContext.Id), this.m_declarations, false, null, false);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003AF58 File Offset: 0x00039158
		private PlanOperationTreeGenerator.LimitMetadataTableInfo BuildLimitMetadataTable(DataShapeContext dsContext, DynamicLimitCounts dynamicLimitCounts, List<IntermediateTelemetryItem> telemetryItems)
		{
			PlanOperationTreeGenerator.LimitMetadataTableInfo limitMetadataTableInfo = default(PlanOperationTreeGenerator.LimitMetadataTableInfo);
			LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(this.m_outputExpressionTable, this.m_context.ErrorContext);
			limitMetadataTableInfo.IntersectionCountId = limitMetadataTableBuilder.AddColumn(PlanNames.IntersectionCount(dsContext.Id), dynamicLimitCounts.ActualIntersectionCount, ObjectType.DynamicLimits, null);
			limitMetadataTableInfo.PrimaryCountId = limitMetadataTableBuilder.AddColumn(PlanNames.PrimaryCount(dsContext.Id), dynamicLimitCounts.ActualPrimaryCount, ObjectType.DynamicLimits, null);
			limitMetadataTableInfo.TargetPrimaryId = limitMetadataTableBuilder.AddColumn(PlanNames.TargetPrimaryCount(dsContext.Id), dynamicLimitCounts.TargetPrimaryCount, ObjectType.DynamicLimits, null);
			limitMetadataTableInfo.SecondaryCountId = limitMetadataTableBuilder.AddColumn(PlanNames.SecondaryCount(dsContext.Id), dynamicLimitCounts.ActualSecondaryCount, ObjectType.DynamicLimits, null);
			limitMetadataTableInfo.TargetSecondaryId = limitMetadataTableBuilder.AddColumn(PlanNames.TargetSecondaryCount(dsContext.Id), dynamicLimitCounts.TargetSecondaryCount, ObjectType.DynamicLimits, null);
			if (telemetryItems != null)
			{
				foreach (IntermediateTelemetryItem intermediateTelemetryItem in telemetryItems)
				{
					intermediateTelemetryItem.ValueId = limitMetadataTableBuilder.AddColumn(intermediateTelemetryItem.ColumnName, intermediateTelemetryItem.Value, ObjectType.DynamicLimits, null);
				}
			}
			limitMetadataTableInfo.TableContext = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
			return limitMetadataTableInfo;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003B09C File Offset: 0x0003929C
		private static PlanLimitInfo BuildPlanLimitInfo(DataShapeContext dsContext, Identifier suppressedLimitId, PlanOperationTreeGenerator.LimitMetadataTableInfo limitMetadataInfo, List<IntermediateTelemetryItem> intermediateTelemetry)
		{
			List<LimitTelemetryItem> list = new List<LimitTelemetryItem>();
			list.Add(new LimitTelemetryItem("IntersCount", limitMetadataInfo.IntersectionCountId));
			if (intermediateTelemetry != null)
			{
				foreach (IntermediateTelemetryItem intermediateTelemetryItem in intermediateTelemetry)
				{
					LimitTelemetryItem limitTelemetryItem = new LimitTelemetryItem(intermediateTelemetryItem.TelemetryItemName, intermediateTelemetryItem.ValueId);
					list.Add(limitTelemetryItem);
				}
			}
			List<LimitOverride> list2 = new List<LimitOverride>(3);
			LimitOverride limitOverride = LimitOverride.OverrideLimit(dsContext.PrimaryHierarchyLimit.Id, new ExpressionId?(limitMetadataInfo.TargetPrimaryId), new ExpressionId?(limitMetadataInfo.PrimaryCountId), new ExpressionId?(limitMetadataInfo.PrimaryCountId), null);
			list2.Add(limitOverride);
			LimitOverride limitOverride2 = LimitOverride.OverrideLimit(dsContext.SecondaryHierarchyLimit.Id, new ExpressionId?(limitMetadataInfo.TargetSecondaryId), new ExpressionId?(limitMetadataInfo.SecondaryCountId), new ExpressionId?(limitMetadataInfo.SecondaryCountId), null);
			list2.Add(limitOverride2);
			if (suppressedLimitId != null)
			{
				LimitOverride limitOverride3 = LimitOverride.RemoveLimit(suppressedLimitId);
				list2.Add(limitOverride3);
			}
			return new PlanLimitInfo(list2, list);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003B1D8 File Offset: 0x000393D8
		private PlanOperationContext JoinPrimaryAndSecondaryTables(DataShapeContext dsContext, PlanOperationContext bodyTable, PlanOperationContext primaryTable, PlanOperationContext secondaryTable, string indexColumnName, bool applyLeftOuterJoin, out PlanColumnSortItem columnIndexSort)
		{
			bodyTable = bodyTable.SubstituteWithIndex(indexColumnName, secondaryTable, dsContext.SecondaryDynamicsExcludingContextOnly.ToSortItems(this.m_context.Annotations, true));
			if (applyLeftOuterJoin)
			{
				bodyTable = primaryTable.LeftOuterJoin(bodyTable, this.m_context.ScopeTree);
			}
			columnIndexSort = new PlanColumnSortItem(indexColumnName, SortDirection.Ascending);
			bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.BodyLimited(dsContext.Id), this.m_declarations, false, null, false);
			return bodyTable;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003B248 File Offset: 0x00039448
		private PlanOperationContext CreatePrimaryWithNoLimitTable(DataShapeContext dsContext, PlanDeclarationCollection declarations, PlanOperationContext coreTable, PlanOperationContext coreTableOnlyOutputTotals, CoreTableArtifacts coreTableArtifacts, InstanceFiltersContext instanceFilterContext, AggregatesInputTableCollector aggregatesInputTableCollector, bool shouldConsiderSecondaryLimits)
		{
			if (!dsContext.HasAnyPrimaryDynamic)
			{
				return coreTableOnlyOutputTotals;
			}
			PlanOperationContext planOperationContext = coreTable;
			if (!BatchDataSetPlanningUtils.AreEquivalentScopes(coreTable.RowScopes.InnermostScope, dsContext.LastPrimaryDynamicExcludingContextOnly, this.m_context.ScopeTree))
			{
				planOperationContext = BatchDataSetPlanningUtils.ApplyGroupByHierarchy(this.m_context.Annotations, coreTable, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryMembersExcludingContextOnly, dsContext.ScopeTree, SubtotalUsage.SortByMeasure, true, false);
			}
			PlanOperationContext planOperationContext2 = this.m_hierarchyPlanner.ApplyPrimaryHierarchyPreLimitOperations(this.m_context, this.m_context.ScopeTree, this.m_context.SortByMeasureExpressionMappings, dsContext, instanceFilterContext, this.m_attributeFilterContextTableManager, coreTable, planOperationContext, aggregatesInputTableCollector, coreTableArtifacts);
			planOperationContext2 = WindowTableBuilder.ApplyLegacyPrimaryHierarchySegmentation(planOperationContext2, this.m_declarations, this.m_context.ErrorContext, this.m_context.Annotations, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryHierarchyLimit, dsContext.DataShape.RequestedPrimaryLeafCount, dsContext.DataShape.RestartMatchingBehavior, dsContext.DataShape.Id);
			if (planOperationContext == planOperationContext2 && (!shouldConsiderSecondaryLimits || !dsContext.HasSecondaryHierarchyLimit))
			{
				return coreTableOnlyOutputTotals;
			}
			return planOperationContext2.DeclareIfNotDeclared(PlanNames.Primary(dsContext.Id), declarations, false, null, false);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003B35C File Offset: 0x0003955C
		private PlanOperationContext CreateSecondaryTableBase(DataShapeContext dsContext, PlanOperationContext coreTableOnlyOutputTotals, CoreTableArtifacts coreTableArtifacts, InstanceFiltersContext instanceFiltersContext, AggregatesInputTableCollector aggregatesInputTableCollector)
		{
			PlanOperationContext planOperationContext = BatchDataSetPlanningUtils.ApplyGroupByHierarchy(this.m_context.Annotations, coreTableOnlyOutputTotals, dsContext.SecondaryDynamicsExcludingContextOnly, dsContext.SecondaryMembersExcludingContextOnly, dsContext.ScopeTree, SubtotalUsage.Output, false, true);
			planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.SecondaryBase(dsContext.Id), this.m_declarations, false, null, false);
			return this.m_hierarchyPlanner.ApplySecondaryHierarchyPreLimitOperations(this.m_context, dsContext, instanceFiltersContext, this.m_attributeFilterContextTableManager, planOperationContext, aggregatesInputTableCollector, coreTableArtifacts, coreTableOnlyOutputTotals.RowScopes);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003B3D0 File Offset: 0x000395D0
		private OutputTableMapping PrepareOutputTableMappingsForPrimaryAndSecondaryPlan(DataShapeContext dsContext, IntermediateMemberDiscardConditions memberDiscardConditions, PlanOperationContext aggregatesTableContext, PlanOperationContext bodyTableContext, PlanOperationContext secondaryTableContext, PlanOperationContext limitMetadataTableContext, List<PlanNamedTableContext> secondarySyncTables, List<PlanNamedTableContext> syncTables)
		{
			OutputTableMapping outputTableMapping = new OutputTableMapping();
			if (limitMetadataTableContext != null)
			{
				if (dsContext.DataShape.DynamicLimits != null)
				{
					outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.LimitMetadata(dsContext.Id), limitMetadataTableContext, false, false, false), dsContext.DataShape.DynamicLimits);
				}
				else
				{
					outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.LimitMetadata(dsContext.Id), limitMetadataTableContext, false, false, false), dsContext.HasIntersectionLimit ? dsContext.IntersectionLimit : (dsContext.HasPrimaryHierarchyLimit ? dsContext.PrimaryHierarchyLimits[0] : dsContext.SecondaryHierarchyLimits[0]));
				}
			}
			this.AddSyncTablesToOutput(syncTables, outputTableMapping);
			this.AddSyncTablesToOutput(secondarySyncTables, outputTableMapping);
			if (aggregatesTableContext != null)
			{
				outputTableMapping.AddOutputTable(new PlanNamedTableContext(PlanNames.Aggregates(dsContext.Id), aggregatesTableContext, false, false, false), dsContext.DataShape);
			}
			string text = PlanNames.Secondary(dsContext.Id);
			outputTableMapping.AddOutputTable(new PlanNamedTableContext(text, secondaryTableContext, false, false, false), dsContext.DataShape, dsContext.FirstSecondaryMember);
			string text2 = PlanNames.Body(dsContext.Id);
			outputTableMapping.AddOutputTable(new PlanNamedTableContext(text2, bodyTableContext, false, false, false), dsContext.FirstPrimaryMember);
			memberDiscardConditions.FixupOutputTable(dsContext.PrimaryDynamicsExcludingContextOnly, text2);
			memberDiscardConditions.FixupOutputTable(dsContext.SecondaryDynamicsExcludingContextOnly, text);
			return outputTableMapping;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003B508 File Offset: 0x00039708
		private PlanOperationContext ApplyScopedAggregatesTransform(DataShapeContext dsContext, PlanDeclarationCollection declarations, PlanOperationContext coreTable, AggregatesInputTableCollector aggregatesInputTableCollector, bool primary)
		{
			if (!primary || !dsContext.HasAnyPrimaryDynamic)
			{
				return coreTable;
			}
			return ScopedAggregateTranslator.GenerateScopedAggregates(dsContext, this.m_outputExpressionTable, declarations, coreTable, this.m_context, dsContext.PrimaryDynamicsExcludingContextOnly, aggregatesInputTableCollector.References);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003B53C File Offset: 0x0003973C
		private void AddSyncTablesToOutput(List<PlanNamedTableContext> syncTables, OutputTableMapping outputTableMapping)
		{
			if (syncTables != null)
			{
				foreach (PlanNamedTableContext planNamedTableContext in syncTables)
				{
					DataShape containingDataShape = this.m_context.Annotations.GetContainingDataShape(planNamedTableContext.TableContext.RowScopes.InnermostScope);
					outputTableMapping.AddOutputTable(planNamedTableContext, containingDataShape);
				}
			}
		}

		// Token: 0x040006CF RID: 1743
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x040006D0 RID: 1744
		private readonly BatchDataSetPlannerContext m_context;

		// Token: 0x040006D1 RID: 1745
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x040006D2 RID: 1746
		private readonly ContextTableManager m_attributeFilterContextTableManager;

		// Token: 0x040006D3 RID: 1747
		private readonly HierarchyPlanOperationApplier m_hierarchyPlanner;

		// Token: 0x040006D4 RID: 1748
		private readonly SubqueryPlanOperationGenerator m_subqueryGenerator;

		// Token: 0x040006D5 RID: 1749
		private GroupSynchronizationApplier m_groupSyncApplier;

		// Token: 0x040006D6 RID: 1750
		private List<InstanceFilterTelemetry> m_instanceFiltersTelemetry;

		// Token: 0x020002FA RID: 762
		private struct LimitMetadataTableInfo
		{
			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x060016EB RID: 5867 RVA: 0x00052343 File Offset: 0x00050543
			// (set) Token: 0x060016EC RID: 5868 RVA: 0x0005234B File Offset: 0x0005054B
			internal ExpressionId IntersectionCountId { readonly get; set; }

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x060016ED RID: 5869 RVA: 0x00052354 File Offset: 0x00050554
			// (set) Token: 0x060016EE RID: 5870 RVA: 0x0005235C File Offset: 0x0005055C
			internal ExpressionId PrimaryCountId { readonly get; set; }

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x060016EF RID: 5871 RVA: 0x00052365 File Offset: 0x00050565
			// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0005236D File Offset: 0x0005056D
			internal ExpressionId TargetPrimaryId { readonly get; set; }

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00052376 File Offset: 0x00050576
			// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0005237E File Offset: 0x0005057E
			internal ExpressionId SecondaryCountId { readonly get; set; }

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00052387 File Offset: 0x00050587
			// (set) Token: 0x060016F4 RID: 5876 RVA: 0x0005238F File Offset: 0x0005058F
			internal ExpressionId TargetSecondaryId { readonly get; set; }

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00052398 File Offset: 0x00050598
			// (set) Token: 0x060016F6 RID: 5878 RVA: 0x000523A0 File Offset: 0x000505A0
			internal PlanOperationContext TableContext { readonly get; set; }
		}
	}
}
