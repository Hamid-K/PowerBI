using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.DataShapeQueryGeneration.DSQ;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000009 RID: 9
	internal sealed class DataShapeBuilderContext : IDsqSortKeyVisitor<ExpressionNode, SortKeyVisitorContext>
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000298B File Offset: 0x00000B8B
		internal DataShapeBuilderContext(int selectCount, QuerySchemaMapping querySchemaMapping, IReadOnlyList<ResolvedQuerySelect> resolvedQuerySelects)
			: this(selectCount, new DataShapeIdGenerator(), querySchemaMapping, resolvedQuerySelects)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000299C File Offset: 0x00000B9C
		private DataShapeBuilderContext(int selectCount, DataShapeIdGenerator ids, QuerySchemaMapping querySchemaMapping, IReadOnlyList<ResolvedQuerySelect> resolvedQuerySelects)
		{
			this._ids = ids;
			this._expressionToIdMapping = new Dictionary<ProjectedDsqExpression, string>();
			this._selectBindingsBuilder = new SelectBindingsBuilder(selectCount, resolvedQuerySelects);
			this._querySchemaMapping = querySchemaMapping;
			this._primaryAxisBuilder = new DataShapeExpressionsAxisBuilder();
			this._secondaryAxisBuilder = new DataShapeExpressionsAxisBuilder();
			this._measureDynamicFormattingIds = new Dictionary<string, global::System.ValueTuple<string, string>>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029F7 File Offset: 0x00000BF7
		internal DataShapeBuilderContext CreateSubqueryBuilderContext(int selectCount, IReadOnlyList<ResolvedQuerySelect> resolvedQuerySelects)
		{
			return new DataShapeBuilderContext(selectCount, this._ids, this._querySchemaMapping, resolvedQuerySelects);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A0C File Offset: 0x00000C0C
		internal DataShapeBuilderContext CreateSynchronizationContext(int selectCount, IReadOnlyList<ResolvedQuerySelect> resolvedQuerySelects)
		{
			return new DataShapeBuilderContext(selectCount, this._ids, this._querySchemaMapping, resolvedQuerySelects);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A21 File Offset: 0x00000C21
		internal string CreateIntersectionId()
		{
			return this._ids.CreateIntersectionId();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A2E File Offset: 0x00000C2E
		internal string CreateMemberId()
		{
			return this._ids.CreateMemberId();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A3B File Offset: 0x00000C3B
		internal string CreateGroupId()
		{
			return this._ids.CreateGroupId();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A48 File Offset: 0x00000C48
		internal string CreateLimitId()
		{
			return this._ids.CreateLimitId();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A55 File Offset: 0x00000C55
		internal string CreateSubqueryDataShapeId()
		{
			return this._ids.CreateSubqueryDataShapeId();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A62 File Offset: 0x00000C62
		internal string CreateGroupSynchronizationDataShapeId()
		{
			return this._ids.CreateGroupSynchronizationDataShapeId();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A6F File Offset: 0x00000C6F
		internal string CreateAggregateId()
		{
			return this._ids.CreateAggregateId();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A7C File Offset: 0x00000C7C
		internal DataShapeIdGenerator DataShapeIdGenerator
		{
			get
			{
				return this._ids;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A84 File Offset: 0x00000C84
		internal ReadOnlyCollection<Identifier> GetHighlightCalculations()
		{
			return this._highlightIds.AsReadOnlyCollection<Identifier>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A94 File Offset: 0x00000C94
		internal void AddGrouping<TParent>(GroupBuilder<DataMemberBuilder<TParent>> group, IReadOnlyList<QueryGroupKey> groupKeys, QueryDetailGroupIdentity detailGroupIdentity, int groupIdx, bool isPrimary, string memberId, string subtotalMemberId, SubtotalType subtotalType, bool contextOnly = false)
		{
			if (detailGroupIdentity != null)
			{
				group.WithDetailGroupIdentity(detailGroupIdentity.Expression, null);
			}
			for (int i = 0; i < groupKeys.Count; i++)
			{
				QueryGroupKey queryGroupKey = groupKeys[i];
				group.WithGroupKey(queryGroupKey.Expression, queryGroupKey.ShowItemsWithNoData ? true : null, null);
			}
			if (contextOnly)
			{
				return;
			}
			DataShapeExpressionsAxisGroupingBuilder groupingBuilder = this.GetGroupingBuilder(isPrimary, new int?(groupIdx));
			groupingBuilder.WithMember(memberId);
			if (subtotalMemberId != null)
			{
				groupingBuilder.WithSubtotalMember(subtotalMemberId, subtotalType);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B24 File Offset: 0x00000D24
		internal void AddGroupingForSynchronization<TParent>(GroupBuilder<DataMemberBuilder<TParent>> groupBuilder, IntermediateGroupSchema groupingSchema, int groupIndex, bool isPrimary, string memberId, string subtotalMemberId, SubtotalType subtotalType, HashSet<int> visitedSelectIndices)
		{
			DataShapeExpressionsAxisGroupingBuilder groupingBuilder = this.GetGroupingBuilder(isPrimary, new int?(groupIndex));
			groupingBuilder.WithMember(memberId);
			if (subtotalMemberId != null)
			{
				groupingBuilder.WithSubtotalMember(subtotalMemberId, subtotalType);
			}
			int? num = (isPrimary ? new int?(groupIndex) : null);
			int? num2 = ((!isPrimary) ? new int?(groupIndex) : null);
			if (groupingSchema.GroupKeys != null)
			{
				foreach (IntermediateGroupingKey intermediateGroupingKey in groupingSchema.GroupKeys)
				{
					groupBuilder.WithGroupKey(intermediateGroupingKey.DsqReferenceExpression, null, null);
					string text;
					this.ProjectIfNeeded<TParent>(groupBuilder.Parent(), intermediateGroupingKey.DsqReferenceExpression, out text);
					if (intermediateGroupingKey.IsIdentityKey)
					{
						bool flag = intermediateGroupingKey.SelectIndex != null;
						ConceptualPropertyReference conceptualPropertyReference = this.GetConceptualPropertyReference(intermediateGroupingKey.LineageProperty);
						this._selectBindingsBuilder.AddToBindingIdentityKeys(intermediateGroupingKey.SelectIndicesWithThisIdentity, intermediateGroupingKey.SelectIndex, null, conceptualPropertyReference, intermediateGroupingKey.LineageProperty, text);
						groupingBuilder.WithKey(conceptualPropertyReference, intermediateGroupingKey.LineageProperty, intermediateGroupingKey.SelectIndex, flag ? null : text, intermediateGroupingKey.IsIdentityKey, null);
					}
				}
			}
			if (groupingSchema.SortKeys != null)
			{
				foreach (IntermediateSortingKey intermediateSortingKey in groupingSchema.SortKeys)
				{
					groupBuilder.WithSortKey(intermediateSortingKey.DsqReferenceExpression, intermediateSortingKey.SortDirection, null);
					string text2;
					this.ProjectIfNeeded<TParent>(groupBuilder.Parent(), intermediateSortingKey.DsqReferenceExpression, out text2);
				}
			}
			if (groupingSchema.DetailValues != null)
			{
				foreach (IntermediateGroupingDetailValue intermediateGroupingDetailValue in groupingSchema.DetailValues)
				{
					visitedSelectIndices.Add(intermediateGroupingDetailValue.SelectIndex);
					string text3;
					this.ProjectIfNeeded<TParent>(groupBuilder.Parent(), intermediateGroupingDetailValue.DsqReferenceExpression, out text3);
					this._selectBindingsBuilder.AddSelectBinding(intermediateGroupingDetailValue.SelectIndex, text3, intermediateGroupingDetailValue.FormatString, SelectKind.Group, num, num2, intermediateGroupingDetailValue.LineageProperty);
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D74 File Offset: 0x00000F74
		private void ProjectIfNeeded<TParent>(DataMemberBuilder<TParent> dataMemberBuilder, ExpressionNode expression, out string calcId)
		{
			Calculation calculationOrDefault = dataMemberBuilder.GetCalculationOrDefault(expression);
			calcId = ((calculationOrDefault != null) ? calculationOrDefault.Id.Value : null);
			if (calculationOrDefault == null)
			{
				calcId = this.CreateGroupId();
				dataMemberBuilder.WithCalculation(calcId, expression, false, null, null, false);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DC4 File Offset: 0x00000FC4
		internal DataMemberBuilder<TParent> AddGroupCalculation<TParent>(DataMemberBuilder<TParent> dataMember, QueryGroupValue groupValue, int groupIdx, bool isPrimary, bool contextOnly = false)
		{
			int? num = null;
			int? num2 = null;
			if (isPrimary)
			{
				num = new int?(groupIdx);
			}
			else
			{
				num2 = new int?(groupIdx);
			}
			int? num3 = (contextOnly ? null : new int?(groupIdx));
			DataShapeExpressionsAxisGroupingBuilder groupingBuilder = this.GetGroupingBuilder(isPrimary, num3);
			DataShapeBuilderGroupValueGenerator<DataMemberBuilder<TParent>>.Generate(groupValue, this._ids, groupingBuilder, this._selectBindingsBuilder, this._expressionToIdMapping, dataMember, new Func<IConceptualProperty, ConceptualPropertyReference>(this.GetConceptualPropertyReference), contextOnly, num, num2);
			return dataMember;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E44 File Offset: 0x00001044
		internal DataMemberBuilder<TParent> AddMeasureCalculation<TParent>(DataMemberBuilder<TParent> dataMember, ProjectedDsqExpression generatedDsqExpr, bool hasHighlightFilters, int? primaryDepth = null)
		{
			string text;
			string text2;
			this.PrepareAddMeasureCalculation(generatedDsqExpr, hasHighlightFilters, out text, out text2);
			DataMemberBuilder<TParent> dataMemberBuilder = this.AddCalculationAndSetValue<DataMemberBuilder<TParent>>(dataMember, generatedDsqExpr, text, SelectKind.Measure, primaryDepth, null);
			this.AddHighlightCalculation<DataMemberBuilder<TParent>>(dataMember, generatedDsqExpr, text2, generatedDsqExpr.SemanticQuerySelectIndex);
			return dataMemberBuilder;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E84 File Offset: 0x00001084
		internal DataIntersectionBuilder<TParent> AddMeasureCalculation<TParent>(DataIntersectionBuilder<TParent> dataIntersection, ProjectedDsqExpression generatedDsqExpr, bool hasHighlightFilters)
		{
			string text;
			string text2;
			this.PrepareAddMeasureCalculation(generatedDsqExpr, hasHighlightFilters, out text, out text2);
			DataIntersectionBuilder<TParent> dataIntersectionBuilder = this.AddCalculationAndSetValue<DataIntersectionBuilder<TParent>>(dataIntersection, generatedDsqExpr, text, SelectKind.Measure, null, null);
			this.AddHighlightCalculation<DataIntersectionBuilder<TParent>>(dataIntersection, generatedDsqExpr, text2, generatedDsqExpr.SemanticQuerySelectIndex);
			return dataIntersectionBuilder;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002EC8 File Offset: 0x000010C8
		internal DataShapeBuilder<TParent> AddMeasureCalculation<TParent>(DataShapeBuilder<TParent> dataShape, ProjectedDsqExpression generatedDsqExpr)
		{
			int? semanticQuerySelectIndex = generatedDsqExpr.SemanticQuerySelectIndex;
			string orCreateMeasureId = DataShapeBuilderUtils.GetOrCreateMeasureId(this._ids, this._selectBindingsBuilder, this._expressionToIdMapping, generatedDsqExpr, generatedDsqExpr.SemanticQuerySelectIndex);
			if (!generatedDsqExpr.IsContextOnly)
			{
				SelectBinding selectBinding = this._selectBindingsBuilder.EnsureSelectBinding(semanticQuerySelectIndex.Value);
				this.PrepareAddMeasureDynamicFormatting(generatedDsqExpr, selectBinding, orCreateMeasureId, new DataShapeBuilderContext.TryPrepareAddDynamicFormattingMeasureDelegate(this.TryPrepareAddDynamicFormattingMeasure));
			}
			return this.AddCalculationAndSetValue<DataShapeBuilder<TParent>>(dataShape, generatedDsqExpr, orCreateMeasureId, SelectKind.Measure, null, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F48 File Offset: 0x00001148
		internal void CreateMeasureCalculationIds(IEnumerable<ProjectedDsqExpression> measures)
		{
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				int? semanticQuerySelectIndex = projectedDsqExpression.SemanticQuerySelectIndex;
				DataShapeBuilderUtils.GetOrCreateMeasureId(this._ids, this._selectBindingsBuilder, this._expressionToIdMapping, projectedDsqExpression, semanticQuerySelectIndex);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002FAC File Offset: 0x000011AC
		private void AddHighlightCalculation<TParent>(ICalculationContainer<TParent> container, ProjectedDsqExpression generatedDsqExpr, string highlightId, int? measureSelectIndex)
		{
			if (highlightId != null)
			{
				DataShapeBuilderUtils.AddCalculation<TParent>(container, generatedDsqExpr, highlightId, new bool?(true));
				IProjectedDsqExpressionValue value = generatedDsqExpr.Value;
				if (value.DynamicFormatString == null && value.DynamicFormatCulture == null)
				{
					return;
				}
				AuxiliarySelectBinding highlight = this._selectBindingsBuilder.EnsureSelectBinding(measureSelectIndex.Value).Highlight;
				this.AddHighlightDynamicFormatting<TParent>(container, value.DynamicFormatString, highlight.DynamicFormat.Format);
				this.AddHighlightDynamicFormatting<TParent>(container, value.DynamicFormatCulture, highlight.DynamicFormat.Culture);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000302B File Offset: 0x0000122B
		private void AddHighlightDynamicFormatting<TParent>(ICalculationContainer<TParent> dataMember, ProjectedDsqExpression dynamicFormattingProjection, string highlightDynamicFormattingId)
		{
			if (dynamicFormattingProjection == null)
			{
				return;
			}
			DataShapeBuilderUtils.AddCalculation<TParent>(dataMember, dynamicFormattingProjection, highlightDynamicFormattingId, new bool?(true));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003040 File Offset: 0x00001240
		internal void PrepareAddMeasureCalculation(ProjectedDsqExpression generatedDsqExpr, bool hasHighlightFilters, out string id, out string highlightId)
		{
			int? semanticQuerySelectIndex = generatedDsqExpr.SemanticQuerySelectIndex;
			id = DataShapeBuilderUtils.GetOrCreateMeasureId(this._ids, this._selectBindingsBuilder, this._expressionToIdMapping, generatedDsqExpr, semanticQuerySelectIndex);
			highlightId = null;
			if (generatedDsqExpr.IsContextOnly)
			{
				return;
			}
			SelectBinding selectBinding = this._selectBindingsBuilder.EnsureSelectBinding(semanticQuerySelectIndex.Value);
			this.PrepareAddMeasureDynamicFormatting(generatedDsqExpr, selectBinding, id, new DataShapeBuilderContext.TryPrepareAddDynamicFormattingMeasureDelegate(this.TryPrepareAddDynamicFormattingMeasure));
			if (hasHighlightFilters && semanticQuerySelectIndex != null)
			{
				AuxiliarySelectBinding highlight = selectBinding.Highlight;
				if (((highlight != null) ? highlight.Value : null) != null)
				{
					highlightId = selectBinding.Highlight.Value;
				}
				else
				{
					highlightId = this._ids.CreateHighlightId();
					Util.EnsureList<Identifier>(ref this._highlightIds).Add(highlightId);
					selectBinding.Highlight = new AuxiliarySelectBinding
					{
						Value = highlightId
					};
				}
				this.PrepareAddMeasureDynamicFormatting(generatedDsqExpr, selectBinding.Highlight, highlightId, new DataShapeBuilderContext.TryPrepareAddDynamicFormattingMeasureDelegate(this.TryPrepareAddHighlightDynamicFormattingMeasure));
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003130 File Offset: 0x00001330
		private void PrepareAddMeasureDynamicFormatting(ProjectedDsqExpression generatedDsqExpr, IProjectionBinding projectionBinding, string measureId, DataShapeBuilderContext.TryPrepareAddDynamicFormattingMeasureDelegate tryPrepareAddDynamicFormattingMeasure)
		{
			IProjectedDsqExpressionValue value = generatedDsqExpr.Value;
			if (value.DynamicFormatString == null && value.DynamicFormatCulture == null)
			{
				return;
			}
			if (projectionBinding.DynamicFormat == null)
			{
				projectionBinding.DynamicFormat = new DynamicFormatBinding();
			}
			string text;
			if (projectionBinding.DynamicFormat.Format == null && tryPrepareAddDynamicFormattingMeasure(value.DynamicFormatString, projectionBinding, out text))
			{
				projectionBinding.DynamicFormat.Format = text;
			}
			string text2;
			if (projectionBinding.DynamicFormat.Culture == null && tryPrepareAddDynamicFormattingMeasure(value.DynamicFormatCulture, projectionBinding, out text2))
			{
				projectionBinding.DynamicFormat.Culture = text2;
			}
			if (!this._measureDynamicFormattingIds.ContainsKey(measureId))
			{
				this._measureDynamicFormattingIds.Add(measureId, new global::System.ValueTuple<string, string>(projectionBinding.DynamicFormat.Format, projectionBinding.DynamicFormat.Culture));
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000031F3 File Offset: 0x000013F3
		private bool TryPrepareAddHighlightDynamicFormattingMeasure(ProjectedDsqExpression dynamicFormatting, IProjectionBinding binding, out string dynamicFormattingHighlightId)
		{
			dynamicFormattingHighlightId = null;
			if (dynamicFormatting == null)
			{
				return false;
			}
			dynamicFormattingHighlightId = this._ids.CreateHighlightId();
			Util.EnsureList<Identifier>(ref this._highlightIds).Add(dynamicFormattingHighlightId);
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003222 File Offset: 0x00001422
		private bool TryPrepareAddDynamicFormattingMeasure(ProjectedDsqExpression dynamicFormatting, IProjectionBinding binding, out string dynamicFormattingId)
		{
			dynamicFormattingId = null;
			if (dynamicFormatting == null)
			{
				return false;
			}
			dynamicFormattingId = DataShapeBuilderUtils.GetOrCreateMeasureId(this._ids, this._selectBindingsBuilder, this._expressionToIdMapping, dynamicFormatting, dynamicFormatting.SemanticQuerySelectIndex);
			return true;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000324D File Offset: 0x0000144D
		internal StructureReferenceExpressionNode GetSelectIndexStructureReference(int? selectIndex)
		{
			return this._selectBindingsBuilder.GetCalcIdForSelect(selectIndex.Value).StructureReference();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003266 File Offset: 0x00001466
		internal StructureReferenceExpressionNode GetColumnStructureReference(int selectIndex, IConceptualColumn column)
		{
			string calcIdForSelectSource = this._selectBindingsBuilder.GetCalcIdForSelectSource(selectIndex, column.Entity.Name, column.Name);
			Contract.RetailAssert(calcIdForSelectSource != null, "Expected to find a calculation identifier for select index {0}, column {1}", selectIndex, column.Name);
			return calcIdForSelectSource.StructureReference();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032A4 File Offset: 0x000014A4
		internal Expression GetSelectIndexExpression(int? selectIndex)
		{
			return this.GetSelectIndexStructureReference(selectIndex);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000032B4 File Offset: 0x000014B4
		internal List<Expression> GetMeasureIdsStructureReferences(IReadOnlyList<ProjectedDsqExpression> measures)
		{
			List<Expression> list = new List<Expression>(measures.Count);
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				int? semanticQuerySelectIndex = projectedDsqExpression.SemanticQuerySelectIndex;
				Expression selectIndexExpression = this.GetSelectIndexExpression(semanticQuerySelectIndex);
				list.Add(selectIndexExpression);
			}
			return list;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003318 File Offset: 0x00001518
		internal void AddAggregates<TParent>(ICalculationContainer<TParent> container, ProjectedDsqExpression item, int? aggregateGroupIdx, int? subtotalIdx, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			DsqExpressionAggregates aggregates = item.Aggregates;
			if (aggregates.IsEmpty())
			{
				return;
			}
			string calcId = this.GetCalcId(item);
			IReadOnlyDictionary<int, IProjectionBinding> readOnlyDictionary;
			if (!item.IsContextOnly)
			{
				readOnlyDictionary = this.GetSelectBindings(item);
			}
			else
			{
				IReadOnlyDictionary<int, IProjectionBinding> readOnlyDictionary2 = Util.EmptyReadOnlyDictionary<int, IProjectionBinding>();
				readOnlyDictionary = readOnlyDictionary2;
			}
			IReadOnlyDictionary<int, IProjectionBinding> readOnlyDictionary3 = readOnlyDictionary;
			DataShapeExpressionsAxisGroupingBuilder dataShapeExpressionsAxisGroupingBuilder = ((aggregateContextOnlyImpact == AggregateContextOnlyImpact.None) ? this.GetGroupingBuilder(true, aggregateGroupIdx) : null);
			this.AddAggregates<TParent>(container, aggregateGroupIdx, subtotalIdx, aggregates, calcId, item.IsContextOnly, readOnlyDictionary3, dataShapeExpressionsAxisGroupingBuilder, suppressedAggregates, item.SuppressJoinPredicate, aggregateContextOnlyImpact);
			IReadOnlyDictionary<int, IProjectionBinding> highlightBindings = DataShapeBuilderContext.GetHighlightBindings(readOnlyDictionary3);
			if (highlightBindings != null)
			{
				int projectedItemSelectIndex = this.GetProjectedItemSelectIndex(item);
				IProjectionBinding projectionBinding;
				if (highlightBindings.TryGetValue(projectedItemSelectIndex, out projectionBinding))
				{
					this.AddAggregates<TParent>(container, aggregateGroupIdx, subtotalIdx, aggregates, projectionBinding.Value, item.IsContextOnly, highlightBindings, dataShapeExpressionsAxisGroupingBuilder, suppressedAggregates, item.SuppressJoinPredicate, aggregateContextOnlyImpact);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000033D0 File Offset: 0x000015D0
		private void AddAggregates<TParent>(ICalculationContainer<TParent> container, int? groupIndex, int? subtotalIndex, DsqExpressionAggregates aggregates, string id, bool projectedItemIsContextOnly, IReadOnlyDictionary<int, IProjectionBinding> selectBindings, DataShapeExpressionsAxisGroupingBuilder groupingBuilder, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, bool suppressJoinPredicate, AggregateContextOnlyImpact aggregateContextOnlyImpact)
		{
			DataShapeAggregatesBuilder<TParent> dataShapeAggregatesBuilder = new DataShapeAggregatesBuilder<TParent>(container, this._ids, id, projectedItemIsContextOnly, selectBindings, groupingBuilder, groupIndex, subtotalIndex, suppressedAggregates, suppressJoinPredicate, aggregateContextOnlyImpact, this._measureDynamicFormattingIds);
			aggregates.Accept(dataShapeAggregatesBuilder);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003408 File Offset: 0x00001608
		private DataShapeExpressionsAxisGroupingBuilder GetGroupingBuilder(bool isPrimary, int? groupIdx)
		{
			if (groupIdx == null)
			{
				return null;
			}
			return (isPrimary ? this._primaryAxisBuilder : this._secondaryAxisBuilder).WithGrouping(groupIdx.Value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003434 File Offset: 0x00001634
		internal string AddLimitPropertyCalculation<TParent>(ICalculationContainer<TParent> container, string referenceId, string propertyName)
		{
			string text = this._ids.CreateAggregateId();
			container.WithCalculation(text, referenceId.StructureReference().LimitProperty(propertyName, FunctionUsageKind.Unassigned), false, null, null, false);
			return text;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003474 File Offset: 0x00001674
		internal string AddSynchronizationIndexCalculation<TParent>(ICalculationContainer<TParent> container, string dataShapeId)
		{
			string text = this._ids.CreateSynchronizationIndexId();
			container.WithCalculation(text, dataShapeId.StructureReference().SynchronizationIndex(FunctionUsageKind.Unassigned), false, null, null, false);
			return text;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000034B3 File Offset: 0x000016B3
		private TParent AddCalculationAndSetValue<TParent>(TParent container, ProjectedDsqExpression generatedDsqExpr, string id, SelectKind kind, int? primaryDepth, int? secondaryDepth) where TParent : class, ICalculationContainer<TParent>
		{
			if (!generatedDsqExpr.IsContextOnly)
			{
				this.AddSelectBinding(generatedDsqExpr, id, kind, primaryDepth, secondaryDepth);
			}
			return DataShapeBuilderUtils.AddCalculations<TParent>(this._ids, this._selectBindingsBuilder, this._expressionToIdMapping, container, generatedDsqExpr, id);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000034E8 File Offset: 0x000016E8
		internal void WithSortKey<TParent>(GroupBuilder<DataMemberBuilder<TParent>> group, DsqSortKey sortKey, QueryMember queryMember, bool hasNestedGroups, bool isPrimary, bool contextOnly, int groupingIndex)
		{
			ExpressionNode expressionNode = sortKey.Accept<ExpressionNode, SortKeyVisitorContext>(this, new SortKeyVisitorContext(hasNestedGroups, queryMember.MeasureCalculations));
			group.WithSortKey(expressionNode, sortKey.Direction, null);
			if (contextOnly)
			{
				return;
			}
			this.ProjectSortKey<TParent>(group.Parent(), queryMember.Group, sortKey, expressionNode, isPrimary, groupingIndex);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000353C File Offset: 0x0000173C
		private void ProjectSortKey<TParent>(DataMemberBuilder<TParent> dataMember, QueryGroup queryGroup, DsqSortKey sortKey, ExpressionNode resultingSortKeyExpr, bool isPrimary, int groupingIndex)
		{
			if (sortKey.IsMeasure || queryGroup.BindingHints == null)
			{
				return;
			}
			bool flag = queryGroup.BindingHints.IsRestartIdentity(sortKey);
			bool trackNonMeasureSortKeysForReferencing = queryGroup.BindingHints.TrackNonMeasureSortKeysForReferencing;
			if (!flag && !trackNonMeasureSortKeysForReferencing)
			{
				return;
			}
			string orAddCalcForSortKey = this.GetOrAddCalcForSortKey<TParent>(dataMember, sortKey, resultingSortKeyExpr);
			if (trackNonMeasureSortKeysForReferencing)
			{
				this.AddSortKeyForInternalReferencing(queryGroup.BindingHints, sortKey, orAddCalcForSortKey, isPrimary, groupingIndex);
			}
			if (flag)
			{
				this.AddRestartIdentityToAxisGrouping(orAddCalcForSortKey, isPrimary, groupingIndex);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000035A8 File Offset: 0x000017A8
		private string GetOrAddCalcForSortKey<TParent>(DataMemberBuilder<TParent> dataMember, DsqSortKey sortKey, ExpressionNode resultingSortKeyExpr)
		{
			string text;
			if (sortKey.SelectIndex != null)
			{
				text = this._selectBindingsBuilder.GetCalcIdForSelect(sortKey.SelectIndex.Value);
			}
			else
			{
				Calculation calculationOrDefault = dataMember.GetCalculationOrDefault(resultingSortKeyExpr);
				if (calculationOrDefault != null)
				{
					text = calculationOrDefault.Id.Value;
				}
				else
				{
					text = this._ids.CreateKeyId();
					dataMember.WithCalculation(text, resultingSortKeyExpr, false, null, null, false);
				}
			}
			return text;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003624 File Offset: 0x00001824
		private void AddSortKeyForInternalReferencing(QueryGroupBindingHints bindingHints, DsqSortKey sortKey, string calcId, bool isPrimary, int groupingIndex)
		{
			ModelSortBindingInfo modelSortBindingInfo;
			bindingHints.TryGetModelSortBindingInfo(sortKey, out modelSortBindingInfo);
			this.AddInternalSortKeyToSelectBinding(modelSortBindingInfo, calcId);
			this.AddNonMeasureSortKeyToGroupForReferencing(calcId, isPrimary, groupingIndex, (modelSortBindingInfo != null) ? modelSortBindingInfo.Field : null, sortKey);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000365C File Offset: 0x0000185C
		private void AddInternalSortKeyToSelectBinding(ModelSortBindingInfo bindingInfo, string calcId)
		{
			if (bindingInfo == null || bindingInfo.SourceSelects.IsNullOrEmpty<int>() || bindingInfo.Field == null)
			{
				return;
			}
			IConceptualColumn field = bindingInfo.Field;
			ConceptualPropertyReference conceptualPropertyReference = this.GetConceptualPropertyReference(field);
			this._selectBindingsBuilder.AddInternalSortKeysToBinding(bindingInfo.SourceSelects, conceptualPropertyReference, field, calcId);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000036A5 File Offset: 0x000018A5
		private void AddRestartIdentityToAxisGrouping(string calc, bool isPrimary, int groupIndex)
		{
			this.GetGroupingBuilder(isPrimary, new int?(groupIndex)).WithRestartIdentity(calc);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000036BB File Offset: 0x000018BB
		private void AddNonMeasureSortKeyToGroupForReferencing(string calc, bool isPrimary, int groupIndex, IConceptualColumn field, DsqSortKey sortKey)
		{
			this.GetGroupingBuilder(isPrimary, new int?(groupIndex)).WithInternalSortKey(field, sortKey.SelectIndex, calc, sortKey.Direction);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036E4 File Offset: 0x000018E4
		private void AddSelectBinding(ProjectedDsqExpression generatedDsqExpr, string id, SelectKind kind, int? primaryDepth, int? secondaryDepth)
		{
			if (generatedDsqExpr.SemanticQuerySelectIndex != null)
			{
				this._selectBindingsBuilder.AddSelectBinding(generatedDsqExpr.SemanticQuerySelectIndex.Value, id, generatedDsqExpr.Value.FormatString, kind, primaryDepth, secondaryDepth, generatedDsqExpr.Value.LineageProperty);
			}
			List<int> additionalSemanticQuerySelectIndices = generatedDsqExpr.AdditionalSemanticQuerySelectIndices;
			if (additionalSemanticQuerySelectIndices != null)
			{
				foreach (int num in additionalSemanticQuerySelectIndices)
				{
					this._selectBindingsBuilder.AddSelectBinding(num, id, generatedDsqExpr.Value.FormatString, kind, primaryDepth, secondaryDepth, generatedDsqExpr.Value.LineageProperty);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000037A4 File Offset: 0x000019A4
		internal void AddSynchronizedSelectBinding(int selectIndex, SelectBinding synchronizedSelectBinding)
		{
			if (synchronizedSelectBinding != null)
			{
				SelectBinding selectBinding = this._selectBindingsBuilder.GetSelectBinding(selectIndex);
				AuxiliarySelectBindingBuilder auxiliarySelectBindingBuilder = new AuxiliarySelectBindingBuilder();
				auxiliarySelectBindingBuilder.Populate(synchronizedSelectBinding);
				selectBinding.Synchronized = auxiliarySelectBindingBuilder.Result;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000037D8 File Offset: 0x000019D8
		internal void AddSynchronizedGroup(bool isPrimary, int groupIndex, DataShapeExpressionsAxisGrouping synchronizedGroup)
		{
			this.GetGroupingBuilder(isPrimary, new int?(groupIndex)).WithSynchronizedGroup(synchronizedGroup);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000037EE File Offset: 0x000019EE
		internal void AddSynchronization(int syncIndx, bool isPrimary, string dataShapeId, IList<int> groupings)
		{
			DataShapeExpressionsAxisSynchronizationBuilder synchronizationBuilder = this.GetSynchronizationBuilder(isPrimary, syncIndx);
			synchronizationBuilder.WithDataShape(dataShapeId);
			synchronizationBuilder.WithGroupings(groupings);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003808 File Offset: 0x00001A08
		private DataShapeExpressionsAxisSynchronizationBuilder GetSynchronizationBuilder(bool isPrimary, int syncIndex)
		{
			return (isPrimary ? this._primaryAxisBuilder : this._secondaryAxisBuilder).WithSynchronization(syncIndex);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003821 File Offset: 0x00001A21
		internal void AddSynchronizationIndex(bool isPrimary, int groupIndex, string syncIndex)
		{
			this.GetGroupingBuilder(isPrimary, new int?(groupIndex)).WithSynchronizationIndex(syncIndex);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003838 File Offset: 0x00001A38
		private IReadOnlyDictionary<int, IProjectionBinding> GetSelectBindings(ProjectedDsqExpression item)
		{
			List<int> additionalSemanticQuerySelectIndices = item.AdditionalSemanticQuerySelectIndices;
			int num = ((additionalSemanticQuerySelectIndices != null) ? additionalSemanticQuerySelectIndices.Count : 0);
			Dictionary<int, IProjectionBinding> dictionary = new Dictionary<int, IProjectionBinding>(1 + num);
			int projectedItemSelectIndex = this.GetProjectedItemSelectIndex(item);
			dictionary.Add(projectedItemSelectIndex, this._selectBindingsBuilder.EnsureSelectBinding(projectedItemSelectIndex));
			for (int i = 1; i < 1 + num; i++)
			{
				int num2 = item.AdditionalSemanticQuerySelectIndices[i - 1];
				dictionary.Add(num2, this._selectBindingsBuilder.EnsureSelectBinding(num2));
			}
			return dictionary;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000038B0 File Offset: 0x00001AB0
		private int GetProjectedItemSelectIndex(ProjectedDsqExpression item)
		{
			if (item.SemanticQuerySelectIndex == null)
			{
				return 0;
			}
			return item.SemanticQuerySelectIndex.Value;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000038E0 File Offset: 0x00001AE0
		private static IReadOnlyDictionary<int, IProjectionBinding> GetHighlightBindings(IReadOnlyDictionary<int, IProjectionBinding> selectBindings)
		{
			Dictionary<int, IProjectionBinding> dictionary = null;
			foreach (KeyValuePair<int, IProjectionBinding> keyValuePair in selectBindings)
			{
				SelectBinding selectBinding = (SelectBinding)keyValuePair.Value;
				AuxiliarySelectBinding auxiliarySelectBinding = ((selectBinding != null) ? selectBinding.Highlight : null);
				if (auxiliarySelectBinding != null)
				{
					Util.AddToLazyDictionary<int, IProjectionBinding>(ref dictionary, keyValuePair.Key, auxiliarySelectBinding, null);
				}
			}
			return dictionary;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003954 File Offset: 0x00001B54
		private string GetCalcId(ProjectedDsqExpression item)
		{
			return this._selectBindingsBuilder.GetCalcIdForSelect(item.SemanticQuerySelectIndex.Value);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000397C File Offset: 0x00001B7C
		private ConceptualPropertyReference GetConceptualPropertyReference(IConceptualProperty property)
		{
			if (property == null)
			{
				return null;
			}
			string text = ConceptualSchemaNames.NormalizeSchemaNameForSerialization(property.Entity);
			string propertyNameUsingQuerySchemaMapping = this.GetPropertyNameUsingQuerySchemaMapping(property, text);
			return new ConceptualPropertyReference(property.Entity.Name, propertyNameUsingQuerySchemaMapping, text);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000039B8 File Offset: 0x00001BB8
		private string GetPropertyNameUsingQuerySchemaMapping(IConceptualProperty property, string schemaName)
		{
			ExtensionEntityMapping extensionEntityMapping;
			if (this._querySchemaMapping != null && this._querySchemaMapping != QuerySchemaMapping.Empty && !this._querySchemaMapping.IsEmpty && !this._querySchemaMapping.Entities.IsNullOrEmpty<ExtensionEntityMapping>() && ConceptualNameComparer.Instance.Equals(schemaName, this._querySchemaMapping.SchemaName) && this._querySchemaMapping.EntitiesByName.TryGetValue(property.Entity.Name, out extensionEntityMapping))
			{
				if (extensionEntityMapping.Measures != null)
				{
					ExtensionPropertyMapping extensionPropertyMapping = extensionEntityMapping.Measures.Where((ExtensionPropertyMapping mm) => ConceptualNameComparer.Instance.Equals(mm.ResolvedName, property.Name)).FirstOrDefault<ExtensionPropertyMapping>();
					if (extensionPropertyMapping != null)
					{
						return extensionPropertyMapping.OriginalName;
					}
				}
				else if (extensionEntityMapping.Columns != null)
				{
					ExtensionPropertyMapping extensionPropertyMapping2 = extensionEntityMapping.Columns.Where((ExtensionPropertyMapping cm) => ConceptualNameComparer.Instance.Equals(cm.ResolvedName, property.Name)).FirstOrDefault<ExtensionPropertyMapping>();
					if (extensionPropertyMapping2 != null)
					{
						return extensionPropertyMapping2.OriginalName;
					}
				}
			}
			return property.Name;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003ABC File Offset: 0x00001CBC
		internal QueryBindingDescriptor CreateDescriptor(IntermediateDataReduction dsqReduction, IFeatureSwitchProvider featureSwitchProvider)
		{
			DataShapeExpressions dataShapeExpressions = null;
			DataShapeExpressionsAxis dataShapeExpressionsAxis = this._primaryAxisBuilder.Build();
			DataShapeExpressionsAxis dataShapeExpressionsAxis2 = this._secondaryAxisBuilder.Build();
			if (dataShapeExpressionsAxis != null || dataShapeExpressionsAxis2 != null)
			{
				dataShapeExpressions = new DataShapeExpressions
				{
					Primary = dataShapeExpressionsAxis,
					Secondary = dataShapeExpressionsAxis2
				};
			}
			DataShapeLimits dataShapeLimits = DataShapeLimitsBuilder.CreateLimitsDescriptor(dsqReduction);
			ExtensionSchemaBinding extensionSchemaBinding = this.CreateExtensionSchemaBinding();
			return new QueryBindingDescriptor
			{
				Select = this._selectBindingsBuilder.ToBindings(),
				Expressions = dataShapeExpressions,
				Limits = dataShapeLimits,
				ExtensionSchema = extensionSchemaBinding,
				Version = new int?(2)
			};
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003B44 File Offset: 0x00001D44
		private ExtensionSchemaBinding CreateExtensionSchemaBinding()
		{
			if (this._querySchemaMapping == null || this._querySchemaMapping.IsEmpty || this._querySchemaMapping.Entities.IsNullOrEmpty<ExtensionEntityMapping>())
			{
				return null;
			}
			List<ExtensionEntityBinding> list = this.CreateExtensionEntityBindings(this._querySchemaMapping.Entities);
			return new ExtensionSchemaBinding
			{
				Name = this._querySchemaMapping.SchemaName,
				Entities = list
			};
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003BAC File Offset: 0x00001DAC
		private List<ExtensionEntityBinding> CreateExtensionEntityBindings(IReadOnlyList<ExtensionEntityMapping> entities)
		{
			List<ExtensionEntityBinding> list = new List<ExtensionEntityBinding>(entities.Count);
			foreach (ExtensionEntityMapping extensionEntityMapping in entities)
			{
				List<ExtensionMeasureBinding> list2 = this.CreateExtensionMeasureBindings(extensionEntityMapping.Measures);
				List<ExtensionColumnBinding> list3 = this.CreateExtensionColumnBindings(extensionEntityMapping.Columns);
				list.Add(new ExtensionEntityBinding
				{
					Name = extensionEntityMapping.OriginalName,
					NativeQueryName = extensionEntityMapping.ResolvedName,
					Measures = list2,
					Columns = list3
				});
			}
			return list;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C48 File Offset: 0x00001E48
		private List<ExtensionMeasureBinding> CreateExtensionMeasureBindings(IReadOnlyList<ExtensionPropertyMapping> measures)
		{
			if (measures.IsNullOrEmpty<ExtensionPropertyMapping>())
			{
				return null;
			}
			List<ExtensionMeasureBinding> list = new List<ExtensionMeasureBinding>(measures.Count);
			list.AddRange(measures.Select((ExtensionPropertyMapping measureMapping) => new ExtensionMeasureBinding
			{
				Name = measureMapping.OriginalName,
				NativeQueryName = measureMapping.ResolvedName
			}));
			return list;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C98 File Offset: 0x00001E98
		private List<ExtensionColumnBinding> CreateExtensionColumnBindings(IReadOnlyList<ExtensionPropertyMapping> columns)
		{
			if (columns.IsNullOrEmpty<ExtensionPropertyMapping>())
			{
				return null;
			}
			List<ExtensionColumnBinding> list = new List<ExtensionColumnBinding>(columns.Count);
			list.AddRange(columns.Select((ExtensionPropertyMapping columnMapping) => new ExtensionColumnBinding
			{
				Name = columnMapping.OriginalName,
				NativeQueryName = columnMapping.ResolvedName
			}));
			return list;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003CE5 File Offset: 0x00001EE5
		internal SelectBinding GetSelectBindingForIndex(int index)
		{
			return this._selectBindingsBuilder.GetSelectBinding(index);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003CF3 File Offset: 0x00001EF3
		ExpressionNode IDsqSortKeyVisitor<ExpressionNode, SortKeyVisitorContext>.Visit(DsqSortKeyExpression sortKey, SortKeyVisitorContext context)
		{
			return sortKey.Expression;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003CFC File Offset: 0x00001EFC
		ExpressionNode IDsqSortKeyVisitor<ExpressionNode, SortKeyVisitorContext>.Visit(DsqSortKeyProjection sortKey, SortKeyVisitorContext context)
		{
			if (!context.HasNestedGroups || context.GroupMeasures.Contains(sortKey.Projection))
			{
				return sortKey.Projection.Value.DsqExpression;
			}
			return this.GetCalcId(sortKey.Projection).StructureReference().Subtotal(FunctionUsageKind.Unassigned);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003D4C File Offset: 0x00001F4C
		internal IntermediateDataShapeTableSchema CreateIntermediateTableSchema(Identifier dataShapeId)
		{
			return IntermediateDataShapeTableSchemaBuilder.BuildTableSchema(this._selectBindingsBuilder, dataShapeId);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003D5A File Offset: 0x00001F5A
		internal IntermediateDataShapeReferenceSchema CreateReferenceSchema(Identifier dataShapeId)
		{
			return IntermediateDataShapeReferenceSchemaBuilder.BuildSchema(this._selectBindingsBuilder, dataShapeId, this._primaryAxisBuilder, this._secondaryAxisBuilder);
		}

		// Token: 0x0400003A RID: 58
		private readonly DataShapeIdGenerator _ids;

		// Token: 0x0400003B RID: 59
		private readonly Dictionary<ProjectedDsqExpression, string> _expressionToIdMapping;

		// Token: 0x0400003C RID: 60
		private readonly SelectBindingsBuilder _selectBindingsBuilder;

		// Token: 0x0400003D RID: 61
		private readonly QuerySchemaMapping _querySchemaMapping;

		// Token: 0x0400003E RID: 62
		private DataShapeExpressionsAxisBuilder _primaryAxisBuilder;

		// Token: 0x0400003F RID: 63
		private DataShapeExpressionsAxisBuilder _secondaryAxisBuilder;

		// Token: 0x04000040 RID: 64
		private List<Identifier> _highlightIds;

		// Token: 0x04000041 RID: 65
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "DynamicFormatId", "DynamicFormatCultureId" })]
		private Dictionary<string, global::System.ValueTuple<string, string>> _measureDynamicFormattingIds;

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x06000925 RID: 2341
		private delegate bool TryPrepareAddDynamicFormattingMeasureDelegate(ProjectedDsqExpression dynamicFormattingMeasure, IProjectionBinding binding, out string dynamicFormattingId);
	}
}
