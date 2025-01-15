using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x0200010D RID: 269
	internal sealed class QueryDefinition
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x0002B55C File Offset: 0x0002975C
		internal QueryDefinition(EntityDataModel entityDataModel, IConceptualSchema schema, bool useConceptualSchema, IEnumerable<Group> groups = null, IEnumerable<IJoinPredicate> explicitJoinPredicates = null, BlankRowBehavior allowBlankRow = BlankRowBehavior.FilterByProjection, GroupFilter groupFilter = null, IEnumerable<Limit> limits = null, Limit postRegroupLimit = null, IEnumerable<Measure> measures = null, Rollup rollup = null, IEnumerable<FilterCondition> slicerConditions = null, Filter slicer = null, IEnumerable<QueryDefinition> applyFilters = null, IEnumerable<IEdmFieldInstance> fieldsRequiringClearDefaultFilterContext = null, IEnumerable<IConceptualColumn> columnsRequiringClearDefaultFilterContext = null, IEnumerable<SortItem> sorting = null, IEnumerable<QueryExpression> startAt = null, IReadOnlyList<QueryBaseDeclarationExpression> declarations = null, IEnumerable<QueryExpression> existsFilters = null, FilterCondition topLevelValueFilter = null, LimitOperator topLevelLimit = null, bool includeDirectManyToManyAssociations = false, IReadOnlyList<QueryParameterDeclarationExpression> queryParameters = null)
		{
			this._entityDataModel = entityDataModel;
			this._schema = schema;
			this._useConceptualSchema = useConceptualSchema;
			this._declarations = declarations ?? Microsoft.DataShaping.Util.EmptyReadOnlyList<QueryBaseDeclarationExpression>();
			this._groups = new QdmNamedItemCollection<Group>(groups.EmptyIfNull<Group>());
			this._queryParameters = queryParameters ?? Microsoft.DataShaping.Util.EmptyReadOnlyList<QueryParameterDeclarationExpression>();
			this.ExplicitJoinPredicates = explicitJoinPredicates;
			this._allowBlankRow = allowBlankRow;
			this._groupFilter = ArgumentValidation.CheckCondition<GroupFilter>(groupFilter, groupFilter == null || groupFilter.IsValid(this._groups), "groupFilter");
			this._limits = new QdmItemCollection<Limit>(limits.EmptyIfNull<Limit>());
			this._postRegroupLimit = postRegroupLimit;
			this._measures = new QdmNamedItemCollection<Measure>(measures.EmptyIfNull<Measure>());
			this._rollup = rollup;
			if (slicer != null)
			{
				this._slicer = slicer;
			}
			else if (slicerConditions != null && slicerConditions.Any<FilterCondition>())
			{
				this._slicer = new Filter(slicerConditions);
			}
			this._applyFilters = applyFilters;
			if (this._useConceptualSchema)
			{
				this._columnsRequiringClearDefaultFilterContext = columnsRequiringClearDefaultFilterContext.ToReadOnlyCollection<IConceptualColumn>();
			}
			else
			{
				this._fieldsRequiringClearDefaultFilterContext = fieldsRequiringClearDefaultFilterContext.ToReadOnlyCollection<IEdmFieldInstance>();
			}
			if (sorting != null)
			{
				this._sorting = new QdmItemCollection<SortItem>(sorting);
			}
			else
			{
				this._sorting = new QdmItemCollection<SortItem>(from g in this._groups
					where g.IsProjected
					from k in g.Keys
					select new SortItem(k.Name, SortDirection.Ascending, k.Expression, false));
			}
			this._startAt = new QdmItemCollection<QueryExpression>(startAt.EmptyIfNull<QueryExpression>());
			this._existsFilters = existsFilters ?? new List<QueryExpression>().ToReadOnlyCollection<QueryExpression>();
			this._topLevelValueFilter = topLevelValueFilter;
			this._topLevelLimit = topLevelLimit;
			this._includeDirectManyToManyAssociations = includeDirectManyToManyAssociations;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0002B741 File Offset: 0x00029941
		public IReadOnlyList<QueryParameterDeclarationExpression> QueryParameters
		{
			get
			{
				return this._queryParameters;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002B749 File Offset: 0x00029949
		public IReadOnlyList<QueryBaseDeclarationExpression> Declarations
		{
			get
			{
				return this._declarations;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0002B751 File Offset: 0x00029951
		public EntityDataModel EntityDataModel
		{
			get
			{
				return this._entityDataModel;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002B759 File Offset: 0x00029959
		public IConceptualSchema ConceptualSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0002B761 File Offset: 0x00029961
		public QdmNamedItemCollection<Group> Groups
		{
			get
			{
				return this._groups;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0002B769 File Offset: 0x00029969
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0002B771 File Offset: 0x00029971
		public IEnumerable<IJoinPredicate> ExplicitJoinPredicates
		{
			get
			{
				return this._explicitJoinPredicates;
			}
			internal set
			{
				this._explicitJoinPredicates = ((value != null) ? value.Distinct(JoinPredicates.Comparer) : null);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0002B78A File Offset: 0x0002998A
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0002B792 File Offset: 0x00029992
		public BlankRowBehavior AllowBlankRow
		{
			get
			{
				return this._allowBlankRow;
			}
			internal set
			{
				this._allowBlankRow = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0002B79B File Offset: 0x0002999B
		public GroupFilter GroupFilter
		{
			get
			{
				return this._groupFilter;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0002B7A3 File Offset: 0x000299A3
		public QdmItemCollection<Limit> Limits
		{
			get
			{
				return this._limits;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0002B7AB File Offset: 0x000299AB
		public Limit PostRegroupLimit
		{
			get
			{
				return this._postRegroupLimit;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0002B7B3 File Offset: 0x000299B3
		public LimitOperator TopLevelLimit
		{
			get
			{
				return this._topLevelLimit;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0002B7BB File Offset: 0x000299BB
		public QdmNamedItemCollection<Measure> Measures
		{
			get
			{
				return this._measures;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0002B7C3 File Offset: 0x000299C3
		internal IEnumerable<IJoinPredicate> DefaultMeasurePredicates
		{
			get
			{
				return this._measures.OfType<IJoinPredicate>();
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0002B7D0 File Offset: 0x000299D0
		public Rollup Rollup
		{
			get
			{
				return this._rollup;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0002B7D8 File Offset: 0x000299D8
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x0002B7E0 File Offset: 0x000299E0
		public Filter Slicer
		{
			get
			{
				return this._slicer;
			}
			internal set
			{
				this._slicer = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002B7E9 File Offset: 0x000299E9
		public IEnumerable<QueryDefinition> ApplyFilters
		{
			get
			{
				return this._applyFilters;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002B7F1 File Offset: 0x000299F1
		public IEnumerable<IEdmFieldInstance> FieldsRequiringClearDefaultFilterContext
		{
			get
			{
				return this._fieldsRequiringClearDefaultFilterContext;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002B7F9 File Offset: 0x000299F9
		public IEnumerable<IConceptualColumn> ColumnsRequiringClearDefaultFilterContext
		{
			get
			{
				return this._columnsRequiringClearDefaultFilterContext;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0002B801 File Offset: 0x00029A01
		public QdmItemCollection<SortItem> Sorting
		{
			get
			{
				return this._sorting;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002B809 File Offset: 0x00029A09
		public ReadOnlyQdmNamedItemCollection<INamedProjection> Projections
		{
			get
			{
				return new ReadOnlyQdmNamedItemCollection<INamedProjection>(this.CalculateProjectedItems());
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0002B816 File Offset: 0x00029A16
		public QdmItemCollection<QueryExpression> StartAt
		{
			get
			{
				return this._startAt;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002B81E File Offset: 0x00029A1E
		public IEnumerable<QueryExpression> ExistsFilters
		{
			get
			{
				return this._existsFilters;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0002B826 File Offset: 0x00029A26
		public FilterCondition TopLevelValueFilter
		{
			get
			{
				return this._topLevelValueFilter;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0002B82E File Offset: 0x00029A2E
		public bool IncludeDirectManyToManyAssociations
		{
			get
			{
				return this._includeDirectManyToManyAssociations;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0002B836 File Offset: 0x00029A36
		public bool UseConceptualSchema
		{
			get
			{
				return this._useConceptualSchema;
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002B83E File Offset: 0x00029A3E
		public bool HasUnconstrainedJoin()
		{
			return this.CreateConstraintValidator(false).HasUnconstrainedJoin();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0002B84C File Offset: 0x00029A4C
		internal QueryConstraintValidator CreateConstraintValidator(bool crossJoinQueryPlan = false)
		{
			return new QueryConstraintValidator(this._entityDataModel, this._schema, this._groups, this._measures, this._explicitJoinPredicates, crossJoinQueryPlan, this._includeDirectManyToManyAssociations, this._useConceptualSchema);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002B880 File Offset: 0x00029A80
		private QueryConstraintValidator CreateConstraintValidatorHierarchicalGroupsUpTo(Group group, bool crossJoinQueryPlan)
		{
			List<Group> list = this.GetHierarchicallyRelatedGroups(group, this.Groups.TakeWhile((Group g) => g != group)).ToList<Group>();
			return new QueryConstraintValidator(this._entityDataModel, this._schema, list, null, null, crossJoinQueryPlan, this._includeDirectManyToManyAssociations, this._useConceptualSchema);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		private IEnumerable<Group> GetHierarchicallyRelatedGroups(Group targetGroup, IEnumerable<Group> priorGroups)
		{
			foreach (Group group in priorGroups)
			{
				if (this._useConceptualSchema ? QueryAlgorithms.AreHierarchicallyRelated(targetGroup, group, this._schema, this._includeDirectManyToManyAssociations) : QueryAlgorithms.AreHierarchicallyRelated(targetGroup, group, this._entityDataModel, this._includeDirectManyToManyAssociations))
				{
					yield return group;
				}
			}
			IEnumerator<Group> enumerator = null;
			yield return targetGroup;
			yield break;
			yield break;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002B902 File Offset: 0x00029B02
		internal IEnumerable<IJoinPredicate> GetJoinPredicates(bool crossJoinQueryPlan)
		{
			return this.CreateConstraintValidator(crossJoinQueryPlan).GetJoinPredicates();
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0002B910 File Offset: 0x00029B10
		internal IEnumerable<IJoinPredicate> GetHierarchicalJoinPredicatesUpToGroup(Group group, bool crossJoinQueryPlan)
		{
			bool flag;
			return this.GetHierarchicalJoinPredicatesUpToGroup(group, crossJoinQueryPlan, out flag);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002B928 File Offset: 0x00029B28
		internal IEnumerable<IJoinPredicate> GetHierarchicalJoinPredicatesUpToGroup(Group group, bool crossJoinQueryPlan, out bool anyHierarchicallyRelatedGroups)
		{
			QueryConstraintValidator queryConstraintValidator = this.CreateConstraintValidatorHierarchicalGroupsUpTo(group, crossJoinQueryPlan);
			anyHierarchicallyRelatedGroups = queryConstraintValidator.MultipleGroups;
			return queryConstraintValidator.GetJoinPredicates();
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002B94C File Offset: 0x00029B4C
		public QueryTranslationResult Translate(IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken, QueryTrimmer getGroupsToTrimFromQuery = null)
		{
			return this.Translate(default(QdmTranslationSettings), featureSwitchProvider, QueryDefinition.Translator, cancellationToken, getGroupsToTrimFromQuery);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002B970 File Offset: 0x00029B70
		internal QueryTranslationResult Translate(QdmTranslationSettings settings, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			return this.Translate(settings, featureSwitchProvider, QueryDefinition.Translator, cancellationToken, null);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002B984 File Offset: 0x00029B84
		internal QueryTranslationResult Translate(QdmTranslationSettings settings, IFeatureSwitchProvider featureSwitchProvider, CommandTreeTranslator translator, CancellationToken cancellationToken, QueryTrimmer getGroupsToTrimFromQuery = null)
		{
			QueryCommandTree queryCommandTree = this.ToQueryCommandTree(settings, featureSwitchProvider, cancellationToken, getGroupsToTrimFromQuery);
			Dictionary<string, ConceptualTypeColumn> dictionary = queryCommandTree.GetRowResultType().Columns.ToDictionary((ConceptualTypeColumn f) => f.EdmName, EdmItem.IdentityComparer);
			global::System.ValueTuple<ConceptualTypeColumn, QueryDefinition.AggregateIndicatorField>[] aggIndicatorFields = this.GetAggregateIndicatorFields(dictionary).ToArray<global::System.ValueTuple<ConceptualTypeColumn, QueryDefinition.AggregateIndicatorField>>();
			TranslationResult translationResult = translator.Translate(queryCommandTree, cancellationToken, featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
			IEnumerable<QueryResultField> enumerable = from f in translationResult.DataFields
				let af = aggIndicatorFields.SingleOrDefault(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "AggregateField" })] global::System.ValueTuple<ConceptualTypeColumn, QueryDefinition.AggregateIndicatorField> a) => a.Item1.Equals(f.Field)).Item2
				select new QueryResultField(f.Field, f.ColumnName, f.RawUnqualifiedColumnName, af.IndicatorField, af.NonAggregatedGroup, f.SortInfo);
			return new QueryTranslationResult(translationResult.CommandText, enumerable, translationResult.QuerySourceMap);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002BA50 File Offset: 0x00029C50
		internal QueryCommandTree ToQueryCommandTree(IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			return this.ToQueryCommandTree(default(QdmTranslationSettings), featureSwitchProvider, cancellationToken, null);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002BA70 File Offset: 0x00029C70
		internal QueryCommandTree ToQueryCommandTree(QdmTranslationSettings settings, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken, QueryTrimmer getGroupsToTrimFromQuery = null)
		{
			QueryCommandTree queryCommandTree;
			try
			{
				queryCommandTree = QdmCommandTreeTranslator.Translate(this, settings, featureSwitchProvider, cancellationToken, getGroupsToTrimFromQuery);
			}
			catch (QueryDefinitionTranslationException)
			{
				throw;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new QueryDefinitionTranslationException(SR.QueryDefinitionTranslationFailed, ex);
			}
			return queryCommandTree;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002BAC8 File Offset: 0x00029CC8
		internal string GetAggregateIndicatorNameForGroup(Group group)
		{
			string text = null;
			if (this.Rollup != null)
			{
				RollupGroup rollupGroup = this.Rollup.RollupGroups.FirstOrDefault((RollupGroup rg) => rg.RefersTo(group));
				if (rollupGroup != null)
				{
					text = rollupGroup.AggregateIndicatorName;
				}
			}
			return text;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002BB14 File Offset: 0x00029D14
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "AggregateField" })]
		private IEnumerable<global::System.ValueTuple<ConceptualTypeColumn, QueryDefinition.AggregateIndicatorField>> GetAggregateIndicatorFields(IDictionary<string, ConceptualTypeColumn> resultFields)
		{
			if (this.Rollup == null)
			{
				yield break;
			}
			foreach (Group group in this.Groups)
			{
				string aggregateIndicatorNameForGroup = this.GetAggregateIndicatorNameForGroup(group);
				QueryDefinition.AggregateIndicatorField aggIndicatorField;
				if (aggregateIndicatorNameForGroup == null)
				{
					aggIndicatorField = QueryDefinition.AggregateIndicatorField.NonAggregatedGroupIndicator;
				}
				else
				{
					aggIndicatorField = QueryDefinition.AggregateIndicatorField.Create(resultFields[aggregateIndicatorNameForGroup]);
				}
				foreach (GroupField groupField in group.GetProjectedFields(false))
				{
					if (groupField.Type == GroupFieldType.Key)
					{
						yield return new global::System.ValueTuple<ConceptualTypeColumn, QueryDefinition.AggregateIndicatorField>(resultFields[groupField.Name], aggIndicatorField);
					}
				}
				IEnumerator<GroupField> enumerator2 = null;
				aggIndicatorField = default(QueryDefinition.AggregateIndicatorField);
			}
			IEnumerator<Group> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002BB2B File Offset: 0x00029D2B
		internal string CreateAdditionalProjectionName(string candidateName)
		{
			return QueryNamingContext.CreateUniqueName(this.Projections.Select((INamedProjection p) => p.Name), candidateName, null);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002BB5E File Offset: 0x00029D5E
		private IEnumerable<INamedProjection> CalculateProjectedItems()
		{
			foreach (Group group in this._groups)
			{
				if (group.IsProjected)
				{
					foreach (GroupKey groupKey in group.Keys)
					{
						yield return groupKey;
					}
					IEnumerator<GroupKey> enumerator2 = null;
					foreach (GroupDetail groupDetail in group.Details)
					{
						if (groupDetail.IsProjected)
						{
							yield return groupDetail;
						}
					}
					IEnumerator<GroupDetail> enumerator3 = null;
					group = null;
				}
			}
			IEnumerator<Group> enumerator = null;
			foreach (Measure measure in this._measures)
			{
				yield return measure;
			}
			IEnumerator<Measure> enumerator4 = null;
			if (this._rollup != null)
			{
				foreach (RollupGroup rollupGroup in this._rollup.RollupGroups)
				{
					yield return rollupGroup;
				}
				IEnumerator<RollupGroup> enumerator5 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0002BB70 File Offset: 0x00029D70
		internal QueryDefinition UpdateExplicitJoinPredicates(IEnumerable<IJoinPredicate> explicitJoinPredicates)
		{
			EntityDataModel entityDataModel = this._entityDataModel;
			IConceptualSchema schema = this._schema;
			bool useConceptualSchema = this._useConceptualSchema;
			IEnumerable<Group> groups = this._groups;
			BlankRowBehavior allowBlankRow = this._allowBlankRow;
			GroupFilter groupFilter = this._groupFilter;
			IEnumerable<Limit> limits = this._limits;
			Limit postRegroupLimit = this._postRegroupLimit;
			IEnumerable<Measure> measures = this._measures;
			Rollup rollup = this._rollup;
			IEnumerable<FilterCondition> enumerable = null;
			IEnumerable<IEdmFieldInstance> fieldsRequiringClearDefaultFilterContext = this._fieldsRequiringClearDefaultFilterContext;
			IEnumerable<IConceptualColumn> columnsRequiringClearDefaultFilterContext = this._columnsRequiringClearDefaultFilterContext;
			return new QueryDefinition(entityDataModel, schema, useConceptualSchema, groups, explicitJoinPredicates, allowBlankRow, groupFilter, limits, postRegroupLimit, measures, rollup, enumerable, this._slicer, this._applyFilters, fieldsRequiringClearDefaultFilterContext, columnsRequiringClearDefaultFilterContext, this._sorting, this._startAt, this._declarations, this._existsFilters, this._topLevelValueFilter, null, false, this._queryParameters);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0002BC04 File Offset: 0x00029E04
		internal QueryDefinition UpdateSlicer(Filter slicer)
		{
			EntityDataModel entityDataModel = this._entityDataModel;
			IConceptualSchema schema = this._schema;
			bool useConceptualSchema = this._useConceptualSchema;
			IEnumerable<Group> groups = this._groups;
			IEnumerable<IJoinPredicate> explicitJoinPredicates = this._explicitJoinPredicates;
			BlankRowBehavior allowBlankRow = this._allowBlankRow;
			GroupFilter groupFilter = this._groupFilter;
			IEnumerable<Limit> limits = this._limits;
			Limit postRegroupLimit = this._postRegroupLimit;
			IEnumerable<Measure> measures = this._measures;
			Rollup rollup = this._rollup;
			IEnumerable<FilterCondition> enumerable = null;
			IEnumerable<IEdmFieldInstance> fieldsRequiringClearDefaultFilterContext = this._fieldsRequiringClearDefaultFilterContext;
			IEnumerable<IConceptualColumn> columnsRequiringClearDefaultFilterContext = this._columnsRequiringClearDefaultFilterContext;
			return new QueryDefinition(entityDataModel, schema, useConceptualSchema, groups, explicitJoinPredicates, allowBlankRow, groupFilter, limits, postRegroupLimit, measures, rollup, enumerable, slicer, this._applyFilters, fieldsRequiringClearDefaultFilterContext, columnsRequiringClearDefaultFilterContext, this._sorting, this._startAt, this._declarations, this._existsFilters, this._topLevelValueFilter, null, false, this._queryParameters);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0002BC98 File Offset: 0x00029E98
		public QueryDefinition ToQueryDefinitionWithoutClearDefaultFilterContext()
		{
			if (this._useConceptualSchema ? this._columnsRequiringClearDefaultFilterContext.Any<IConceptualColumn>() : this._fieldsRequiringClearDefaultFilterContext.Any<IEdmFieldInstance>())
			{
				EntityDataModel entityDataModel = this._entityDataModel;
				IConceptualSchema schema = this._schema;
				bool useConceptualSchema = this._useConceptualSchema;
				IEnumerable<Group> groups = this._groups;
				IEnumerable<IJoinPredicate> explicitJoinPredicates = this._explicitJoinPredicates;
				BlankRowBehavior allowBlankRow = this._allowBlankRow;
				GroupFilter groupFilter = this._groupFilter;
				IEnumerable<Limit> limits = this._limits;
				Limit postRegroupLimit = this._postRegroupLimit;
				IEnumerable<Measure> measures = this._measures;
				Rollup rollup = this._rollup;
				IEnumerable<FilterCondition> enumerable = null;
				IEnumerable<IEdmFieldInstance> enumerable2 = Enumerable.Empty<IEdmFieldInstance>();
				IEnumerable<IConceptualColumn> enumerable3 = Enumerable.Empty<IConceptualColumn>();
				return new QueryDefinition(entityDataModel, schema, useConceptualSchema, groups, explicitJoinPredicates, allowBlankRow, groupFilter, limits, postRegroupLimit, measures, rollup, enumerable, this._slicer, this._applyFilters, enumerable2, enumerable3, this._sorting, this._startAt, this._declarations, this._existsFilters, this._topLevelValueFilter, null, false, this._queryParameters);
			}
			return this;
		}

		// Token: 0x04000A20 RID: 2592
		private static readonly CommandTreeTranslator Translator = CommandTreeTranslatorFactory.CreateDaxTranslator();

		// Token: 0x04000A21 RID: 2593
		private readonly EntityDataModel _entityDataModel;

		// Token: 0x04000A22 RID: 2594
		private readonly IConceptualSchema _schema;

		// Token: 0x04000A23 RID: 2595
		private readonly QdmNamedItemCollection<Group> _groups;

		// Token: 0x04000A24 RID: 2596
		private readonly IEnumerable<IEdmFieldInstance> _fieldsRequiringClearDefaultFilterContext;

		// Token: 0x04000A25 RID: 2597
		private readonly IEnumerable<IConceptualColumn> _columnsRequiringClearDefaultFilterContext;

		// Token: 0x04000A26 RID: 2598
		private readonly GroupFilter _groupFilter;

		// Token: 0x04000A27 RID: 2599
		private readonly QdmItemCollection<Limit> _limits;

		// Token: 0x04000A28 RID: 2600
		private readonly Limit _postRegroupLimit;

		// Token: 0x04000A29 RID: 2601
		private readonly QdmNamedItemCollection<Measure> _measures;

		// Token: 0x04000A2A RID: 2602
		private readonly Rollup _rollup;

		// Token: 0x04000A2B RID: 2603
		private readonly IEnumerable<QueryDefinition> _applyFilters;

		// Token: 0x04000A2C RID: 2604
		private readonly QdmItemCollection<SortItem> _sorting;

		// Token: 0x04000A2D RID: 2605
		private readonly QdmItemCollection<QueryExpression> _startAt;

		// Token: 0x04000A2E RID: 2606
		private readonly IReadOnlyList<QueryBaseDeclarationExpression> _declarations;

		// Token: 0x04000A2F RID: 2607
		private readonly IReadOnlyList<QueryParameterDeclarationExpression> _queryParameters;

		// Token: 0x04000A30 RID: 2608
		private readonly IEnumerable<QueryExpression> _existsFilters;

		// Token: 0x04000A31 RID: 2609
		private readonly FilterCondition _topLevelValueFilter;

		// Token: 0x04000A32 RID: 2610
		private readonly LimitOperator _topLevelLimit;

		// Token: 0x04000A33 RID: 2611
		private readonly bool _includeDirectManyToManyAssociations;

		// Token: 0x04000A34 RID: 2612
		private readonly bool _useConceptualSchema;

		// Token: 0x04000A35 RID: 2613
		private IEnumerable<IJoinPredicate> _explicitJoinPredicates;

		// Token: 0x04000A36 RID: 2614
		private BlankRowBehavior _allowBlankRow;

		// Token: 0x04000A37 RID: 2615
		private Filter _slicer;

		// Token: 0x0200035D RID: 861
		private struct AggregateIndicatorField
		{
			// Token: 0x17000814 RID: 2068
			// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00055772 File Offset: 0x00053972
			// (set) Token: 0x06001F03 RID: 7939 RVA: 0x0005577A File Offset: 0x0005397A
			internal ConceptualTypeColumn IndicatorField { readonly get; private set; }

			// Token: 0x17000815 RID: 2069
			// (get) Token: 0x06001F04 RID: 7940 RVA: 0x00055783 File Offset: 0x00053983
			// (set) Token: 0x06001F05 RID: 7941 RVA: 0x0005578B File Offset: 0x0005398B
			internal bool NonAggregatedGroup { readonly get; private set; }

			// Token: 0x06001F06 RID: 7942 RVA: 0x00055794 File Offset: 0x00053994
			internal static QueryDefinition.AggregateIndicatorField Create(ConceptualTypeColumn indicatorField)
			{
				return new QueryDefinition.AggregateIndicatorField
				{
					IndicatorField = indicatorField
				};
			}

			// Token: 0x17000816 RID: 2070
			// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000557B4 File Offset: 0x000539B4
			internal static QueryDefinition.AggregateIndicatorField NonAggregatedGroupIndicator
			{
				get
				{
					return new QueryDefinition.AggregateIndicatorField
					{
						NonAggregatedGroup = true
					};
				}
			}
		}
	}
}
