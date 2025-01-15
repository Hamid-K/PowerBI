using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x02000111 RID: 273
	internal static class SemanticQueryDataShapeBuilder
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00023B14 File Offset: 0x00021D14
		private static IReadOnlyList<DsqExpressionAggregateKind> AllAggregatesExcept(params DsqExpressionAggregateKind[] allowedAggregates)
		{
			return ((DsqExpressionAggregateKind[])Enum.GetValues(typeof(DsqExpressionAggregateKind))).Except(allowedAggregates).ToList<DsqExpressionAggregateKind>();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00023B38 File Offset: 0x00021D38
		public static bool TryBuildDataShape(DataShapeBuilderContext dataShapeBuilderContext, QueryTranslationContext queryTranslationContext, QueryProjections projections, DataShapeBuilder dataShapeBuilder, IReadOnlyList<IntermediateQueryTransform> transforms, SparklineDataStatistics sparklineStatistics, ParameterMappings parameterMappings = null)
		{
			IntermediateDataReduction intermediateDataReduction;
			return SemanticQueryDataShapeBuilder.TryBuildDataShape(dataShapeBuilderContext, queryTranslationContext, projections, dataShapeBuilder, false, transforms, null, null, null, null, parameterMappings, sparklineStatistics, out intermediateDataReduction);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00023B5C File Offset: 0x00021D5C
		public static bool TryBuildDataShape(DataShapeBuilderContext dataShapeBuilderContext, QueryTranslationContext queryTranslationContext, QueryProjections projections, DataShapeBuilder dataShapeBuilder, bool contextOnly, IReadOnlyList<IntermediateQueryTransform> transforms, QueryExtensionSchemaContext resolvedQueryExtensionSchema, string dataSourceVariables, DataReductionConfiguration dataReductionConfig, DataReductionConfiguration dataReductionConfigForLegacyLimits, ParameterMappings parameterMappings, SparklineDataStatistics sparklineStatistics, out IntermediateDataReduction reduction)
		{
			reduction = null;
			dataShapeBuilder.Result.Transforms = DsqTransformBuilder.BuildTransforms(transforms);
			SemanticQueryDataShapeBuilder.BuildExtensionSchemas(resolvedQueryExtensionSchema, dataShapeBuilder);
			SemanticQueryDataShapeBuilder.BuildDataSourceVariables(dataSourceVariables, dataShapeBuilder);
			IReadOnlyList<DataMemberBuilderPair> readOnlyList;
			IReadOnlyList<DataMemberBuilderPair> readOnlyList2;
			DsqScopeLookup dsqScopeLookup;
			if (!SemanticQueryDataShapeBuilder.TryBuildProjections(dataShapeBuilderContext, queryTranslationContext, projections, dataShapeBuilder, contextOnly, out readOnlyList, out readOnlyList2, out dsqScopeLookup))
			{
				return false;
			}
			DsqTargetResolver dsqTargetResolver = new DsqTargetResolver(queryTranslationContext, dataShapeBuilderContext, dsqScopeLookup, projections, dataShapeBuilder.Id.Value);
			if (!SemanticQueryDataShapeBuilder.TryBuildVisualCalculationMetadata(dataShapeBuilderContext, queryTranslationContext, dataShapeBuilder, dsqTargetResolver))
			{
				return false;
			}
			if (!SemanticQueryDataShapeBuilder.TryBuildFilters(queryTranslationContext, dataShapeBuilderContext, dataShapeBuilder, dsqTargetResolver, projections.ProjectionFiltersBySelectIndex, contextOnly))
			{
				return false;
			}
			if (queryTranslationContext.SharedContext.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.MParameterColumnMapping))
			{
				ModelParametersGenerator.BuildModelParameters(queryTranslationContext.Expressions, parameterMappings, queryTranslationContext.SharedContext.ErrorContext, dataShapeBuilder);
				if (queryTranslationContext.SharedContext.ErrorContext.HasError)
				{
					return false;
				}
			}
			if (!contextOnly && dataReductionConfig != null)
			{
				if (!SemanticQueryDataShapeBuilder.TryBuildDataReduction(queryTranslationContext, dataReductionConfig, dataReductionConfigForLegacyLimits, projections, dataShapeBuilderContext, dataShapeBuilder, readOnlyList, readOnlyList2, sparklineStatistics, out reduction))
				{
					return false;
				}
			}
			else if (!DataShapeBuilderUtils.TryBuildStrictLimit(queryTranslationContext, dataShapeBuilderContext, dataShapeBuilder, readOnlyList, readOnlyList2))
			{
				return false;
			}
			if (!contextOnly)
			{
				GroupSynchronizationBuilder.BuildGroupSynchronizationDataShapes(dataShapeBuilderContext, queryTranslationContext.DataShapeBinding, dataShapeBuilder, readOnlyList, readOnlyList2, queryTranslationContext.SharedContext.FeatureSwitchProvider, queryTranslationContext.QueryDefinition.Select.Count);
			}
			return true;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00023C78 File Offset: 0x00021E78
		private static void BuildExtensionSchemas(QueryExtensionSchemaContext resolvedQueryExtensionSchema, DataShapeBuilder dataShapeBuilder)
		{
			if (resolvedQueryExtensionSchema == null)
			{
				return;
			}
			foreach (QueryExtensionSchema queryExtensionSchema in resolvedQueryExtensionSchema.ExtensionSchemas)
			{
				IList<QueryExtensionEntity> entities = queryExtensionSchema.Entities;
				ExtensionSchemaBuilder<DataShapeBuilder<DataShape>> extensionSchemaBuilder = dataShapeBuilder.WithExtensionSchema(queryExtensionSchema.Name);
				if (entities != null)
				{
					for (int i = 0; i < entities.Count; i++)
					{
						QueryExtensionEntity queryExtensionEntity = entities[i];
						ExtensionEntityBuilder<ExtensionSchemaBuilder<DataShapeBuilder<DataShape>>> extensionEntityBuilder = extensionSchemaBuilder.WithEntity(queryExtensionEntity.Name, queryExtensionEntity.Extends);
						if (queryExtensionEntity.Measures != null)
						{
							for (int j = 0; j < queryExtensionEntity.Measures.Count; j++)
							{
								QueryExtensionMeasure queryExtensionMeasure = queryExtensionEntity.Measures[j];
								extensionEntityBuilder.WithMeasure(queryExtensionMeasure.Name, queryExtensionMeasure.Expression, queryExtensionMeasure.DataType);
							}
						}
						if (queryExtensionEntity.Columns != null)
						{
							for (int k = 0; k < queryExtensionEntity.Columns.Count; k++)
							{
								QueryExtensionColumn queryExtensionColumn = queryExtensionEntity.Columns[k];
								Expression expression;
								if (resolvedQueryExtensionSchema.ExtensionColumnDsqExpressions != null && resolvedQueryExtensionSchema.ExtensionColumnDsqExpressions.TryGetValue(queryExtensionColumn, out expression))
								{
									extensionEntityBuilder.WithColumn(queryExtensionColumn.Name, expression, queryExtensionColumn.DataType);
								}
								else
								{
									extensionEntityBuilder.WithColumn(queryExtensionColumn.Name, queryExtensionColumn.Expression, queryExtensionColumn.DataType);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00023E10 File Offset: 0x00022010
		private static void BuildDataSourceVariables(string dataSourceVariables, DataShapeBuilder dataShapeBuilder)
		{
			if (string.IsNullOrEmpty(dataSourceVariables))
			{
				return;
			}
			dataShapeBuilder.WithDataSourceVariables(dataSourceVariables);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00023E24 File Offset: 0x00022024
		public static bool TryBuildContextOnlyDataShape(DataShapeBuilderContext dataShapeBuilderContext, QueryTranslationContext queryTranslationContext, QueryProjections projections, DataShapeBuilder dataShapeBuilder)
		{
			bool flag = true;
			DataReductionConfiguration dataReductionConfiguration = null;
			DataReductionConfiguration dataReductionConfiguration2 = null;
			IReadOnlyList<IntermediateQueryTransform> readOnlyList = null;
			QueryExtensionSchemaContext queryExtensionSchemaContext = null;
			string text = null;
			IntermediateDataReduction intermediateDataReduction;
			return SemanticQueryDataShapeBuilder.TryBuildDataShape(dataShapeBuilderContext, queryTranslationContext, projections, dataShapeBuilder, flag, readOnlyList, queryExtensionSchemaContext, text, dataReductionConfiguration, dataReductionConfiguration2, ParameterMappings.Empty, SparklineDataStatistics.Empty, out intermediateDataReduction);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00023E5C File Offset: 0x0002205C
		private static bool TryBuildDataReduction(QueryTranslationContext queryTranslationContext, DataReductionConfiguration dataReductionConfig, DataReductionConfiguration dataReductionConfigForLegacyLimits, QueryProjections projections, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, SparklineDataStatistics sparklineStatistics, out IntermediateDataReduction reduction)
		{
			DataShapeBinding dataShapeBinding = queryTranslationContext.DataShapeBinding;
			int? legacyMaxRowCount = queryTranslationContext.LegacyMaxRowCount;
			DataShapeGenerationInternalContext sharedContext = queryTranslationContext.SharedContext;
			int? top = queryTranslationContext.QueryDefinition.Top;
			reduction = null;
			DataReductionConfiguration dataReductionConfiguration;
			if (dataShapeBinding != null && dataShapeBinding.DataReduction != null)
			{
				dataReductionConfiguration = dataReductionConfig;
				if (!DataReductionLoader.TryLoadFromDataReduction(sharedContext.ErrorContext, queryTranslationContext.ResolvedDataReduction, out reduction))
				{
					return false;
				}
			}
			else if (top != null)
			{
				List<Expression> list;
				if (!DataShapeBuilderUtils.ValidateStrictLimit(queryTranslationContext, primaryDynamics, secondaryDynamics, out list))
				{
					return false;
				}
				dataReductionConfiguration = dataReductionConfig;
				reduction = new IntermediateDataReduction
				{
					Primary = new IntermediateSimpleLimit
					{
						Kind = IntermediateSimpleLimitKind.Top,
						Count = new int?(top.Value),
						IsStrict = new bool?(true),
						Skip = queryTranslationContext.QueryDefinition.Skip
					}
				};
			}
			else
			{
				dataReductionConfiguration = dataReductionConfigForLegacyLimits;
				if (!DataShapeBindingLimitUpgrader.TryUpgrade(sharedContext.ErrorContext, projections, dataShapeBinding, legacyMaxRowCount, out reduction))
				{
					return false;
				}
			}
			DataReductionTelemetry dataReductionTelemetry = new DataReductionTelemetry();
			sharedContext.Telemetry.Reduction = dataReductionTelemetry;
			return DataReductionResolver.TryResolveDataReduction(sharedContext, dataReductionConfiguration, reduction, projections, queryTranslationContext.QueryDefinition, queryTranslationContext.DataShapeBinding, sparklineStatistics, dataReductionTelemetry) && DsqDataReductionGenerator.TryGenerate(sharedContext, dataShapeBuilderContext, dataShapeBuilder, reduction, primaryDynamics, secondaryDynamics, queryTranslationContext.Expressions);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00023F90 File Offset: 0x00022190
		private static bool TryBuildProjections(DataShapeBuilderContext context, QueryTranslationContext queryTranslationContext, QueryProjections projections, DataShapeBuilder dataShape, bool contextOnlyDataShape, out IReadOnlyList<DataMemberBuilderPair> primaryDynamics, out IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, out DsqScopeLookup dsqScopesLookup)
		{
			DataShapeBinding dataShapeBinding = queryTranslationContext.DataShapeBinding;
			dsqScopesLookup = new DsqScopeLookup();
			bool flag = dataShapeBinding != null && !dataShapeBinding.Highlights.IsNullOrEmptyCollection<FilterDefinition>();
			if (projections.PrimaryMembers.Count == 0)
			{
				DataShapeBuilderUtils.CreatePrimaryHierarchyMeasuresOnly(context, projections, dataShape, flag);
				SemanticQueryDataShapeBuilder.AddDataShapeProjections(context, projections.DataShapeProjections, dataShape);
				primaryDynamics = null;
				secondaryDynamics = null;
				return true;
			}
			bool flag2 = SemanticQueryDataShapeBuilder.NeedsStaticsForAggregates(projections, projections.PrimaryMembers);
			if (!DataShapeBuilderUtils.TryBuildDynamicMembers(context, queryTranslationContext, dataShape, projections.PrimaryMembers, dsqScopesLookup, flag, true, flag2, contextOnlyDataShape, out primaryDynamics))
			{
				secondaryDynamics = null;
				return false;
			}
			List<DataMemberBuilderPair> leafDynamics = DataShapeBuilderUtils.GetLeafDynamics(primaryDynamics);
			if (!DataShapeBuilderUtils.TryBuildDynamicMembers(context, queryTranslationContext, dataShape, projections.SecondaryMembers, dsqScopesLookup, flag, false, false, contextOnlyDataShape, out secondaryDynamics))
			{
				return false;
			}
			IReadOnlyList<ProjectedDsqExpression> measures = projections.Measures;
			IReadOnlyList<ProjectedDsqExpression> readOnlyList;
			IReadOnlyList<DsqExpressionAggregateKind> readOnlyList2;
			IReadOnlyList<DsqExpressionAggregateKind> readOnlyList3;
			if (secondaryDynamics.Count == 0)
			{
				DataMemberBuilderPair dataMemberBuilderPair = leafDynamics.Single<DataMemberBuilderPair>();
				for (int i = 0; i < measures.Count; i++)
				{
					dataMemberBuilderPair.Dynamic.WithMeasureCalculation(context, measures[i], flag, null);
				}
				Identifier id = dataMemberBuilderPair.Dynamic.Result.Id;
				dsqScopesLookup.SetInnermostScopeId(id);
				readOnlyList = measures;
				readOnlyList2 = SemanticQueryDataShapeBuilder.SuppressedGroupAggregates;
				readOnlyList3 = SemanticQueryDataShapeBuilder.SuppressedGroupAggregatesAndSubtotal;
			}
			else
			{
				List<DataMemberBuilderPair> leafDynamics2 = DataShapeBuilderUtils.GetLeafDynamics(secondaryDynamics);
				SemanticQueryDataShapeBuilder.BuildIntersections(context, dsqScopesLookup, dataShape, primaryDynamics, secondaryDynamics, measures, leafDynamics, leafDynamics2, flag, out readOnlyList);
				readOnlyList2 = SemanticQueryDataShapeBuilder.SuppressedGroupWithIntersectionAggregates;
				readOnlyList3 = SemanticQueryDataShapeBuilder.SuppressedGroupWithIntersectionAggregates;
			}
			readOnlyList = SemanticQueryDataShapeBuilder.ComputeFinalAggregates(projections, readOnlyList);
			SemanticQueryDataShapeBuilder.AddAggregates(context, projections, readOnlyList, primaryDynamics, dataShape, readOnlyList2, readOnlyList3);
			SemanticQueryDataShapeBuilder.AddDataShapeProjections(context, projections.DataShapeProjections, dataShape);
			IReadOnlyList<DataMemberBuilderPair> readOnlyList4 = primaryDynamics;
			IReadOnlyList<QueryMember> primaryMembers = projections.PrimaryMembers;
			bool flag3 = true;
			bool flag4 = projections.SecondaryMembers.Count > 0;
			DataShapeBuilderUtils.AddGroupSorting(context, readOnlyList4, primaryMembers, flag3, contextOnlyDataShape, flag4);
			DataShapeBuilderUtils.AddGroupSorting(context, secondaryDynamics, projections.SecondaryMembers, false, contextOnlyDataShape, false);
			return true;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002415C File Offset: 0x0002235C
		private static bool TryBuildVisualCalculationMetadata(DataShapeBuilderContext context, QueryTranslationContext queryTranslationContext, DataShapeBuilder dataShapeBuilder, IDsqTargetResolver dsqTargetResolver)
		{
			bool flag = queryTranslationContext.Annotations.QueryHasVisualCalculationsExpressions(queryTranslationContext.QueryDefinition.Name);
			IReadOnlyList<ResolvedQueryAxis> visualShape = queryTranslationContext.QueryDefinition.VisualShape;
			if (!flag || visualShape.IsNullOrEmpty<ResolvedQueryAxis>())
			{
				return true;
			}
			foreach (ResolvedQueryAxis resolvedQueryAxis in visualShape)
			{
				VisualAxisBuilder<DataShapeBuilder<DataShape>> visualAxisBuilder = dataShapeBuilder.WithVisualAxis(resolvedQueryAxis.Name);
				foreach (ResolvedQueryAxisGroup resolvedQueryAxisGroup in resolvedQueryAxis.Groups)
				{
					Identifier identifier;
					if (!dsqTargetResolver.TryTranslateToDsqScopeForVisualAxis(resolvedQueryAxisGroup.Keys, out identifier))
					{
						queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.CouldNotResolveTargetScopeForVisualAxis(EngineMessageSeverity.Error, resolvedQueryAxis.Name));
						return false;
					}
					visualAxisBuilder.WithGroup(identifier.Value);
				}
			}
			return true;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00024260 File Offset: 0x00022460
		private static bool TryBuildFilters(QueryTranslationContext queryTranslationContext, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, DsqTargetResolver dsqFilterTargetResolver, IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFiltersBySelectIndex, bool contextOnly)
		{
			QueryFilters queryFilters;
			return QueryFiltersGenerator.TryRun(queryTranslationContext, dsqFilterTargetResolver, projectionFiltersBySelectIndex, contextOnly, out queryFilters) && DataShapeBuilderUtils.TryBuildFilters(queryTranslationContext, dataShapeBuilderContext, queryFilters, dataShapeBuilder);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002428C File Offset: 0x0002248C
		private static void BuildIntersections(DataShapeBuilderContext context, DsqScopeLookup dsqScopesLookup, DataShapeBuilder dataShape, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, IReadOnlyList<ProjectedDsqExpression> intersectionMeasures, List<DataMemberBuilderPair> primaryLeafDynamics, List<DataMemberBuilderPair> secondaryLeafDynamics, bool hasHighlightFilters, out IReadOnlyList<ProjectedDsqExpression> measuresWithRemainingAggregates)
		{
			Identifier identifier = DsqIntersectionBuilder.BuildIntersections(context, primaryDynamics, secondaryDynamics, intersectionMeasures, dataShape, hasHighlightFilters, SemanticQueryDataShapeBuilder.SuppressedIntersectionAggregates, SemanticQueryDataShapeBuilder.SuppressedIntersectionAggregatesAndSubtotal);
			dsqScopesLookup.AddIntersectionScopeMapping(primaryLeafDynamics[0].Dynamic.Result.Id, secondaryLeafDynamics[0].Dynamic.Result.Id, identifier);
			dsqScopesLookup.SetInnermostScopeId(identifier);
			measuresWithRemainingAggregates = SemanticQueryDataShapeBuilder.DetermineMeasuresForTopLevelAggregates(intersectionMeasures);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000242F8 File Offset: 0x000224F8
		private static IReadOnlyList<ProjectedDsqExpression> DetermineMeasuresForTopLevelAggregates(IEnumerable<ProjectedDsqExpression> measures)
		{
			return measures.Where((ProjectedDsqExpression m) => m.Aggregates.Any((DsqExpressionAggregateBase a) => SemanticQueryDataShapeBuilder.SuppressedIntersectionAggregates.Contains(a.Kind))).ToReadOnlyCollection<ProjectedDsqExpression>();
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00024324 File Offset: 0x00022524
		private static IReadOnlyList<ProjectedDsqExpression> ComputeFinalAggregates(QueryProjections projections, IReadOnlyList<ProjectedDsqExpression> measuresFromInnermostScope)
		{
			IReadOnlyList<ProjectedDsqExpression> readOnlyList = SemanticQueryDataShapeBuilder.DetermineMeasuresForTopLevelAggregates(projections.PrimaryMembers.SelectMany((QueryMember g) => g.MeasureCalculations).Concat(projections.SecondaryMembers.SelectMany((QueryMember g) => g.MeasureCalculations)));
			if (readOnlyList.Any<ProjectedDsqExpression>())
			{
				return measuresFromInnermostScope.Concat(readOnlyList).ToList<ProjectedDsqExpression>();
			}
			return measuresFromInnermostScope;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000243A8 File Offset: 0x000225A8
		private static bool NeedsStaticsForAggregates(QueryProjections projections, IReadOnlyList<QueryMember> members)
		{
			using (IEnumerator<ProjectedDsqExpression> enumerator = projections.Measures.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (SemanticQueryDataShapeBuilder.NeedsStaticsForAggregates(enumerator.Current))
					{
						return true;
					}
				}
			}
			bool needsStaticsForAggregates = false;
			QueryGroupValueProjectionVisitor queryGroupValueProjectionVisitor = new QueryGroupValueProjectionVisitor(delegate(ProjectedDsqExpression projection)
			{
				needsStaticsForAggregates = SemanticQueryDataShapeBuilder.NeedsStaticsForAggregates(projection);
			});
			for (int i = 0; i < members.Count; i++)
			{
				foreach (QueryGroupValue queryGroupValue in members[i].Values)
				{
					needsStaticsForAggregates = false;
					queryGroupValue.Accept<QueryGroupValue>(queryGroupValueProjectionVisitor);
					if (needsStaticsForAggregates)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002448C File Offset: 0x0002268C
		private static bool NeedsStaticsForAggregates(ProjectedDsqExpression projectedExpression)
		{
			return projectedExpression.Aggregates.Any((DsqExpressionAggregateBase agg) => agg.Kind == DsqExpressionAggregateKind.Count);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000244B8 File Offset: 0x000226B8
		internal static void AddDataShapeProjections(DataShapeBuilderContext context, IReadOnlyList<ProjectedDsqExpression> projections, DataShapeBuilder dataShape)
		{
			if (projections.IsNullOrEmpty<ProjectedDsqExpression>())
			{
				return;
			}
			foreach (ProjectedDsqExpression projectedDsqExpression in projections)
			{
				dataShape.WithMeasureCalculation(context, projectedDsqExpression);
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002450C File Offset: 0x0002270C
		private static void AddAggregates(DataShapeBuilderContext context, QueryProjections projections, IReadOnlyList<ProjectedDsqExpression> measures, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, DataShapeBuilder dataShape, IReadOnlyList<DsqExpressionAggregateKind> effectiveSuppressedGroupAggregates, IReadOnlyList<DsqExpressionAggregateKind> effectiveSuppressedGroupAggregatesWithSubtotal)
		{
			IReadOnlyList<QueryMember> primaryMembers = projections.PrimaryMembers;
			for (int i = 0; i < primaryMembers.Count; i++)
			{
				QueryMember queryMember = primaryMembers[i];
				AggregateContextOnlyImpact aggregateContextOnlyImpactForGroup = SemanticQueryDataShapeBuilder.GetAggregateContextOnlyImpactForGroup(primaryMembers, i);
				SemanticQueryDataShapeBuilder.AddAggregatesTargetingGroupValue(context, dataShape, queryMember, aggregateContextOnlyImpactForGroup);
				DataMemberBuilder @static = primaryDynamics[i].Static;
				if (@static != null)
				{
					AggregateContextOnlyImpact aggregateContextOnlyImpactForStatic = SemanticQueryDataShapeBuilder.GetAggregateContextOnlyImpactForStatic(primaryMembers, i);
					SemanticQueryDataShapeBuilder.AddAggregates<DataMember>(context, measures, @static, projections.PrimaryMembers, i, queryMember.HasExplicitSubtotal ? effectiveSuppressedGroupAggregatesWithSubtotal : effectiveSuppressedGroupAggregates, aggregateContextOnlyImpactForStatic);
				}
			}
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				dataShape.WithAggregates(context, projectedDsqExpression, SemanticQueryDataShapeBuilder.SuppressedDataShapeAggregates, AggregateContextOnlyImpact.None);
			}
			for (int j = 1; j < primaryMembers.Count; j++)
			{
				DataMemberBuilder static2 = primaryDynamics[j].Static;
				if (static2 != null)
				{
					AggregateContextOnlyImpact aggregateContextOnlyImpactForStatic2 = SemanticQueryDataShapeBuilder.GetAggregateContextOnlyImpactForStatic(primaryMembers, j);
					SemanticQueryDataShapeBuilder.AddAggregates<DataMember>(context, measures, static2, projections.PrimaryMembers, j - 1, SemanticQueryDataShapeBuilder.SuppressedScopedAggregates, aggregateContextOnlyImpactForStatic2);
				}
			}
			IReadOnlyList<QueryMember> secondaryMembers = projections.SecondaryMembers;
			for (int k = 0; k < secondaryMembers.Count; k++)
			{
				QueryMember queryMember2 = secondaryMembers[k];
				AggregateContextOnlyImpact aggregateContextOnlyImpactForGroup2 = SemanticQueryDataShapeBuilder.GetAggregateContextOnlyImpactForGroup(secondaryMembers, k);
				SemanticQueryDataShapeBuilder.AddAggregatesTargetingGroupValue(context, dataShape, queryMember2, aggregateContextOnlyImpactForGroup2);
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002465C File Offset: 0x0002285C
		private static AggregateContextOnlyImpact GetAggregateContextOnlyImpactForGroup(IReadOnlyList<QueryMember> members, int index)
		{
			if (members[index].IsContextOnly)
			{
				return AggregateContextOnlyImpact.InsideAContextOnlyMember;
			}
			if (members.Count > index + 1 && members[index + 1].IsContextOnly)
			{
				return AggregateContextOnlyImpact.NewInnermostIntersection;
			}
			return AggregateContextOnlyImpact.None;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002468C File Offset: 0x0002288C
		private static AggregateContextOnlyImpact GetAggregateContextOnlyImpactForStatic(IReadOnlyList<QueryMember> members, int index)
		{
			if (index > 0 && members[index - 1].IsContextOnly)
			{
				return AggregateContextOnlyImpact.InsideAContextOnlyMember;
			}
			if (members[index].IsContextOnly)
			{
				return AggregateContextOnlyImpact.NewInnermostIntersection;
			}
			if (members[index].Group.IsSubtotalContextOnly)
			{
				return AggregateContextOnlyImpact.InsideAContextOnlyMember;
			}
			return AggregateContextOnlyImpact.None;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000246CC File Offset: 0x000228CC
		private static void AddAggregatesTargetingGroupValue(DataShapeBuilderContext context, DataShapeBuilder dataShape, QueryMember member, AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			QueryGroupValueProjectionVisitor queryGroupValueProjectionVisitor = new QueryGroupValueProjectionVisitor(delegate(ProjectedDsqExpression projection)
			{
				dataShape.WithAggregates(context, projection, SemanticQueryDataShapeBuilder.SuppressedDataShapeAggregates, aggregateContextOnlyImpact);
			});
			foreach (QueryGroupValue queryGroupValue in member.Values)
			{
				queryGroupValue.Accept<QueryGroupValue>(queryGroupValueProjectionVisitor);
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00024744 File Offset: 0x00022944
		private static void AddAggregates<TParent>(DataShapeBuilderContext context, IEnumerable<ProjectedDsqExpression> measures, DataMemberBuilder<TParent> dataMember, IReadOnlyList<QueryMember> queryMembers, int groupIdx, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				dataMember.WithAggregates(context, projectedDsqExpression, groupIdx, suppressedAggregates, aggregateContextOnlyImpact);
			}
			QueryGroupValueProjectionVisitor queryGroupValueProjectionVisitor = new QueryGroupValueProjectionVisitor(delegate(ProjectedDsqExpression projection)
			{
				dataMember.WithAggregates(context, projection, groupIdx, suppressedAggregates, aggregateContextOnlyImpact);
			});
			for (int i = groupIdx; i < queryMembers.Count; i++)
			{
				foreach (QueryGroupValue queryGroupValue in queryMembers[i].Values)
				{
					queryGroupValue.Accept<QueryGroupValue>(queryGroupValueProjectionVisitor);
				}
			}
		}

		// Token: 0x0400048C RID: 1164
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedDataShapeAggregates = SemanticQueryDataShapeBuilder.AllAggregatesExcept(new DsqExpressionAggregateKind[]
		{
			DsqExpressionAggregateKind.Min,
			DsqExpressionAggregateKind.Max,
			DsqExpressionAggregateKind.Percentile,
			DsqExpressionAggregateKind.Average,
			DsqExpressionAggregateKind.Median
		});

		// Token: 0x0400048D RID: 1165
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedScopedAggregates = SemanticQueryDataShapeBuilder.AllAggregatesExcept(new DsqExpressionAggregateKind[]
		{
			DsqExpressionAggregateKind.Min,
			DsqExpressionAggregateKind.Max,
			DsqExpressionAggregateKind.Average
		});

		// Token: 0x0400048E RID: 1166
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedGroupAggregates = SemanticQueryDataShapeBuilder.AllAggregatesExcept(new DsqExpressionAggregateKind[]
		{
			DsqExpressionAggregateKind.Subtotal,
			DsqExpressionAggregateKind.Count
		});

		// Token: 0x0400048F RID: 1167
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedGroupAggregatesAndSubtotal = SemanticQueryDataShapeBuilder.AllAggregatesExcept(new DsqExpressionAggregateKind[] { DsqExpressionAggregateKind.Count });

		// Token: 0x04000490 RID: 1168
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedGroupWithIntersectionAggregates = SemanticQueryDataShapeBuilder.AllAggregatesExcept(new DsqExpressionAggregateKind[] { DsqExpressionAggregateKind.Count });

		// Token: 0x04000491 RID: 1169
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedIntersectionAggregates = SemanticQueryDataShapeBuilder.AllAggregatesExcept(new DsqExpressionAggregateKind[] { DsqExpressionAggregateKind.Subtotal });

		// Token: 0x04000492 RID: 1170
		private static readonly IReadOnlyList<DsqExpressionAggregateKind> SuppressedIntersectionAggregatesAndSubtotal = SemanticQueryDataShapeBuilder.AllAggregatesExcept(Array.Empty<DsqExpressionAggregateKind>());
	}
}
